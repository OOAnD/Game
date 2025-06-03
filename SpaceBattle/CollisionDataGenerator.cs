using App;

namespace SpaceBattle
{
    public class CollisionDataGenerator
    {
        private readonly int _requiredSamples;
        private readonly Random _random;
        private readonly int _gridSize;

        public int GridSize => _gridSize;
        public int RequiredSamples => _requiredSamples;

        public CollisionDataGenerator(int samples, int gridSize, int? seed = null)
        {
            _requiredSamples = samples;
            _gridSize = gridSize;
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public IEnumerable<(int x, int y, int vx, int vy)> Generate()
        {
            return Ioc.Resolve<IEnumerable<ICollisionChecker>>("Collision.Checkers")
                .SelectMany(checker =>
                    Enumerable.Repeat(0, int.MaxValue)
                        .Select(_ => GenerateScaledParams())
                        .Where(p => checker.IsCollision(p.x, p.y, p.vx, p.vy))
                        .Take(_requiredSamples)
                );
        }

        public (int x, int y, int vx, int vy) GenerateScaledParams()
        {
            var positionRange = _gridSize * 2;
            var velocityRange = Math.Max(1, _gridSize / 10);

            return (
                _random.Next(-positionRange, positionRange + 1),
                _random.Next(-positionRange, positionRange + 1),
                _random.Next(-velocityRange, velocityRange + 1),
                _random.Next(-velocityRange, velocityRange + 1)
            );
        }
    }
}
