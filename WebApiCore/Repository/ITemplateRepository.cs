﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{
    public interface ITemplateRepository
    {
        Task<int> SendTemplateData(TemplateModel htmlTemplate);
    }
}
