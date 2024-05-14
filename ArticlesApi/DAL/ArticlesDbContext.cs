using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using MongoDB.Driver;
using NuGet.Configuration;

namespace ArticlesApi.DAL
{
    public class ArticlesDbContext
    {
        private readonly IMongoDatabase database;
        private IDatabaseSettings dbSettings;

        public ArticlesDbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
            dbSettings = settings;
        }

        public IMongoCollection<Article> Articles => database.GetCollection<Article>(dbSettings.ArticlesCollectionName);
    }
}
