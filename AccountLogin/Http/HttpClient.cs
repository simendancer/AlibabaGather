using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AccountLogin.Http
{
    public class HttpClient
    {
        const string Accept = "*/*";
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko)";
        const int Timeout = 15000;//超时时间，但前提是请求允许超时才行。
        const string AcceptLanguage = "Accept-Language:zh-CN,zh;q=0.8";
        const string AcceptEncoding = "Accept-Encoding:gzip, deflate, sdch, br";
        const string Contenttype = "application/x-www-form-urlencoded";
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static HttpClient()
        {
            //伪造证书,验证服务器证书回调自动验证
            ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            //客户端系统 win7或者winxp上可能会出现 could not create ssl/tls secure channel的问题导致加载ali登陆验证码报错
            //这里必须设置
            ServicePointManager.Expect100Continue = false;//默认是true，要手动设为false 
            ServicePointManager.SecurityProtocol=SecurityProtocolType.Ssl3;

            ServicePointManager.DefaultConnectionLimit = 1000;
        }

        public static string Get(string url, CookieContainer cookie, int retryCount = 3)
        {
            return Request(url, cookie, "GET", null, retryCount);
        }
        public static string Post(string url, CookieContainer cookie, string postData, int retryCount = 3)
        {
            return Request(url, cookie, "POST", postData, retryCount);
        }

        public static string Post(string url, CookieContainer cookie, string postData, Action<HttpWebRequest> beginRequest, int reTry = 0)
        {
            return Request(url, cookie, "POST", postData, reTry, beginRequest);
        }

        private static string Request(string url, CookieContainer cookie, string method, string postData, int retryCount, Action<HttpWebRequest> beginRequest = null)
        {
            string html = string.Empty;
            for (var i = 0; i <= retryCount; i++)
            {
                try
                {
                    html = Request(url, cookie, method, postData, beginRequest);
                    return html;
                }
                catch (Exception e)
                {
                    if (i == retryCount)
                    {
                        throw new Exception(e.ToString());
                    }
                }
            }
            return html;
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 伪造证书,总是接受  
            return true;
        }

        private static string Request(string url, CookieContainer cookie, string method, string postData, Action<HttpWebRequest> beginRequestHandle = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.CookieContainer = cookie;
            request.AllowAutoRedirect = true;
            request.ContentType = Contenttype;
            request.Accept = Accept;
            request.Timeout = Timeout;
            request.UserAgent = UserAgent;
            request.Headers.Add(AcceptLanguage);
            request.Headers.Add(AcceptEncoding);
            request.Host = "passport.alipay.com";
            request.Referer = "https://login.alibaba.com/";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            if (beginRequestHandle != null)
                beginRequestHandle(request);

            if (!string.IsNullOrEmpty(postData))
            {
                byte[] byteRequest = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteRequest.Length;
                var stream = request.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();
            }
            var httpWebResponse = (HttpWebResponse)request.GetResponse();

            var responseStream = httpWebResponse.GetResponseStream();
            if (responseStream == null) return string.Empty;
            if (responseStream.CanTimeout)
            {
                responseStream.ReadTimeout = 15000;
            }
            var encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet ?? "UTF8");

            var streamReader = new StreamReader(responseStream, encoding);

            string html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            request.Abort();
            httpWebResponse.Close();
            return html;
        }

        public static byte[] DownloadFile(string url, CookieContainer cookie)
        {
            var webClient = new CookieWebClient { Cookie = cookie };
            byte[] data = webClient.DownloadData(url);
            return data;
        }
        public static Image DownloadImage(string url, CookieContainer cookie)
        {
            byte[] data = DownloadFile(url, cookie);
            var ms = new MemoryStream(data);
            var image = Image.FromStream(ms);
            ms.Close();
            return image;
        }
    }
}
