﻿using Dapper;
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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IConfiguration _config;

        public AppointmentRepository(IConfiguration configuration)
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
                        PatientID = @PatientID
                        PractitionerID = @PractitionerID";
            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, appointment);
                return affectedRows;
            }
        }
        //get all appointments based on practitioner
        //create stored procedure spGetALlAppointments to retreive all appointments under a practitioner?
        public async Task<IEnumerable<Appointment>> GetAllByPractitionerId(int practitionerId)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Appointment>("dbo.spGetAllAppointments", new { PractitionerId = practitionerId },
                   commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }


        public async Task<IEnumerable<Appointment>> GetAllByPatientId(int patientId)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<Appointment>("dbo.spGetAllAppointments", new { PractitionerId = patientId },
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}