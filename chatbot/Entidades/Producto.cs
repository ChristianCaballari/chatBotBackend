﻿namespace chatbot.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Foto { get; set; } = null!;
        public int IdCategoria { get; set; }
    }
}
