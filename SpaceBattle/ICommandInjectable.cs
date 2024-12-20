using App;

namespace SpaceBattle
{
    public interface ICommandInjectable
    {
        void InjectCommand(ICommand command);
    }
}
