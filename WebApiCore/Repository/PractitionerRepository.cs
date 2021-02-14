using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApiCore.Models;


namespace WebApiCore.Repository
{

    public class PractitionerRepository : IPractitionerRepository
    {
        private readonly IConfiguration _config;

        public PractitionerRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("TestConnection"));
            }
        }

        public async Task<Practitioner> Get(int id)
        {
            var sql = @"SELECT PractitionerID AS Id,
                               FirstName,
                               LastName,
                               Title,
                               EmailAddress,
                               TestPassword
                      FROM Practitioners WHERE PractitionerID = @Id";

            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Practitioner>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<Practitioner> Get(string email, string password)
        {
            var sql = @"SELECT PractitionerID AS Id, FirstName, LastName, Title, EmailAddress, TestPassword
                        FROM Practitioners Where EmailAddress = @EmailAddress AND TestPassword = @Password";

            var dbparams = new DynamicParameters();
            dbparams.Add(email);
            dbparams.Add(password);

            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Practitioner>(sql, dbparams);
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Practitioner>> GetAll()
        {
            var sql = @"SELECT * FROM Practitioners";

            using(IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Practitioner>(sql);
                return result.ToList();
            }
        }

        /// <summary>
        /// Return all practitioners associated with a specific patients.
        /// </summary>
        /// <param name="patientId">Id of specific patients.</param>
        /// <returns>A list of practitioners that "belong" to the specified patient.</returns>
        public async Task<IEnumerable<Practitioner>> GetAllById(int patientId)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Practitioner>("dbo.spGetAllPractitioners", new { PatientID = patientId },
                   commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> Add(Practitioner practitioner)
        {
            var sql = @"INSERT INTO Practitioners (FirstName, LastName, TestPassword, EmailAddress, Title)
                        VALUES (@FirstName, @LastName, @TestPassword, @EmailAddress, @Title)";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, practitioner);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE FROM Practitioners WHERE PractitionerID = @Id";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<int> Update(Practitioner practitioner)
        {
            var sql = @"UPDATE Practitioners 
                        SET FirstName = @FirstName, 
                        LastName = @LastName, 
                        TestPassword = @TestPassword, 
                        EmailAddress = @EmailAddress, 
                        Title = @Title 
                        WHERE PractitionerId = @PractitionerId";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, practitioner);
                return affectedRows;
            }
        }
    }
}
