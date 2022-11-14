namespace ProyectoFinalCoderHouse.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }

    }
    public class VentaCarga
    {
        public int IdUsuario { get; set; }
        public List<Producto> Productos { get; set; }
        public string Comentarios { get; set; }
    }

    public class VentaTraer
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public string Descripciones { get; set; }
        public int Stock { get; set; }
       
    }
}
