using GalaSoft.MvvmLight.Command;
using Library.Database;
using System.Linq;
using System.Security.Cryptography;
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
                User user = dbContext.Users.FirstOrDefault(u => u.Username == Username);

                if (user == null)
                {
                    MessageBox.Show("The user with the specified login does not exist. Try again.");
                    Username = string.Empty;
                    Password = string.Empty;
                    return;
                }

                byte[] hashedPassword = HashPassword(Password, user.Salt);

                if (!hashedPassword.SequenceEqual(user.Password))
                {
                    MessageBox.Show("The password is incorrect. Try again.");
                    Password = string.Empty;
                    return;
                }

                MessageBox.Show("Successfully logged in!");
            }
        }
        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return pbkdf2.GetBytes(32);
            }
        }
    }

}
