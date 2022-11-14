using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Repository
{
    public class ADO_Venta
    {
        public static void CargarVenta(VentaCarga ven)
        {
            int idInsertado;
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Venta(comentarios,idUsuario) VALUES(@com,@idUsu) " +
                   "select @@IDENTITY";

                var paramCom = new SqlParameter();
                paramCom.ParameterName = "com";
                paramCom.SqlDbType = SqlDbType.VarChar;
                paramCom.Value = ven.Comentarios;

                var param = new SqlParameter();
                param.ParameterName = "idUsu";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = ven.IdUsuario;
                
                cmd.Parameters.Add(paramCom);
                cmd.Parameters.Add(param);
                
                idInsertado = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            foreach (var p in ven.Productos)
            {
                using (SqlConnection connection = new SqlConnection(General.connectionString()))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO productoVendido(stock,idProducto,idVenta) VALUES(@stock,@idProducto,@idVenta)";

                    var paramStock = new SqlParameter();
                    paramStock.ParameterName = "stock";
                    paramStock.SqlDbType = SqlDbType.Int;
                    paramStock.Value = p.Stock;

                    var paramIdProducto = new SqlParameter();
                    paramIdProducto.ParameterName = "idProducto";
                    paramIdProducto.SqlDbType = SqlDbType.BigInt;
                    paramIdProducto.Value = p.Id;

                    var paramIdVenta = new SqlParameter();
                    paramIdVenta.ParameterName = "idVenta";
                    paramIdVenta.SqlDbType = SqlDbType.BigInt;
                    paramIdVenta.Value = idInsertado;

                    cmd.Parameters.Add(paramStock);
                    cmd.Parameters.Add(paramIdProducto);
                    cmd.Parameters.Add(paramIdVenta);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                using (SqlConnection connection = new SqlConnection(General.connectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "update Producto set [stock] = stock - @stock where id = @idProd";

                    var paramStock = new SqlParameter();
                    paramStock.ParameterName = "stock";
                    paramStock.SqlDbType = SqlDbType.Int;
                    paramStock.Value = p.Stock;

                    var paramIdProducto = new SqlParameter();
                    paramIdProducto.ParameterName = "idProd";
                    paramIdProducto.SqlDbType = SqlDbType.BigInt;
                    paramIdProducto.Value = p.Id;

                    cmd.Parameters.Add(paramIdProducto);
                    cmd.Parameters.Add(paramStock);

                    cmd.ExecuteNonQuery();
                    connection.Close(); 
                }
            }


        }

        public static List<VentaTraer> TraerVentas(int idUsuario)
        {
            List<VentaTraer> listaVentas = new List<VentaTraer>();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select v.*,pv.IdProducto,p.Descripciones,pv.Stock from venta v " + 
                    "inner join ProductoVendido pv on pv.IdVenta=v.id "+ 
                    "inner join Producto p on p.id=pv.IdProducto "+
                    "where v.IdUsuario=@idUsu";

                var param = new SqlParameter();
                param.ParameterName = "idUsu";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = idUsuario;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var vt = new VentaTraer();

                    vt.Id = Convert.ToInt32(reader.GetValue(0));
                    vt.Comentarios = reader.GetValue(1).ToString();
                    vt.IdUsuario = Convert.ToInt32(reader.GetValue(2));
                    vt.IdProducto = Convert.ToInt32(reader.GetValue(3));
                    vt.Descripciones = reader.GetValue(4).ToString();
                    vt.Stock = Convert.ToInt32(reader.GetValue(5));

                    listaVentas.Add(vt);

                }
                connection.Close();
            }
            return listaVentas;
        }

        public static void EliminarVenta(int idVenta)
        {
            var listaProductosVendidos = new List<ProductoVenta>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * FROM ProductoVendido WHERE idVenta = @idVen ";
                    
                var paramId = new SqlParameter();
                paramId.ParameterName = "idVen";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = idVenta;

                cmd.Parameters.Add(paramId);
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
                connection.Close();
            }
            foreach (var pv in listaProductosVendidos)
            {
                using (SqlConnection connection = new SqlConnection(General.connectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE Producto SET stock = stock + @stock " +
                        "WHERE id = @id";

                    var paramStock = new SqlParameter();
                    paramStock.ParameterName = "stock";
                    paramStock.SqlDbType = SqlDbType.Int;
                    paramStock.Value = pv.Stock;

                    var paramId = new SqlParameter();
                    paramId.ParameterName = "id";
                    paramId.SqlDbType = SqlDbType.BigInt;
                    paramId.Value = pv.IdProducto;

                    cmd.Parameters.Add(paramStock);
                    cmd.Parameters.Add(paramId);

                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
            }

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM ProductoVendido WHERE idVenta = @idVen";

                var paramId = new SqlParameter();
                paramId.ParameterName = "idVen";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = idVenta;
                cmd.Parameters.Add(paramId);

                cmd.ExecuteNonQuery ();
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM venta WHERE id= @idVen";

                var paramId = new SqlParameter();
                paramId.ParameterName = "idVen";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = idVenta;
                cmd.Parameters.Add(paramId);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
