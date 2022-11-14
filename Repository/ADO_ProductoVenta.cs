using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinalCoderHouse.Models;




namespace ProyectoFinalCoderHouse.Repository
{
    public class ADO_ProductoVenta
    {
        public static List<ProductoVenta> TraerProductosVendidos(int idUsuario)
        {
            var listaProductosVendidos = new List<ProductoVenta>();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select pv.* from ProductoVendido pv " +
                    "inner join producto p on p.Id = pv.IdProducto " +
                    "where p.IdUsuario=@idUsu";

                var param = new SqlParameter();
                param.ParameterName = "idUsu";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = idUsuario;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var pv = new ProductoVenta();
                    pv.Id = Convert.ToInt32(reader.GetValue(0));
                    pv.Stock = Convert.ToInt32(reader.GetValue(1));
                    pv.IdProducto = Convert.ToInt32(reader.GetValue(2));
                    pv.IdVenta = Convert.ToInt32(reader.GetValue(3));
                    
                    listaProductosVendidos.Add(pv);

                }
                reader.Close();

                connection.Close();

            }
            return listaProductosVendidos;
        }
    }
}
