using chatbot.DTOs;
using chatbot.Entidades;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace chatbot.Repositorios
{
    public class RepositorioOpcionesChatbot : IRepositorioOpcionesChatbot
    {
        private readonly string connectionString;
        public RepositorioOpcionesChatbot(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<OpcionesChatbot>> OpcionesChatbots()
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var productos = await conexion.QueryAsync<OpcionesChatbot>("SP_Get_OpcionesChatbot",
                    commandType: CommandType.StoredProcedure);
                return productos.ToList();
            }
        }

        public async Task<OpcionesChatbotDTO> OptenerPreguntasOpcionesChatbot(int id)
        {
 
                using (var conexion = new SqlConnection(connectionString))
                {
                    //Para poder obtener multiples consultas por separado, usamos QueryMultipleAsync
                    using (var multi = await conexion.QueryMultipleAsync("SP_Get_Preguntas_OpcionesChat_Bot",
                        new { IdOpcionChatbot =  id }, commandType: CommandType.StoredProcedure))
                    {
                       
                        var preguntas = await multi.ReadAsync<PreguntasChatbotDTO>();//ReadAsync, porque se esperan multiples registros
                        var opcion = await multi.ReadFirstAsync<OpcionesChatbotDTO>();//ReadFirstAsync, porque se espera un solo registro

                        opcion.Preguntas = preguntas.ToList();

                        return opcion;
                    }
                }
            }
    }
}
