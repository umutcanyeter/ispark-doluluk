using IsparkDoluluk.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Dto.Concrete
{
    public class ParkPlaceAddDto : IDto
    {
        public string Name { get; set; }
        public string ParkType { get; set; }
        public int Capacity { get; set; }
        public string WorkHours { get; set; }
        public string Adress { get; set; }
        public string District { get; set; }
    }
}
