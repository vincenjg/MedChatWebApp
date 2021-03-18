using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Repository
{
    public interface IPractitionerRepository
    {
        Task<Practitioner> Get(int id);
        Task<Practitioner> Get(string email, string password);
        Task<IEnumerable<Practitioner>> GetAllById(int practitionerId);
        Task<IEnumerable<Practitioner>> GetAll();
        Task<int> Add(Practitioner patient);
        Task<int> Update(Practitioner patient);
        Task<int> Delete(int id);

        Task<IdentityResult> CreateAsync(Practitioner user, CancellationToken cancellationToken);
    }
}
