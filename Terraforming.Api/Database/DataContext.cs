using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Terraforming.Api.Models;

namespace Terraforming.Api.Database
{
    public class DataContext
    {
        public IMongoDatabase Database { get; private set; }
       
        public MongoDbRepository<User> Users { get; set; }
        public MongoDbRepository<Game> Games { get; set; }

        public DataContext(string connectionString)
        {
            var url = new MongoUrl(connectionString);

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);
            if (url.DatabaseName == null)
            {
                throw new ArgumentException("Your connection string must contain a database name", connectionString);
            }
            this.Database = client.GetDatabase(url.DatabaseName);

            Users = new MongoDbRepository<User>(this.Database, "users");
            Users = new MongoDbRepository<User>(this.Database, "games");

        }
    }
}
