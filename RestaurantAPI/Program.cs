using RestaurantAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Startup.ConfigurationService(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Startup.Configure(app);
app.Run();

