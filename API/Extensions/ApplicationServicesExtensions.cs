using API.Errors;
using Core.Data;
using Core.Identity;
using Core.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationservicesExtensions
    {
        public static IServiceCollection AddApplicationservices(this IServiceCollection services, IConfiguration config)
        {
                // Add services to the container.

                services.AddControllers();
           
                services.AddDbContext<StoreContext>(options =>
                {
                    options.UseSqlite(config.GetConnectionString("DefaultConnection"));
                });
          
            // Add redis in start up
            services.AddSingleton<IConnectionMultiplexer>(c => {
                    var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                    return ConnectionMultiplexer.Connect(options);
                });
                services.AddScoped<IBasketRepository, BasketRepository>();
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddScoped<ITokenService, TokenService>();
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                //Change the beahvior options of APi in controller 
                services.Configure<ApiBehaviorOptions>(options => {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                             .SelectMany(x => x.Value.Errors)
                                                             .Select(x => x.ErrorMessage).ToArray();

                        var errorResponse = new ApiValidationErrorResponse
                        {
                            Errors = errors
                        };

                        return new BadRequestObjectResult(errors);
                    };
                });
            //Enable CORS Cross  Original Resource Service
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyHeader().WithOrigins("https://localhost:4200");
                });
            });
           return services;
        }
    }
}