namespace SpaceBattle
{
    public interface IRotating
    {
        Angle Angle { get; set; }
        Angle Velocity { get; }
    }
}
