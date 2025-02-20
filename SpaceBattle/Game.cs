using App;

namespace SpaceBattle
{
    public class Game
    {
        private readonly IIoCContainer _iocContainer;

        public Game(IIoCContainer iocContainer)
        {
            _iocContainer = iocContainer ?? throw new ArgumentNullException(nameof(iocContainer));
        }

        public void ProcessOrder(IDictionary<string, object> order)
        {
            _ = order ?? throw new ArgumentNullException(nameof(order));

            try
            {
                var command = _iocContainer.Resolve<ICommand>("Commands.Shoot", order);
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении приказа: {ex.Message}");
                throw;
            }
        }
    }
}
