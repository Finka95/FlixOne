using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Interfaces;

namespace FlixOne.InventoryManagement
{
    internal class UpdateQuantityCommand : NonTerminatingCommand, IParameterisedCommand
    {
        private readonly IInventoryContext _context;
        public string InventoryName { get; set; }
        private int _quantity;
        internal int Quantity { get => _quantity; private set => _quantity = value; }

        public UpdateQuantityCommand(IUserInterface userInterface, IInventoryContext context) : base(userInterface)
        {
            _context = context;
        }
        public bool GetParameters()
        {
            if (string.IsNullOrWhiteSpace(InventoryName))
                InventoryName = GetParameter("name");

            if (Quantity == 0)
                int.TryParse(GetParameter("quantity"), out _quantity);

            return !string.IsNullOrWhiteSpace(InventoryName) && _quantity != 0;
        }

        internal override bool InternalCommand()
        {
            return _context.UpdateQuantity(InventoryName, Quantity);
        }
    }
}
