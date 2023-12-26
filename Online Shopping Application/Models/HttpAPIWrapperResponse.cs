using System.Net;

namespace Online_Shopping_Application.Models
{
    public class HttpAPIWrapperResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public T data { get; set; }
    }
}
