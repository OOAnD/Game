namespace SpaceBattle
{
    public class RepeatCommand(ICommandQueue queue, ICommand command) : ICommand
    {
        private readonly ICommandQueue _queue = queue
            ?? throw new ArgumentNullException(nameof(queue));
        private readonly ICommand _command = command
            ?? throw new ArgumentNullException(nameof(command));

        public void Execute() => _queue.Enqueue(_command);
    }
}
