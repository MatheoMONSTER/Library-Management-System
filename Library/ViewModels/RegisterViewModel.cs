using GalaSoft.MvvmLight.Command;
using Library.Database;
using Library.Views;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private int _id; 
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
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

        public ICommand RegisterCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
            BackCommand = new RelayCommand(Back);
        }

        private void Register()
        {
            using (var dbContext = new AppDbContext())
            {
                User existingUser = dbContext.Users.FirstOrDefault(u => u.Username == Username);

                if (existingUser != null)
                {
                    MessageBox.Show("The user with the specified login already exists. Try again with a different username.");
                    Username = string.Empty;
                    Password = string.Empty;
                    return;
                }
            }

            var newUser = new User
            {
                Id = Id,
                Username = Username
            };

            byte[] salt = GenerateSalt();
            newUser.Salt = salt;
            newUser.Password = HashPassword(Password, salt);

            using (var dbContext = new AppDbContext())
            {
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
            }

            MessageBox.Show("User registered successfully!");
            Username = string.Empty;
            Password = string.Empty;

        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        private void Back()
        {

        }
    }
}
