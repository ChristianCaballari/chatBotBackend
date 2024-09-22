using chatbot.Entidades;

namespace chatbot.Repositorios
{
    public interface IRepositorioUsuarios
    {
        Task<int> Crear(Usuario usuario);
    }
}
