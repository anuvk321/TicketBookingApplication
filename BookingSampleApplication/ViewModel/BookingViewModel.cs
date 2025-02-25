using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingSampleApplication;
using BookingSampleApplication.BusinessLogic;

namespace BookingSampleApplication
{
    public class BookingViewModel : ViewModelBase
    {
        BookingLogic bookingLogic;
        //BusinessLogic logic;
        public BookingViewModel()
        {
            _numberOfSeatCol = new List<int>();
            for (int i = 1; i <= 6; i++)
            {                
                _numberOfSeatCol.Add(i);
            }

            bookingLogic = new BookingLogic();
            BookCommand = new DelegateCommand(new Action<object>(BookingClicked));
            _result = "Welcome.! Please selecct a Date and Click 'Refresh View' to see seat allocation";
        }

        public ObservableCollection<Spectator> SpectatorList { get; set; }
        public string SelectedSpectator { get; set; }

        private ICommand _bookCommand;
        public ICommand BookCommand
        {
            get { return _bookCommand; }
            set { _bookCommand = value; }
        }

        private ICommand _refreshBooking;
        public ICommand RefreshBooking
        {
            get { return _refreshBooking; }
            set { _refreshBooking = value; }
        }
        
        private string _venueName;
        public string VenueName
        {
            get { return _venueName; }
            set
            {
                _venueName = value;
                OnPropertyChanged("VenueName");
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                OnPropertyChanged("CustomerName");
                OnPropertyChanged("IsBookingButtonEnabled");
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
                OnPropertyChanged("IsBookingButtonEnabled");
            }
        }

        private DateTime _bookedDateFor = DateTime.Now;
        public DateTime BookedDateFor
        {
            get { return _bookedDateFor; }
            set
            {
                _bookedDateFor = value;
                OnPropertyChanged("BookedDateFor");
            }
        }

        public bool IsBookingButtonEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(_customerName) || string.IsNullOrEmpty(_phoneNumber) || _selectedNoOfSeats == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }           
        }
        

        public DateTime DisplayStartDate
        {
            get { return DateTime.Now; }          
        }

        public DateTime DisplayEndDate
        {
            get { return DateTime.Now.AddDays(7); }          
        }
        

        private List<int> _numberOfSeatCol;

        public List<int> NumberOfSeatCol
        {
            get { return _numberOfSeatCol; }
            set
            {
                _numberOfSeatCol = value;
            OnPropertyChanged("NumberOfSeatCol");
            }
        }
        

        private string _selectedTime;
        public string SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                OnPropertyChanged("SelectedTime");
            }
        }

        private int _selectedNoOfSeats;
        public int SelectedNoOfSeats
        {
            get { return _selectedNoOfSeats; }
            set
            {
                _selectedNoOfSeats = value;
                OnPropertyChanged("SelectedNoOfSeats");
                OnPropertyChanged("IsBookingButtonEnabled");
                OnPropertyChanged("IsCheckboxEnabled");                
            }
        }

        private bool _isDisabled;
        public bool IsDisabled
        {
            get { return _isDisabled; }
            set
            {
                _isDisabled = value;
                OnPropertyChanged("IsDisabled");
            }
        }

        public bool IsCheckboxEnabled
        {
            get {
                if (_selectedNoOfSeats <=1)
                {
                    _isDisabled = false;
                    OnPropertyChanged("IsDisabled");
                    return false;
                }
                else
                {
                    return true;
                }
            }            
        }
        

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public void BookingClicked(object parameter)
        {
            IBookingDetail bookingDetail = new BookingDetail
            {
                    Name = _customerName,
                    PhoneNumber = _phoneNumber,
                    NoOfSeatsRequired =Convert.ToInt32( _selectedNoOfSeats),
                    BookedFor = _bookedDateFor.Date,
                    BookedTiming = _selectedTime,
                    IsDisable = _isDisabled
            };
            // Bind thi to UI
            Result = bookingLogic.BookTicket(bookingDetail);
        }        
    }
}
