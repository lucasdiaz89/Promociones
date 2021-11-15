
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Data.Configuration
{
    public interface IPromocionDbConfig
    {

        public string PromocionesCollectionName { get ; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }


    }
}
