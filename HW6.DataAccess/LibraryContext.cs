using HW6.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.DataAccess
{
    public class LibaryContext : DbContext
    {
        private readonly string connectionString;

        public LibaryContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BooksAuthors> BooksAuthors { get; set; }
        public DbSet<LibraryEntry> LibraryEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksAuthors>().HasOne(x => x.Book).WithMany(x => x.Authors);
            modelBuilder.Entity<BooksAuthors>().HasOne(x => x.Author).WithMany(x => x.Books);
        }
    }
}
