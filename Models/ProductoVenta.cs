﻿namespace ProyectoFinalCoderHouse.Models
{
    public class ProductoVenta
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
    }
}
