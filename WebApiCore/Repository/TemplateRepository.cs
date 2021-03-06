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
    public class TemplateRepository : ITemplateRepository
    {

        private readonly IConfiguration _config;

       

        public TemplateRepository(IConfiguration configuration)
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
        public async Task<int> SendTemplateData(TemplateModel htmlTemplate)
        {
            var sql = @"INSERT INTO Templates (TemplateData) VALUES (@TemplateData)";

            /*            var m = htmlTemplate;
                        Console.WriteLine(htmlTemplate);
                        return View();*/
            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, htmlTemplate);
                return affectedRows;
            }
        }
    }
}
