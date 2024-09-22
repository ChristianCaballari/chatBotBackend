using chatbot.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace chatbot.Repositorios
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly string connectionString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<CategoriaResumenDTO?> ObtenerTodos()
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var categoria = await conexion.QuerySingleAsync<string>("SP_Get_Categorias",
                    commandType: CommandType.StoredProcedure);
                return new CategoriaResumenDTO() { Categorias = categoria };
            }
        }
    }
}
