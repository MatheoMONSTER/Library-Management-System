using Library.Database;
using Library.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var serviceProvider = new ServiceCollection() 
            //    .AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=DESKTOP-1HI3IIL;Initial Catalog=Library;Integrated Security=True;"))
            //    .BuildServiceProvider();

            var welcomeScreen = new WelcomeScreen();
            welcomeScreen.Show();
        }

    }
}
