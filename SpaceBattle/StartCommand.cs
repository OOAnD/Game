namespace SpaceBattle
{
    public class StartCommand : ICommand
    {

        private IUtilCommandFactory _factory;
        private ICommand _command;
        private ICommandQueue _commandQueue;

        public StartCommand(IUtilCommandFactory factory, ICommand command, ICommandQueue commandQueue)
        {
            _factory = factory;
            _command = command;
            _commandQueue = commandQueue;
        }

        public void Execute()
        {
            var commandBox = _factory.CreateCommandBox();
            var macroCommand = _factory.CreateMacroCommand();
            var commandSetToQueue = _factory.CreateQueueSetter(commandBox);
            macroCommand.Put(_command);
            macroCommand.Put(commandSetToQueue);
            commandBox.Set(macroCommand);
            _commandQueue.Put(commandBox);
        }
    }
}