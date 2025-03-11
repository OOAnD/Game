using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyShouldLoopRun : ICommand
    {
        public void Execute()
        {
            var queueCount = Ioc.Resolve<IQueueCount>("Game.Queue");
            var gameLoop = new GameLoop(queueCount);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Game.ShouldLoopRun",
                (object[] _) => (object)gameLoop.ShouldLoopRun()).Execute();
        }
    }
}
