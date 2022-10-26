using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Interfaces;

namespace FlixOne.InventoryManagement
{
    public abstract class InventoryCommand
    {
        private readonly bool _isTerminatingCommand;
        private bool commandIsTerminating;

        protected IUserInterface Interfase { get; }

        protected InventoryCommand(bool commandIsTerminating, IUserInterface userInterface)
        {
            _isTerminatingCommand = commandIsTerminating;
            Interfase = userInterface;
        }

        protected InventoryCommand(bool commandIsTerminating)
        {
            this.commandIsTerminating = commandIsTerminating;
        }

        public (bool wasSuccessful, bool shouldQuit) RunCommand()
        {
            if(this is IParameterisedCommand parameterisedCommand)
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
            return Interfase.ReadValue($"Enter {parameterName}:");
        }
    }
}
