using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyCollisionCheckerBySat : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "PolygonCollisionChecker.BySAT", (object[] args) =>
            {
                return new PolygonCollisionCheckerBySAT((IEnumerable<Point>)args[0], (IEnumerable<Point>)args[1]);
            }).Execute();
        }
    }
}
