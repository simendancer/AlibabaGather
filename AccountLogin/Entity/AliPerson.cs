namespace AccountLogin.Entity
{
    /// <summary>
    ///     用户基本信息
    /// </summary>
    public class AliPerson
    {
        public string login_id { get; set; }
        public string p_status { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
        public string last_name { get; set; }
        public string v_status { get; set; }
        public string country { get; set; }
    }
}