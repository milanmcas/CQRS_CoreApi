using Alachisoft.NCache.Common.DataStructures.Clustered;
using Alachisoft.NCache.Web.SessionState;
using Asp.Versioning;
using CQRS.Data;
using CQRS.Data.Repository;
using CQRS.DesignPattern.Behavioral.Observer.Notification;
using CQRS.DesignPattern.Behavioral.Strategy;
using CQRS.DesignPattern.Builder;
using CQRS.DesignPattern.Factory;
using CQRS.DesignPattern.Singleton;
using CQRS.DesignPattern.Structural.Adapter;
using CQRS.DesignPattern.Structural.Decorator;
using CQRS.DesignPattern.Structural.Decorator.Live;
using CQRS.DesignPattern.Structural.Decorator.Live.FQCost;
using CQRS.DesignPattern.Structural.Decorator.Notification;
using CQRS.Features;
using CQRS.Formatter;
using CQRS.Handler;
using CQRS.Interceptor;
using CQRS.Middleware;
using CQRS.Models;
using CQRS.NotificationSystem;
using CQRS.Resolution;
using CQRS.Resolution.Generic;
using CQRS.Security.Models;
using CQRS.Security;
using CQRS.ServiceLife;
using CQRS.Services;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProtoBuf.Extended.Meta;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using static CQRS.Services.SingletonService;
using CQRS.Hubs;
using Microsoft.AspNetCore.Mvc.Filters;
using CQRS.OOPS;
using CQRS.CircuitBreaker;
using Polly;
using System.Text;
using Hangfire;
using CQRS.DesignPattern.Structural.Decorator.Exam1;
using CQRS.DesignPattern.Behavioral.Template;
//using FluentValidation;

var builder = WebApplication.CreateBuilder(args);//creates WebApplicationBuilder class object

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 209715;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});
builder.Services.AddDataProtection();
// Add memory cache service for rate limiting
builder.Services.AddMemoryCache();
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);

    options.ExcludedHosts.Add("test.com");
});
/*
 6RgfeSBru6_mYgbK_7ctpDwRQQc0wmdp
var configurationOptions = new ConfigurationOptions
{
    EndPoints = { builder.Configuration.GetValue<string>(AppSettingsKeys.RedisConnectionString) },
    SyncTimeout = Convert.ToInt32(builder.Configuration.GetValue<string>(AppSettingsKeys.SyncTimeout)),
    ReconnectRetryPolicy = new ExponentialRetry(Convert.ToInt32(Configuration.GetValue<string>(AppSettingsKeys.reconnectTime))),
    DefaultDatabase = Convert.ToInt32(Configuration.GetValue<string>(AppSettingsKeys.DefaultDatabase))
};
var redis = ConnectionMultiplexer.Connect(configurationOptions);
builder.Services.AddDataProtection(item => item.ApplicationDiscriminator = Configuration.GetValue<string>(AppSettingsKeys.ApplicationName))
                .PersistKeysToRedis(redis, builder.Configuration.GetValue<string>(AppSettingsKeys.ProtectionKey))
                .SetApplicationName(builder.Configuration.GetValue<string>(AppSettingsKeys.ApplicationName));
*/
builder.Services.AddHttpsRedirection(options =>
{
    //options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    //options.HttpsPort = 5001;
    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;//default 307(TemporaryRedirect)
    options.HttpsPort = 443;
});
//builder.WebHost.ConfigureKestrel((context, serverOptions) =>
//{
//    serverOptions.Listen(IPAddress.Loopback, 5000);
//    serverOptions.Listen(IPAddress.Loopback, 5001, listenOptions =>
//    {
//        listenOptions.UseHttps("testCert.pfx", "testPassword");
//    });
//});

builder.Services.RegisterDependencies();
//First way
builder.Services.AddKeyedTransient<IService, Service1>("service1");
builder.Services.AddKeyedTransient<IService, Service2>("service2");
//Second way
builder.Services.AddTransient<Service1>();
builder.Services.AddTransient<Service2>();
builder.Services.AddTransient<IGenericService<Service1>, GenericService<Service1>>();
builder.Services.AddTransient<IGenericService<Service2>, GenericService<Service2>>();
// Add services to the container.
builder.Services.AddTransient<ITransientService,TransientService>();
builder.Services.AddTransient<ITransientService1, TransientService1>();
builder.Services.AddTransient<ITransientService2, TransientService2>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddScoped<IScopedService1, ScopedService1>();
builder.Services.AddScoped<IScopedService2, ScopedService2>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IMasterUser,MasterUser>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

