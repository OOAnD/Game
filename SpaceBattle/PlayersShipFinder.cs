using App;

namespace SpaceBattle
{
    public class PlayersShipFindStrategy(string playerId)
    {
        public IEnumerable<object> Find()
        {
            var players = Ioc.Resolve<IDictionary<string, IDictionary<string, object>>>("Game.Players.GetAll");
            var player = players[playerId];

            return (IEnumerable<object>)player["ownShips"];
        }
    }
}
