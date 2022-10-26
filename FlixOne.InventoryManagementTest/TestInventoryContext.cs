using FlixOne.InventoryManagement;
using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementTest
{
    public class TestInventoryContext : IInventoryContext
    {
        private readonly IDictionary<string, Book> _seedDictionary;
        private readonly IDictionary<string, Book> _books;

        public TestInventoryContext(IDictionary<string, Book> books)
        {
            _seedDictionary = books.ToDictionary(book => book.Key,
                book => new Book
                {
                    Id = book.Value.Id,
                    Name = book.Value.Name,
                    Quantity = book.Value.Quantity
                });
            _books = books;
        }

        public bool AddBook(string name)
        {
            _books.Add(name, new Book() { Name = name });
            return true;
        }

        public Book[] GetBooks()
        {
            return _books.Values.ToArray();
        }

        public bool UpdateQuantity(string name, int quantity)
        {
            _books[name].Quantity += quantity;
            return true;
        }

        public Book[] GetAddedBooks()
        {
            return _books.Where(book => !_seedDictionary.ContainsKey(book.Key))
                .Select(book => book.Value).ToArray();
        }
        public Book[] GetUpdatedBooks()
        {
            return _books.Where(book => _seedDictionary[book.Key].Quantity != book.Value.Quantity)
                .Select(book => book.Value).ToArray();
        }
    }
}
