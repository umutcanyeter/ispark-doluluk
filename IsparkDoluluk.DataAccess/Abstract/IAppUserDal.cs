using System.Collections.Generic;
using System.Threading.Tasks;
using IsparkDoluluk.Entities.Concrete;

namespace IsparkDoluluk.DataAccess.Abstract
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        Task<List<AppRole>> GetRolesByUsername(string username);
    }
}