using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Interfaces;

namespace FlixOne.InventoryManagement.Command
{
    internal class AddInventoryCommand : NonTerminatingCommand, IParameterisedCommand
    {
        private readonly IInventoryContext _context;
        public string InventoryName { get; set; }

        public AddInventoryCommand(IUserInterface userInterface, IInventoryContext context) : base(userInterface)
        {
            _context = context;
        }

        /// <summary>
        /// AddInventoryCommand
        /// </summary>
        /// <returns></returns>
        public bool GetParameters()
        {
            if (string.IsNullOrWhiteSpace(InventoryName))
                InventoryName = GetParameter("name");
            return !string.IsNullOrWhiteSpace(InventoryName);
        }
        internal override bool InternalCommand()
        {
            return _context.AddBook(InventoryName);
        }
    }
}
