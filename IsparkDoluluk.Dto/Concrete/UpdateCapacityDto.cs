using IsparkDoluluk.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Dto.Concrete
{
    public class UpdateCapacityDto : IDto
    {
        public int ParkPlaceId { get; set; }
        public int Capacity { get; set; }
    }
}
