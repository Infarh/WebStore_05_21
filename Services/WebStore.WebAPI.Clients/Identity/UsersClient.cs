using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;
using WebStore.Interfaces.Services.Identity;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Identity
{
    public class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(HttpClient Client) : base(Client, WebAPIAddress.Identity.Users) { }

        public async Task<string> GetUserIdAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetUserNameAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetUserNameAsync(User user, string UserName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetNormalizedUserNameAsync(User user, string NormalizedName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<User> FindByIdAsync(string userId, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<User> FindByNameAsync(string NormalizedUserName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task AddToRoleAsync(User user, string RoleName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task RemoveFromRoleAsync(User user, string RoleName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<bool> IsInRoleAsync(User user, string RoleName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IList<User>> GetUsersInRoleAsync(string RoleName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetEmailAsync(User user, string email, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetEmailAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<User> FindByEmailAsync(string NormalizedEmail, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetNormalizedEmailAsync(User user, string NormalizedEmail, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken Cancel) { throw new System.NotImplementedException(); }
    }
}
