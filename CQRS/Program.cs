using Alachisoft.NCache.Web.SessionState;
using CQRS.Data;
using CQRS.Data.Repository;
using CQRS.DesignPattern.Behavioral.Observer.Notification;
using CQRS.DesignPattern.Builder;
using CQRS.DesignPattern.Factory;
using CQRS.DesignPattern.Structural.Adapter;
using CQRS.DesignPattern.Structural.Decorator;
using CQRS.Interceptor;
using CQRS.ServiceLife;
using CQRS.Services;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;
using static CQRS.Services.SingletonService;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSingleton<ISingletonService1, SingletonService1>();
builder.Services.AddTransient<INotifierService,NotifierService>();
builder.Services.AddTransient<ObserverFactory>();
builder.Services.AddTransient<Notifier>();
//builder.Services.AddScoped<OnlineShopDbContext>();
//builder.Services.AddScoped<IOnlineShopRepository, OnlineShopRepository>();
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlayerConnection"));
});
builder.Services.AddScoped<IOnlineShopRepository, OnlineShopRepository>();

builder.Services.AddScoped<SampleCpntext>();

builder.Services.AddDbContext<SportsDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddInterceptors(new DemoDbCommandInterceptor());
});
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();
builder.Services.AddScoped<Food, Sandwich>();
builder.Services.AddScoped<FoodDecorator>(options => {
    var _food = options.GetService<Food>();
    return new CheeseDecorator(_food);
});
builder.Services.AddScoped<IAnalyticsAdapter, AnalyticsAdapter>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IPlayerService,PlayerService>();
builder.Services.AddScoped<IHouseBuilder,HouseBuilder>();
builder.Services.AddScoped<PlayerDbContext>();
//builder.Services.AddScoped<SportsDbContext>();
builder.Services.AddScoped<IFootballService, FootballService>();
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

        options.AddPolicy("AnotherPolicy",
            policy =>
            {
                policy.WithOrigins("http://www.contoso.com")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });
    });
});

builder.Services.AddScoped<INotificationFactory,NotificationFactory>();

builder.Services.AddScoped<PushNotification>()
            .AddScoped<CQRS.DesignPattern.Factory.INotification, PushNotification>(s => s.GetService<PushNotification>());

builder.Services.AddScoped<SmsNotification>()
            .AddScoped<CQRS.DesignPattern.Factory.INotification, SmsNotification>(s => s.GetService<SmsNotification>());
builder.Services.AddScoped<EmailNotification>()
            .AddScoped<CQRS.DesignPattern.Factory.INotification, EmailNotification>(s => s.GetService<EmailNotification>());

builder.Services.AddKeyedScoped<CreditCard, MoneyBack>("MoneyBack");
builder.Services.AddKeyedScoped<CreditCard, Titanium>("Titanium");
builder.Services.AddKeyedScoped<CreditCard, Platinum>("Platinum");

builder.Services.AddControllers();

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = @"Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=milan;Trust Server Certificate=True;";
    options.SchemaName = "dbo";
    options.TableName = "SQLSessions";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "CQRS.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});
builder.Services.AddControllers().AddXmlSerializerFormatters()
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseSession();
//app.UseNCacheSession(); // store NCache session data    
app.MapGraphQL();///graphql
//app.MapGraphQL("/my/graphql/endpoint");
app.MapControllers();

app.Run();
