using chatbot.DTOs;
using chatbot.Entidades;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;

namespace chatbot.Repositorios
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private readonly string connectionString;
        public RepositorioProductos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> Crear(Producto producto)
        {
            using var conection = new SqlConnection(connectionString);

            var id = await conection.QuerySingleAsync<int>("SP_Insert_Product",
            new
            {
                 producto.Nombre,
                 producto.Precio,
                 producto.Foto,
                 producto.IdCategoria,

            }, commandType: CommandType.StoredProcedure);
            return id;
        }
        public async Task<List<ProductoViewDTO>> ObtenerTodos()
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var productos = await conexion.QueryAsync<ProductoViewDTO>("SP_Get_Productos",
                    commandType: CommandType.StoredProcedure);
                return productos.ToList();
            }
        }
        public async Task<List<ProductoPromocionDTO>> ObtenerPromocion()
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var productos = await conexion.QueryAsync<ProductoPromocionDTO>("SP_Get_Promociones",
                    commandType: CommandType.StoredProcedure);
                return productos.ToList();
            }
        }
        public async Task<List<ProductoViewDTO>> BusquedaGenerica(List<string> frases)
        {
            var dt = new DataTable();
            dt.Columns.Add("Nombre", typeof(string));
            foreach (var frase in frases)
            {
                dt.Rows.Add(frase);
            }
            using (var conexion = new SqlConnection(connectionString))
            {
                var productos = await conexion.QueryAsync<ProductoViewDTO>("Sp_Busqueda_Generica",
                    new { Palabras = dt },
                    commandType: CommandType.StoredProcedure);
                return productos.ToList();
            }
        }

    }
}
