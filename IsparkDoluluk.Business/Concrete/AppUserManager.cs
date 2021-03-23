using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.Dto.Concrete;
using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IsparkDoluluk.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IAppUserDal _appUserDal;
        public AppUserManager(IGenericDal<AppUser> genericDal, IAppUserDal appUserDal) : base(genericDal)
        {
            _appUserDal = appUserDal;
        }

        public async Task<bool> CheckPassword(AppUserLoginDto appUserLoginDto)
        {
            var appUser = await _appUserDal.GetByFilter(I => I.Username == appUserLoginDto.Username);
            return appUser.Password == appUserLoginDto.Password ? true : false;
        }

        public async Task<AppUser> FindByUserName(string userName)
        {
            return await _appUserDal.GetByFilter(I => I.Username == userName);
        }

        public async Task<List<AppRole>> GetRolesByUserName(string username)
        {
            return await _appUserDal.GetRolesByUsername(username);
        }
    }
}
