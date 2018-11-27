using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AccountLogin.Http;
using Newtonsoft.Json;
using AccountLogin.Entity;

namespace AccountLogin.Ali
{
    /// <summary>
    /// 阿里巴巴登陆主方法
    /// AliClient当需要多账号登陆时，每个账号的cookie 各种验证id等独有的东西放到各自的Client中
    /// </summary>
    public static class AliPassporter
    {
        private const string LoginUrl = "https://login.alibaba.com/";
        /// <summary>
        /// 登陆前先请求一下页面获取sessionId,DmtrackPageid,初始化cookie的值,跟正常访问一样
        /// </summary>
        /// <param name="aliClient"> AliClient当需要多账号登陆时，每个账号的cookie 各种验证id等独有的东西放到各自的Client中，相当于每个独立的浏览器进程</param>
        /// <param name="isSpec">针对某种特殊情况取值方法不太一样，默认false(出现过几次这种情况)</param>
        public static void PrepareLogin(AliClient aliClient, bool isSpec = false)
        {
            if (aliClient.Cookie == null)
            {
                throw new InvalidOperationException("请先对Cookie属性赋值");
            }

            string html = HttpClient.Get(LoginUrl, aliClient.Cookie);
            #region 获取/设置sessionid,从cookie中获取sessionId，保证都在一个会话中
            var getSessionIdFunc = new Func<string>(() =>
            {
                if (isSpec)
                {
                    string cookievalueS = CookieHelper.GetCookie(aliClient.Cookie, "r_session_id");
                    return cookievalueS;
                }
                string cookievalue = CookieHelper.GetCookie(aliClient.Cookie, "acs_usuc_t");
                var ss = cookievalue.Split(new[] { '=' });
                string sessionId = ss[1];
                return sessionId;
            });
            aliClient.SessionId = getSessionIdFunc();
            #endregion
            #region 获取/设置GetDmtrackPageid，阿里访问每次产生的pageid，照着请求中需要东西的去找就行了
            var getDmtrackPageIdFunc = new Func<string>(() =>
            {
                var r = new Regex(@"[^\?&]?pageid=[^&]+");
                var gc = r.Match(html).Groups;
                string dmtrackPageId = gc.Count > 0 && gc[0].Value.Split('=').Length > 1 ? gc[0].Value.Split('=')[1] : "";
                return EncryptDmtrackPage(dmtrackPageId);
            });
            aliClient.DmtrackPageid = getDmtrackPageIdFunc();
            #endregion
        }

