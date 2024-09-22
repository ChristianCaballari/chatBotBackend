using AutoMapper;
using chatbot.DTOs;
using chatbot.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace chatbot.Endpoints
{
    public static class OpcionesChatbotEndpoints
    {
        public static RouteGroupBuilder MapOpcionesChatbot(this RouteGroupBuilder group)
        {
            group.MapGet("/", Obtener).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("opciones-get"));
            group.MapGet("/{id}", ObtenerPreguntasOpcionesChatbot);
            return group;
        }

        static async Task<Ok<List<OpcionesChatbotDTO>>> Obtener(IRepositorioOpcionesChatbot repositorioOpcionesChatbot,
        IMapper mapper)
        {
            var opcionesChatBot = await repositorioOpcionesChatbot.OpcionesChatbots();
            var opcionesChatbotDTO = mapper.Map<List<OpcionesChatbotDTO>>(opcionesChatBot);

            return TypedResults.Ok(opcionesChatbotDTO);
        }

        static async Task<Ok<OpcionesChatbotDTO>> ObtenerPreguntasOpcionesChatbot(IRepositorioOpcionesChatbot repositorioOpcionesChatbot,
       IMapper mapper,int id)
        {
            var opcion = await repositorioOpcionesChatbot.OptenerPreguntasOpcionesChatbot(id);
            return TypedResults.Ok(opcion);
        }
    }
}
