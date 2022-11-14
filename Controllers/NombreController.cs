using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NombreController
    {
        [HttpGet]
        public Nombre Traer()
        {
            return ADO_Nombre.TraerNombre();
        }
    }
}
