using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Data.Configuration
{
    public class PromocionDbConfig : IPromocionDbConfig
    {
        public string PromocionesCollectionName { get => "Promocion"; set => throw new NotImplementedException(); }
        //public string ConnectionString { get => "mongodb+srv://userPromocion:admin123@ds011111.mongolab.com:11111/PromocionesDb"; set => throw new NotImplementedException(); }
       // public string ConnectionString { get => "mongodb+srv://userPromocion:admin123@ds011111.mongolab.com:11111/PromocionesDb?connect=replicaSet"; set => throw new NotImplementedException(); }
        public string ConnectionString { get=> "mongodb://127.0.01:27017"; set => throw new NotImplementedException(); }//=> "mongodb+srv://userPromocion:admin123@cluster0.fp1ss.mongodb.net/PromocionesDb?retryWrites=true&w=majority"; set => throw new NotImplementedException(); }
        public string DatabaseName { get => "PromocionDb"; set => throw new NotImplementedException(); }

      }
}
