
using EFCore_Ejemplo.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace EFCore_Ejemplo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //CUando se trabaja con referencias circulares
            builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Decimos que trabaje con SQLServer
            builder.Services.AddDbContext<ContextoDB>(options => options.UseSqlServer("name=DefaultConnection"));

            builder.Services.AddAutoMapper(typeof(Program));//Toma la configuracion de automapper en todo el proyecto

            var app = builder.Build();

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