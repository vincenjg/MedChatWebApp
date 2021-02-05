using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private string _connectionString;

        public AppointmentsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestConnection");
        }

        [HttpGet(nameof(GetById))]
        public async Task<Appointment> GetById(int Id)
        {
            /*var result = await Task.FromResult(_dapper.Get<Patient>($"SELECT PatientID AS Id, EpicId AS EpicId, FirstName, LastName, EmailAddress, TestPassword FROM Patients WHERE PatientID = {Id} ", null, commandType: CommandType.Text));
            return result;*/
            string sql = @"SELECT * FROM Appointments WHERE AppointmentId = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {

                var result = await connection.QueryAsync<Appointment>(sql, new { Id = Id });
                return result.FirstOrDefault();
            }

        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE * FROM Appointments WHERE AppointmentID = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }
    }
}
