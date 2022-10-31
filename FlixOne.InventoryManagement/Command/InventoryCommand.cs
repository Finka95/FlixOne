using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlixOne.InventoryManagement.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlixOne.InventoryManagement.Command
{
    public abstract class InventoryCommand
    {
        private readonly bool _isTerminatingCommand;
        protected IUserInterface Interface { get; }
        public static Func<IServiceProvider, Func<string, InventoryCommand>> GetInventoryCommand =>
            provider => input =>
            {
                switch (input.ToLower())
                {
                    case "q":
                    case "quit":
                        return new QuitCommand(provider.GetService<IUserInterface>());
                    case "a":
                    case "addinventory":
                        return new AddInventoryCommand(provider.GetService<IUserInterface>(),
                        provider.GetService<IWriteInventoryContext>());
                    case "g":
                    case "getinventory":
                        return new GetInventoryCommand(provider.GetService<IUserInterface>(),
                        provider.GetService<IReadInventoryContext>());
                    case "u":
                    case "updatequantity":
                        return new UpdateQuantityCommand(provider.GetService<IUserInterface>(),
                        provider.GetService<IWriteInventoryContext>());
                    case "?":
                        return new HelpCommand(provider.GetService<IUserInterface>());
                    default:
                        return new UnknownCommand(provider.GetService<IUserInterface>());
                }
            };

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
