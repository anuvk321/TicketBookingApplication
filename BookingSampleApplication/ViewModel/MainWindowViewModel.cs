using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingSampleApplication;
using System.Windows;

namespace BookingSampleApplication
{
    public class MainWindowViewModel:ViewModelBase
    {
        public ObservableCollection<Spectator> SpectatorList { get; set; }
        public string SelectedSpectator { get; set; }

        private ICommand _aboutCommand;
        public ICommand AboutCommand
        {
            get { return _aboutCommand; }
            set { _aboutCommand = value; }
        }
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; }
        }   

        private string _selectedName;
        public string SelectedName 
        {
            get { return _selectedName; }
            set
            {
                    _selectedName = value;
                    OnPropertyChanged("SelectedName");                    
            }
        }

        LoginControlViewModel _loginControlViewModel;
        public LoginControlViewModel LoginControlViewModel
        {
            get { return _loginControlViewModel; }
            set
            {
                if (_loginControlViewModel != value)
                {
                    _loginControlViewModel = value;
                    OnPropertyChanged("LoginControlViewModel");
                }
            }
        }

        private BookingViewModel _bookingViewModel;
        public BookingViewModel BookingViewModel
        {
            get { return _bookingViewModel; }
            set
            {
                    _bookingViewModel = value;
                    OnPropertyChanged("BookingViewModel");
            }
        }

        private Visibility _showloginControl = Visibility.Visible;
        public Visibility ShowloginControl
        {
            get { return _showloginControl; }
            set
            {
                    _showloginControl = value;
                    OnPropertyChanged("ShowloginControl");                    
            }
        }

        private Visibility _showMainControl = Visibility.Collapsed;
        public Visibility ShowMainControl
        {
            get { return _showMainControl; }
            set
            {
                    _showMainControl = value;
                    OnPropertyChanged("ShowMainControl");
            }
        }

        public MainWindowViewModel()
        {
            AboutCommand = new DelegateCommand(new Action<object>(SelectedSpectatorDetails));


            CloseCommand = new DelegateCommand(new Action<object>(CloseApplicationClicked));
            //SpectatorList = new ObservableCollection<Spectator> 
            //{ 
            //        new Spectator { FirstName = "Jenny" },
            //        new Spectator { FirstName = "Harry" },
            //        new Spectator { FirstName = "Stuart" },
            //        new Spectator { FirstName = "Robert" }
            //};          

            //LoadLoginScreen(); 
          }

        public void LoadMainWindow()
        {
           
            ShowMainControl = Visibility.Visible;
            ShowloginControl = Visibility.Collapsed;
            OnPropertyChanged("ShowloginControl");
            OnPropertyChanged("ShowMainControl");
            SelectedName = SelectedName + "ff";
        }

        private void LoadLoginScreen()
        {
            //_loginControlViewModel = new LoginControlViewModel();
            //_loginControlViewModel.LoginClick += _loginControlViewModel_LoginClick;
            //this.DataContext = loginControlViewModel;
        }

        private void _loginControlViewModel_LoginClick(string value)
        {
            LoadMainScreen();
        }

        private void LoadMainScreen()
        {
            //_bookingViewModel = new BookingViewModel();
            ShowloginControl = Visibility.Collapsed;
            ShowMainControl = Visibility.Visible;
        }

        public void SelectedSpectatorDetails(object parameter)
        {
            if (parameter != null)
                SelectedName = (parameter as Spectator).Name;
        }

        public void CloseApplicationClicked(object parameter)
        {
            Application.Current.Shutdown();
        }       

        
      
    }
}
