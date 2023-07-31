using System.Net;

namespace Common.CustomExceptions;
public class DataAlreadyExistsException : Exception
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.Conflict;
    public List<string> Messages { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }

    public DataAlreadyExistsException(string message, Dictionary<string, object>? metadata = null) : base(message)
    {
        StatusCode = (int)HttpStatusCode.Conflict;
        Metadata = metadata;
        Messages = new List<string>() { message };
    }

    public DataAlreadyExistsException(List<string> messages, Dictionary<string, object>? metaData = null) : base(messages[0])
    {
        StatusCode = (int)HttpStatusCode.Conflict;
        Messages = messages;
        Metadata = metaData;
    }
}
