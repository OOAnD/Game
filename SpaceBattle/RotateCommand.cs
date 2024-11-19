namespace SpaceBattle{
    public class RotateCommand : ICommand{
        public IRotating _rotatingObject;
        public RotateCommand(IRotating rotatingObject){
            _rotatingObject = rotatingObject;
        }
        public void Execute(){
            var currentAngle = _rotatingObject.GetAngle();
            var velocity = _rotatingObject.GetVelocity();
            var newAngle = currentAngle.Sum(velocity);
            _rotatingObject.SetAngle(newAngle);
        }
    }

}