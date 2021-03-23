using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.Abstract
{
    public interface IJwtService
    {
        string GenerateJwt(AppUser appUser, List<AppRole> roles);
    }
}
