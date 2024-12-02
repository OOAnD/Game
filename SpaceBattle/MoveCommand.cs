namespace SpaceBattle
{
    public class MoveCommand : ICommand
    {
        private readonly IMoving _movingObject;

        public MoveCommand(IMoving movingObject)
        {
            _movingObject = movingObject ?? throw new ArgumentNullException(nameof(movingObject));
        }

        public void Execute()
        {
            _ = _movingObject.Position;
            var velocity = _movingObject.Velocity;
            _movingObject.Position += velocity;
        }
    }
}
