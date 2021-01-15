using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Repository.IRepository;

namespace WebApiCore.Repository
{
    public class PractitionerRepository : IPractitionerRepository
    {
        private readonly IDbConnection _connection;

        public PractitionerRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task<int> Add(Practitioner entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Practitioner>> GetAllById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Practitioner> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Practitioner entity)
        {
            throw new NotImplementedException();
        }
    }
}
