using BookingSampleApplication.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSampleApplication.BusinessLogic
{
    public class BookingLogic
    {
        private Booking dataAccessBooking;
        private DataSet seatAvailability;
        public BookingLogic()
        {
            dataAccessBooking = new Booking();
            seatAvailability = new DataSet();
        }

        public string BookTicket(IBookingDetail bookingDetail)
        {
            string value = "Success";
            bool isSeatAvailable = false;
            seatAvailability = GetSeatAvailability(bookingDetail.BookedFor);
            List<SeatAvaialability> leftSideAvailability, rightSideAvailability;
            if (seatAvailability.Tables.Count == 2)
            {
                leftSideAvailability = TransformDataTableToEnitity(seatAvailability.Tables[0]);
                rightSideAvailability = TransformDataTableToEnitity(seatAvailability.Tables[1]);
                string seats = FindPossibleSeatsForBooking(bookingDetail, leftSideAvailability, rightSideAvailability, ref isSeatAvailable);
                if (isSeatAvailable)
                {
                    // book ticket
                    value = SaveBooking(bookingDetail, seats)?"Booking Success":"Booking Failed";
                }
                else
                {
                    value = seats;
                }
            }
            return value;
        }

        private string FindPossibleSeatsForBooking(IBookingDetail bookingDetail, List<SeatAvaialability> leftSideAvailability, List<SeatAvaialability> rightSideAvailability, ref bool isSeatAvailable)
        {
            string result = string.Empty;
            int minPriorityLeftSide = leftSideAvailability.Min(p => p.Priority);
            int minPriorityRightSide = rightSideAvailability.Min(p => p.Priority);
            List<string> seatIds = new List<string>();

            if (bookingDetail.IsDisable)
            {
                seatIds = GetIDsOfSeatsFromWheelChairFriendlySeats(leftSideAvailability, rightSideAvailability, ref seatIds);
                if (seatIds.Count == 0)
                {
                    isSeatAvailable = false;
                    result = "No Seats avaialable for selected numbers";
                    return result;
                }
            }
            if (minPriorityLeftSide == minPriorityRightSide)
            {
                int noOfPrioritySeatsLeftSide = leftSideAvailability.Count(p => p.Priority == minPriorityLeftSide);
                int noOfPrioritySeatsRightSide = rightSideAvailability.Count(p => p.Priority == minPriorityRightSide);
                if (noOfPrioritySeatsLeftSide >= noOfPrioritySeatsRightSide)
                {
                    //Book start from Left Side
                    GetIDsOfSeats(bookingDetail.NoOfSeatsRequired, leftSideAvailability, rightSideAvailability, ref seatIds);
                }
                else
                {
                    //Book start from right side
                    GetIDsOfSeats(bookingDetail.NoOfSeatsRequired, rightSideAvailability, leftSideAvailability, ref seatIds);
                }
            }
            else if (minPriorityLeftSide < minPriorityRightSide)
            {
                //Book start from Left Side
                GetIDsOfSeats(bookingDetail.NoOfSeatsRequired, leftSideAvailability, rightSideAvailability, ref seatIds);
            }
            else
            {
                //Book start from right side
                GetIDsOfSeats(bookingDetail.NoOfSeatsRequired, rightSideAvailability, leftSideAvailability, ref seatIds);
            }

            if (bookingDetail.NoOfSeatsRequired == seatIds.Count)
            {
                StringBuilder seats = new StringBuilder();
                foreach (var item in seatIds)
                {
                    seats.Append(item);
                    seats.Append(",");
                }
                isSeatAvailable = true;
                result = seats.ToString();
                if (result.Contains(","))
                {
                    result = result.Substring(0, result.LastIndexOf(','));
                }
            }
            else
            {
                result = "No Seats avaialable for selected numbers";
                isSeatAvailable = false;
            }

            return result;
        }

        private List<string> GetIDsOfSeatsFromWheelChairFriendlySeatsIfOtherSeatsBooked(int count, List<SeatAvaialability> firstPriority, List<SeatAvaialability> secondPriority, ref List<string> seatIds)
        {
            if (count > seatIds.Count)
            {
                if (firstPriority.Where(p => p.Priority == 5).Any())
                {
                    foreach (var item in firstPriority.Where(p => p.Priority == 5).ToList())
                    {
                        if (!(seatIds.Contains(Convert.ToString(item.Id))))
                        {
                            seatIds.Add(Convert.ToString(item.Id));
                            if (seatIds.Count == count)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (seatIds.Count < count)
            {
                if (secondPriority.Where(p => p.Priority == 6).Any())
                {
                    foreach (var item in secondPriority.Where(p => p.Priority == 6).ToList())
                    {
                        if (!(seatIds.Contains(Convert.ToString(item.Id))))
                        {
                            seatIds.Add(Convert.ToString(item.Id));
                            if (seatIds.Count == count)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return seatIds;
        }

        private List<string> GetIDsOfSeatsFromWheelChairFriendlySeats(List<SeatAvaialability> firstPriority, List<SeatAvaialability> secondPriority, ref List<string> seatIds)
        {

            if (firstPriority.Where(p => p.Priority == 5).Any())
            {
                foreach (var item in firstPriority.Where(p => p.Priority == 5).ToList())
                {
                    if (!(seatIds.Contains(Convert.ToString(item.Id))))
                    {
                        seatIds.Add(Convert.ToString(item.Id));
                    }
                }
            }
            if (seatIds.Count != 2)
            {
                if (secondPriority.Where(p => p.Priority == 6).Any())
                {
                    foreach (var item in secondPriority.Where(p => p.Priority == 6).ToList())
                    {
                        if (!(seatIds.Contains(Convert.ToString(item.Id))))
                        {
                            seatIds.Add(Convert.ToString(item.Id));
                        }
                    }
                }
            }
            return seatIds;
        }

        private bool GetIDsOfSeats(int count, List<SeatAvaialability> firstPriority, List<SeatAvaialability> secondPriority, ref List<string> seatIds)
        {
            if (count <= seatIds.Count)
            {
                return true;
            }

            foreach (var item in firstPriority)
            {
                if (!(item.Priority == 5 || item.Priority == 6))
                {
                    seatIds.Add(Convert.ToString(item.Id));
                    if (seatIds.Count == count)
                    {
                        break;
                    }
                }
            }

            if (count > seatIds.Count)
            {
                foreach (var item in secondPriority)
                {
                    if (!(item.Priority == 5 || item.Priority == 6))
                    {
                        seatIds.Add(Convert.ToString(item.Id));
                        if (seatIds.Count == count)
                        {
                            break;
                        }
                    }
                }
            }
            if (count > seatIds.Count)
            {
                GetIDsOfSeatsFromWheelChairFriendlySeatsIfOtherSeatsBooked(count, firstPriority, secondPriority, ref seatIds);
            }
            return true;
        }

        private List<SeatAvaialability> TransformDataTableToEnitity(DataTable table)
        {
            List<SeatAvaialability> availability = new List<SeatAvaialability>();
            var products = table.AsEnumerable().Select(row => new SeatAvaialability
            {
                Id = row.Field<int>("Id"),
                Priority = row.Field<int>("Priority"),
                TotalAvailableSeats = row.Field<int>("AvailableSeat"),
                SeatNumber = row.Field<string>("SeatNumber"),
                IsLeftToIsle = row.Field<bool>("IsLeftToIsle")
            });
            availability = new List<SeatAvaialability>(products);
            return (availability);
        }

        private DataSet GetSeatAvailability(DateTime dateTime)
        {
            return (dataAccessBooking.GetAvailability(dateTime));
        }

        public bool SaveBooking(IBookingDetail bookingDetail, string seatIDs)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@SeatIDs", seatIDs);
                parameters.Add("@Name", bookingDetail.Name);
                parameters.Add("@RequiredSeat", bookingDetail.NoOfSeatsRequired);
                parameters.Add("@BookingFor", bookingDetail.BookedFor);
                parameters.Add("@Venue", "Venue1");
                parameters.Add("@Contact", bookingDetail.PhoneNumber);

                return (dataAccessBooking.SaveBooking(parameters));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
