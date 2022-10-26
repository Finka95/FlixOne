using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Command
{
    internal class QuitCommand : InventoryCommand
    {
        internal QuitCommand(IUserInterface userInterface)
            : base(commandIsTerminating: true, userInterface: userInterface) { }

        internal override bool InternalCommand()
        {
            Interface.WriteMessage("Thank you for using FlixOne Inventory Management System");
            return true;
        }
    }
}
