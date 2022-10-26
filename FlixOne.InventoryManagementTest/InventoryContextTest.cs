using FlixOne.InventoryManagement;
using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementTest
{
    [TestClass]
    public class InventoryContextTest
    {
        [TestMethod]
        public void MaintainBooks_Succesful()
        {
            var context = InventoryContext.Singleton;
            List<Task> tasks = new List<Task>();

            // add 30 books
            foreach (var id in Enumerable.Range(1, 30))
            {
                tasks.Add(AddBook($"Book_{id}"));
            }
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();

            //Update quantity
            foreach (var quantity in Enumerable.Range(1, 10))
            {
                foreach (var id in Enumerable.Range(1, 30))
                {
                    tasks.Add(UpdateQuantity($"Book_{id}", quantity));
                }
            }

            // обновим количество книг, отняв 1, 2, 3, 4, 5...
            foreach (var quantity in Enumerable.Range(1, 10))
            {
                foreach (var id in Enumerable.Range(1, 30))
                {
                    tasks.Add(UpdateQuantity($"Book_{id}", -quantity));
                }
            }
            // ждем, пока все сложения и выч
            Task.WaitAll(tasks.ToArray());

            //
            foreach (var book in context.GetBooks())
            {
                Assert.AreEqual(0, book.Quantity);
            }
        }

        public Task AddBook(string book)
        {
            return Task.Run(() =>
            {
                var context = InventoryContext.Singleton;
                Assert.IsTrue(context.AddBook(book));
            });
        }

        public Task UpdateQuantity(string book, int quantity)
        {
            return Task.Run(() =>
            {
                var context = InventoryContext.Singleton;
                Assert.IsTrue(context.UpdateQuantity(book, quantity));
            });
        }
    }
}
