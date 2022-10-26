using FlixOne.InventoryManagement;
using FlixOne.InventoryManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementTest
{
    [TestClass]
    public class UnknownCommandTests
    {
        [TestMethod]
        public void UnknownCommand_Successfull()
        {
            var expectedInterface = new Helpers.TestUserInterface(
                new List<Tuple<string, string>>(),
                new List<string>(),
                new List<string>
                {
                    "Unable t odetermine the dessired command."
                }
                );

            var command = new UnknownCommand(expectedInterface);
            var result = command.RunCommand();

            Assert.IsFalse(result.shouldQuit, "Unknown is not a terminating command.");
            Assert.IsFalse(result.wasSuccessful, "Unknown should not complete Successfully.");
        }
    }
}
