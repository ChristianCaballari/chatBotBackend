using chatbot.DTOs;

namespace chatbot.Repositorios
{
    public interface IRepositorioRespuestasSimples
    {
        Task<RespuestaSimpleDTO?> ObternerRespuestaSimple(string codigo);
    }
}
