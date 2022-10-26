using FlixOne.InventoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagementTest
{
    [TestClass]
    public class InventoryCommandFactoryTest
    {
        internal InventoryCommandFactory Factory { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            var expectedInterface = new TestUserInterface(
                 new List<Tuple<string, string>>(),
                 new List<string>(),
                 new List<string>()
                 );
            Factory = new InventoryCommandFactory(expectedInterface);
        }

        [TestMethod]
        public void QuitCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("q"), typeof(QuitCommand),
            "q should be QuitCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("quit"), typeof(QuitCommand),
            "quit should be QuitCommand");
        }

        [TestMethod]
        public void HelpCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("?"), typeof(HelpCommand),
            "h should be HelpCommand");
        }

        [TestMethod]
        public void UnknownCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("add"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("addinventry"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("h"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("help"), typeof(UnknownCommand), "unmatched command should be UnknownCommand");
        }

        [TestMethod]
        public void UpdateQuantityCommand_Successful()
        {
            Assert.IsInstanceOfType(Factory.GetCommand("u"), typeof(UpdateQuantityCommand), "u should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("updatequantity"), typeof(UpdateQuantityCommand), "updatequantity should be UpdateQuantityCommand");
            Assert.IsInstanceOfType(Factory.GetCommand("UpdaTEQuantity"), typeof(UpdateQuantityCommand), "UpdaTEQuantity should be UpdateQuantityCommand");
        }
    }
}
