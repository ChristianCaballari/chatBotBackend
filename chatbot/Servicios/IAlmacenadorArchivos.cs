namespace chatbot.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<string> Almacenar(string contenedor, IFormFile archivo);
    }
}
