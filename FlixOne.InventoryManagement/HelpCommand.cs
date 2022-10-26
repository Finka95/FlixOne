using FlixOne.InventoryManagement.Interfaces;
using System.Windows.Input;

namespace FlixOne.InventoryManagement;

public class HelpCommand : NonTerminatingCommand
{
    public HelpCommand(IUserInterface userInterface) : base(userInterface)
    {
    }

    internal override bool InternalCommand()
    {
        Interfase.WriteMessage("Thank you for using FlixOne Inventory Management System");
        return true;
    }
}
