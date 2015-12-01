using System.Collections.Generic;

namespace WebAPIBookStore2.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Book> Books { get; set; }
    }
}