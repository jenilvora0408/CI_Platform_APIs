using Common.Constants;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;

public static class Appconfig
{
    public static void WebApplicationconfig(this WebApplication app)
    {
        app.UseCors(SystemConstant.CORS_ALLOW_ANY);

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.ConfigureCustomExceptionMiddleware();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseStaticFiles();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;

            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        app.Run();
    }
}