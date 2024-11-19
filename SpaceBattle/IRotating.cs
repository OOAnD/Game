namespace SpaceBattle{
    public interface IRotating{
        Angle GetAngle();
        Angle GetVelocity();
        void SetAngle(Angle newAngle);
    }

}