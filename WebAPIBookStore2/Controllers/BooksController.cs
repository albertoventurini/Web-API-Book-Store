using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBookStore2.Models;
using WebAPIBookStore2.Repositories;

namespace WebAPIBookStore2.Controllers
{
    public class BooksController : ApiController
    {
        private static BookRepository _bookRepository = new BookRepository();

        //public IEnumerable<Book> Get()
        //{
        //    var books = _bookRepository.GetAll();
        //    return books;
        //}

        public Book Get(int id)
        {
            var book = _bookRepository.Get(id);
            return book;
        }

        [HttpGet]
        public IEnumerable<Book> Get(string query = null, int? rating = null)
        {
            var books = _bookRepository
                .GetAll();

            if (!string.IsNullOrEmpty(query))
            {
                books = books.Where(x => x.Title.Contains(query) || x.Author.Name.Contains(query));
            }

            if (rating.HasValue)
            {
                books = books.Where(x => x.Rating >= rating);
            }
  
            return books;
        }

        //public HttpResponseMessage Patch(int id, Book book)
        //{
        //    if (book == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    var originalBook = _bookRepository.Get(id);
        //    if (!String.IsNullOrEmpty(book.Title))
        //        originalBook.Title = book.Title;
        //    if (book.Author != null)
        //    {
        //        originalBook.Author = book.Author;
        //    }

        //     _bookRepository.Save(originalBook);

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        public HttpResponseMessage Put(int id, Book book)
        {
            if (book == null || !ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bookRepository.Save(book);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(Book book)
        {
            if (book == null || !ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var bookId = _bookRepository.Add(book);

            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Path.Combine(Request.RequestUri.AbsoluteUri, bookId.ToString()));
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            _bookRepository.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }


        public HttpResponseMessage DeleteFromBody([FromBody] int id)
        {
            return null;
        }
    }
}
