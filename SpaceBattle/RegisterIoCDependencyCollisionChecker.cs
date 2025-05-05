using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyCollisionChecker : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "PolygonCollisionCheckers", (object[] args) =>
            {
                var shape1 = Ioc.Resolve<IEnumerable<Point>>("Shapes.Get", (string)args[0]);
                var shape2 = Ioc.Resolve<IEnumerable<Point>>("Shapes.Get", (string)args[1]);

                var checkerByRays = Ioc.Resolve<ICollisionChecker>("PolygonCollisionChecker.ByRays", shape1, shape2);
                var checkerBySAT = Ioc.Resolve<ICollisionChecker>("PolygonCollisionChecker.BySAT", shape1, shape2);

                return (IEnumerable<ICollisionChecker>)[checkerBySAT, checkerByRays];
            }).Execute();
        }
    }
}
