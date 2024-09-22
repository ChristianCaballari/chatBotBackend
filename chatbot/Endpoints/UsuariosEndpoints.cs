using AutoMapper;
using chatbot.DTOs;
using chatbot.Entidades;
using chatbot.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;


namespace chatbot.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static RouteGroupBuilder MapUsuarios(this RouteGroupBuilder group)
        {
            group.MapPost("/", Crear);
            return group;
        }

        static async Task<Results<Created<UsuarioDTO>, NotFound, BadRequest<string>>> Crear(CrearUsuarioDTO crearUsuarioDTO,
               IMapper mapper,IRepositorioUsuarios repositorioUsarios)
        {
            var usuario = mapper.Map<Usuario>(crearUsuarioDTO);
            var id = await repositorioUsarios.Crear(usuario);
            usuario.Id = id;
            var usuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            return TypedResults.Created($"/comentario/{id}", usuarioDTO);
        }
    }
}
