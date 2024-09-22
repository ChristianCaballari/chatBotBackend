namespace chatbot.DTOs
{
    public class ProductoViewDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Foto { get; set; } = null!;
        public string Categoria { get; set; } = null!;
    }
}
