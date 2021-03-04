using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _config;

        public TemplateController(IConfiguration configuration)
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

        [HttpPost]
        public async Task<int> SendTemplateData(TemplateModel htmlTemplate)
        {
            var sql = @"INSERT Templates (TemplateData) VALUES (@TemplateData)";

            using (IDbConnection conn = Connection)
            {
                var affectedRows = await conn.ExecuteAsync(sql, htmlTemplate);
                return affectedRows;
            }
        }

        /*        [HttpPost]
                public ActionResult SendTemplateData(TemplateModel htmlTemplate)
                {

                    var sql = @"INSERT INTO tbl_html_info (html_content) VALUES (@html_content)";

                    var m = htmlTemplate;
                    Console.WriteLine(htmlTemplate);
                    return View();
        *//*            using (IDbConnection conn = Connection)
                    {
                        var affectedRows = await conn.ExecuteAsync(sql, htmlTemplate);
                        return affectedRows;
                    }*//*
                }*/
    }


}
