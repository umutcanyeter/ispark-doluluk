using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.DataAccess.Concrete.Repository;
using IsparkDoluluk.Entities.Concrete;

namespace IsparkDoluluk.DataAccess.Concrete.Repository
{
    public class EfAppUserRoleRepository : EfGenericRepository<AppUserRole>, IAppUserRoleDal
    {
        
    }
}