using System;
using System.Net;

namespace AccountLogin.Http
{
    public class CookieWebClient : WebClient
    {
        public CookieContainer Cookie { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = Cookie;
            }
            return request;
        }
    }
}
