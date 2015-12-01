using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIBookStore2.Models;

namespace WebAPIBookStore2.Repositories
{
    public class AuthorRepository
    {
        private List<Author> _authors;

        public AuthorRepository()
        {
            _authors = new List<Author>
            {
                new Author { Id = 0, Name = "Tolkien" },
                new Author { Id = 1, Name = "Martin" }
            };
        }

        public IEnumerable<Author> GetAll()
        {
            // in realtà, va a fare una query su DB
            return _authors;
        }

        public Author Get(int id)
        {
            return _authors.FirstOrDefault(x => x.Id == id);
        }
    }
}