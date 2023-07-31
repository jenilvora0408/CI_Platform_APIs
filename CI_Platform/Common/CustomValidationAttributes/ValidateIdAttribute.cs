using Common.Constants;
using Common.CustomExceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.CustomValidationAttributes
{
    public class ValidateIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey("id"))
            {
                throw new ArgumentNullException("id");
            }

            long id = (dynamic)context.ActionArguments["id"];
            if (id <= 0)
            {
                throw new EntityNullException(ExceptionMessage.ID_IS_NULL_OR_ZERO);
            }

            base.OnActionExecuting(context);
        }
    }
}