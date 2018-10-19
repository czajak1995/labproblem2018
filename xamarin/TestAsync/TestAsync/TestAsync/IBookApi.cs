using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestAsync
{
   
        [Headers("Content-Type: application/json")]
        public interface IBookApi
        {
        [Get("/api/books")]
        Task<List<Book>> GetItems();

        //[Post("/item")]
        [Post("/api/books")]
        Task PostItem([Body(BodySerializationMethod.Json)] Dictionary<string, object> data);
       

        }
    
}
