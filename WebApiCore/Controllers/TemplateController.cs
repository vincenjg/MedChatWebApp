using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    public class TemplateController : Controller

    {

        /*public TemplateController(ITemplateRepository templates)
        {
            _templates = templates;
        }*/



        private static IConfiguration _config;

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

        private TemplateRepository _templates = new TemplateRepository(_config);


        /*        [HttpPost]
                public async Task<int>  SendTemplateData(string htmlTemplate)
                {
                    var sql = @"INSERT Templates2 (TemplateData) VALUES (@TemplateData)";

                    using (IDbConnection conn = Connection)
                    {
                        var affectedRows = await conn.ExecuteAsync(sql, htmlTemplate);
                        return affectedRows;
                    }
                }*/

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendTemplateData(TemplateModel data)
        {

            //TemplateModel htmlInfo = JsonConvert.DeserializeObject<TemplateModel>(data2);
             _templates.SendTemplateData(data);    
            return Json(new { Message = "Success" });

        }


    }
}
