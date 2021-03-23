using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.Concrete
{
    public class LiveCapacityManager : GenericManager<LiveCapacity>, ILiveCapacityService
    {
        private readonly IGenericDal<LiveCapacity> _genericDal;

        public LiveCapacityManager(IGenericDal<LiveCapacity> genericDal) : base(genericDal)
        {

        }
    }
}
