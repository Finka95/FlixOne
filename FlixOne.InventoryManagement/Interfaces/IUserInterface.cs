using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixOne.InventoryManagement.Interfaces
{
    public interface IUserInterface
    {
        public string ReadValue(string message);
        //message to console
        public void WriteMessage(string message);
        // write alarm to console
        public void WriteWarning(string message);
    }
}
