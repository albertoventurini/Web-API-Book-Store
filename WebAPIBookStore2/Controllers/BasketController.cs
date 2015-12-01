using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBookStore2.Models;
using WebAPIBookStore2.Repositories;

namespace WebAPIBookStore2.Controllers
{
    [Authorize]
    public class BasketController : ApiController
    {
        private static BasketRepository _basketRepository = new BasketRepository();
        private static BookRepository _bookRepository = new BookRepository();

        //// PUT /basket/id
        //public HttpResponseMessage Put(Basket basket)
        //{
        //    // dato un utente, va a reperire il basket dell'utente, se esiste
        //    // aggiorna il basket
        //    return null;
        //}

        // PUT /api/basket/book
        [HttpPut]
        [Route("api/baskets/book/")]
        public HttpResponseMessage AddBook(dynamic body)
        {
            // dato un utente, va a reperire il basket dell'utente, se esiste
            // aggiorna il basket

            int bookId = body.bookId;

            var basket = _basketRepository
                .GetAll()
                .FirstOrDefault(x => x.Username == User.Identity.Name);

            if (basket == null)
            {
                basket = new Basket
                {
                    Username = User.Identity.Name
                };
            }

            var book = _bookRepository.Get(bookId);

            _basketRepository.AddBook(basket, book);
            _basketRepository.SaveOrUpdate(basket);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // GET /basket/username
        [Route("api/baskets/{username}")]
        public HttpResponseMessage Get(string username)
        {
            var basket = _basketRepository.Get(username);

            if (basket == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, basket);
            return response;
        }
    }
}
