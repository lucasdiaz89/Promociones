using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Repositories
{
    public class PromocionRepository
    {

        public MongoClient client;
        public IMongoDatabase db;
        public PromocionRepository()
        {
            client = new MongoClient("mongodb://127.0.01:27017");// new MongoClient("mongodb+srv://userPromocion:admin123@cluster0.fp1ss.mongodb.net/PromocionesDb?retryWrites=true&w=majority");
            db = client.GetDatabase("PromocionDb");
        }
    }
}
