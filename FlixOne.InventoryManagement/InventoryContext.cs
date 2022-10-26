using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement
{
    internal class InventoryContext : IInventoryContext
    {
        private static InventoryContext _context;
        private static object _lock = new object();
        private readonly ConcurrentDictionary<string, Book> _books;

        public static InventoryContext Singleton
        {
            get
            {
                //if( _context == null )
                //{
                    //lock (_lock)
                    //{
                        if(_context == null)
                        {
                            _context = new InventoryContext();
                        }
                    //}
                //}

                return _context;
            }
        }

        public static IInventoryContext Instance { get; internal set; }

        protected InventoryContext()
        {
            _books = new ConcurrentDictionary<string, Book>();
        }
        public bool AddBook(string name)
        {
            lock (_lock)
            {
                _books.TryAdd(name, new Book { Name = name });
            }
            return true;
        }

        public Book[] GetBooks()
        {
            return _books.Values.ToArray();
        }

        public bool UpdateQuantity(string name, int quantity)
        {
            lock(_lock)
            {
                _books[name].Quantity += quantity;
            }
            return true;
        }
    }
}
