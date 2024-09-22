namespace chatbot.DTOs
{
    public class OpcionesChatbotDTO
    {
        public int Id { get; set; }
        public string Opcion { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public List<PreguntasChatbotDTO> Preguntas { get; set; } = new List<PreguntasChatbotDTO>();
    }
}
