using Library.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class BorrowBooksViewModel : BaseViewModel
    {
        public ObservableCollection<Book> AvailableBooks { get; set; }  

        public BorrowBooksViewModel()
        {
            using (var dbContext = new AppDbContext())
            {
                AvailableBooks = new ObservableCollection<Book>(dbContext.Books.ToList());
            }
        }
    }
}
