using Library.Database;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Library
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Close();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var dbContext = new AppDbContext(optionsBuilder.Options);
        }
    }
}
