using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IsparkDoluluk.Business.Abstract
{
    public interface IAppUserRoleService : IGenericService<AppUserRole>
    {
        Task<AppRole> FindByName(string name);
    }
}
