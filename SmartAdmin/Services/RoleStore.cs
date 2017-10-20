using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartAdmin.Services
{
    public class RoleStore : IRoleStore<UserRole>
    {
        private SmartAdminDataContext dataContext;
        
        public RoleStore(SmartAdminDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        public Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Code);
        }

        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Code);
        }

        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(roleId, out id))
            {
                return await dataContext.UserRoles.FirstOrDefaultAsync(r => r.Id == id);
            }

            return null;
        }

        public async Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await dataContext.UserRoles.FirstOrDefaultAsync(r => r.Code == normalizedRoleName);
        }
        
        public void Dispose()
        {
            dataContext.Dispose();
        }
    }
}