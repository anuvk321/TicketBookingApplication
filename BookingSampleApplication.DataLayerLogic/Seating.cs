using BookingSampleApplication.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BookingSampleApplication.DataAccess
{
    public class Seating
    {
        public DataTable GetSeatingArrangement(DateTime dateTime)
        {
            DataSet result = new DataSet();
            DataLayerLogic dataLayer = new DataLayerLogic();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@BookingDate", dateTime);
            result = dataLayer.getDataSetProcedure(parameters, "USP_GetSeatingArrangement");
            if (result.Tables.Count > 0)
            {
                return result.Tables[0];
            }
            else
            {
                return new DataTable();
            }
        }
    }
}
