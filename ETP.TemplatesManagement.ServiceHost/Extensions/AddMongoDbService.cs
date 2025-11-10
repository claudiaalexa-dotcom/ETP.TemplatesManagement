using ETP.TemplatesManagement.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ETP.TemplatesManagement.ServiceHost.Extensions
{
    public static class AddMongoDbService
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB configuration (use appsettings or fallbacks)
            var mongoConn = configuration.GetValue<string>("MongoSettings:ConnectionString") ?? "mongodb://localhost:27017";
            var mongoDbName = configuration.GetValue<string>("MongoSettings:Database") ?? "templatesdb";
            var mongoCollectionName = configuration.GetValue<string>("MongoSettings:Collection") ?? "template";

            // Register Mongo client + collection
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConn));
            services.AddSingleton(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                var db = client.GetDatabase(mongoDbName);
                return db.GetCollection<Template>(mongoCollectionName);
            });

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            
            return services;
        }
    }
}
