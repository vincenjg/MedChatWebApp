﻿using Dapper;
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
    public class TemplateRepository : ITemplateRepository
    {

        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public TemplateRepository(IConfiguration configuration, IUserService userService)
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
        //this method is used to send the template data to the database
        public async Task<int> SendTemplateData(TemplateModel htmlTemplate)
        {

            //var sql = @"INSERT INTO Templates (TemplateName, TemplateData) VALUES (@TemplateName, @TemplateData)";
            var sql = @"INSERT INTO Templates (TemplateName, TemplateData, PractitionerID) VALUES (@TemplateName, @TemplateData, @PractitionerID)";


            using (IDbConnection conn = Connection)
            {                
                var affectedRows = await conn.ExecuteAsync(sql, htmlTemplate);
                return affectedRows;
            }
        }

        //this method will be used to get the TemplateData based on the value selected in the dropdownlist
        public async Task<IEnumerable<TemplateModel>> GetById(int id)
        {
            string sql = @"Select TemplateData FROM Templates WHERE TemplateID = @TemplateID";

            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<TemplateModel>(sql, new { TemplateID = id });
                //return result.ToList();
                return result;
            }
        }

        public async Task<TemplateModel> Get(int id)
        {
            TemplateModel tmp = new TemplateModel();
            string sql = @"Select TemplateData FROM Templates WHERE TemplateID = @TemplateID";

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<TemplateModel>(sql, new { TemplateID = id });
                tmp.TemplateData = result.FirstOrDefault().TemplateData;
                return tmp;
            }
        }

        //this is used to bind the dropdown list with the TemplateName column.
        public IEnumerable<TemplateModel> GetTemplateList()
        {
            var userID = _userService.GetUserId();
            string sql = @"Select TemplateID, TemplateName FROM Templates WHERE PractitionerID = @PractitionerID";
            var result = Connection.Query<TemplateModel>(sql, new { PractitionerID = userID });
            //string sql = @"Select TemplateID, TemplateName FROM Templates";
            //var result = Connection.Query<TemplateModel>(sql);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"DELETE FROM Templates WHERE TemplateID = @Id";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<IEnumerable<TemplateModel>> GetAllTemplateNames()
        {
            string sql = @"Select TemplateID, TemplateName From Templates";

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<TemplateModel>(sql);
                return result;
            }
        }

    }
}
