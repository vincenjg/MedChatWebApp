using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Repository;
using WebApiCore.Repository.IRepository;

namespace WebApiCore.Repository
{
    /// <summary>
    /// This Class allows us to separate the data access layer from the business logic parts of the application.
    /// </summary>
    public static class DataAccessManager
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPractitionerRepository, PractitionerRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
