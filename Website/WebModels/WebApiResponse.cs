using System.Net;

namespace Website.WebModels
{
    public class WebApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
