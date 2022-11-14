using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Repository
{
    public class ADO_Producto
    {
        internal static void CrearProducto(Producto prod)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO producto(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) " +
                "VALUES(@descripcion,@costo,@precioVenta,@stock,@idUsuario)";

                var paramDescripcion = new SqlParameter();
                paramDescripcion.ParameterName = "descripcion";
                paramDescripcion.SqlDbType = SqlDbType.VarChar;
                paramDescripcion.Value = prod.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "costo";
                paramCosto.SqlDbType = SqlDbType.Money;
                paramCosto.Value = prod.Costo;

                var paramPrecioVenta = new SqlParameter();
                paramPrecioVenta.ParameterName = "precioVenta";
                paramPrecioVenta.SqlDbType = SqlDbType.Money;
                paramPrecioVenta.Value = prod.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "stock";
                paramStock.SqlDbType = SqlDbType.Int;
                paramStock.Value = prod.Stock;
                
                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "idUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = prod.IdUsuario;

                cmd.Parameters.Add(paramDescripcion);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramPrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsuario);
                
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            
        }

        public static void ModificarProducto(Producto prod)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE producto " +
                    "SET [Descripciones] = @descripcion, [Costo] = @costo, [PrecioVenta] = @precioVenta, [Stock] = @stock, [IdUsuario] = @idUsuario " +
                    "WHERE id = @idProd";
                

                var paramDescripcion = new SqlParameter();
                paramDescripcion.ParameterName = "descripcion";
                paramDescripcion.SqlDbType = SqlDbType.VarChar;
                paramDescripcion.Value = prod.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "costo";
                paramCosto.SqlDbType = SqlDbType.Money;
                paramCosto.Value = prod.Costo;

                var paramPrecioVenta = new SqlParameter();
                paramPrecioVenta.ParameterName = "precioVenta";
                paramPrecioVenta.SqlDbType = SqlDbType.Money;
                paramPrecioVenta.Value = prod.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "stock";
                paramStock.SqlDbType = SqlDbType.Int;
                paramStock.Value = prod.Stock;

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "idUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = prod.IdUsuario;

                var paramIdProd = new SqlParameter();
                paramIdProd.ParameterName = "idProd";
                paramIdProd.SqlDbType = SqlDbType.BigInt;
                paramIdProd.Value = prod.Id;

                cmd.Parameters.Add(paramDescripcion);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramPrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsuario);
                cmd.Parameters.Add(paramIdProd);


                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void EliminarProducto(int id)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE ProductoVendido WHERE idProducto = @idProducto";

                var param = new SqlParameter();
                param.ParameterName = "idProducto";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE producto WHERE id = @idProducto";

                var param = new SqlParameter();
                param.ParameterName = "idProducto";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

        public static List<Producto> TraerProductos()
        {
            var listaProductos = new List<Producto>();
            
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto prod = new Producto();
                    prod.Id = Convert.ToInt32(reader.GetValue(0));
                    prod.Descripciones = reader.GetValue(1).ToString();
                    prod.Costo = Convert.ToDouble(reader.GetValue(2));
                    prod.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    prod.Stock = Convert.ToInt32(reader.GetValue(4));
                    prod.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(prod);
                }

                reader.Close();
                connection.Close();
            }
            return listaProductos;
        }
    }
}
