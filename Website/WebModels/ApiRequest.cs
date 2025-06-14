namespace Website.WebModels
{
    public class ApiRequest
    {
        public HttpMethod Method { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
