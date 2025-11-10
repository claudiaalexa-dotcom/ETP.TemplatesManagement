using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.ServiceHost.Behaviors;
using ETP.TemplatesManagement.ServiceHost.Extensions;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace ETP.TemplatesManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ServiceHost.Filters.GlobalExceptionFilter>();
            });

            builder.Services.AddAutoMapper();
            builder.Services.AddValidationAndMediatR();         

            builder.Services.AddMongoDb(builder.Configuration);
            builder.Services.AddSingleton<ITemplateRepository, TemplateRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
