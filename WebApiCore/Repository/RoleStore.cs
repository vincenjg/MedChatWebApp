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
    public class RoleStore : IRoleStore<PractitionerRoleModel>
    {
        private readonly IConfiguration _config;

        public RoleStore(IConfiguration configuration)
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

        public async Task<IdentityResult> CreateAsync(PractitionerRoleModel role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                role.Id = await conn.QuerySingleAsync<int>($@"INSERT INTO [PractitionerRole] ([Name], [NormalizedName])
                    VALUES (@{nameof(PractitionerRoleModel.Name)}, @{nameof(PractitionerRoleModel.NormalizedName)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", role);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(PractitionerRoleModel role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                await conn.ExecuteAsync($@"UPDATE [PractitionerRole] SET
                    [Name] = @{nameof(PractitionerRoleModel.Name)},
                    [NormalizedName] = @{nameof(PractitionerRoleModel.NormalizedName)}
                    WHERE [Id] = @{nameof(PractitionerRoleModel.Id)}", role);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(PractitionerRoleModel role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                await conn.ExecuteAsync($"DELETE FROM [PractitionerRole] WHERE [Id] = @{nameof(PractitionerRoleModel.Id)}", role);
            }

            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(PractitionerRoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(PractitionerRoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(PractitionerRoleModel role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(PractitionerRoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(PractitionerRoleModel role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<PractitionerRoleModel> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                return await conn.QuerySingleOrDefaultAsync<PractitionerRoleModel>($@"SELECT * FROM [PractitionerRole]
                    WHERE [Id] = @{nameof(roleId)}", new { roleId });
            }
        }

        public async Task<PractitionerRoleModel> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                return await conn.QuerySingleOrDefaultAsync<PractitionerRoleModel>($@"SELECT * FROM [PractitionerRole]
                    WHERE [NormalizedName] = @{nameof(normalizedRoleName)}", new { normalizedRoleName });
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
