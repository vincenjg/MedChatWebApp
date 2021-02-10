using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{
    public interface IPatientRepository
    {
        // find method based on id. 
        Patient Find(int id);

        List<Patient> GetAll();
        Patient Add(Patient patient);

        Patient Update(Patient patient);
        void Remove(int id);
    }
}
