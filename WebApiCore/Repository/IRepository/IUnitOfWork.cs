using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPatientRepository Patients { get;  }
        IPractitionerRepository Practitioners { get; }
        IAppointmentRepository Appointments { get; }
    }
}
