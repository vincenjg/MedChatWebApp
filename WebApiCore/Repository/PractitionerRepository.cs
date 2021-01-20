using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository.IRepository;
using WebApiCore.Utilities;

namespace WebApiCore.Repository
{
    public class PractitionerRepository : IPractitionerRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public PractitionerRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public async Task<int> Add(Practitioner entity)
        {
            var sql = @"INSERT INTO Practitioners (FirstName, LastName, Title, EmailAddress, TestPassword)
                        VALUES (@FirstName, @LastName, @Title, @EmailAddress, @TestPassword)";

            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"DELETLE * FROM Practitioners WHERE PractitionerID = @Id";

            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<IEnumerable<Practitioner>> GetAllById(int id)
        {
            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var result = await connection.QueryAsync<Practitioner>("dbo.spGetAllPractitioners", new { PatientID = id },
                     commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Practitioner> GetById(int id)
        {
            var sql = @"SELECT PractitionerID AS Id,
                               FirstName,
                               LastName,
                               Title,
                               EmailAddress,
                               TestPassword
                      FROM Practitioners WHERE PractitionerID = @Id";
            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var result = await connection.QueryAsync<Practitioner>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public Task<int> Update(Practitioner entity)
        {
            throw new NotImplementedException();
        }
    }
}