//Timeout: Setting a Time Limit for an Operation

//A timeout policy sets a time limit for an operation to complete.
//If the operation doesn’t finish within the specified time,
//it is considered failed.
//This is useful in scenarios where an operation is taking too long and needs
//to be terminated to free up resources.
var timeoutPolicy = Policy.TimeoutAsync(5);


//Retry: Automatically Retrying a Failed Operation

//A retry policy is designed to automatically retry a failed operation a specified number of times.
//This is particularly useful when dealing with transient faults such as temporary network
//issues, where a subsequent attempt might succeed.
var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2));

//Circuit Breaker: Stopping Requests to a Failing Service
//A circuit breaker policy helps prevent overwhelming a failing service
//by temporarily stopping requests after a certain number of failures.
//Once the circuit breaker is “tripped,”
//further attempts are blocked for a specified duration.
var circuitBreakerPolicy = Policy
            //.Handle<HttpRequestException>()
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 4,
                durationOfBreak: TimeSpan.FromMinutes(1),
                onBreak: (exception, timespan) =>
                {
                    Console.WriteLine($"Circuit broken due to: {exception.Message}");
                },
                onReset: () => Console.WriteLine("Circuit closed."),
                onHalfOpen: () => Console.WriteLine("Circuit in half-open state.")
            );

//Fallback: Providing an Alternative Response
//A fallback policy provides an alternative response when an operation fails.
//This ensures that the application can still respond,
//even if the primary operation encounters issues.

//var fallbackPolicy = Policy<HttpResponseMessage>
//        .Handle<Exception>()
//        .FallbackAsync(new HttpResponseMessage(HttpStatusCode.OK)
//        {
//            Content = new StringContent("{\"message\": \"Weather data currently unavailable\"}", Encoding.UTF8, "application/json")
//        });

var fallbackPolicy = Policy<Result<string>>
       .Handle<Exception>()
       .FallbackAsync(new Result<string>("Fallback data: Inventory data unavailable"));
builder.Services.AddSingleton<IAsyncPolicy<Result<string>>>(fallbackPolicy);

var fallbackPolicy1 = Policy
       .Handle<Exception>()
       .FallbackAsync((abc) =>  Task.Run(() => { return "Fallback data: Inventory data unavailable"; }));
//Combining Policies for Better Fault Handling
var combinedPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy, fallbackPolicy1);
//builder.Services.AddSingleton<IAsyncPolicy>(combinedPolicy);
builder.Services.Configure<CacheConfiguration>(builder.Configuration.GetSection("CacheConfiguration"));
//For In-Memory Caching
builder.Services.AddMemoryCache();
builder.Services.AddTransient<MemoryCacheService>();
builder.Services.AddTransient<RedisCacheService>();
builder.Services.AddTransient<Func<CacheTech, ICacheService>>(serviceProvider => key =>
{
    switch (key)
    {
        case CacheTech.Memory:
            return serviceProvider.GetService<MemoryCacheService>()!;
        case CacheTech.Redis:
            return serviceProvider.GetService<RedisCacheService>()!;
        default:
            return serviceProvider.GetService<MemoryCacheService>()!;
    }
});
builder.Services.AddTransient<ExternalService>();
builder.Services.AddSingleton<IAsyncPolicy>(circuitBreakerPolicy);
builder.Services.AddTransient<IExternalService>(serviceprovider =>
{
    var service = serviceprovider.GetService<ExternalService>();
    var policy = serviceprovider.GetService<IAsyncPolicy>();
    return new ResilientService(service!, policy!);
});

builder.Services.AddSingleton<ISingleton>(options =>
{
    return Singleton.GetInstance();
});

builder.Services.AddSingleton<ISingletonService1, SingletonService1>();
//builder.Services.AddTransient<INotifierService,NotifierService>();
builder.Services.AddTransient<INotifierService, NotifierDelegateService>();
builder.Services.AddTransient<ObserverFactory>();
builder.Services.AddTransient<Notifier>();
//builder.Services.AddScoped<OnlineShopDbContext>();
//builder.Services.AddScoped<IOnlineShopRepository, OnlineShopRepository>();
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    
});
builder.Services.AddScoped<IOnlineShopRepository, OnlineShopRepository>();

