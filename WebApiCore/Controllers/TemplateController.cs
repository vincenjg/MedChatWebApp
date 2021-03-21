using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository;
using WebApiCore.Services;
namespace WebApiCore.Controllers
{
    public class TemplateController : Controller

    {
        private readonly ITemplateRepository _templates;
        private readonly IUserService _userService;

        public TemplateController(ITemplateRepository templates, IUserService userService)
        {
            _templates = templates;
            _userService = userService;
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
            var userID = _userService.GetUserId();
            data.PractitionerID = userID;
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
