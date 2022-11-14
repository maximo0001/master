using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Repository;
using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [Route("{nombreUsuario}/{contraseña}")]
        [HttpGet]
        public Usuario inicioSesion(string nombreUsuario, string contraseña)
        {
            return ADO_Usuario.IniciarSesion(nombreUsuario, contraseña);

        }
        
        [Route("{nombreUsuario}")]
        [HttpGet]
        public Usuario Traer(string nombreUsuario)
        {
            return ADO_Usuario.TraerUsuario(nombreUsuario); 
        }
        
        [Route("{id}")]
        [HttpDelete]
        public void Eliminar(int id)
        {
            ADO_Usuario.EliminarUsuario(id);
        }

        [HttpPut]
        public void Modificar([FromBody] Usuario usu)
        {
            ADO_Usuario.ModificarUsuario(usu);
        }

        [HttpPost]
        public void Crear([FromBody] Usuario usu)
        {
            ADO_Usuario.CrearUsuario(usu);
        }
    }
}
