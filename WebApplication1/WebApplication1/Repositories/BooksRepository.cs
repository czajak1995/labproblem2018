using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Repositories
{
    public class BooksRepository
    {
        private WebApplication1Context db = new WebApplication1Context();
        public Task Add(Book book)
        {
            db.Books.Add(book);
            return db.SaveChangesAsync();
        }

        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }
    }
}