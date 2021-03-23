using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.DataAccess.Concrete.Repository
{
    public class EfParkPlaceRepository : EfGenericRepository<ParkPlace>, IParkPlaceDal
    {
    }
}
