using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Interfaces;

namespace FlixOne.InventoryManagement.Command
{
    public abstract class InventoryCommand
    {
        private readonly bool _isTerminatingCommand;

        protected IUserInterface Interface { get; }

        protected InventoryCommand(bool commandIsTerminating, IUserInterface userInterface)
        {
            _isTerminatingCommand = commandIsTerminating;
            Interface = userInterface;
        }

        public (bool wasSuccessful, bool shouldQuit) RunCommand()
        {
            if (this is IParameterisedCommand parameterisedCommand)
            {
                var allParametersComplited = false;

                while (allParametersComplited == false)
                {
                    allParametersComplited = parameterisedCommand.GetParameters();
                }
            }

            return (InternalCommand(), _isTerminatingCommand);
        }

        internal abstract bool InternalCommand();

        internal string GetParameter(string parameterName)
        {
            return Interface.ReadValue($"Enter {parameterName}:");
        }
    }
}
