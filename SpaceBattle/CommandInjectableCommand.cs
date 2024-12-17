namespace SpaceBattle
{
    public class CommandInjectableCommand : ICommand, ICommandInjectable
    {
        private ICommand _command = new ThrowExceptionCommand();
        public void Execute()
        {
            _command.Execute();
        }

        public void InjectCommand(ICommand command)
        {
            _command = command;
        }
    }
}
