using App;

namespace SpaceBattle
{
    public class RegisterIoCDepenedncyAdapterBuilder : ICommand
    {
        private readonly IDictionary<string, Type> _cache = new Dictionary<string, Type>();
        public void Execute()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "Adapter.Instance", (object[] args) =>
            {
                var interfaceType = (Type)args[0];
                var obj = (IDictionary<string, object>)args[1];

                if (_cache.TryGetValue(interfaceType.FullName!, out var value))
                {
                    return value;
                }

                var adapterCode = Ioc.Resolve<string>("Adapters.CodeGenerator", interfaceType);
                var adapterType = Ioc.Resolve<Type>("Adapter.Compile", adapterCode);
                _cache[interfaceType.FullName!] = adapterType;

                var adapter = Activator.CreateInstance(adapterType, obj);

                return adapter;
            }).Execute();
        }
    }
}
