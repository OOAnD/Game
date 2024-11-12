namespace SpaceBattle
{
    public class StartCommand : ICommand
    {
        private readonly IUtilCommandFactory _factory;
        private readonly ICommand _command;
        private readonly ICommandQueue _commandQueue;

        public StartCommand(IUtilCommandFactory factory, ICommand command, ICommandQueue commandQueue)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _commandQueue = commandQueue ?? throw new ArgumentNullException(nameof(commandQueue));
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
