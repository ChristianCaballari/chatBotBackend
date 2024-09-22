using chatbot.Entidades;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace chatbot.Repositorios
{
    public class RepositorioUsarios : IRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> Crear(Usuario usuario)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
               var id = await conexion.QuerySingleAsync<int>("Sp_Insert_Usuario",
                    new
                    {
                      usuario.Correo,
                      usuario.Nombre
                    }, commandType: CommandType.StoredProcedure);
                return usuario.Id = id; 
            }
        }

    }
}
