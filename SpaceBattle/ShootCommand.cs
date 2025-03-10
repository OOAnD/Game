using App;

namespace SpaceBattle
{
    public class ShootCommand : ICommand
    {
        private readonly object _playerId;
        private readonly object _gameObjectId;

        public ShootCommand(object playerId, object gameObjectId)
        {
            _playerId = playerId;
            _gameObjectId = gameObjectId;
        }

        public void Execute()
        {
            Ioc.Resolve<ICommand>("Commands.ShootAuth", _playerId, _gameObjectId).Execute();
            var shootingObject = Ioc.Resolve<object>("Game.Objects.Get", _gameObjectId);
            var torpedo = Ioc.Resolve<object>("Game.Objects.Create", "Objects.Torpedo");
            Ioc.Resolve<ICommand>("Configuration.MovingObject.ByMoveConfProvider", torpedo, shootingObject).Execute();
            Ioc.Resolve<ICommand>("Game.Objects.Save", torpedo).Execute();
            Ioc.Resolve<ICommand>("Actions.Start", "Move", torpedo).Execute();
        }
    }
}
