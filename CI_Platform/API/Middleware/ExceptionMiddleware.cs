using API.CustomExceptions;
using API.Response;
using Common.Constants;
using Common.CustomExceptions;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq.Dynamic.Core.Exceptions;
using System.Net;
using System.Security.Authentication;

namespace API.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions and generating error responses.
    /// </summary>
    public class ExceptionMiddleware : IMiddleware
    {
        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and generates an error response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="ex">The exception to handle.</param>
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            ErrorApiResponse response = GenerateErrorApiResponse(context, ex);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string jsonResponse = JsonConvert.SerializeObject(response, serializerSettings);
            await context.Response.WriteAsync(jsonResponse);

        }

        /// <summary>
        /// Generates an error response based on the given exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>An instance of <see cref="ErrorApiResponse"/>.</returns>
        public ErrorApiResponse GenerateErrorApiResponse(HttpContext context, Exception ex)
        {
            (int httpStatusCode, List<string> messages, Dictionary<string, object>? metadata) = GetExceptionInfo(ex);

            context.Response.StatusCode = httpStatusCode;

            return new ErrorApiResponse()
            {
                Success = false,
                HttpStatusCode = httpStatusCode,
                Messages = messages,
                Metadata = metadata
            };
        }

        /// <summary>
        /// Retrieves the appropriate HTTP status code and message for the given exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>A tuple containing the HTTP status code, message and metadata</returns>
        public (int, List<string>, Dictionary<string, object>?) GetExceptionInfo(Exception ex)
        {
            List<string> messages = new();
            int httpStatusCode = (int)HttpStatusCode.InternalServerError;

            void AddStatusCodeAndMessage(HttpStatusCode statusCode, string message)
            {
                httpStatusCode = (int)statusCode;
                messages.Add(message);
            }

            switch (ex)
            {
                case SmtpProtocolException:
                    AddStatusCodeAndMessage(HttpStatusCode.InternalServerError, ExceptionMessage.MAIL_NOT_SENT);
                    break;

                case UnauthorizedAccessException unauthorizedAccessException:
                    AddStatusCodeAndMessage(HttpStatusCode.Unauthorized, unauthorizedAccessException.Message);
                    break;

                case DbUpdateConcurrencyException:
                    AddStatusCodeAndMessage(HttpStatusCode.Conflict, ExceptionMessage.CONCURRENCY);
                    break;

                case DbUpdateException dbUpdateException:
                    AddStatusCodeAndMessage(HttpStatusCode.BadRequest, dbUpdateException.Message);
                    break;

                case InvalidOperationException:
                    AddStatusCodeAndMessage(HttpStatusCode.BadRequest, ExceptionMessage.INTERNAL_SERVER);
                    break;

                case AuthenticationException authenticationException:
                    AddStatusCodeAndMessage(HttpStatusCode.Unauthorized, authenticationException.Message);
                    break;

                case ParseException:
                    return ((int)HttpStatusCode.NotFound, new List<string> { ExceptionMessage.INVALID_SORCOLUMN }, null);

                case ValidationException validationException:
                    return (validationException.StatusCode, validationException.Messages, validationException.Metadata);

                case FileNullException fileNullException:
                    return (fileNullException.StatusCode, fileNullException.Messages, fileNullException.Metadata);

                case ForbiddenException forbiddenException:
                    return (forbiddenException.StatusCode, forbiddenException.Messages, null);

                case EntityNullException entityNullException:
                    return (entityNullException.StatusCode, entityNullException.Messages, entityNullException.Metadata);

                case DataAlreadyExistsException dataAlreadyExistsException:
                    return (dataAlreadyExistsException.StatusCode, dataAlreadyExistsException.Messages, dataAlreadyExistsException.Metadata);

                case SecurityTokenExpiredException:
                    return ((int)HttpStatusCode.Unauthorized, new List<string> { ExceptionMessage.TOKEN_EXPIRED }, null);

                case InvalidModelStateException invalidModelStateException:
                    return (invalidModelStateException.StatusCode, invalidModelStateException.Messages, null);

                default:
                    messages.Add(ex.Message);
                    break;
            }

            return (httpStatusCode, messages, null);
        }
    }
}
