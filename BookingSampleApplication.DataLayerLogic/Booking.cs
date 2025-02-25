using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BookingSampleApplication.DataAccess
{
    public class Booking
    {
        public DataSet GetAvailability(DateTime dateTime)
        {
            DataSet result = new DataSet();
            DataLayerLogic dataLayer = new DataLayerLogic();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@BookingDate", dateTime);
            result = dataLayer.getDataSetProcedure(parameters, "usp_CheckSeatAvailability");          
            return result;
        }
        public bool SaveBooking(Dictionary<string, object> bookingDetail)
        {
            bool isCompleted = false;
            DataSet result = new DataSet();
            DataLayerLogic dataLayer = new DataLayerLogic();
           
            result = dataLayer.getDataSetProcedure(bookingDetail, "USP_SaveBooking");
            if (result != null && result.Tables.Count > 0)
            {
                if (Convert.ToString(result.Tables[0].Rows[0][0]) == "Success")
                {
                    isCompleted = true;
                }
            }
            return isCompleted;
        }
    }
}
