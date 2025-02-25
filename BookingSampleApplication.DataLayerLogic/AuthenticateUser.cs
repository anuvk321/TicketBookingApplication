using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BookingSampleApplication.DataAccess
{
    public class AuthenticateUser
    {
        public bool Autenticate(Dictionary <string, object> parameters)
        {
            bool result = false;
            DataLayerLogic dataLayer = new DataLayerLogic();
            DataTable dTable = new DataTable();
            dTable = dataLayer.getDataTableProcedure(parameters, "usp_validateUser");
            if (dTable.Rows.Count > 0)
            {
                result = Convert.ToBoolean(dTable.Rows[0][0]);
            }
            return result;
        }
    }
}
