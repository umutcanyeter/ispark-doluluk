using System.Collections.Generic;
using IsparkDoluluk.Entities.Abstract;

namespace IsparkDoluluk.Entities.Concrete
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<AppUserRole> AppUserRoles { get; set; }
    }
}