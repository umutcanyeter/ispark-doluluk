using IsparkDoluluk.Dto.Abstract;
using IsparkDoluluk.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Dto.Concrete
{
    public class LiveCapacityGetByDistrictReponseDto : IDto
    {
        public ParkPlace ParkPlace { get; set; }
        public int CurrentCapacity { get; set; }
    }
}
