
using ETP.TemplatesManagement.Data.Models;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.ServiceHost.Behaviors;
using FluentValidation;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
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

            // MongoDB configuration (use appsettings or fallbacks)
            var mongoConn = builder.Configuration.GetValue<string>("MongoSettings:ConnectionString") ?? "mongodb://localhost:27017";
            var mongoDbName = builder.Configuration.GetValue<string>("MongoSettings:Database") ?? "templatesdb";
            var mongoCollectionName = builder.Configuration.GetValue<string>("MongoSettings:Collection") ?? "template";

            // Register Mongo client + collection
            builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConn));
            builder.Services.AddSingleton(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var db = client.GetDatabase(mongoDbName);
                return db.GetCollection<Template>(mongoCollectionName);
            });

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            builder.Services.AddSingleton<ITemplateRepository, TemplateRepository>();

            builder.Services.AddAutoMapper(config => { }, Assembly.GetExecutingAssembly());
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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
