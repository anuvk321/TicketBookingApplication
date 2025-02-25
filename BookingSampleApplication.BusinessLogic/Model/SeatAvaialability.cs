using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSampleApplication.BusinessLogic
{
    public class SeatAvaialability
    {
        public int TotalAvailableSeats { get; set; }
        public int Priority { get; set; }
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsLeftToIsle { get; set; }
    }
}
