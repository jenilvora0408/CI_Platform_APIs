using Common.CustomExceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.CustomValidationAttributes
{
    public class ValidateIdOnCreateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            object model = context.ActionArguments["dto"];
            var idProperty = model.GetType().GetProperty("Id");

            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(model);
                if (idValue != null && !idValue.Equals(GetDefault(idValue.GetType())))
                {
                    throw new EntityNullException("Id cannot be given when creating a new entity");
                }
            }

            base.OnActionExecuting(context);
        }

        private static object? GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}