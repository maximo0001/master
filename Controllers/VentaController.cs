using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController
    {
        [HttpPost]
        public void Cargar([FromBody] VentaCarga ven)
        {
            ADO_Venta.CargarVenta(ven);
        }
        [Route("{idUsuario}")]
        [HttpGet]
        public List<VentaTraer> Traer(int idUsuario)
        {
            return ADO_Venta.TraerVentas(idUsuario);
        }

        [Route("{idVenta}")]
        [HttpDelete]
        public void Eliminar(int idVenta)
        {
            ADO_Venta.EliminarVenta(idVenta);
        }
    }
}
