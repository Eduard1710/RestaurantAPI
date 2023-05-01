using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantAPI.Services.Managers;
using RestaurantAPI.Services.Repositories;
using RestaurantAPI.Services.UnitsOfWork;

namespace RestaurantAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void ConfigurationService(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["ConnectionStrings:RestaurantDBConnectionString"];
            builder.Services.AddDbContext<RestaurantContext>(o => o.UseSqlServer(connectionString));

            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<MenuService>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderMenuRepository, OrderMenuRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<AuthorizationService>();

            builder.Services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
            builder.Services.AddScoped<IMenuUnitOfWork, MenuUnitOfWork>();
            builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddControllers();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = "LabAPI-Backend",
                    ValidAudience = "LabAPI-Anyone",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
                };
            });

            builder.Services.AddAuthorization();
        }

        public static void Configure(WebApplication app)
        {
            //Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
