using System.Collections.Generic;
using IsparkDoluluk.Entities.Abstract;

namespace IsparkDoluluk.Entities.Concrete
{
    public class AppRole : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppUserRole> AppUserRoles { get; set; }
    }
}