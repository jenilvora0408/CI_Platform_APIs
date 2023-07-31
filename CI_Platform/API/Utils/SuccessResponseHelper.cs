using API.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Utils;

public class SuccessResponseHelper<T> where T : class
{
    public IActionResult GetSuccessResponse(int statusCode, List<string>? messages,T? content = null)
    {
        SuccessApiResponse<T?> successApiResponse = new(statusCode, messages ?? new List<string>(), content);

        return new ObjectResult(successApiResponse)
        {
            StatusCode= statusCode,
        };

    }

    public IActionResult GetSuccessResponse(int statusCode, string message, T? content = null)
    {
        SuccessApiResponse<T?> successApiResponse = new(statusCode, new List<string>() { message}, content);

        return new ObjectResult(successApiResponse)
        {
            StatusCode = statusCode,
        };

    }

    public IActionResult GetSuccessResponse(int statusCode, T? content = null)
    {
        SuccessApiResponse<T?> successApiResponse = new(statusCode, new List<string>(), content);

        return new ObjectResult(successApiResponse)
        {
            StatusCode = statusCode,
        };

    }
}
