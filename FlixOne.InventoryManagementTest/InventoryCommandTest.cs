using FlixOne.InventoryManagement;
using NuGet.Frameworks;

namespace FlixOne.InventoryManagementTest;

[TestClass]
public class InventoryCommandTest
{
    [TestMethod]
    public void AddInventoryCommand_Successful()
    {
        const string expectedBookName = "AddINventoryUnitTest";
        var expectedInterface = new TestUserInterface(
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
        var expectedInterface = new TestUserInterface(
            new List<Tuple<string, string>>(),
            new List<string>
            {
                "Gremlins               \tQuantity:7",
                "Willowsong             \tQuantity:3",
            },
            new List<string>());

        var context = new TestInventoryContext(new Dictionary<string, Book>
        {
            { "Gremlins", new Book { Id = 1, Name = "Gremlins", Quantity = 7 } },
            { "Willowsong", new Book { Id = 2, Name = "Willowsong", Quantity = 3 } }
        });

        var command = new GetInventoryCommand(expectedInterface, context);
        var result = command.RunCommand();

        Assert.IsFalse(result.shouldQuit, "GetInventory is not a terminating command.");

        Assert.AreEqual(0, context.GetAddedBooks().Length, "GetInventory should Not have added any books.");
        Assert.AreEqual(0, context.GetUpdatedBooks().Length, "GetInventory should have updated any books.");

    }

    [TestMethod]
    public void HelpCommand_succesful()
    {
        //������� ��������� �������
        // ��������� ����� ����� � ���������� "name"
        // ��������� ���� �� ������ ����� � ������ ��������� � ���������� 0

        Assert.Inconclusive("HelpCommand_Succesful has not been implemented.");

    }

    [TestMethod]
    public void QuitCommand_succesful()
    {
        Assert.Inconclusive("QuitCommand_Succesful has not been implemented.");

    }

    [TestMethod]
    public void UpdateCommand_succesful()
    {
        const string expectedBookName = "UpdateQuantityUnitTest";
        var expectedInterface = new TestUserInterface(new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Enter name:", expectedBookName),
            new Tuple<string, string>("Enter quantity:", "6")
        },
        new List<string>(),
        new List<string>());

        var context = new TestInventoryContext(new Dictionary<string, Book>
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