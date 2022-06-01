

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Services.Data;
using Services.Implementation;
using System.Text.Json.Serialization;

namespace WebAPI.Helpers
{
    public static class Configuration
    {
        public static void AddApiConfiguration(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options => options.Filters.Add(typeof(ModelValidationFilter)))
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

           });

            services.AddCors(options =>
            {
                options.AddPolicy("CheckOut", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod()
                    .AllowAnyHeader().Build();
                });
            });

            services.AddEndpointsApiExplorer();
   
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert ApiKey into the field",
                    Name = "ApiKey",
                    Type = SecuritySchemeType.ApiKey,


                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id= "ApiKey"
                            }
                        } , Array.Empty<string>()
                    }
                });


            });

        }

        public static void AddDatabaseConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
        {


            services.AddDbContext<ApplicationDbContext>(opt =>
                       opt.UseInMemoryDatabase("APIDatabase")
                       .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));



        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthentication, AuthenticationService>();
            services.AddScoped<IPayment, PaymentService>();
        }
    }
}
