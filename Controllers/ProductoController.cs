using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
        
    {
        [HttpGet]
        public List<Producto> Traer()
        {
            return ADO_Producto.TraerProductos();
        }
        [HttpPost]
        public void Crear([FromBody] Producto prod)
        {
            if (prod.Id == 0)
            {
                ADO_Producto.CrearProducto(prod);
            }
        }

        [HttpPut]
        public void Modificar([FromBody] Producto prod)
        {
            ADO_Producto.ModificarProducto(prod);
        }

        [Route("{idProducto}")]
        [HttpDelete]
        public void Eliminar(int idProducto)
        {
            ADO_Producto.EliminarProducto(idProducto);
        }

    }
}
