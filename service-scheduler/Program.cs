using Newtonsoft.Json.Serialization;
using secure_ftp_service.Core.Services;
using Serilog;
using service_scheduler.Helpers;
using service_scheduler.Models;
using service_scheduler.Services;
using System.Text.Json.Serialization;

// Reading appsettings 
IConfiguration config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile(ConstantSupplier.APP_SETTINGS_FILE_NAME)
                  .Build();

// Setup a static Log.Logger instance
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .CreateLogger();

try
{
    // Scheduler is started and it is captured in the log.
    Log.Information(ConstantSupplier.LOG_INFO_SCHEDULER_START_MSG);
    var builder = WebApplication.CreateBuilder(args);

    // In case of null property, Add newtonsoftjon into the services container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }).AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });


    //Set serilog as a logging provider.
    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
      loggerConfiguration.ReadFrom
      .Configuration(hostingContext.Configuration));
    builder.Services.AddHostedService<BackgroundHostedService>();
    builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

    //------Configure Services started. Add services to the container (ConfigureServices(IServiceCollection services) Method from the last .NET 5).----
    builder.Services.AddTransient<ILogService, LogService>();
    builder.Services.AddSingleton<IWorkExecutor, WorkExecutor>();


    // Registering IHttpClientFactory in the DI container into the service with the extension method AddHttpClient
    // (AddHttpClient method registers the internal DefaultHttpClientFactory class to be used as a singleton for the interface IHttpClientFactory).
    // During the registration of IHttpClientFactory ino the service, The HttpClient can be configured with Polly's policies.
    var apimodel = builder.Configuration.GetSection(nameof(ApiSettingsModel)).Get<ApiSettingsModel>();
    builder.Services.AddHttpClient<IWorkExecutor, WorkExecutor>(ConstantSupplier.HTTP_CLIENT_LOGICAL_NAME, client =>
    {
        client.BaseAddress = new Uri(apimodel.APIBaseURL);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(ConstantSupplier.HTTP_HEADERS_CONTENT_TYPE_NAME, ConstantSupplier.HTTP_HEADERS_CONTENT_TYPE_VALUE);
    }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

    var app = builder.Build();


    //--------- Configuring the HTTP request pipeline, which consists of middlewares.---------------
    app.UseHttpsRedirection();

    app.Run();
}
catch (Exception Ex)
{
    Log.Fatal(Ex, ConstantSupplier.LOG_ERROR_SCHEDULER_TERMINATE_MSG);
    throw;
}

