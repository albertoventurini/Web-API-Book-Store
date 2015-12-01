using System.Collections.Generic;
using System.Linq;
using WebAPIBookStore2.Models;

namespace WebAPIBookStore2.Repositories
{
    public class BasketRepository
    {
        private List<Basket> _baskets;

        public BasketRepository()
        {
            _baskets = new List<Basket>();
        }

        public Basket Get(int id)
        {
            return _baskets.FirstOrDefault(x => x.Id == id);
        }

        public Basket Get(string username)
        {
            return _baskets.FirstOrDefault(x => x.Username == username);
        }

        public void Add(Basket basket)
        {
            _baskets.Add(basket);
        }

        public IEnumerable<Basket> GetAll()
        {
            return _baskets;
        }

        public void Save(Basket basket)
        {
            var index = _baskets.FindIndex(x => x.Id == basket.Id);
            _baskets.RemoveAt(index);
            _baskets.Insert(index, basket);
        }

        public void SaveOrUpdate(Basket basket)
        {
            if (!_baskets.Contains(basket))
            {
                _baskets.Add(basket);
            }
            Save(basket);
        }

        public void AddBook(Basket basket, Book book)
        {
            if (basket.Books == null)
            {
                basket.Books = new List<Book>();
            }
            basket.Books.Add(book);
        }
    }
}