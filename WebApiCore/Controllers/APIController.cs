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

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IDapper _dapper;
        public APIController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(Patient data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.PatientId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Add_Article]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

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
        public async Task<Patient> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Patient>($"SELECT PatientID AS Id, EpicId AS EpicId, FirstName, LastName, EmailAddress, TestPassword FROM Patients WHERE PatientID = {Id} ", null, commandType: CommandType.Text));
            return result;

        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [Patients] Where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(Count))]
        public Task<int> Count(int num)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [Patients] WHERE Age like '%{num}%'", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(Patient data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.PatientId);
            dbPara.Add("FName", data.FirstName, DbType.String);
            dbPara.Add("LName", data.LastName, DbType.String);
            dbPara.Add("EpicId", data.EpicId, DbType.String);

            var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}