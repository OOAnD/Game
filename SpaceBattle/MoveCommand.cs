using App;
namespace SpaceBattle
{
    public class MoveCommand(IMoving movingObject) : ICommand
    {
        private readonly IMoving _movingObject = movingObject;

        public void Execute()
        {
            var velocity = _movingObject.Velocity;
            _movingObject.Position += velocity;
        }
    }
}
