using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyCollisionGenerator : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.Generator",
                (object[] args) => new CollisionDataGenerator(
                    (int)args[0],
                    (int)args[1],
                    args.Length > 2 ? (int?)args[2] : null
                )
            ).Execute();
        }
    }
}
