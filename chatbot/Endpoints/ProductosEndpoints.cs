using AutoMapper;
using chatbot.DTOs;
using chatbot.Entidades;
using chatbot.Repositorios;
using chatbot.Servicios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace chatbot.Endpoints
{
    public static class ProductosEndpoints
    {
        private static readonly string contenedor = "productos";
        public static RouteGroupBuilder MapProductos(this RouteGroupBuilder group)
        {
            group.MapPost("/", Crear).DisableAntiforgery()
               .WithOpenApi();
            group.MapGet("/", Obtener).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("productos-get"));
            group.MapGet("/promocion", ObtenerPromocion);
            group.MapGet("/busquedaGenerica", BusquedaGenerica);
            return group;
        }

        static async Task<Created<ProductoDTO>> Crear([FromForm] CrearProductoDTO crearProductoDTO,
         IRepositorioProductos repositorioProductos, IAlmacenadorArchivos almacenadorArchivos,
         IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var producto = mapper.Map<Producto>(crearProductoDTO);

            if (crearProductoDTO.Foto is not null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, crearProductoDTO.Foto);
                producto.Foto = url;
            }
            var id = await repositorioProductos.Crear(producto);
            await outputCacheStore.EvictByTagAsync("productos-get", default);
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            productoDTO.Id = id;

            return TypedResults.Created($"/productos/{id}", productoDTO);
        }

        static async Task<Ok<List<ProductoViewDTO>>> Obtener(IRepositorioProductos repositorioProductos,
         IMapper mapper)
        {
            var productosViewDTOs = await repositorioProductos.ObtenerTodos();

            return TypedResults.Ok(productosViewDTOs);
        }

        static async Task<Results<Ok<List<ProductoViewDTO>>,Ok<RespuestaSimpleDTO>>> BusquedaGenerica(IRepositorioProductos repositorioProductos,
                IMapper mapper, string frases)
        {
            string[] terminosArray = frases.Split('|');
            List<string> terminosList = new List<string>(terminosArray);
            var productosViewDTOs = await repositorioProductos.BusquedaGenerica(terminosList);

            if (productosViewDTOs.Count() == 0)
            {
                var resp = new RespuestaSimpleDTO
                {
                    Id = 1,
                    Respuesta = "Lo siento, no encontré información relacionada con tu consulta",
                    Codigo = "404"
                };
                return TypedResults.Ok(resp);
                //  return TypedResults.Results(resp);
            }
            return TypedResults.Ok(productosViewDTOs);
        }

    
        static async Task<Ok<List<ProductoPromocionDTO>>> ObtenerPromocion(IRepositorioProductos repositorioProductos)
        {
            var productoPromocionDTOs = await repositorioProductos.ObtenerPromocion();

            return TypedResults.Ok(productoPromocionDTOs);
        }

    }
}
