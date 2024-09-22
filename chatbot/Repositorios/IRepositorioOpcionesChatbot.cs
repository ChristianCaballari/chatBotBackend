using chatbot.DTOs;
using chatbot.Entidades;
using System.Collections;

namespace chatbot.Repositorios
{
    public interface IRepositorioOpcionesChatbot
    {
        Task<List<OpcionesChatbot>> OpcionesChatbots();
        Task<OpcionesChatbotDTO> OptenerPreguntasOpcionesChatbot(int id);
    }
}
