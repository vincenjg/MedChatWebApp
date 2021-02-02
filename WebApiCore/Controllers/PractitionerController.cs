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
    public class PractitionerController : ControllerBase
    {
        /*private readonly IDapper _dapper;
        public PractitionerController(IDapper dapper)
        {
            _dapper = dapper;
        }*/

        private string _connectionString;

        public PractitionerController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestConnection");
        }

        // This returns all patients associated with a practitioner.
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
        public async Task<Practitioner> GetById(int Id)
        {
            /*var result = await Task.FromResult(_dapper.Get<Practitioner>($"SELECT PractitionerID AS Id, FirstName, LastName, EmailAddress, TestPassword FROM Practitioners WHERE PractitionerID = {Id} ", null, commandType: CommandType.Text));
            return result;*/
            var sql = @"SELECT PractitionerID AS Id,
                               FirstName,
                               LastName,
                               Title,
                               EmailAddress,
                               TestPassword
                      FROM Practitioners WHERE PractitionerID = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<Practitioner>(sql, new { Id = Id });
                return result.FirstOrDefault();
            }
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var sql = @"DELETE FROM Practitioners WHERE PractitionerID = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = Id });
                return affectedRows;
            }
        }
        [HttpPost(nameof(Add))]
        public async Task<int> Add(Practitioner entity)
        {
            var sql = @"INSERT INTO Practitioners (FirstName, LastName, TestPassword, EmailAddress, Title)
                        VALUES (@FirstName, @LastName, @TestPassword, @EmailAddress, @Title)";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }


    }
}