builder.Services.AddScoped<SampleCpntext>();

builder.Services.AddDbContext<SportsDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddInterceptors(new DemoDbCommandInterceptor());    
});

builder.Services
    .AddValidatorsFromAssemblyContaining<UserValidator>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();
builder.Services.AddScoped<Food, Sandwich>();
builder.Services.AddScoped<FoodDecorator>(options => {
    var _food = options.GetService<Food>();
    return new CheeseDecorator(_food!);
});
builder.Services.AddSingleton<CQRS.DesignPattern.Structural.Decorator.Notification.INotification,CQRS.DesignPattern.Structural.Decorator.Notification.EmailNotification>();
builder.Services.AddScoped(serviceProvider =>
{
    var notificationService = serviceProvider.GetService<CQRS.DesignPattern.Structural.Decorator.Notification.INotification>();
    //var logger = serviceProvider.GetService<ILogger<NotificationDecorator>>();

    //var playerService = serviceProvider.GetRequiredService<PlayersService>();

    NotificationDecorator smsDecorator = new SMSNotification(notificationService!);
    NotificationDecorator tpaDecorator = new ThirdpartyAPINotification(smsDecorator);

    return tpaDecorator;
});
builder.Services.AddScoped<ClassicPriceService>();
builder.Services.AddScoped<IPriceService>(provider =>new TelematicsPriceService(
    provider.GetRequiredService<ClassicPriceService>()
    ));
//https://andrewlock.net/adding-decorated-classes-to-the-asp.net-core-di-container-using-scrutor/ - decorator pattern with DI
builder.Services.AddScoped<IAnalyticsAdapter, AnalyticsAdapter>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IPlayerService,PlayerService>();
builder.Services.AddScoped<IHouseBuilder,HouseBuilder>();
builder.Services.AddScoped<PlayerDbContext>();
//builder.Services.AddScoped<SportsDbContext>();
builder.Services.AddScoped<IFootballService, FootballService>();

builder.Services.AddSingleton<TranslationDatabase>();
builder.Services.AddTransient<TranslationTransformer>();

//builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddSignalR();
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
    // Setting the publisher directly will make the instance a Singleton.
    config.NotificationPublisher = new TaskWhenAllPublisher();
    // Setting the publisher directly will make the instance a Singleton.
    //config.NotificationPublisher = new ForeachAwaitPublisher();

    // Seting the publisher type will:
    // 1. Override the value set on NotificationPublisher
    // 2. Use the service lifetime from the ServiceLifetime property below
    config.NotificationPublisherType = typeof(TaskWhenAllPublisher);

    //config.ServiceLifetime = ServiceLifetime.Transient;

    //// Seting the publisher type will:
    //// 1. Override the value set on NotificationPublisher
    //// 2. Use the service lifetime from the ServiceLifetime property below
    //config.NotificationPublisherType = typeof(TaskWhenAllPublisher);

    ////config.ServiceLifetime = ServiceLifetime.Transient;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"AllowAllOrigins",configurePolicy: policy =>
    {
        /*
        policy.AllowAnyOrigin()
        .AllowAnyHeader()//Access-Control-Allow-Header
        .AllowAnyMethod();//Access-Control-Allow-Method
                          //policy.WithOrigins("", "");//Access-Control-Allow-Origin

        options.AddPolicy("Policy1",
       policy =>
       {
           policy.WithOrigins("http://example.com",
                               "http://www.contoso.com");
       });
        options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().
        AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
        options.AddPolicy("AnotherPolicy",
            policy =>
            {
                policy.WithOrigins("http://www.contoso.com")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });
        */
        
    });
    options.AddPolicy(name: "VerbPolicy",
            policy =>
            {
                policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .WithMethods("POST", "GET", "OPTIONS");
            });
});

builder.Services.AddScoped<INotificationFactory,NotificationFactory>();

builder.Services.AddScoped<PushNotification>()
            .AddScoped<CQRS.DesignPattern.Factory.INotification, PushNotification>(s => s.GetService<PushNotification>());

builder.Services.AddScoped<SmsNotification>()
            .AddScoped<CQRS.DesignPattern.Factory.INotification, SmsNotification>(s => s.GetService<SmsNotification>());
