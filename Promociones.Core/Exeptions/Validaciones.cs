using MongoDB.Driver;
using Promociones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Core.Exeptions
{
    public class Validaciones
    {
        public bool ValidacionGral(List<Promocion> promocionesVigentes, Promocion promocionNueva)
        {
            if (CuotasYPorcentajes(promocionNueva.MaximaCantidadDeCuotas, promocionNueva.PorcentajeDeDescuento))
                if (CuotasEInteres(promocionNueva.MaximaCantidadDeCuotas, promocionNueva.ValorInteresCuotas))
                    if (CantidadDescuento(promocionNueva.PorcentajeDeDescuento))
                        if (validacionFechas(Convert.ToDateTime(promocionNueva.FechaInicio), Convert.ToDateTime(promocionNueva.FechaFin)))
                            return true;


            return false;
        }

        public bool ValidacionBancos(Promocion promocion)
        {
            return true;
        }
        public bool ValidacionMediosdePago(Promocion promocion)
        {
            return true;
        }
        public bool ValidacionCategorias(Promocion promocion)
        {
            return true;
        }
        // No se deben solapar promociones para al menos un medio de pago, banco o categoría
        public bool SolapamientoMedPagoBanco(List<Promocion> promocionesVigentes, Promocion promocionNueva, IEnumerable<string> MediosDePagos, IEnumerable<string> Bancos)
        {
            return true;
        }
       
        //Se deben contemplar duplicidad
        public bool dulicidad(List<Promocion> promocionesVigentes, Promocion promocionNueva)
        {
            return true;
        }

        //Cantidad de cuotas y porcentaje de descuento son nullables pero al menos una debe tener valor
        public bool CuotasYPorcentajes(int? MaximaCantidadDeCuotas, decimal? PorcentajeDeDescuento)
        {
            if ((MaximaCantidadDeCuotas == 0 || MaximaCantidadDeCuotas == null) && (PorcentajeDeDescuento == 0 || PorcentajeDeDescuento == null))
            {
                return false;
            }
            return true;
        }


        //Porcentaje interés cuota solo puede tener valor si cantidad de cuotas tiene valor
        public bool CuotasEInteres(int? MaximaCantidadDeCuotas, decimal? ValorInteresCuotas)
        {
            if (ValorInteresCuotas != 0 && (MaximaCantidadDeCuotas == 0 || MaximaCantidadDeCuotas == null))
            {
                return false;
            }
            return true;
        }

        //Porcentaje descuento en caso de tener valor, debe estar comprendido entre 5 y 80
        public bool CantidadDescuento(decimal? PorcentajeDeDescuento)
        {
            if (PorcentajeDeDescuento >= 0)
            {
                if (PorcentajeDeDescuento >= 5 && PorcentajeDeDescuento <= 80)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        //No debe haber solapamiento de fechas
        public bool validacionFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            if (FechaInicio <= FechaFin)
            {
                return true;
            }
            return false;

        }
    }
}
