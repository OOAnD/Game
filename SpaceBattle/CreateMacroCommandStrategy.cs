using App;

namespace SpaceBattle
{
    public class CreateMacroCommandStrategy(string commandSpec)
    {
        public ICommand Resolve(object[] args)
        {
            var commandNames = Ioc.Resolve<IEnumerable<string>>($"Specs.{commandSpec}");
            var commands = commandNames.Select(name => Ioc.Resolve<ICommand>($"Commands.{name}", args[0])).ToArray();
            return Ioc.Resolve<ICommand>("Commands.Macro", commands);
        }
    }
}
