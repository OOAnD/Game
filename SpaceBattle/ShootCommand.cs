using App;

namespace SpaceBattle
{
    public class ShootCommand : ICommand
    {
        private readonly object _gameObjectId;

        public ShootCommand(object gameObjectId)
        {
            _gameObjectId = gameObjectId;
        }

        public void Execute()
        {
            var shootingObject = Ioc.Resolve<object>("Game.Objects.Get", _gameObjectId);
            var torpedo = Ioc.Resolve<object>("Game.Objects.Torpedo.Resolve");
            Ioc.Resolve<ICommand>("Configuration.MovingObject.ByMoveConfProvider", torpedo, shootingObject).Execute();
            Ioc.Resolve<ICommand>("Actions.Start", "Move", torpedo).Execute();
        }
    }
}
