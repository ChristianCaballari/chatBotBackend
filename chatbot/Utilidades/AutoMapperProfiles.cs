using AutoMapper;
using chatbot.DTOs;
using chatbot.Entidades;

namespace chatbot.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearProductoDTO, Producto>();
            CreateMap<ProductoDTO, Producto>().ReverseMap();
            CreateMap<OpcionesChatbot, OpcionesChatbotDTO>();
            CreateMap<CrearUsuarioDTO, Usuario>();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
        }
    }
}
