namespace SpaceBattle
{
    public class ThrowExceptionCommand : ICommand
    {
        public void Execute() => throw new Exception();
    }
}
