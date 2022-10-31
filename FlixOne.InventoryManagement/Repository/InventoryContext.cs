using FlixOne.InventoryManagement.Interfaces;
using FlixOne.InventoryManagement.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Repository
{
    public class InventoryContext : IInventoryContext
    {
        private static object _lock = new object();
        private readonly IDictionary<string,Book> _books;

        public InventoryContext()
        {
            _books = new ConcurrentDictionary<string, Book>();
        }
        public bool AddBook(string name)
        {

            _books.Add(name, new Book { Name = name });
            return true;
        }

        public Book[] GetBooks()
        {
            return _books.Values.ToArray();
        }

        public bool UpdateQuantity(string name, int quantity)
        {
            lock (_lock)
            {
                _books[name].Quantity += quantity;
            }
            return true;
        }
    }
}
