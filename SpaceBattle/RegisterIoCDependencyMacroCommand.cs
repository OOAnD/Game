using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyMacroCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.Macro",
                (object[] args) => new MacroCommand(Array.ConvertAll(args, o => (ICommand)o))
            ).Execute();
        }
    }
}
