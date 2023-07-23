using GalaSoft.MvvmLight.Command;
using Library.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }   
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            using (var dbContext = new AppDbContext())
            {
                User user = dbContext.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

                if (user == null)
                {
                    MessageBox.Show("The user with the specified login does not exist. Try again.");
                    Username = string.Empty;
                    Password = string.Empty;
                    return;
                }

                if(user.Password != Password)
                {
                    MessageBox.Show("The password is incorrect. Try again.");
                }

                MessageBox.Show("Successfully logged in!");
            }
        }

        private string SecureStringToString(SecureString secureString)
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
