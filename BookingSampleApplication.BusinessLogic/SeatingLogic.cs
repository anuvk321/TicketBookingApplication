using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSampleApplication.DataAccess;
using System.Data;

namespace BookingSampleApplication.BusinessLogic
{
    
    public class SeatingLogic
    {
        Seating dataAccessSeating;

        public SeatingLogic()
        {
            dataAccessSeating = new Seating();           
        }

        public DataTable GetSeatingArrangement(DateTime date)
        {
            DataTable result = new DataTable();
            result = dataAccessSeating.GetSeatingArrangement(date);
            return result;
        }
    }
}
