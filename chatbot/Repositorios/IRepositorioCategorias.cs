using chatbot.DTOs;

namespace chatbot.Repositorios
{
    public interface IRepositorioCategorias
    {
        Task<CategoriaResumenDTO?> ObtenerTodos();
    }
}
