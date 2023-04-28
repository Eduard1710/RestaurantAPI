using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddControllers();
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
