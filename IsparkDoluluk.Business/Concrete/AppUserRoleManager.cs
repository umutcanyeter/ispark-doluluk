using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IsparkDoluluk.Business.Concrete
{
    public class AppUserRoleManager : GenericManager<AppUserRole>, IAppUserRoleService
    {
        private readonly IGenericDal<AppRole> _genericDal;
        public AppUserRoleManager(IGenericDal<AppUserRole> genericDal) : base(genericDal)
        {

        }

        public Task<AppRole> FindByName(string name)
        {
            return _genericDal.GetByFilter(I => I.Name == name);
        }
    }
}

