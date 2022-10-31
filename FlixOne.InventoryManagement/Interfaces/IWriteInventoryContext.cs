namespace FlixOne.InventoryManagement.Interfaces
{
    public interface IWriteInventoryContext
    {
        bool AddBook(string name);
        bool UpdateQuantity(string name, int quantity);
    }
}