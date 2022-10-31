using FlixOne.InventoryManagement.Models;

namespace FlixOne.InventoryManagement.Interfaces
{
    public interface IReadInventoryContext
    {
        Book[] GetBooks();
    }
}