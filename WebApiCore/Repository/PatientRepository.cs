using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{

    //we're isolating the Dapper inside this repo, so it's easier to work on on, switch, etc. 
    public class PatientRepository : IPatientRepository
    {
        //haveto assign the connection to idbconnection
        private IDbConnection _db;

        public PatientRepository(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("TestConnection"));
        }

        
        public Patient Add(Patient patient)
        {
            var sql = @"INSERT INTO Patients (FirstName, LastName, TestPassword, EmailAddress, EpicID)
                        VALUES (@FirstName, @LastName, @TestPassword, @EmailAddress, @EpicId)";
            //directly passing the patient object cuz dapper is able to link it. 
            var id = _db.Query<int>(sql, patient).Single();
            patient.PatientId = id;
            return patient;
        }

        public Patient Find(int id)
        {
            var sql = "Select * FROM Patients WHERE PatientID = @PatientId";
            return _db.Query<Patient>(sql, new { @PatientId = id }).Single();
        }

        public List<Patient> GetAll()
        {
            var sql = "SELECT * FROM Patients";
            return _db.Query<Patient>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Patients WHERE PatientId = @Id";
            _db.Execute(sql, new { id });
        }

        public Patient Update(Patient patient)
        {
            var sql = "UPDATE Patients SET FirstName = @FirstName, LastName = @LastName, TestPassword = @TestPassword, EmailAddress = @EmailAddress, EpicID = @EpicID WHERE PatientId = @PatientId";
            _db.Execute(sql, patient);
            return patient;
        }
    }
}
