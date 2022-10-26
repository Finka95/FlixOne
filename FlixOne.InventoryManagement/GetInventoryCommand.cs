using FlixOne.InventoryManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement
{
    public class GetInventoryCommand : NonTerminatingCommand
    {
        private readonly IInventoryContext _context;

        internal GetInventoryCommand(IUserInterface userInterface, IInventoryContext context) : base(userInterface)
        {
            _context = context;
        }

        internal override bool InternalCommand()
        {
            foreach (var book in _context.GetBooks())
            {
                Interfase.WriteMessage($"{book.Name,-30}\tQuantity:{book.Quantity}");
            }
            return true;
        }
    }
}
