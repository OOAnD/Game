using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyRayCollisionChecker : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "PolygonCollisionChecker.ByRays", (object[] args) =>
            {
                return new PolygonCollisionCheckerByRays((IEnumerable<Point>)args[0], (IEnumerable<Point>)args[1]);
            }).Execute();
        }
    }
}
