using App;

namespace SpaceBattle
{
    public class RegisterGameDependenciesCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Strategies.Create.Torpedo",
                (object[] _) => CreateTorpedoStrategy.Create()
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.GameObject.Save",
                (object[] args) => new SaveGameObjectCommand((IDictionary<string, object>)args[0])
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Commands.GameObject.Remove",
                (object[] args) => new RemoveGameObject((IDictionary<string, object>)args[0])
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Strategies.Find.GameObject",
                (object[] args) => new FindGameObjectStrategy((string)args[0])
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Strategies.Find.PlayerShips",
                (object[] args) => new PlayersShipFindStrategy((string)args[0])
            ).Execute();
        }
    }
}
