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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public AppointmentRepository(IConfiguration configuration, IUserService userService)
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

        public async Task<Appointment> Get(int id)
        {
            string sql = @"SELECT * FROM Appointments WHERE AppointmentId = @Id";

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<Appointment>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            string sql = @"SELECT * FROM Appointments";
            /*string sql = @"SELECT Appointments.AppointmentID, Appointments.StartTime, Appointments.EndTime, Appointments.AppointmentReason, Appointments.AppointmentInstructions, Appointments.PatientID, Appointments.PractitionerID, Patients.EpicID
                            FROM Appointments
                            INNER JOIN Patients ON Patients.PatientID = Appointments.PatientID";*/

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<Appointment>(sql);
                return result.ToList();
            }
        }

        public async Task<int> Add(Appointment appointment)
        {
            var sql = @"Insert INTO Appointments (StartTime, EndTime, AppointmentReason, AppointmentInstructions, PatientID, PractitionerID)
                        VALUES(@StartTime, @EndTime, @AppointmentReason, @AppointmentInstructions, @PatientID, @PractitionerID)";
            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, appointment);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE FROM Appointments WHERE AppointmentID = @Id";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<int> Update(Appointment appointment)
        {
            var sql = @"UPDATE Appointments
                        SET StartTime = @StartTime,
                        EndTime = @EndTime,
                        AppointmentReason = @AppointmentReason,
                        AppointmentInstructions = @AppointmentInstructions,
                        PatientID = @PatientID,
                        PractitionerID = @PractitionerID
                        where AppointmentID = @AppointmentID";
            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, appointment);
                return affectedRows;
            }
        }
        //get all appointments based on practitioner
        //create stored procedure spGetALlAppointments to retreive all appointments under a practitioner?
        public async Task<IEnumerable<Appointment>> GetAllByPractitionerId(int userId)
        {
            using (IDbConnection conn = Connection)
            {
                var userID = _userService.GetUserId();
                var result = await conn.QueryAsync<Appointment>("dbo.spGetAllByPractitionerId", new { PractitionerId = userID },
                   commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        //get all appointments based on patients
        public async Task<IEnumerable<Appointment>> GetAllByPatientId(int patientId)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Appointment>("dbo.spGetAllByPatientId", new { PatientId = patientId },
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
