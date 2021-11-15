using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Promociones.Core.Models;
using Promociones.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promociones.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PromocionesController : ControllerBase
    {

        private readonly IPromocionesDb _promocionServices;
        public PromocionesController(IPromocionesDb promocionServices)
        {
            _promocionServices = promocionServices;
        }
       
        [HttpGet]
        [ActionName("ObtenerTodos")]
        public IActionResult GetPromociones()
        {
            return Ok(_promocionServices.GetAllPromociones());
        }
       
        [HttpGet("{id}")]
        [ActionName("ObtenerPorId")]
        public IActionResult GetPromocionById(string id)
        {
            return Ok(_promocionServices.GetPromocionById(id));
        }

        [HttpGet]
        [ActionName("OptenerVigentes")]
        public IActionResult GetPromocionesVigentes()
        {
            return Ok(_promocionServices.GetPromocionesVigentes());
        }
        [HttpGet("{Fecha}")]
        [ActionName("ObtenerVigentesXFecha")]
        public IActionResult GetPromocionVigenteFecha(DateTime Fecha)
        {
            return Ok(_promocionServices.GetPromocionByFecha(Fecha));
        }
       
        [HttpGet("{MediosDePagos,Bancos,CategoriasProductos}")]
        [ActionName("ObtenerXFiltros")]
        public IActionResult GetPromocionVigenteFecha(string MediosDePagos, string Bancos, string CategoriasProductos)//IEnumerable<string> CategoriasProductos)
        {
            return Ok(_promocionServices.GetPromocionByFiltros(MediosDePagos, Bancos, CategoriasProductos));
        }

        [HttpPost]
        [ActionName("AgregarPromocion")]
        public IActionResult AddPromociones(Promocion promocion)
        {
            string respuesta = _promocionServices.InsertPromocion(promocion);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ActionName("EliminarUnaPorId(borrado Logico)")]
        public IActionResult DeletePromocionById(string id)
        {
            return Ok(NoContent());
        }

    }
}
