using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement
{
    public abstract class NonTerminatingCommand : InventoryCommand
    {
        private IUserInterface userInterface;

        protected NonTerminatingCommand() : base(commandIsTerminating: false) { }

        protected NonTerminatingCommand(IUserInterface userInterface)
            :base(commandIsTerminating: false)
        {
            this.userInterface = userInterface;
        }
    }
}
