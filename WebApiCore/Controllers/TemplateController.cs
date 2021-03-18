using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;

namespace WebApiCore.Controllers
{
    public class TemplateController : Controller

    {
        private readonly ITemplateRepository _templates;

        public TemplateController(ITemplateRepository templates)
        {
            _templates = templates;
        }
        public IActionResult Index()
        {
            ViewBag.TemplateList = new SelectList(_templates.GetTemplateList(), "TemplateID", "TemplateName");
            return View();
        }

        [HttpPost]
        public ActionResult SendTemplateData(TemplateModel data)
        {
            //TemplateModel htmlInfo = JsonConvert.DeserializeObject<TemplateModel>(data2);
             _templates.SendTemplateData(data);
            return Json(new { Message = "Success" });
        }

        public async Task<string> GetTemplateData(int id)
        {
            TemplateModel temp = await _templates.Get(id);
            return temp.TemplateData;
        }

        /*        [HttpGet]
                public ActionResult GetTemplateData(int dataId)
                {
                    _templates.GetById(dataId);
                    return Json(new { Message = "Nice" });
                }*/

    }
}
