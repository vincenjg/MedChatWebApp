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
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
 /*      private readonly IDapper _dapper;
        public APIController(IDapper dapper)
        {
            _dapper = dapper;
        }*/

        private string _connectionString;
        
        public PatientController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestConnection");
        }

        [HttpPost(nameof(GetUserInformation))]
        public async Task<Patient> GetUserInformation([FromBody] JObject data)
        {
            var sql = @"SELECT PatientID, EpicID, FirstName, LastName, EmailAddress, TestPassword
                        FROM Patients Where EmailAddress = @EmailAddress AND TestPassword = @Password";

            var dbparams = new DynamicParameters();
            dbparams.Add("EmailAddress", data["emailAddress"].ToString());
            dbparams.Add("Password", data["password"].ToString());

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<Patient>(sql, dbparams);
                return result.FirstOrDefault();
            }
        }

        // This returns all practitioners associated with a patient.
        [HttpGet(nameof(GetAllById))]
        public async Task<IEnumerable<Practitioner>> GetAllById(int id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = "TestConnection";

                var result = await connection.QueryAsync<Practitioner>("dbo.spGetAllPractitioners", new { PractitionerID = id },
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
                return result.FirstOrDefault();
            }

        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var sql = @"DELETE FROM Patients WHERE PatientID = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = Id });
                return affectedRows;
            }
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