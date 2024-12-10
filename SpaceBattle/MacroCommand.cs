
namespace SpaceBattle
{
    public class MacroCommand(params ICommand[] commands) : ICommand
    {
        private readonly IEnumerable<ICommand> _commands = commands;

        public void Execute() => _commands.ToList().ForEach(c => c.Execute());
    }
}
