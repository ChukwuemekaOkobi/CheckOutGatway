using Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAPI.Helpers;
namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDatabaseConfiguration(builder); 

            builder.Services.AddApiConfiguration();

            builder.Services.AddServices(); 




            var app = builder.Build();

            //add seed data
            var scopeservice = app.Services.CreateScope().ServiceProvider;
            var context = scopeservice.GetService<ApplicationDbContext>();
            Database.AddData(context);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}