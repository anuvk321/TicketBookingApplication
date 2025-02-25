using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSampleApplication
{
    public class BookingDetail: IBookingDetail
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDisable { get; set; }
        public int NoOfSeatsRequired { get; set; }
        public DateTime BookedFor { get; set; }
        public string BookedTiming { get; set; }
    }
}
