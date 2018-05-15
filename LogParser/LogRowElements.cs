namespace LogParser
{
    public class LogRowElements
    {
        public int PartsCount { get; set; }
        public string IP { get; set; }
        public string Identity { get; set; }
        public string UserId { get; set; }
        public string Timestamp { get; set; }
        public string Offset { set; get; }
        public string RequestMessage { get; set; }
        public string StatusCode { get; set; }
        public string Size { get; set; }
        public string Referer { get; set; }
        public string URL { get; set; }
        public string UserAgent { get; set; }
        public string Forwarded { get; set; }
        public bool Error { get; set; }
    }
}
