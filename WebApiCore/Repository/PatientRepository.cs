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
    public class PatientRepository : IPatientRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public PatientRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        //TODO: modify patient object to allow for this, but since this isn't that important it can be put on the back burner.
        /// <summary>
        /// This function allows Practitioners to add patients to EZMedChat, it requires information from their EMR software.
        /// </summary>
        /// <param name="entity"> A patient object containing the new patients information.</param>
        /// <returns> 1 if the INSERT was successful, 0 if it was not.</returns>
        public async Task<int> Add(Patient entity)
        {
            var sql = @"INSERT INTO Patients (FirstName, LastName, TestPassword, EmailAddress, EpicID)
                        VALUES (@FirstName, @LastName, @TestPassword, @EmailAddress, @EpicId)";
            
            using(var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE FROM Patients WHERE PatientID = @Id";

            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        /// <summary>
        /// Calls the stored procedure spGetAllPatients to return all Patients associated with a
        /// specific practitioner.
        /// </summary>
        /// <param name="id">The Practitioner's database id.</param>
        /// <returns>A list of Patients associated with the practitioner (through the Patient_Practitioner_Relationships linking table).</returns>
        public async Task<IEnumerable<Patient>> GetAllById(int id)
        {
            using (var connection = new SqlConnection(_connectionStrings.TestConnection))
            {
                var result = await connection.QueryAsync<Patient>("dbo.spGetAllPatients", new { PractitionerID = id },
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Patient> GetById(int id)
        {
            string sql = @"SELECT PatientID AS Id, 
                                  EpciID AS EpicId,
                                  FirstName,
                                  LastName, 
                                  EmailAddress,
                                  TestPassword
                         FROM Patients WHERE PatientID = @Id";

            using (var connection = new SqlConnection(_connectionStrings.TestConnection)) 
            {
                var result = await connection.QueryAsync<Patient>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        //TODO: come back to this after changing Patient object.
        public Task<int> Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
