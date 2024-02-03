using GITPL.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GITPL.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController(IConfiguration configuration) : ControllerBase
    {

        private readonly string _connectionString = configuration.GetConnectionString("taskString");

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                await connection.OpenAsync();
                SqlCommand command = new("GetAllClients", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<ClientModel> clients = [];
                while (await reader.ReadAsync())
                {
                    ClientModel client = new()
                    {
                        ClientID = reader.GetInt32(0),
                        ClientName = reader.GetString(1),
                        Project = reader.GetString(2),
                        Cost = reader.GetDecimal(3)
                    };
                    clients.Add(client);
                }
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientsById(int id)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                SqlCommand command = new("GetClientByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Client_ID", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    ClientModel client = new()
                    {
                        ClientID = reader.GetInt32(0),
                        ClientName = reader.GetString(1),
                        Project = reader.GetString(2),
                        Cost = reader.GetDecimal(3)
                    };
                    return Ok(client);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
