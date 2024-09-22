using chatbot.DTOs;

namespace chatbot.Entidades
{
    public class OpcionesChatbot
    {
        public int Id { get; set; }
        public string Opcion { get; set; } = null!;
        public string Codigo { get; set; } = null!;
    }
}
