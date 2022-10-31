using FlixOne.InventoryManagement.Interfaces;
using FlixOne.InventoryManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FlixOne.InventoryManagementTest
{
    [TestClass]
    public class InventoryContextTest
    {
        private ServiceProvider _serviceProvider;

        [TestInitialize]
        public void Startup()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IInventoryContext, InventoryContext>();
            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public void MaintainBooks_Succesful()
        {
            var context = _serviceProvider.GetService<IInventoryContext>();
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
                var context = _serviceProvider.GetService<IInventoryContext>();
                Assert.IsTrue(context.AddBook(book));
            });
        }

        public Task UpdateQuantity(string book, int quantity)
        {
            return Task.Run(() =>
            {
                var context = _serviceProvider.GetService<IInventoryContext>();
                Assert.IsTrue(context.UpdateQuantity(book, quantity));
            });
        }
    }
}
