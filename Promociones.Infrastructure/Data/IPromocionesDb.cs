using Promociones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Data
{
   public interface IPromocionesDb
    {

        string InsertPromocion(Promocion promocion);
        string UpdatePromocion(Promocion promocion, string id);
        void DeletePromocion(string id);
        Promocion GetPromocionById(string id);
        List<Promocion> GetAllPromociones();
        List<Promocion> GetPromocionesVigentes();
        List<Promocion> GetPromocionByFecha(DateTime fecha);
        List<PromocionFiltro> GetPromocionByFiltros(string MediosDePagos, string Bancos, string CategoriasProductos);
        string UpdateFechasPromocion(DateTime FechaInicio, DateTime FechaFin, string id);
    }
}
