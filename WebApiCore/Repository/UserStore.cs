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
    public class UserStore : IUserStore<Practitioner>, IUserEmailStore<Practitioner>, IUserPhoneNumberStore<Practitioner>,
        IUserTwoFactorStore<Practitioner>, IUserPasswordStore<Practitioner>, IUserRoleStore<Practitioner>
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
                //return new SqlConnection(_config.GetConnectionString("RandConnectionTwo"));
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }


        public async Task<IdentityResult> CreateAsync(Practitioner user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                user.PractitionerID = await conn.QuerySingleAsync<int>($@"INSERT INTO [Practitioners] ([LastName], [FirstName], [Title], [UserName], [NormalizedUserName], [EmailAddress],
                    [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled])
                    VALUES (@{nameof(Practitioner.LastName)}, @{nameof(Practitioner.FirstName)}, @{nameof(Practitioner.Title)}, @{nameof(Practitioner.UserName)}, @{nameof(Practitioner.NormalizedUserName)}, @{nameof(Practitioner.EmailAddress)},
                    @{nameof(Practitioner.NormalizedEmail)}, @{nameof(Practitioner.EmailConfirmed)}, @{nameof(Practitioner.PasswordHash)},
                    @{nameof(Practitioner.PhoneNumber)}, @{nameof(Practitioner.PhoneNumberConfirmed)}, @{nameof(Practitioner.TwoFactorEnabled)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Practitioner user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                await conn.ExecuteAsync($"DELETE FROM [Practitioners] WHERE [PractitionerID] = @{nameof(Practitioner.PractitionerID)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<Practitioner> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                return await conn.QuerySingleOrDefaultAsync<Practitioner>($@"SELECT * FROM [Practitioners]
                    WHERE [PractitionerID] = @{nameof(userId)}", new { userId });
            }
        }

        public async Task<Practitioner> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                return await conn.QuerySingleOrDefaultAsync<Practitioner>($@"SELECT * FROM [Practitioners]
                    WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PractitionerID.ToString());
        }

        public Task<string> GetUserNameAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(Practitioner user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(Practitioner user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(Practitioner user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                await conn.ExecuteAsync($@"UPDATE [Practitioners] SET
                    [LastName] = @{nameof(Practitioner.LastName)},
                    [FirstName] = @{nameof(Practitioner.FirstName)},
                    [Title] = @{nameof(Practitioner.Title)},
                    [TestPassword] = @{nameof(Practitioner.TestPassword)},
                    [UserName] = @{nameof(Practitioner.UserName)},
                    [NormalizedUserName] = @{nameof(Practitioner.NormalizedUserName)},
                    [EmailAddress] = @{nameof(Practitioner.EmailAddress)},
                    [NormalizedEmail] = @{nameof(Practitioner.NormalizedEmail)},
                    [EmailConfirmed] = @{nameof(Practitioner.EmailConfirmed)},
                    [PasswordHash] = @{nameof(Practitioner.PasswordHash)},
                    [PhoneNumber] = @{nameof(Practitioner.PhoneNumber)},
                    [PhoneNumberConfirmed] = @{nameof(Practitioner.PhoneNumberConfirmed)},
                    [TwoFactorEnabled] = @{nameof(Practitioner.TwoFactorEnabled)}
                    WHERE [PractitionerID] = @{nameof(Practitioner.PractitionerID)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(Practitioner user, string email, CancellationToken cancellationToken)
        {
            user.EmailAddress = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailAddress);
        }

        public Task<bool> GetEmailConfirmedAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(Practitioner user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<Practitioner> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                return await conn.QuerySingleOrDefaultAsync<Practitioner>($@"SELECT * FROM [Practitioners]
                    WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(Practitioner user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(Practitioner user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(Practitioner user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(Practitioner user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(Practitioner user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Practitioner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(Practitioner user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                var normalizedName = roleName.ToUpper();
                var roleId = await conn.ExecuteScalarAsync<int?>($"SELECT [Id] FROM [PractitionerRole] WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                    roleId = await conn.ExecuteAsync($"INSERT INTO [PractitionerRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                        new { roleName, normalizedName });

                await conn.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [PractitionerUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [PractitionerUserRole]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.PractitionerID, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(Practitioner user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                var roleId = await conn.ExecuteScalarAsync<int?>("SELECT [Id] FROM [PractitionerRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await conn.ExecuteAsync($"DELETE FROM [PractitionerUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.PractitionerID, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(Practitioner user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                var queryResults = await conn.QueryAsync<string>("SELECT r.[Name] FROM [PractitionerRole] r INNER JOIN [PractitionerUserRole] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.PractitionerID });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(Practitioner user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                var roleId = await conn.ExecuteScalarAsync<int?>("SELECT [Id] FROM [PractitionerRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await conn.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [PractitionerUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.PractitionerID, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<Practitioner>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (IDbConnection conn = Connection)
            {
                var queryResults = await conn.QueryAsync<Practitioner>("SELECT u.* FROM [Practitioners] u " +
                    "INNER JOIN [PractitionerUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [PractitionerRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
