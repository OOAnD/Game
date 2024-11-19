namespace SpaceBattle
{
    public class MoveCommand
    {
        private readonly IMoving _movingObject;

        public MoveCommand(IMoving movingObject)
        {
            _movingObject = movingObject ?? throw new ArgumentNullException(nameof(movingObject));
        }

        public void Execute()
        {
            try
            {
                var position = _movingObject.Position;
                var velocity = _movingObject.Velocity;

                if (position.Dimension != velocity.Dimension)
                {
                    throw new ArgumentException("Position and velocity must have the same dimension.");
                }

                _movingObject.Position += velocity;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Move operation failed.", ex);
            }
        }
    }
}
