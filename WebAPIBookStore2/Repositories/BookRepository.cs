using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIBookStore2.Models;

namespace WebAPIBookStore2.Repositories
{
    public class BookRepository
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>
            {
                new Book { Id = 0, Title = "Il signore degli anelli", Author = new Author { Name = "Tolkien" }, Rating = 3},
                new Book { Id = 1, Title = "Titolo 2", Author = new Author { Name = "Autore 2" }, Rating = 1 },
                new Book { Id = 2, Title = "Titolo 3", Author = new Author { Name = "Autore 3" }, Rating = 4 }
            };
        }

        public IEnumerable<Book> GetAll()
        {
            // SQL SELECT
            return _books;
        }

        public int Add(Book book)
        {
            // SQL INSERT
            book.Id = _books.Max(x => x.Id) + 1;
            _books.Add(book);
            return book.Id;
        }

        public Book Get(int id)
        {
            // SQL SELECT
            return _books.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Book book)
        {
            // SQL UPDATE
            var index = _books.FindIndex(x => x.Id == book.Id);
            _books.RemoveAt(index);
            _books.Insert(index, book);
        }

        public void Delete(int id)
        {
            _books.RemoveAll(x => x.Id == id);
        }
    }
}