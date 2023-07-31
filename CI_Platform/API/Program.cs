using API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ApplicationConfiguration applicationConfiguration = new();

applicationConfiguration.ExecuteBuilderConfiguration(builder);

WebApplication app = builder.Build();

applicationConfiguration.ExecuteAppConfiguration(app);