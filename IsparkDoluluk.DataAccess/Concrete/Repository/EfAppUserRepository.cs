using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.DataAccess.Concrete.Context;
using IsparkDoluluk.Entities.Concrete;
using IsparkDoluluk.DataAccess.Concrete.Repository;

namespace IsparkDoluluk.DataAccess.Concrete.Repository
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {
        public async Task<List<AppRole>> GetRolesByUsername(string username)
        {
            using var context = new IsparkDolulukDbContext();
            return await context.AppUsers.Join(context.AppUserRoles, u => u.Id, ur => ur.AppUserId, (user, userRole) => new
            {
                user = user,
                userRole = userRole
            }).Join(context.AppRoles, two => two.userRole.AppRoleId, r => r.Id, (twoTable, role) => new
            {
                user = twoTable.user,
                userRole = twoTable.userRole,
                role = role
            }).Where(I => I.user.Username == username).Select(I => new AppRole
            {
                Id = I.role.Id,
                Name = I.role.Name
            }).ToListAsync();
        }
    }
}