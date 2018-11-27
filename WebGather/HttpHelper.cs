using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools.Tool;

namespace WebGather
{
    public class HttpHelper
    {
        private static CookieContainer cookie = new CookieContainer();
        private static string[] userAgents = {
                                    "HTTP/1.1 Sosospider+(+ http://help.soso.com/webspider.htm)",
                                    "HTTP/1.1 Baiduspider+(+ http://www.baidu.com/search/spider.htm)",
                                    "HTTP/1.1 Mozilla/5.0+(compatible;+Googlebot/2.1;++ http://www.google.com/bot.html)",
                                    "HTTP/1.1 Mozilla/4.0+(compatible;+MSIE+8.0;+Windows+NT+6.1;+WOW64;+Trident/4.0;+SLCC2;+.NET+CLR+2.0.50727;+.NET+CLR+3.5.30729;+.NET+CLR+3.0.30729;+Media+Center+PC+6.0;+MDDC)",
                                    "HTTP/1.1 Mozilla/5.0+(Windows;+U;+Windows+NT+6.1;+en-US)+AppleWebKit/534.10+(KHTML,+like+Gecko)+Chrome/8.0.552.224+Safari/534.10 ASP.NET_SessionId=k5mdnp2yy1zidz45jagvc455",
                                    "Mozilla/5.0 (compatible; +Googlebot/2.1;++http://www.google.com/bot.html)",
                                    "Mozilla/4.0 (compatible; MSIE 6.01; Windows NT 5.0)",
                                    "Mozilla/5.0 (Windows NT 5.2) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.186 Safari/535.1",
                                    "Mozilla/5.0 (Windows NT 5.2; rv:10.0.2) Gecko/20100101 Firefox/10.0.2",
                                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.71 Safari/537.36",
                                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3026.3 Safari/537.36"
                                  };

        #region 发送请求
        private string HttpPost(Uri uri, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.Timeout = 30000;
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.UserAgent = userAgents[new Random().Next(0, userAgents.Length - 1)];
                if (cookie.Count > 0)
                    request.CookieContainer = cookie;

                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string HttpGet(Uri uri, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.Timeout = 30000;
                request.KeepAlive = true;
                request.ContentType = "text/html;charset=utf-8";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.UserAgent = userAgents[new Random().Next(0, userAgents.Length - 1)];
                if (cookie.Count > 0)
                    request.CookieContainer = cookie;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string HttpPostPlus(string Url, byte[] postData, CookieContainer cookie, String encodingFormat, String referer, string ProxyStr)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url.ToString());
                if (ProxyStr != "" && ProxyStr != null)
                {
                    //设置代理
                    WebProxy proxy = new WebProxy();
                    proxy.Address = new Uri(ProxyStr);
                    myRequest.UseDefaultCredentials = true;
                    myRequest.Proxy = proxy;
                }
                //myRequest.ServicePoint.Expect100Continue = false;
                myRequest.CookieContainer = cookie;
                myRequest.Method = "POST";
                myRequest.Timeout = 30000;
                myRequest.KeepAlive = true;//modify by yang
                if (referer != "")
                    myRequest.Referer = referer;
                myRequest.Headers["Cache-control"] = "no-cache";//.CachePolicy = .c "no-cache";//["Cache-control: no-cache"]
                myRequest.Headers["Accept-Language"] = "zh-cn";
                //myRequest.Headers["x-requested-with"] = "XMLHttpRequest";
                myRequest.UserAgent = userAgents[new Random().Next(0, userAgents.Length - 1)];
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.Accept = "*/*";
                myRequest.ContentLength = postData.Length;

                //setRequestHeader(requestHearder, myRequest);

                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(postData, 0, postData.Length);
                newStream.Close();
                //if (waitTime != 0)
                //    Thread.Sleep(waitTime);
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding(encodingFormat));
                string outdata = reader.ReadToEnd();
                reader.Close();
                if (!outdata.Contains("基础连接已经关闭: 连接被意外关闭") && !outdata.Contains("无法连接到远程服务器") && !outdata.Contains("基础连接已经关闭: 接收时发生错误。"))
                    return outdata;
                else
                    return "基础连接已经关闭: 连接被意外关闭";

            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("基础连接已经关闭: 连接被意外关闭") && !ex.Message.Contains("无法连接到远程服务器") && !ex.Message.Contains("基础连接已经关闭: 接收时发生错误。"))
                    return ex.Message;
                else
                    return "基础连接已经关闭: 连接被意外关闭";
            }

        }
        #endregion

        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetHTTPResponseHeaders(string Url)
        {
            Dictionary<string, string> HeaderList = new Dictionary<string, string>();

            WebRequest WebRequestObject = HttpWebRequest.Create(Url);
            WebResponse ResponseObject = WebRequestObject.GetResponse();

            foreach (string HeaderKey in ResponseObject.Headers)
                HeaderList.Add(HeaderKey, ResponseObject.Headers[HeaderKey]);

            ResponseObject.Close();

            return HeaderList;
        }
    }
}
