using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class MainViewModel
    {
        public ICommand LoginCommand { get; set; }  
        public ICommand CreateAccountCommand { get; set; }

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(OpenLoginWindow);
            CreateAccountCommand = new RelayCommand(OpenCreateAccountWindow);
        }

        private void OpenLoginWindow()
        {

        }

        private void OpenCreateAccountWindow()
        {

        }
    }
}
