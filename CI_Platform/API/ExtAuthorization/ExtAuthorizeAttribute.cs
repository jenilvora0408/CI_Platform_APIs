namespace API.ExtAuthorization;

public class ExtAuthorizeAttribute : TypeFilterAttribute
{
    public ExtAuthorizeAttribute() : base(typeof(ExtAuthorizeFilter))
    {

    }
}