        /// <summary>
        ///     登录阿里巴巴
        /// </summary>
        /// <param name="aliClient"> AliClient当需要多账号登陆时，每个账号的cookie 各种验证id等独有的东西放到各自的Client中</param>
        /// <param name="account">帐户名</param>
        /// <param name="password">密码</param>
        /// <param name="checkCode">验证码</param>
        /// <returns>登陆结果(true/false)</returns>
        public static bool DoLoginStep1(AliClient aliClient, string account, string password, string checkCode)
        {
            if (aliClient.Cookie == null)
            {
                throw new InvalidOperationException("请先对Cookie属性赋值");
            }

            if (string.IsNullOrEmpty(aliClient.DmtrackPageid))
            {
                return false;
            }
            //如果获取不到这个token，那就是出现其他情况了，以前可以无视手机验证码，现在貌似不能。
            string token = GetToken(aliClient.Cookie, account, password, aliClient.DmtrackPageid, checkCode);
            if (string.IsNullOrEmpty(token))
            {
                //如果这里没有获取到，告诉用户让用户到网页中输入手机验证码就行了，或者自己做一下。毕竟手机验证码很少出现(别老是异地登陆)
                return false;
            }
            var st = GetSt(aliClient.Cookie, token);
            return !string.IsNullOrEmpty(st) && DoLoginStep2(aliClient, account, password, aliClient.DmtrackPageid, st);
        }
        /// <summary>
        ///     登录处理第二步
        /// </summary>
        /// <param name="aliClient"></param>
        /// <param name="userId">账户</param>
        /// <param name="password">密码</param>
        /// <param name="dmtrackPageid">令牌1</param>
        /// <param name="st">令牌三</param>
        /// <returns>登陆结果</returns>
        private static bool DoLoginStep2(AliClient aliClient, string userId, string password, string dmtrackPageid, string st)
        {
            //依然是照着fildder产生的请求写，没啥好说的，需要啥给啥
            const string preUrl =
                "https://login.alibaba.com/validateST.htm?pd=alibaba&pageFrom=standardlogin&u_token=&xloginPassport={0}&xloginPassword={1}&dmtrack_pageid={2}&st={3}";
            var url = string.Format(preUrl, userId, password, dmtrackPageid, st);
            var html = HttpClient.Get(url, aliClient.Cookie);
            const string xloginCallBackForRisUrl = "https://login.alibaba.com/xloginCallBackForRisk.do";
            var postString = "dmtrack_pageid_info=" + dmtrackPageid + "&xloginPassport=" + userId +
                             "&xloginPassword=" + password + "&ua=&pd=alibaba";
            //登陆成功后callback，模拟产生请求写，作用就是设置cookie
            HttpClient.Post(xloginCallBackForRisUrl, aliClient.Cookie, postString);

            if (string.IsNullOrEmpty(html) || html.IndexOf("var xman_success=", StringComparison.Ordinal) == -1)
            {
                //登陆失败了，自己查找下为啥，可能是多种情况
                return false;
            }
            //懒得用正则了
            string context = html.Replace("var xman_success=", "").Trim();
            aliClient.AliLoginUser = JsonConvert.DeserializeObject<AliLoginUser>(context);
            List<string> urls = aliClient.AliLoginUser.xlogin_urls;
            //按照产生的请求写，别管他有没有用，这样最安全
            foreach (var urlstring in urls)
            {
                HttpClient.Get(urlstring + "&moduleKey=common.xman.SetCookie&rnd=1365869799921", aliClient.Cookie);
            }
            //目前看到这个里面有CsrfToken，可能其他页面也有，找个体积小店的页面会更好
            string manageUrl = "http://hz.productposting.alibaba.com/product/manage_products.htm#tab=approved";
            string manageHtml = HttpClient.Get(manageUrl, aliClient.Cookie);
            //CsrfToken,特别重要，后面请求大部分都要带着他
            aliClient.CsrfToken = GetCsrfToken(manageHtml);
            return true;
        }
        /// <summary>
        /// 异步:登录阿里巴巴
        /// </summary>
        /// <param name="aliClient"> AliClient当需要多账号登陆时，每个账号的cookie 各种验证id等独有的东西放到各自的Client中</param>
        /// <param name="account">帐户名</param>
        /// <param name="password">密码</param>
        /// <param name="checkCode">验证码</param>
        /// <returns>登陆结果(true/false)</returns>
        public static async Task<bool> DoLoginAsync(AliClient aliClient, string account, string password, string checkCode)
        {
            return DoLoginStep1(aliClient, account, password, checkCode);
        }
        /// <summary>
        /// 找到阿里的加密方法，在一个js中，照着写了个c#的，具体在哪个js中忘了
        /// </summary>
        /// <param name="dmtrackPageId">页面中解析的pageid</param>
        /// <returns>加密后dmtrackPageId</returns>
        public static string EncryptDmtrackPage(string dmtrackPageId)
        {
            if (dmtrackPageId == "")
                return "";
            string k = dmtrackPageId;

            var f = k.Substring(0, 16);
            var j = k.Substring(16, 10);
            var result = Regex.IsMatch(j, "^[-+]?[0-9]+$");
            var a = result ? Convert.ToInt32(j) : 1234567891;
            k = f + Convert.ToString(a, 16);
            var h = 1400567053906;
            var b = k + Convert.ToString(h, 16); // [k, h.toString(16)].join("");
            for (var c = 1; c < 10; c++)
            {
                var nextRan = Math.Round(0.6070097303017974 * 10000000000);
                var l = Convert.ToString(Convert.ToInt64(nextRan), 16);
                b += l;
            }
            b = b.Substring(0, 42);
            return b;
        }
        /// <summary>
        ///     loginToken令牌
        ///     阿里算法动态改变
        ///     1.正常登陆情况下
        ///     使用xman_login_token令牌
        ///     2.异常情况下(需要输入手机验证码)
        ///     使用类似xman_abnormal令牌
        /// </summary>
        /// <param name="cookie">cookie:上下文cookie对象</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="dmtrackPageid"></param>
        /// <param name="checkcode">验证码</param>
        /// <returns></returns>
        private static string GetToken(CookieContainer cookie, string userId, string password, string dmtrackPageid, string checkcode)
        {
            const string preUrl =
                "https://login.alibaba.com/xman/xlogin.js?pd=alibaba&pageFrom=standardlogin&u_token=&xloginPassport={0}&xloginPassword={1}&xloginCheckToken={3}&dmtrack_pageid={2}";
            var url = string.Format(preUrl, userId, password, dmtrackPageid, checkcode);
            var html = HttpClient.Get(url, cookie);
            if (html.IndexOf("illegal_password", StringComparison.Ordinal) > 0)
            {
                return null;
            }

            #region 如果方法一执行不成功,那么使用方法二。

            var r = new Regex("var xman_login_token={\"token\":\"(.*?)\"}");
            var gc = r.Match(html).Groups;
            if (gc.Count > 1)
            {
                return gc[1].Value.Trim();
            }

            #region 方法2

            var arr = html.Replace("\"", "").Split(":}".ToCharArray());
            if (arr.Length >= 4)
            {
                return arr[2];
            }
            return null;

            #endregion

            #endregion
        }
        /// <summary>
        ///     获取st令牌，也不知道啥作用，照着fildder:<see cref="http://www.telerik.com/fiddler">fildder</see>发出的请求写就行了
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static string GetSt(CookieContainer cookie, string token)
        {
            const string preUrl =
                "https://passport.alipay.com/mini_apply_st.js?site=4&callback=window.xmanDealTokenCallback&token={0}";
            var url = string.Format(preUrl, token);

            var html = HttpClient.Get(url, cookie);
            var r = new Regex("{\"data\":{\"st\":\"(.*?)\"}");
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }
            var gc = r.Match(html).Groups;
            return gc.Count > 1 ? gc[1].Value.Trim() : "";
        }
        /// <summary>
        /// 获取CsrfToken 防攻击令牌
        /// </summary>
        /// <param name="html">从页面中解析</param>
        /// <returns>CsrfToken</returns>
        private static string GetCsrfToken(string html)
        {
            var r = new Regex("'_csrf_token_':'(.*?)'");
            GroupCollection gc = r.Match(html).Groups;
            return gc.Count > 1 ? gc[1].Value.Trim() : "";
        }
        /// <summary>
        /// 获取checkcode，图片验证码，其实有时候是不需要的。可以先判断一下，不行再获取验证码
        /// </summary>
        /// <param name="aliClient">aliClient</param>
        /// <returns>验证码图片，如果是web应用，直接把checkCodeUrl(img src=checkCodeUrl)写进去就行了</returns>
        public static Image DoCheckCode(AliClient aliClient)
        {
            if (string.IsNullOrWhiteSpace(aliClient.SessionId) || string.IsNullOrWhiteSpace(aliClient.DmtrackPageid))
            {
                //登陆前执行，获取SessionId与DmtrackPageid，无验证码登陆时这个放在aliclient后面执行就行了。一定要注意点，不然登陆不了的
                PrepareLogin(aliClient);
            }
            //验证码url变了两次，如果发现登陆不行，先检查这里变了没
            string checkCodeUrl = string.Format("https://pin.aliyun.com/get_img?identity=alibaba.com&sessionid={0}&kjtype=default&random={1}", aliClient.SessionId, DateTime.Now.Ticks);
            //下载图片，保险起见带着cookie
            Image image = HttpClient.DownloadImage(checkCodeUrl, aliClient.Cookie);
            return image;
        }
    }
}