using System.Globalization;
using FlixOne.InventoryManagement;
using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Interfaces;
using FlixOne.InventoryManagement.Repository;
using Microsoft.Extensions.DependencyInjection;

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
            Console.ReadLine();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IUserInterface, ConsoleUserInterface>();
            services.AddTransient<ICatalogService, CatalogService>();

            // command usign a function
            services.AddTransient<Func<string, InventoryCommand>>(InventoryCommand.GetInventoryCommand);

            var context = new InventoryContext();
            services.AddSingleton<IReadInventoryContext, InventoryContext>(p => context);
            services.AddSingleton<IWriteInventoryContext, InventoryContext>(p => context);
            services.AddSingleton<IInventoryContext, InventoryContext>(p => context);
        }
    }
}