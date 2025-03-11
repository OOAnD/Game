using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyCreateGame : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.Create",
                (object[] args) => new Game(args[0])).Execute();
        }
    }
}
