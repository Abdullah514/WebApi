using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Names : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Names(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select personName from firstapi.name";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AuthenticationDbContextContextConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }


            }

            return new JsonResult(table);


        }

    }
}
