using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository.IRepository;

namespace WebApiCore.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public Task<int> Add(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAllById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
