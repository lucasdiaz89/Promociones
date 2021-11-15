using Microsoft.Build.Evaluation;
using MongoDB.Driver;
using Promociones.Core.Exeptions;
using Promociones.Core.Models;
using Promociones.Infrastructure.Data.Configuration;
using Promociones.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promociones.Infrastructure.Data
{
    public class PromocionesDb : IPromocionesDb
    {
        private readonly IMongoCollection<Promocion> _PromocionCollection;
        private readonly Validaciones _Validaciones;
        internal PromocionRepository _PromocionRepository = new PromocionRepository();
        public PromocionesDb(IPromocionDbConfig config, IPromocionConfigServices promocionConfig)
        {

            // var mdbClient = new MongoClient(settings.ConnectionString);
            var mdbClient = promocionConfig.Client;

            var database = mdbClient.GetDatabase(config.DatabaseName);

            _PromocionCollection = _PromocionRepository.db.GetCollection<Promocion>("Promociones");  //database.GetCollection<Promocion>(config.PromocionesCollectionName);

        }


        public List<Promocion> GetAllPromociones()
        {
         return _PromocionCollection.Find(p=>true).ToList();
        }

        public Promocion GetPromocionById(string id)
        {
            var filtro = "{id:{$eq:id}}";
            return _PromocionCollection.Find(filtro).First();
            //return _PromocionCollection.Find(promo => promo.Id == id).First();
        }

        public List<Promocion> GetPromocionesVigentes()
        {
            var filtros = Builders<Promocion>.Filter.Lte("FechaFin",DateTime.Now);

            return _PromocionCollection.Find(filtros).ToList();

            //return _PromocionCollection.Find(promo => promo.FechaFin <= DateTime.Now).ToList();
        }

        public List<Promocion> GetPromocionByFecha(DateTime fecha)
        {
            var filtros = Builders<Promocion>.Filter.Lte("FechaFin", fecha);
           
            return _PromocionCollection.Find(filtros).ToList();

            //  return _PromocionCollection.Find(promo => promo.FechaFin <= fecha).ToList();
        }

        public List<PromocionFiltro> GetPromocionByFiltros(string MediosDePagos, string Bancos,string CategoriasProductos) //IEnumerable<string> CategoriasProductos)
        {

            List<Promocion> promocion = _PromocionCollection.Find(promo =>
                                 String.Join(", ", promo.MediosDePagos.Select(o => o.ToString())) == MediosDePagos
                                 &&
                                 String.Join(", ", promo.Bancos.Select(o => o.ToString())) == Bancos
                                 &&
                                 String.Join(", ", promo.CategoriasProductos.Select(o => o.ToString())) == CategoriasProductos).ToList();
                                // promo.CategoriasProductos.SequenceEqual(CategoriasProductos)).ToList();

            List <PromocionFiltro> promocionFiltro = new List<PromocionFiltro>();
            int cant = promocion.Count;
            for (int i = 0; i <= cant; i++)
            {
                promocionFiltro[i].Id = promocion[i].Id;
                promocionFiltro[i].MediosDePagos = promocion[i].MediosDePagos;
                promocionFiltro[i].Bancos = promocion[i].Bancos;
                promocionFiltro[i].CategoriasProductos = promocion[i].CategoriasProductos;
                promocionFiltro[i].MaximaCantidadDeCuotas = promocion[i].MaximaCantidadDeCuotas;
                promocionFiltro[i].ValorInteresCuotas = promocion[i].ValorInteresCuotas;
                promocionFiltro[i].PorcentajeDeDescuento = promocion[i].PorcentajeDeDescuento;

            }
            return promocionFiltro;
        }


        public string InsertPromocion(Promocion promocion)
        {

            _PromocionCollection.InsertOne(promocion);
            return promocion.Id;
            //if (_Validaciones.ValidacionGral(GetPromocionesVigentes(), promocion))
            //{
            //    _PromocionCollection.InsertOne(promocion);
            //    return promocion.Id;
            //}

            //return "No se cumplio con el esquema de validaciones";
        }

        public string UpdatePromocion(Promocion promocion, string id)
        {
            if (_Validaciones.ValidacionGral(GetPromocionesVigentes(), promocion))
            {
                GetPromocionById(id);
                _PromocionCollection.ReplaceOne(x => x.Id == promocion.Id, promocion);
                return promocion.Id;
            }

            return "No se cumplio con el esquema de validaciones";
        }

        public string UpdateFechasPromocion(DateTime FechaInicio, DateTime FechaFin, string id)
        {

            if (_Validaciones.validacionFechas(FechaInicio, FechaFin))
            {
                Promocion promocion = GetPromocionById(id);
                _PromocionCollection.ReplaceOne(x => x.Id == promocion.Id, promocion);
                return promocion.Id;
            }


            return "No se cumplio con el esquema de validaciones";
        }

        public void DeletePromocion(string id)
        {

            Promocion promocion = GetPromocionById(id);
            promocion.Activo = false;
            UpdatePromocion(promocion, id);
        }


    }
}
