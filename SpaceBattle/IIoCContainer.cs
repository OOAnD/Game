namespace SpaceBattle
{
    public interface IIoCContainer
    {
        T Resolve<T>(string commandName, IDictionary<string, object> parameters);
    }
}
