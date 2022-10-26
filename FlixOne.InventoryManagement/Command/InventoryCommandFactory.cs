using FlixOne.InventoryManagement.Interfaces;
using FlixOne.InventoryManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Command
{
    public class InventoryCommandFactory : IInventoryCommandFactory
    {
        private readonly IUserInterface _userInterface;
        private readonly IInventoryContext _context = InventoryContext.Singleton;

        public InventoryCommandFactory(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public InventoryCommand GetCommand(string input)
        {
            switch (input.ToLower())
            {
                case "q":
                case "quit":
                    return new QuitCommand(_userInterface);
                case "a":
                case "addinventory":
                    return new AddInventoryCommand(_userInterface, _context);
                case "g":
                case "getinventory":
                    return new GetInventoryCommand(_userInterface, _context);
                case "u":
                case "updatequantity":
                    return new UpdateQuantityCommand(_userInterface, _context);
                case "?":
                    return new HelpCommand(_userInterface);
                default:
                    return new UnknownCommand(_userInterface);
            }
        }
    }
}
