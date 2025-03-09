namespace SpaceBattle
{
    public class PlayersShipFinder(IDictionary<string, string> ships)
    {
        private readonly IDictionary<string, string> _ships = ships ?? throw new ArgumentNullException(nameof(ships));
        public IEnumerable<string> Execute(string? userId)
        {
            return [.. _ships.Where(ship => ship.Value == (userId ?? throw new ArgumentNullException(nameof(userId), "Идентификатор пользователя не может быть null."))).Select(ship => ship.Key)];
        }
    }
}