builder.Services.AddScoped<CQRS.DesignPattern.Factory.EmailNotification>()
            .AddScoped<CQRS.DesignPattern.Factory.INotification, CQRS.DesignPattern.Factory.EmailNotification>(s => s.GetService<CQRS.DesignPattern.Factory.EmailNotification>());

builder.Services.AddKeyedScoped<CreditCard, MoneyBack>("MoneyBack");
builder.Services.AddKeyedScoped<CreditCard, Titanium>("Titanium");
builder.Services.AddKeyedScoped<CreditCard, Platinum>("Platinum");
builder.Services.AddScoped<HouseTemplate, ConcreteHouse>();
builder.Services.AddScoped<IAggregatorRequestServiceFactory, AggregatorRequestServiceFactory>();
builder.Services.AddScoped<IRequestProcessor, RequestProcessor>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IValidator<UserRegistrationRequest>, UserRegistrationValidator>();
//builder.Services.AddKeyedScoped<CreditCard, MoneyBack>(Card.MoneyBack);
//builder.Services.AddKeyedScoped<CreditCard, Titanium>(Card.Titanium);
//builder.Services.AddKeyedScoped<CreditCard, Platinum>(Card.Platinum);

//builder.Services.AddTransient<Func<Card, CreditCard>>(serviceProvider => key =>
//{
//    switch (key)
//    {
//        case Card.MoneyBack:
//            return serviceProvider.GetService<MoneyBack>();
//        case Card.Titanium:
//            return serviceProvider.GetService<Titanium>();
//        default:
//            return serviceProvider.GetService<Platinum>();
//    }
//});

builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.Decorate<IPlayersService, PlayersServiceLoggingDecorator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = @"Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=priya;Encrypt=True;Trust Server Certificate=True;";
    options.SchemaName = "dbo";
    options.TableName = "SQLSessions";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "CQRS.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

builder.Services.AddApiVersioning(apiVerConfig =>
{
    apiVerConfig.ReportApiVersions = true;
    apiVerConfig.AssumeDefaultVersionWhenUnspecified = true;
    apiVerConfig.DefaultApiVersion = new ApiVersion(1,0);
}).AddApiExplorer(
    options =>
    {
        options.AssumeDefaultVersionWhenUnspecified=true;
        options.DefaultApiVersion = new ApiVersion(1,0);
        options.GroupNameFormat = "'v'VVV";
        //options.SubstituteApiVersionInUrl = true;
    });
//builder.Services.AddAuthorization(config =>
//{
//    config.AddPolicy("BlockGet", p => p.RequireAssertion(context =>
//    {
//        var filterContext = context.Resource as AuthorizationFilterContext;
//        var httpMethod = filterContext.HttpContext.Request.Method;
//        if (httpMethod == HttpMethod.Get.Method)
//            return false;
//        // add conditional authorization here
//        return true;
//    }));

//    config.AddPolicy("Edit", p => p.RequireAssertion(context =>
//    {
//        var filterContext = context.Resource as AuthorizationFilterContext;
//        var httpMethod = filterContext.HttpContext.Request.Method;
//        // add conditional authorization here
//        return true;
//    }));
//});
//builder.Services.AddStartupTask();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
    {
        AbortOnConnectFail = true,
        EndPoints = { options.Configuration }
    };
});
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddControllers(options =>
{
    //options.RespectBrowserAcceptHeader = true;
    //options.ReturnHttpNotAcceptable = true;//ReturnHttpNotAcceptable = true; with this configuration, we won’t get the default configuration if the requested format type is not supported by the server.
    options.FormatterMappings.SetMediaTypeMappingForFormat("txt", "text/plain");
    //options.InputFormatters.Add();
    options.OutputFormatters.Add(new CsvOutputFormatter());
    //options.Filters.Add(new ProducesAttribute("application/xml"));//default response format for entire application
    
    //options.Filters.Add(new ConsumesAttribute("application/json"));
    //Content-Type:application/xml
    //Accept:application/xml
}).AddXmlSerializerFormatters()
    .AddXmlDataContractSerializerFormatters()
    .AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); ;

// Configure NCache Session State
//builder.Services.AddNCacheSession(options =>
//{
//    options.CacheName = "demoCache";
//    options.EnableLogs = true;
//    options.UseJsonSerialization = true; // To avoid the Binary Format error
//    options.SessionAppId = "demoApp";
//    options.SessionOptions.IdleTimeout = 5;
//    options.SessionOptions.CookieName = "AspNetCore.Session";
//});

