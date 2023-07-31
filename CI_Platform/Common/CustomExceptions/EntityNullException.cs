using System.Net;

namespace Common.CustomExceptions;
public class EntityNullException : Exception
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.NotFound;
    public List<string> Messages { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }

    public EntityNullException(string message, Dictionary<string, object>? metadata = null) : base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        Metadata = metadata;
        Messages = new List<string>() { message };
    }

    public EntityNullException(List<string> messages, Dictionary<string, object>? metaData = null) : base(messages[0])
    {   
        StatusCode = (int)HttpStatusCode.BadRequest;
        Messages = messages;
        Metadata = metaData;
    }
}
