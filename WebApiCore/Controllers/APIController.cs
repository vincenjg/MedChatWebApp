using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WebApiCore.Models;
using WebApiCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
/*        private readonly IDapper _dapper;
        public APIController(IDapper dapper)
        {
            _dapper = dapper;
        }*/

        private string _connectionString;
        
        public APIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestConnection");
        }


        [HttpGet(nameof(GetAllById))]
        public async Task<IEnumerable<Patient>> GetAllById(int id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = "TestConnection";

                var result = await connection.QueryAsync<Patient>("dbo.spGetAllPatients", new { PractitionerID = id },
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        [HttpGet(nameof(GetById))]
        public async Task<Patient> GetById(int Id)
        {
            /*var result = await Task.FromResult(_dapper.Get<Patient>($"SELECT PatientID AS Id, EpicId AS EpicId, FirstName, LastName, EmailAddress, TestPassword FROM Patients WHERE PatientID = {Id} ", null, commandType: CommandType.Text));
            return result;*/
            string sql = @"SELECT PatientID AS Id, 
                                  EpicId AS EpicId,
                                  FirstName,
                                  LastName, 
                                  EmailAddress,
                                  TestPassword
                         FROM Patients WHERE PatientID = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {

                var result = await connection.QueryAsync<Patient>(sql, new { Id = Id });
                List<Patient> patientList = result.ToList();
                return result.FirstOrDefault();
            }

        }
        [HttpDelete(nameof(Delete))]
        public async Task<ActionResult<Patient>> Delete(int Id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sqlStatment = "DELETE FROM Patients WHERE PatientID = @Id";
                await connection.ExecuteAsync(sqlStatment, new { Id = Id });
            }
            return Ok();

        }

        [HttpPost(nameof(Add))]
        public async Task<int> Add(Patient entity)
        {
            var sql = @"INSERT INTO Patients (FirstName, LastName, TestPassword, EmailAddress, EpicID)
                        VALUES (@FirstName, @LastName, @TestPassword, @EmailAddress, @EpicId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

    }
}