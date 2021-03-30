using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{
    public interface ITemplateRepository
    {
        Task<int> SendTemplateData(TemplateModel htmlTemplate);
        Task<IEnumerable<TemplateModel>> GetAllTemplateNames();
        IEnumerable<TemplateModel> GetTemplateList();

        Task<IEnumerable<TemplateModel>> GetById(int id);

        Task<TemplateModel> Get(int id);
    }
}
