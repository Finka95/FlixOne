using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Command
{
    internal class UnknownCommand : NonTerminatingCommand
    {
        public UnknownCommand(IUserInterface userInterface) : base(userInterface)
        {
        }

        internal override bool InternalCommand()
        {
            Interface.WriteWarning("Unable t odetermine the dessired command.");
            return false;
        }
    }
}
