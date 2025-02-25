using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSampleApplication
{
    public interface IBookingDetail
    {
        string Name { get; set; }
        string PhoneNumber { get; set; }
        bool IsDisable { get; set; }
        int NoOfSeatsRequired { get; set; }
        DateTime BookedFor { get; set; }
        string BookedTiming { get; set; }
    }
}
