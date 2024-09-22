namespace chatbot.DTOs
{
    public class CrearProductoDTO
    {
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public IFormFile? Foto { get; set; }
        public int IdCategoria { get; set; }
    }
}
