using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingSampleApplication
{
    public class LoginControlViewModel : ViewModelBase
    {
        public delegate void NotifyLoginClick(string value);
        public event NotifyLoginClick LoginClick;
  
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }

        private string _name = "Anu";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _password = "1234";
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _loginFailMessage;
          public string LoginFailMessage
        {
            get { return _loginFailMessage; }
            set
            {
                _loginFailMessage = value;
                OnPropertyChanged("LoginFailMessage");
            }
        }
        


        public LoginControlViewModel()
        {
            LoginCommand = new DelegateCommand(new Action<object>(LoginClicked));
        }

        
        public void LoginClicked(object parameter)
        {
            if (LoginClick != null)
            {
                User user = new User();
                user.Name = _name;
                user.Password = _password;
                if (user.Authnticate())
                {
                    LoginClick("");
                }
                else
                {
                    //Login failed
                    LoginFailMessage = "Incorrect Login.";
                }
            }
        }
    }
}
