namespace API_Demo.Security
{
    public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpiersMinutes { get; set; }
    }
}
