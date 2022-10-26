namespace FlixOne.InventoryManagement.Interfaces
{
    public class CatalogService : ICatalogService
    {
        private readonly IUserInterface _userInterface;
        private readonly IInventoryCommandFactory _commandFactory;
        public CatalogService(IUserInterface userInterface, IInventoryCommandFactory commandFactory)
        {
            _userInterface = userInterface;
            _commandFactory = commandFactory;
        }

        public void Run()
        {
            Greeting();
            var response = _commandFactory.GetCommand("?").RunCommand();
            while (!response.shouldQuit)
            {
                // посмотрите на ошибку с ToLower()
                var input = _userInterface.ReadValue("> ").ToLower();
                var command = _commandFactory.GetCommand(input);
                response = command.RunCommand();
                if (!response.wasSuccessful)
                {
                    _userInterface.WriteMessage("Enter ? to view options.");
                }
            }
        }

        private void Greeting()
        {
            _userInterface.WriteMessage("Hello!!");
        }
    }
}
