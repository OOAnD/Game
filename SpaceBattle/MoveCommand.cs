using App;
namespace SpaceBattle
{
    public class MoveCommand : ICommand
    {
        private readonly IMoving _movingObject;

        public MoveCommand(IMoving movingObject)
        {
            _movingObject = movingObject;
        }

        public void Execute()
        {
            var velocity = _movingObject.Velocity;
            _movingObject.Position += velocity;
        }
    }
}
