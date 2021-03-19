using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> Get(int id);

        //Task<Appointment> Get(string email, string password);

        Task<IEnumerable<Appointment>> GetAllByPractitionerId(int userId);

        Task<IEnumerable<Appointment>> GetAllByPatientId(int patientId);

        Task<IEnumerable<Appointment>> GetAll();

        Task<int> Add(Appointment appointment);

        Task<int> Update(Appointment appointment);

        Task<int> Delete(int id);
    }
}
