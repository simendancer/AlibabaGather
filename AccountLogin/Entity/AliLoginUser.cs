using System.Collections.Generic;

namespace AccountLogin.Entity
{
    /// <summary>
    ///     登录用户json反序列化对应类
    /// </summary>
    public class AliLoginUser
    {
        public List<string> xlogin_urls { get; set; }
        public AliPerson person_data { get; set; }
        public string time_out { get; set; }
        public List<string> proxy_cookies { get; set; }
    }
}