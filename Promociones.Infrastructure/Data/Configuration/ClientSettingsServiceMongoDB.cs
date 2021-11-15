using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Promociones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Data.Configuration
{
    public class ClientSettingsServiceMongoDB : IPromocionConfigServices
    {
        private readonly IMongoCollection<Promocion> _Promocion;

       
        public MongoClient Client { get; }
        public ClientSettingsServiceMongoDB(IPromocionDbConfig config)
        {
            Client = GetClient(config);
            var database = Client.GetDatabase(config.DatabaseName);
            _Promocion = database.GetCollection<Promocion>(config.PromocionesCollectionName);
           //  = Client.GetDatabase.Get database.GetCollection<Book>(bookstoreDbConfig.Value.Books_Collection_Name);

        }
        public IMongoCollection<Promocion> GetPromocionesCollection() => _Promocion;


        private MongoClient GetClient(IPromocionDbConfig config)
        {
            // Extrae el host y puerto del connectionstring
            var uriMongoDB = new Uri(config.ConnectionString);

            string host = uriMongoDB.Host;
            int port = uriMongoDB.Port;
            string[] userInfo = uriMongoDB.UserInfo.Split(':');
            string user = userInfo[0];
            string pwd = userInfo[0];

            // Crea las credenciales para conectarse a MongoDB
            var credentials = MongoCredential.CreateCredential(config.DatabaseName, user, pwd);

            // Instancia el cliente de MongoDB
            var mdbClient = new MongoClient(new MongoClientSettings()
            {

                Server = new MongoServerAddress(host, 27017),
                //Server = new MongoServerAddress(host, port),
                Credential = credentials,
                ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        ConsoleColor c = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("MongoDb:");
                        Console.BackgroundColor = c;
                        Console.WriteLine($" ManagedThreadId: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine($" CommandName: {e.CommandName}");
                        Console.WriteLine($"\t Command: {e.Command.ToJson()}");
                    });
                }
            });

            return mdbClient;
        }


        
        
    }
}
