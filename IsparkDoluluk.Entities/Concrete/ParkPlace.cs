using IsparkDoluluk.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Entities.Concrete
{
    public class ParkPlace : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParkType { get; set; }
        public int Capacity { get; set; }
        public string WorkHours { get; set; }
        public string Adress { get; set; }
        public string District { get; set; }
    }
}
