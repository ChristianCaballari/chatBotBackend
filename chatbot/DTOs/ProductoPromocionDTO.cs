namespace chatbot.DTOs
{
    public class ProductoPromocionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioConDescuento { get; set; }
        public string Descuento { get; set; } = null!;
        public string Foto { get; set; } = null!;
    }
}
