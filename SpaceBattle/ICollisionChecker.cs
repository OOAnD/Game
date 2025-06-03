namespace SpaceBattle
{
    public interface ICollisionChecker
    {
        bool IsCollision(int xPosition, int yPosition, int xVelocity, int yVelocity);
    }
}
