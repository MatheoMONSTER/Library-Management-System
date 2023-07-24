using GalaSoft.MvvmLight.Command;
using Library.Database;
using Library.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class ManageBooksViewModel : BaseViewModel
    {
        public ICommand BorrowBookCommand { get; set; }
        public ICommand ReturnBookCommand { get; set;}
        public ICommand ViewBooksCommand { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public ManageBooksViewModel()
        {
            BorrowBookCommand = new RelayCommand(BorrowBook);
            ReturnBookCommand = new RelayCommand(ReturnBook);
            ViewBooksCommand = new RelayCommand(ViewBooks);

            LoadBooks();
        }

        private void BorrowBook()
        {
            BorrowBooks borrowBooks = new BorrowBooks();
            borrowBooks.DataContext = this;
            borrowBooks.Show();

        }

        private void ReturnBook()
        {
            MessageBox.Show("Return book command");
        }

        private void ViewBooks()
        {
            MessageBox.Show("View books");
        }

        private void LoadBooks()
        {
            using (var dbContext = new AppDbContext())
            {
                Books = new ObservableCollection<Book>(dbContext.Books.ToList());
            }
        }
    }
}
