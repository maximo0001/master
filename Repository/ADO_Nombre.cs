using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Repository
{
    public class ADO_Nombre
    {
        internal static Nombre TraerNombre()
        {
            Nombre nombre1 = new Nombre();
            nombre1.NombreApp = "ProyectoFinalCoderHouse";
            return nombre1;
        }
    }
}
