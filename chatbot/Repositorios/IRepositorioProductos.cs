using chatbot.DTOs;
using chatbot.Entidades;

namespace chatbot.Repositorios
{
    public interface IRepositorioProductos
    {
        Task<int> Crear(Producto producto);
        Task<List<ProductoViewDTO>> ObtenerTodos();
        Task<List<ProductoPromocionDTO>> ObtenerPromocion();
        Task<List<ProductoViewDTO>> BusquedaGenerica(List<string> frases);
    }
}
