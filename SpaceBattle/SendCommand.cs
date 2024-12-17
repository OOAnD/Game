namespace SpaceBattle
{
    public class SendCommand(ICommandReciever reciever, ICommand command) : ICommand
    {
        public void Execute() => reciever.RecieveCommand(command);
    }
}
