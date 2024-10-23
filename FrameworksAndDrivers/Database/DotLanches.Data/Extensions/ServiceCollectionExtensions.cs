using DotLanches.DataMongo.Repositories;
using DotLanches.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DotLanches.DataMongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string DATABASE_NAME = "dotlanche";

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider => new MongoClient(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton(provider => provider.GetRequiredService<MongoClient>().GetDatabase(DATABASE_NAME));

            RegisterConventions();

            services.AddScoped<IPedidoRepository, PedidoRepository>();

            return services;
        }

        private static void RegisterConventions()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
            };
            ConventionRegistry.Register("DotlancheConventions", pack, t => true);
        }
    }
}