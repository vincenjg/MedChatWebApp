using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository.IRepository;
using WebApiCore.Utilities;

namespace WebApiCore.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public AppointmentRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public Task<int> Add(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE * FROM Appointments WHERE AppointmentID = @Id";

            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public Task<IEnumerable<Appointment>> GetAllById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Appointment> GetById(int id)
        {
            //TODO: do we need to add a practitioner and patient object to model or should we just add practitionerID and patientID to model?
            var sql = @"SELECT * FROM Appointments WHERE AppointmentId = @Id";

            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var result = await connection.QueryAsync<Appointment>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public Task<int> Update(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
