using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Repository;
using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    
    public class ProductoVentaController
    {
        [Route("{idUsuario}")]
        [HttpGet]
        public List<ProductoVenta> Traer(int idUsuario)
        {
            return ADO_ProductoVenta.TraerProductosVendidos(idUsuario);
        }
    }

    

}
