using HW6.DataAccess;
using HW6.Domain;
using HW6.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HW6.UI
{
    public class LibraryUI : IDisposable
    {
        private LibaryContext context;
        private LibraryService libraryService;

        public LibraryUI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();
            var connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
            context = new LibaryContext(connectionString);
            libraryService = new LibraryService(context);
        }

        public void Action()
        {
            FillTables();
            var isExit = false;
            while (!isExit)
            {
                Console.Clear();
                Console.WriteLine("1 - Cписок должников");
                Console.WriteLine("2 - Список авторов книги №3");
                Console.WriteLine("3 - Список книг, которые доступны в данный момент");
                Console.WriteLine("4 - Список книг, которые на руках у пользователя №2");
                Console.WriteLine("5 - Обнуление задолженности всех должников");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите пункт меню: ");
                if (int.TryParse(Console.ReadLine(), out var menu))
                {
                    switch (menu)
                    {
                        case 1: ShowDebtors(); break;
                        case 2: ShowThirdBookAuthors(); break;
                        case 3: ShowAvailableBooks(); break;
                        case 4: ShowSecondClientsBooks(); break;
                        case 5: libraryService.ResetDebtors(); Console.ReadKey(); break;
                        case 0: isExit = true; break;
                        default: Console.WriteLine("фиговый выбор"); Console.Read(); break;
                    }
                }
            }
        }

        private void ShowDebtors()
        {
            Console.Clear();
            Console.WriteLine("Список должников:");
            var debtors = libraryService.GetDebtors();
            foreach(var debtor in debtors)
            {
                Console.WriteLine($"Имя: {debtor.FullName}");
            }
            Console.ReadKey();
        }

        private void ShowThirdBookAuthors()
        {
            Console.Clear();
            Console.WriteLine("Список авторов третьей книги:");
            var authors = libraryService.GetAuthorsThirdBook();
            authors.ForEach(x => Console.WriteLine($"Имя автора: {x.FullName}"));
            Console.ReadKey();
        }

        private void ShowAvailableBooks()
        {
            Console.Clear();
            Console.WriteLine("Список доступных книг:");
            var books = libraryService.GetAvailableBooks();
            books.ForEach(x => Console.WriteLine($"kNigga: {x.TitleName}"));
            Console.ReadKey();
        }

        private void ShowSecondClientsBooks()
        {
            Console.Clear();
            Console.WriteLine("Список авторов третьей книги:");
            var books = libraryService.GetBooksFromSecondClient();
            books.ForEach(x => Console.WriteLine($"кNigga: {x.TitleName}"));
            Console.ReadKey();
        }

        private void FillTables()
        {
            var firstClient = new Client
            {
                FullName = "Наруто Удзумаки",
                IsDebtor = false
            };
            var secondClient = new Client
            {
                FullName = "Саске Учиха",
                IsDebtor = true
            };
            var thirdClient = new Client
            {
                FullName = "Олег Сергеевич",
                IsDebtor = true
            };

            var firstAuthor = new Author
            {
                FullName = "Джирайя"
            };
            var secondAuthor = new Author
            {
                FullName = "Джоан Роулинг"
            };
            var thirdAuthor = new Author
            {
                FullName = "Масаши Кишимото"
            };

            var firstBook = new Book
            {
                TitleName = "Наруто: Ураганные Хроники"
            };
            var secondBook = new Book
            {
                TitleName = "Гарри Поттер"
            };
            var thirdBook = new Book
            {
                TitleName = "Наруто Поттер, тактика"

            };
            var fourthBook = new Book
            {
                TitleName = "Приди, приди, тактика"
            };

            var firstBooksAuthors = new BooksAuthors
            {
                Book = firstBook,
                Author = thirdAuthor
            };
            var secondBooksAuthors = new BooksAuthors
            {
                Book = secondBook,
                Author = secondAuthor
            };
            var thirdBooksAuthors = new BooksAuthors
            {
                Book = thirdBook,
                Author = firstAuthor
            };
            var fourthBooksAuthors = new BooksAuthors
            {
                Book = fourthBook,
                Author = firstAuthor
            };
            var firstEntry = new LibraryEntry
            {
                Book = firstBook,
                Client = firstClient,
                ReturnDate = DateTime.Now
            };
            var secondEntry = new LibraryEntry
            {
                Book = secondBook,
                Client = firstClient,
                ReturnDate = DateTime.Now
            };
            var thirdEntry = new LibraryEntry
            {
                Book = thirdBook,
                Client = secondClient,
                ReturnDate = DateTime.Now
            };

            context.BooksAuthors.AddRange(
                new BooksAuthors
                {
                    Book = thirdBook,
                    Author = secondAuthor
                },
                thirdBooksAuthors,
                new BooksAuthors
                {
                    Book = thirdBook,
                    Author = thirdAuthor
                });

            context.Clients.AddRange(firstClient, secondClient, thirdClient);
            context.Authors.AddRange(firstAuthor, secondAuthor, thirdAuthor);
            context.Books.AddRange(firstBook, secondBook, thirdBook, fourthBook);
            context.BooksAuthors.AddRange(firstBooksAuthors, secondBooksAuthors, fourthBooksAuthors);
            context.LibraryEntries.AddRange(firstEntry, secondEntry, thirdEntry);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
