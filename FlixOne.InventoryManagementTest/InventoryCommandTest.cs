using FlixOne.InventoryManagement;
using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Models;
using FlixOne.InventoryManagementTest.Helpers;
using NuGet.Frameworks;

namespace FlixOne.InventoryManagementTest;

[TestClass]
public class InventoryCommandTest
{
    [TestMethod]
    public void AddInventoryCommand_Successful()
    {
        const string expectedBookName = "AddINventoryUnitTest";
        var expectedInterface = new Helpers.TestUserInterface(
            new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Enter name:", expectedBookName)
            },
            new List<string>(),
            new List<string>()); 

        var context = new TestInventoryContext(new Dictionary<string, Book>
        {
            { "Gremlins", new Book { Id = 1, Name = "Gremlins", Quantity = 7 } }
        });

        var command = new AddInventoryCommand(expectedInterface, context);

        var result = command.RunCommand();


        Assert.IsFalse(result.shouldQuit, "AddInventory is not a terminating  command.");
        Assert.IsTrue(result.wasSuccessful, "AddInventory did not complite Succesfully.");

        Assert.AreEqual(1, context.GetAddedBooks().Length, "AddInventory should have added one new book.");

        var newBook = context.GetAddedBooks().First();
        Assert.AreEqual(expectedBookName, newBook.Name, "AddInventory did not add book Successfully.");

    }

    [TestMethod]
    public void GetInventoryCommand_Successful()
    {
        var expectedInterface = new Helpers.TestUserInterface(
            new List<Tuple<string, string>>(),
            new List<string>
            {
                    "Gremlins                      \tQuantity:7",
                    "Willowsong                    \tQuantity:3",
            },
            new List<string>()
        );

        var context = new TestInventoryContext(new Dictionary<string, Book>
            {
                { "Gremlins", new Book { Id = 1, Name = "Gremlins", Quantity = 7 } },
                { "Willowsong", new Book { Id = 2, Name = "Willowsong", Quantity = 3 } },
            });

        var command = new GetInventoryCommand(expectedInterface, context);
        var result = command.RunCommand();

        Assert.IsFalse(result.shouldQuit, "GetInventory is not a terminating command.");
        Assert.IsTrue(result.wasSuccessful, "GetInventory did not complete Successfully.");

        Assert.AreEqual(0, context.GetAddedBooks().Length, "GetInventory should not have added any books.");
        Assert.AreEqual(0, context.GetUpdatedBooks().Length, "GetInventory should not have updated any books.");
    }

    [TestMethod]
    public void HelpCommand_succesful()
    {
        var expectedInterface = new Helpers.TestUserInterface(
            new List<Tuple<string, string>>(),
            new List<string>
            {
                    "USAGE:",
                    "\taddinventory (a)",
                    "\tgetinventory (g)",
                    "\tupdatequantity (u)",
                    "\tquit (q)",
                    "\t?",
                    "Examples:",
                    "\tNew Inventory",
                    "\t> addinventory",
                    "\tEnter name:The Meaning of Life",
                    "",
                    "\tGet Inventory",
                    "\t> getinventory",
                    "\tThe Meaning of Life        Quantity:10",
                    "\tThe Life of a Ninja        Quantity:2",
                    "",
                    "\tUpdate Quantity (Increase)",
                    "\t> updatequantity",
                    "\tEnter name:The Meaning of Life",
                    "\t11",
                    "\t11 added to quantity",
                    "",
                    "\tUpdate Quantity (Decrease)",
                    "\t> updatequantity",
                    "\tEnter name:The Life of a Ninja",
                    "\t-3",
                    "\t3 removed from quantity",
                    ""
            },
            new List<string>()
        );

        // create an instance of the command
        var command = new HelpCommand(expectedInterface);

        var result = command.RunCommand();

        Assert.IsFalse(result.shouldQuit, "Help is not a terminating command.");
        Assert.IsTrue(result.wasSuccessful, "Help did not complete Successfully.");
    }

    [TestMethod]
    public void QuitCommand_succesful()
    {
        {
            var expectedInterface = new Helpers.TestUserInterface(
                new List<Tuple<string, string>>(),  // ReadValue()
                new List<string>  // WriteMessage()
                {
                    "Thank you for using FlixOne Inventory Management System"
                },
                new List<string>()  // WriteWarning()
            );

            // create an instance of the command
            var command = new QuitCommand(expectedInterface);

            // add a new book with parameter "name"
            var result = command.RunCommand();

            expectedInterface.Validate();

            Assert.IsTrue(result.shouldQuit, "Quit is a terminating command.");
            Assert.IsTrue(result.wasSuccessful, "Quit did not complete Successfully.");
        }
    }

    [TestMethod]
    public void UpdateCommand_succesful()
    {
        const string expectedBookName = "UpdateQuantityUnitTest";
        var expectedInterface = new Helpers.TestUserInterface(new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Enter name:", expectedBookName),
            new Tuple<string, string>("Enter quantity:", "6")
        },
        new List<string>(),
        new List<string>());

        var context = new Helpers.TestInventoryContext(new Dictionary<string, Book>
        {
            {"Beavers", new Book {Id = 1, Name = "Beavers", Quantity = 3} },
            {expectedBookName, new Book {Id = 2, Name = expectedBookName, Quantity = 7} },
            {"Ducks", new Book {Id = 3, Name = "Ducks", Quantity = 12} }
        });

        var command = new UpdateQuantityCommand(expectedInterface, context);
        var result = command.RunCommand();

        Assert.IsTrue(result.wasSuccessful, "UpdateQuantity did not complite Succesfully.");

        Assert.AreEqual(0, context.GetAddedBooks().Length, "UpdateQuantity Should not have added one new book.");
        var updatedBooks = context.GetUpdatedBooks();
        Assert.AreEqual(1, updatedBooks.Length, "UpdateQuantity Should have updated one new book.");
        Assert.AreEqual(expectedBookName, updatedBooks.First().Name, "UpdateQuantity did not update the corect book.");
        Assert.AreEqual(13, updatedBooks.First().Quantity, "UpdateQuantity did not update book quantity successfully.");
    }
}