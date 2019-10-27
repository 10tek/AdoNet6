using HW6.DataAccess;
using HW6.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HW6.Services
{
    public class LibraryService
    {
        private LibaryContext context;

        public LibraryService(LibaryContext context)
        {
            this.context = context;
        }

        public List<Client> GetDebtors()
        {
            CheckDates();
            var clients = context.LibraryEntries
                .Where(x => x.Client.IsDebtor == true && x.IsDeleted == false)
                .Select(x=>x.Client)
                .ToList();
            return clients;
        }

        public List<Author> GetAuthorsThirdBook()
        {
            var book = context.Books
                .Skip(2)
                .Take(1) as Book;
            var authors = context.BooksAuthors.Where(x => x.Book == book)
                .Select(x=>x.Author)
                .ToList();
            return authors;
        }

        public List<Book> GetAvailableBooks()
        {
            var allBooks = context.Books
                .Where(x => x.IsDeleted == false)
                .ToList();
            var pickedBooks = context.LibraryEntries
                .Select(x => x.Book)
                .ToList();
            var availableBooks = allBooks.Except(pickedBooks).ToList();
            return availableBooks;
        }

        public List<Book> GetBooksFromSecondClient()
        {
            var secondClient = context.Clients.Skip(1).Take(1) as Client;
            var books = context.LibraryEntries.Where(x => x.Client == secondClient).Select(x=>x.Book).ToList();
            return books;
        }

        public void ResetDebtors()
        {
            var clients = context.Clients.Where(x => x.IsDebtor == true).ToList();
            clients.ForEach(x=> x.IsDebtor = false);
            context.Clients.UpdateRange(clients);
            context.SaveChanges();
        }

        private void CheckDates()
        {
            var entries = context.LibraryEntries
                .Where(x => x.ReturnDate < DateTime.Now && x.IsDeleted == false)
                .ToList();
            entries.ForEach(x => x.Client.IsDebtor = true);
            context.LibraryEntries.UpdateRange(entries);
            context.SaveChanges();
        }
    }
}
