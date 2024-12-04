namespace SpaceBattle
{

    public class MacroCommand : ICommand
    {
        private readonly IEnumerable<ICommand> _commands;

        public MacroCommand(params ICommand[] commands)
        {
            if (commands.Any(c => c == null))
            {
                throw new ArgumentNullException(nameof(commands), "Команды не могут быть null.");
            }

            _commands = [.. commands];
        }

        public void Execute() => _commands.ToList().ForEach(c => c.Execute());
    }
}
