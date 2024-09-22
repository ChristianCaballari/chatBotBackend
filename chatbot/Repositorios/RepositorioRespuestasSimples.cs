using chatbot.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
namespace chatbot.Repositorios
{
    public class RepositorioRespuestasSimples : IRepositorioRespuestasSimples
    {
        private readonly string connectionString;
        public RepositorioRespuestasSimples(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<RespuestaSimpleDTO?> ObternerRespuestaSimple(string codigo)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var respuesta = await conexion.QueryFirstOrDefaultAsync<RespuestaSimpleDTO>("SP_Get_RespuestasGenerales",
                    new { codigo },
                    commandType: CommandType.StoredProcedure);
                return respuesta;
              
            }
        }
    }
}
