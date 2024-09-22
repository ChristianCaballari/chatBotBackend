namespace chatbot.Entidades
{
    public class PreguntasChatbot
    {
        public  int Id { get; set; }
        public string Pregunta { get; set; } = null!;
        public string Codigo { get; set; } = null!; 
        public int IdOpcionChatbot { get; set; }

    }
}
