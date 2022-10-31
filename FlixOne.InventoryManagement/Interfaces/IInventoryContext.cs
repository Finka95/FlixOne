using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Models;

namespace FlixOne.InventoryManagement.Interfaces
{
    public interface IInventoryContext : IReadInventoryContext, IWriteInventoryContext
    { 
    }
}
