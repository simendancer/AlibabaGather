using System;
using System.Collections;
using System.Net;
using System.Reflection;

namespace AccountLogin.Http
{
    public static class CookieHelper
    {
        public static CookieCollection GetAllCookies(CookieContainer cookie)
        {
            var cookieCollection = new CookieCollection();

            var table = (Hashtable)cookie.GetType().InvokeMember("m_domainTable",
                                                                            BindingFlags.NonPublic |
                                                                            BindingFlags.GetField |
                                                                            BindingFlags.Instance,
                                                                            null,
                                                                            cookie,
                                                                            new object[] { });

            foreach (var tableKey in table.Keys)
            {
                var strTableKey = (string)tableKey;

                if (strTableKey[0] == '.')
                {
                    strTableKey = strTableKey.Substring(1);
                }

                var list = (SortedList)table[tableKey].GetType().InvokeMember("m_list",
                                                                            BindingFlags.NonPublic |
                                                                            BindingFlags.GetField |
                                                                            BindingFlags.Instance,
                                                                            null,
                                                                            table[tableKey],
                                                                            new object[] { });

                foreach (var listKey in list.Keys)
                {
                    var url = "https://" + strTableKey + (string)listKey;
                    cookieCollection.Add(cookie.GetCookies(new Uri(url)));
                }
            }

            return cookieCollection;
        }

        public static string GetCookie(CookieContainer cookie, string key)
        {
            CookieCollection cc = GetAllCookies(cookie);

            Cookie c = cc[key];//如果不存在key会返回null
            return c == null ? null : c.Value;
        }
    }
}
