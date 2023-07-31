using API.Extension;
using Common.Utils.Models;

namespace API;

public class ApplicationConfiguration
{
	public void ExecuteBuilderConfiguration(WebApplicationBuilder builder)
	{
        JwtSetting jwtSetting = new();

        EmailSetting emailSetting = new();

        builder.DbConfig();

        builder.InternalServicesConfig();
        
        builder.ExtAuthConfig(jwtSetting, emailSetting);

        builder.MiddlewareConfig();
        
        builder.ServiceAndRepoConfig();
        
        builder.ExternalServiceConfig();

        builder.SwaggerConfig();
    }

    public void ExecuteAppConfiguration(WebApplication app)
    {
        app.WebApplicationconfig();
    }
}
