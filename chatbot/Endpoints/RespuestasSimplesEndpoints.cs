using chatbot.DTOs;
using chatbot.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace chatbot.Endpoints
{
    public static class RespuestasSimplesEndpoints
    {
        
        public static RouteGroupBuilder MapRespuestasSimples(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerRespuesta).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("respuestaSimples-get"));

            return group;
        }

        static async Task<Ok<RespuestaSimpleDTO>> ObtenerRespuesta(IRepositorioRespuestasSimples repositorioRespuestasSimples,
            string codigo)
        {
            var respuesta = await repositorioRespuestasSimples.ObternerRespuestaSimple(codigo);

            return TypedResults.Ok(respuesta);
        }
    }
}
