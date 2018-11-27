using System;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using AccountLogin.Http;
using AccountLogin.Entity;

namespace AccountLogin.Ali
{
    public class AliClient
    {
        const string _csrf_token_ = "_csrf_token_";
        public CookieContainer Cookie { get; set; }
        public string SessionId { get; set; }

        public string CsrfToken { get; set; }
        public AliLoginUser AliLoginUser { get; set; }
        public string DmtrackPageid { get; set; }
        public string UserName
        {
            get
            {
                if (AliLoginUser == null) return null;
                if (AliLoginUser.person_data == null) return null;
                return AliLoginUser.person_data.login_id;
            }
        }
        public AliClient()
        {
            Cookie = new CookieContainer();
        }
        public string Get(string url)
        {
            return HttpClient.Get(AddCsrfTokenToUrl(url), this.Cookie, 3);
        }
        public string Post(string url, string postData)
        {
            return HttpClient.Post(AddCsrfTokenToUrl(url), this.Cookie, AddCsrfTokenToPostData(postData), 3);
        }
        //post请求时不用手动加了，默认都加上
        private string AddCsrfTokenToPostData(string postData)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(postData);
            if (queryString[_csrf_token_] == null)
            {
                queryString.Add("_csrf_token_", this.CsrfToken);
            }
            return queryString.ToString();
        }
        //Get请求时不用手动加了，默认都加上
        private string AddCsrfTokenToUrl(string url)
        {

            if (string.IsNullOrWhiteSpace(this.CsrfToken)) return url;
            UriBuilder ub = new UriBuilder(url);
            NameValueCollection queryString = HttpUtility.ParseQueryString(ub.Query);
            if (queryString[_csrf_token_] == null)
            {
                queryString.Add("_csrf_token_", this.CsrfToken);
            }
            ub.Query = queryString.ToString();
            string newurl = ub.Uri.ToString();
            return newurl;
        }

    }
}
