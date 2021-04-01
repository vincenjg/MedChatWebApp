using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Services;

namespace WebApiCore.Repository
{

    /// <summary>
    /// This is representative of the Patients table in out database.
    /// All operations regarding it are handled here.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public PatientRepository(IConfiguration configuration, IUserService userService)
        {
            _config = configuration;
            _userService = userService;
        }

        private IDbConnection Connection
        {
            get 
            { 
                return new SqlConnection(_config.GetConnectionString("DefaultConnection")); 
            }
        }


        public async Task<Patient> Get(int id)
        {
            string sql = @"SELECT PatientID AS Id, 
                                  EpicId AS EpicId,
                                  FirstName,
                                  LastName, 
                                  EmailAddress,
                                  TestPassword
                         FROM Patients WHERE PatientID = @Id";

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<Patient>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<Patient> Get(string email, string password)
        {
            var sql = @"SELECT PatientID AS Id, EpicID, FirstName, LastName, EmailAddress, TestPassword
                        FROM Patients Where EmailAddress = @EmailAddress AND TestPassword = @Password";

            var dbparams = new DynamicParameters();
            dbparams.Add(email);
            dbparams.Add(password);

            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Patient>(sql, dbparams);
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            var sql = @"SELECT * FROM Patients";

            using(IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Patient>(sql);
                return result.ToList();
            }
        }

        /// <summary>
        /// Return all patients associated with a specific practitioner.
        /// </summary>
        /// <param name="practitionerId">Id of specific practitioner</param>
        /// <returns>A list of patients that "belong" to the specified practitioner.</returns>
        public async Task<IEnumerable<Patient>> GetAllById(string userId)
        {
            using (IDbConnection conn = Connection)
            {
                //var pracID = _userService.GetUserId();
                var result = await conn.QueryAsync<Patient>("dbo.spGetAllPatients", new { PractitionerID = userId },
                   commandType: CommandType.StoredProcedure);
                return (List<Patient>)result;
                //return result.ToList();
            }
        }

        public async Task<int> Add(Patient patient)
        {
            var sql = @"INSERT INTO Patients (FirstName, LastName, TestPassword, EmailAddress, EpicID)
                        VALUES (@FirstName, @LastName, @TestPassword, @EmailAddress, @EpicId)";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, patient);
                return affectedRows;
            }
        }


        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE FROM Patients WHERE PatientID = @Id";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<int> Update(Patient patient)
        {
            var sql = @"UPDATE Patients 
                        SET FirstName = @FirstName, 
                        LastName = @LastName, 
                        TestPassword = @TestPassword, 
                        EmailAddress = @EmailAddress, 
                        EpicID = @EpicID 
                        WHERE PatientId = @PatientId";

            using(IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, patient);
                return affectedRows;
            }
        }

        //used to retrive patient emails associated with practitioner
        public IEnumerable<Patient> GetpatientsList()
        {
            var userId = _userService.GetUserId();
            //string sql = @"Select EmailAddress FROM Patients WHERE PatientID = @PatientID";
            var result = Connection.QueryAsync<Patient>("dbo.spGetAllPatientByPracId", new { PractitionerID = userId },
                   commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
