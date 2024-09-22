using AutoMapper;
using chatbot.DTOs;
using chatbot.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace chatbot.Endpoints
{
    public static class CategoriasEndpoints
    {
        public static RouteGroupBuilder MapCategorias(this RouteGroupBuilder group)
        {
            group.MapGet("/", Obtener).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("categorias-get"));
           
            return group;
        }

        static async Task<Ok<CategoriaResumenDTO>> Obtener(IRepositorioCategorias repositorioCategorias)
        {
            var categorias = await repositorioCategorias.ObtenerTodos();

            return TypedResults.Ok(categorias);
        }
    }
}
