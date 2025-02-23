using App;

namespace SpaceBattle
{
    public class AuthCommand : ICommand
    {
        public string _playerId;
        public string _objectId;
        public AuthCommand(string playerId, string objectId)
        {
            _playerId = playerId;
            _objectId = objectId;
        }

        public void Execute()
        {
            var gameItems = Ioc.Resolve<IEnumerable<string>>("Players.GetBelongings", _playerId);
            var item = gameItems.FirstOrDefault(item => item == _objectId) ?? throw new Exception();
        }
    }
}