var app = builder.Build();//creates WebApplication class instance
//app.UseExceptionHandler("/error");
app.UseExceptionHandler("/error");
StaticDIClass.Initialize(app.Services.GetRequiredService<ISingletonService1>(), app.Services.GetRequiredService<IGenericService<Service1>>());//correct
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts(); // Add this line
app.UseHttpsRedirection();
//app.MapWhen(context => context.Request.Path.Value!.StartsWith("/api/Factory/country", StringComparison.OrdinalIgnoreCase),
//    appBuilder =>
//    {
//        appBuilder.UseMiddleware<MyMiddleware1>();
//    });
//app.UseWhen(context => context.Request.Path.Value!.StartsWith("/api/Factory/country", StringComparison.OrdinalIgnoreCase),
//    appBuilder =>
//    {
//        appBuilder.UseMiddleware<MyMiddleware1>();
//    });
app.UseRouting();
//app.UseCors("CORSPolicy");
app.UseCors("VerbPolicy");
app.UseAuthorization();
var rateLimitRule = new RateLimitRule
{
    Limit = 115,
    Window = TimeSpan.FromSeconds(10)
};

app.UseMiddleware<RateLimitingMiddleware>(app.Services.GetRequiredService<IMemoryCache>(), rateLimitRule);
app.UseSession();
//app.UseSignalR(routes =>
//{
//    routes.MapHub<GridEventsHub>("/hubs/gridevents");
//});
//app.UseNCacheSession(); // store NCache session data    
app.MapGraphQL();///graphql
//app.MapGraphQL("/my/graphql/endpoint");

//app.MapDynamicControllerRoute()
app.Map("/default", () => "Hello World!");
app.MapGet("/default1", () => "Hello default1 World!");
app.MapPost("/default2", () => "Hello default2 World!");
app.MapPost("/OnlineShopping3", async (context) =>
{
    Console.WriteLine("Mapped OnlineShopping3 middleware to /map");
    await context.Response.WriteAsync("Hello OnlineShopping3 from the /map path");

});
app.Map("/api/OnlineShopping1", mappedApp =>
{
    mappedApp.Use(async (context, next) =>
    {
        Console.WriteLine("Mapped middleware to /map");
        await context.Response.WriteAsync("Hello from the /map path");
        await next.Invoke(context);
    });
});
app.MapPost("/api/OnlineShopping2", async(context) =>
{
    Console.WriteLine("Mapped OnlineShopping2 middleware to /map");
    await context.Response.WriteAsync("Hello OnlineShopping2 from the /map path");
    
});
app.UseMiddleware<MyMiddleware>();


app.MapWhen(context => context.Request.Path.Value!.Contains("api/Factory/country", StringComparison.OrdinalIgnoreCase),
    appBuilder =>
{
    appBuilder.UseMiddleware<MyMiddleware1>();
});

//app.MapWhen(context => context.Request.Path.StartsWithSegments("/aaFactory111"), appBuilder =>
//{
//    app.UseMiddleware<MyMiddleware1>();
//});
//app.MapWhen(context => context.Request.Method.ToUpper() == "GET", appBuilder =>
//{
//    appBuilder.UseMiddleware<MyMiddleware1>();
//});
app.UseHangfireDashboard("/jobs");
app.UseMiddleware<MyMiddleware2>();

app.UseMiddleware<CustomMiddleware>();

app.UseWhen(context => context.Request.Path.Value!.Contains("Factory"), appBuilder =>
{
    appBuilder.UseMiddleware<CustomMiddleware3>();
});

//app.UseWhen(context => (context.Request.Method == HttpMethod.Get.Method),
//    appBuilder =>
//{
//    //Console.WriteLine(context.Request.Method);
//    app.UseMiddleware<CustomMiddleware1>();
//});
app.MapPost("/minimal/register", async (UserRegistrationRequest request, IValidator<UserRegistrationRequest> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    // perform actual service call to register the user to the system
    // _service.RegisterUser(request);
    return Results.Accepted();
});

app.UseMiddleware<CustomMiddleware2>();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/offers");
});
app.MapDynamicControllerRoute<TranslationTransformer>("{language}/{controller}/{action}");
app.MapControllers();
app.UseMiddleware<CustomMiddleware33>();
app.Run();
