using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Repository.IRepository;

namespace WebApiCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IPatientRepository patientRepository, IPractitionerRepository practitionerRepository, 
                          IAppointmentRepository appointmentRepository)
        {
            Patients = patientRepository;
            Practitioners = practitionerRepository;
            Appointments = appointmentRepository;
        }

        public IPatientRepository Patients { get;  }

        public IPractitionerRepository Practitioners { get; }
        public IAppointmentRepository Appointments { get; }
    }
}
