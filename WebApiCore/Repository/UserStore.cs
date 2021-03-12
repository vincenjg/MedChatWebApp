using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebApiCore.Models;
namespace WebApiCore.Repository
{
    public class UserStore : IUserStore<Practitioner>
    {
        private readonly IConfiguration _config;

        public UserStore(IConfiguration configuration)
        {
            _config = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("TestConnection"));
            }
        }
        /// <summary>
        /// The following methods will be used to enable the creation of practitioner accounts.         
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(Practitioner user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                //await conn.OpenAsync(cancellationToken);
                user.PractitionerID = await conn.QuerySingleAsync<int>($@"INSERT INTO Practitioners (FirstName, LastName, Title, EmailAddress, TestPassword) 
                                                                        VALUES (@FirstName, @LastName, @Title, @EmailAddress, @TestPassword);
                                                                        SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Practitioner user, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Practitioner> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Practitioner> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Practitioner user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(Practitioner user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(Practitioner user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Practitioner user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(Practitioner user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Practitioner user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
