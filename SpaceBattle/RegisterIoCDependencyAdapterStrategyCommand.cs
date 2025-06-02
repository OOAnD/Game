using App;

namespace SpaceBattle
{
    public class RegisterIoCDependencyAdapterStrategyCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Strategy",
                (object[] args) => new CustomAdapter(
                    (IDictionary<string, object>)args[0],
                    (IDictionary<string, Func<object>>)args[1]
                )
            ).Execute();
        }
    }
}
