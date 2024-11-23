using CQRS.Data;
using CQRS.DesignPattern.Builder;
using CQRS.DesignPattern.Factory;
using CQRS.DesignPattern.Structural.Adapter;
using CQRS.DesignPattern.Structural.Decorator;
using CQRS.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

builder.Services.AddDbContext<OnlineShopDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlayerConnection"));
});
builder.Services.AddDbContext<SportsDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"AllowAllOrigins",configurePolicy: policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
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
app.MapGraphQL();///graphql
//app.MapGraphQL("/my/graphql/endpoint");
app.MapControllers();

app.Run();
