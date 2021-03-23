using IsparkDoluluk.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Entities.Concrete
{
    public class LiveCapacity : IEntity
    {
        public int Id { get; set; }
        public int ParkPlaceId { get; set; }
        public int CurrentCapacity { get; set; }
    }
}
