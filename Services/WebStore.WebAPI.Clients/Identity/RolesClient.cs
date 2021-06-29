using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;
using WebStore.Interfaces.Services.Identity;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(HttpClient Client) : base(Client, WebAPIAddress.Identity.Roles) { }
        public void Dispose() { throw new System.NotImplementedException(); }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetRoleNameAsync(Role role, string RoleName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task SetNormalizedRoleNameAsync(Role role, string NormalizedName, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken Cancel) { throw new System.NotImplementedException(); }

        public async Task<Role> FindByNameAsync(string NormalizedRoleName, CancellationToken Cancel) { throw new System.NotImplementedException(); }
    }
}
