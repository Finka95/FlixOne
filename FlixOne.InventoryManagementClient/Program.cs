using System.Globalization;
using FlixOne.InventoryManagement;
using FlixOne.InventoryManagement.Interfaces;

namespace FlixOne.InventoryManagementClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ICatalogService>();
            service.Run();
            Console.WriteLine("CatalogService has completed.");
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Добавляем сервисы приложения
            services.AddTransient<IUserInterface, ConsoleUserInterface>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IInventoryCommandFactory, InventoryCommandFactory>();
        }
    }
}