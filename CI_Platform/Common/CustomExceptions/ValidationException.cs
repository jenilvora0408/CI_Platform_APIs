using System.Net;

namespace Common.CustomExceptions;

public class ValidationException : Exception
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public List<string> Messages { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }

    public ValidationException(string message, Dictionary<string, object>? metadata = null) : base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        Metadata = metadata;
        Messages = new List<string>() { message };
    }

    public ValidationException(List<string> messages, Dictionary<string, object>? metaData = null) : base(messages[0])
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        Messages = messages;
        Metadata = metaData;
    }
}
