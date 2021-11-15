using MongoDB.Driver;
using Promociones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Data.Configuration
{
    public interface IPromocionConfigServices
    {
        MongoClient Client { get; }
        IMongoCollection<Promocion> GetPromocionesCollection();//last
    }
   
}
