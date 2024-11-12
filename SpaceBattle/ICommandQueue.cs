namespace SpaceBattle{
    public interface ICommandQueue{
        void Put(ICommand command);
    }
}