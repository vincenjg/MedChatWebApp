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
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateViewController : Controller
    {
        private readonly IConfiguration _config;

        public TemplateViewController(IConfiguration configuration)
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

        [HttpGet(nameof(GetByFormName))]
        public async Task<IEnumerable<TemplateModel>> GetByFormName(int id)
        {
            string sql = @"Select TemplateData FROM Templates WHERE TemplateID = @TemplateId";

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<TemplateModel>(sql, new { TemplateId = id });
                return result;
            }
            /*using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<TemplateModel>("dbo.spGetTemplates", new { TemplateID = id },
                   commandType: CommandType.StoredProcedure);
                return result;
            }*/
        }


        [HttpGet(nameof(GetAllTemplateNames))]
        public async Task<IEnumerable<TemplateModel>> GetAllTemplateNames()
        {
            string sql = @"Select TemplateName From Templates";

            /*using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<TemplateModel>(sql);
                return result.ToList();
            }*/

            using (IDbConnection conn = Connection)
            {

                var result = await conn.QueryAsync<TemplateModel>(sql);
                return result;
            }
        }

    }
}
