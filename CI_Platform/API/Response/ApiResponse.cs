namespace API.Response
{
    public abstract class ApiResponse
    {

        public bool Success { get; set; }

        public int HttpStatusCode { get; set; }

        public List<string> Messages { get; set; } = new List<string>();
    }

    public class SuccessApiResponse<T> : ApiResponse
    {
        public T? Content { get; set; }

        public SuccessApiResponse(int httpStatusCode, List<string> message, T? content)
        {
            HttpStatusCode= httpStatusCode;
            Messages = message;
            Content = content;
            Success= true;
        }
    }

    public class ErrorApiResponse : ApiResponse
    {
        public Dictionary<string, object>? Metadata { get; set; }   
    }

    //public class ErrorMetadata
    //{
    //    public string? Source { get; set; }
    //    public string? StackTrace { get; set; }
    //}
}
