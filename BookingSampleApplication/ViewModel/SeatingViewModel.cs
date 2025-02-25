using BookingSampleApplication.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingSampleApplication
{
    public class SeatingViewModel: ViewModelBase
    {
        private SeatingLogic seatingLogic;

        private DateTime _selectedDateFor = DateTime.Now;
        public DateTime SelectedDateFor
        {
            get { return _selectedDateFor.Date; }
            set
            {
                _selectedDateFor = value;
                OnPropertyChanged("SelectedDateFor");
            }
        }

        private DataTable _seatingArrangement;
        public DataTable SeatingArrangement
        {
            get { return _seatingArrangement; }
            set
            {
                _seatingArrangement = value;
                OnPropertyChanged("SeatingArrangement");
            }
        }

        public DateTime DisplayStartDate
        {
            get { return DateTime.Now; }
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get { return _refreshCommand; }
            set { _refreshCommand = value; }
        }

        public SeatingViewModel()
        {
            seatingLogic = new SeatingLogic();
            RefreshCommand = new DelegateCommand(new Action<object>(RefreshClicked));
            DataTable table = seatingLogic.GetSeatingArrangement(DateTime.Now);
            if (table != null)
            {
                SeatingArrangement = GetInversedDataTable(table, "ColumnSequence", "Sequence", "SeatNumber", "Isle");
            }
        }

        public void RefreshClicked(object parameter)
        {
            DataTable table = seatingLogic.GetSeatingArrangement(_selectedDateFor.Date);
            if(table != null)
            {
                SeatingArrangement = GetInversedDataTable(table, "ColumnSequence", "Sequence", "SeatNumber", "Isle");
            }
        }

        public static DataTable GetInversedDataTable(DataTable table, string columnX, string columnY, string columnZ, string nullValue)
        {
            //Create a DataTable to Return
            DataTable returnTable = new DataTable();

            if (columnX == "")
                columnX = table.Columns[0].ColumnName;

            //Add a Column at the beginning of the table
            //returnTable.Columns.Add(columnY);

            //Read all DISTINCT values from columnX Column in the provided DataTale
            List<string> columnXValues = new List<string>();

            foreach (DataRow dr in table.Rows)
            {

                string columnXTemp = dr[columnX].ToString();
                if (!columnXValues.Contains(columnXTemp))
                {
                    //Read each row value, if it's different from others provided, add to 
                    //the list of values and creates a new Column with its value.
                    columnXValues.Add(columnXTemp);
                    returnTable.Columns.Add(columnXTemp);
                }
                if (returnTable.Columns.Count == 3)
                {
                    returnTable.Columns.Add("Isle");
                }
            }

            //Verify if Y and Z Axis columns re provided
            if (columnY != "" && columnZ != "")
            {
                //Read DISTINCT Values for Y Axis Column
                List<string> columnYValues = new List<string>();

                foreach (DataRow dr in table.Rows)
                {
                    if (!columnYValues.Contains(dr[columnY].ToString()))
                        columnYValues.Add(dr[columnY].ToString());
                }

                //Loop all Column Y Distinct Value
                foreach (string columnYValue in columnYValues)
                {
                    //Creates a new Row
                    DataRow drReturn = returnTable.NewRow();
                    drReturn[0] = columnYValue;
                    //foreach column Y value, The rows are selected distincted
                    DataRow[] rows = table.Select(columnY + "='" + columnYValue + "'");

                    //Read each row to fill the DataTable
                    foreach (DataRow dr in rows)
                    {
                        string rowColumnTitle = dr[columnX].ToString();

                        //Read each column to fill the DataTable
                        foreach (DataColumn dc in returnTable.Columns)
                        {
                            if (dc.ColumnName == rowColumnTitle)
                            {
                                drReturn[rowColumnTitle] = Convert.ToString(dr[columnZ]) + "\n" + (Convert.ToString(dr["IsBooked"]) == "0" ? "Available" : "Booked");
                            }
                        }
                    }
                    returnTable.Rows.Add(drReturn);
                }
            }
            else
            {
                throw new Exception("The columns to perform inversion are not provided");
            }

            //if a nullValue is provided, fill the datable with it
            if (nullValue != "")
            {
                foreach (DataRow dr in returnTable.Rows)
                {
                    foreach (DataColumn dc in returnTable.Columns)
                    {
                        if (dr[dc.ColumnName].ToString() == "")
                            dr[dc.ColumnName] = nullValue;
                    }
                }
            }
            return returnTable;
        }
    }
}
