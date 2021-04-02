using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{
    public interface IPatientRepository
    {
        Task<Patient> Get(int id);

        Task<Patient> Get(string email, string password);

        Task<IEnumerable<Patient>> GetAllById(string userId);

        Task<IEnumerable<Patient>> GetAll();

        Task<int> Add(Patient patient);

        Task<int> Update(Patient patient);

        Task<int> Delete(int id);

    }
}
