using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Interfaces;
using FlixOne.InventoryManagement.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementTest
{
    [TestClass]
    public class InventoryCommandFunctionTest
    {
        //internal InventoryCommandFunctionTest Factory { get; private set; }
        public ServiceProvider Services { get; private set; }

        [TestInitialize]
        public void Startup()
        {
            var expectedInterface = new Helpers.TestUserInterface(
            new List<Tuple<string, string>>(),
            new List<string>(),
            new List<string>()
            );
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IInventoryContext, InventoryContext>();
            services.AddTransient<Func<string, InventoryCommand>>(InventoryCommand.GetInventoryCommand);
            Services = services.BuildServiceProvider();
        }

        [TestMethod]
        public void QuitCommand_Successful()
        {
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("q"),
            typeof(QuitCommand), "q should be QuitCommand");

            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("quit"),
            typeof(QuitCommand), "quit should be QuitCommand");
        }

        [TestMethod]
        public void HelpCommand_Successful()
        {
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("?"),
            typeof(QuitCommand), "? should be HelpCommand");
        }

        [TestMethod]
        public void UnknownCommand_Successful()
        {
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("add"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("addinventry"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("h"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("help"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
        }

        [TestMethod]
        public void UpdateQuantityCommand_Successful()
        {
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("u"), typeof(UpdateQuantityCommand), "u should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("updatequantity"), typeof(UpdateQuantityCommand), "updatequantity should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(Services.GetService<Func<string, InventoryCommand>>().Invoke("UpdaTEQuantity"), typeof(UpdateQuantityCommand), "UpdaTEQuantity should be UpdateQuantityCommand");
        }
    }
}
