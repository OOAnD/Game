namespace SpaceBattle
{
    public interface ICollisionDataExporter
    {
        void Export(string checkerType, IEnumerable<string> collisionData);
    }
}
