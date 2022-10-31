using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Command
{
    internal class GetInventoryCommand : NonTerminatingCommand
    {
        private readonly IReadInventoryContext _context;

        internal GetInventoryCommand(IUserInterface userInterface, IReadInventoryContext context) : base(userInterface)
        {
            _context = context;
        }

        internal override bool InternalCommand()
        {
            foreach (var book in _context.GetBooks())
            {
                Interface.WriteMessage($"{book.Name,-30}\tQuantity:{book.Quantity}");
            }
            return true;
        }
    }
}
