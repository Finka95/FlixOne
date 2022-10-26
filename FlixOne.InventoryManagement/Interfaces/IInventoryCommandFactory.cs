using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Command;

namespace FlixOne.InventoryManagement.Interfaces
{
    public interface IInventoryCommandFactory
    {
        InventoryCommand GetCommand(string input);
    }
}
