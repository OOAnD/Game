using App;

namespace SpaceBattle
{
    public class RotateCommand : ICommand
    {
        public IRotating _rotatingObject;

        public RotateCommand(IRotating rotatingObject)
        {
            _rotatingObject = rotatingObject;
        }

        public void Execute()
        {
            var currentAngle = _rotatingObject.Angle;
            var velocity = _rotatingObject.Velocity;
            var newAngle = currentAngle + velocity;
            _rotatingObject.Angle = newAngle;
        }
    }
}
