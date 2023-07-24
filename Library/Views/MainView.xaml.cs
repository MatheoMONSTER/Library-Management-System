using Library.ViewModels;
using System.Windows;

namespace Library.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new ManageBooksViewModel();
        }
    }
}
