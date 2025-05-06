using App;

namespace SpaceBattle
{
    public class CollisionDataGenerator : ICommand
    {
        private readonly int _requiredSamples;
        private readonly Random _random;
        private readonly int _gridSize;
        private readonly List<string> _generatedCollisions;

        public CollisionDataGenerator(int samples, int gridSize, int? seed = null)
        {
            _requiredSamples = samples;
            _gridSize = gridSize;
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
            _generatedCollisions = new List<string>();
        }

        public void Execute()
        {
            var checkers = Ioc.Resolve<IEnumerable<ICollisionChecker>>("Collision.Checkers");
            _generatedCollisions.Clear();

            foreach (var checker in checkers)
            {
                GenerateCollisions(checker);
            }
        }

        private void GenerateCollisions(ICollisionChecker checker)
        {
            var generated = 0;
            while (generated < _requiredSamples)
            {
                var (x, y, vx, vy) = GenerateScaledParams();
                if (checker.IsCollision(x, y, vx, vy))
                {
                    _generatedCollisions.Add($"{x},{y},{vx},{vy}");
                    generated++;
                }
            }
        }

        public (int x, int y, int vx, int vy) GenerateScaledParams()
        {
            var positionRange = _gridSize * 2;
            var velocityRange = _gridSize / 10;

            return (
                _random.Next(-positionRange, positionRange + 1),
                _random.Next(-positionRange, positionRange + 1),
                _random.Next(-velocityRange, velocityRange + 1),
                _random.Next(-velocityRange, velocityRange + 1)
            );
        }
        public IEnumerable<string> GetGeneratedCollisions() => _generatedCollisions;
    }
}
