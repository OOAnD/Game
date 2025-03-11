using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyNextCommand : ICommand
    {
        public void Execute()
        {
            var q = Ioc.Resolve<IQueue>("Game.Queue");
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.GetNextCommand",
                (object[] _) => q.Take()).Execute();
        }
    }
}
