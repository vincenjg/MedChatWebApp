using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository.IRepository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        // Maybe add delete and add here if we need special logic for it
        // and we don't want to keep it in IRepository.
    }
}
