using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.Concrete
{
    public class ParkPlaceManager : GenericManager<ParkPlace>, IParkPlaceService
    {
        private readonly IGenericDal<ParkPlace> _genericDal;

        public ParkPlaceManager(IGenericDal<ParkPlace> genericDal) : base(genericDal)
        {

        }
    }
}
