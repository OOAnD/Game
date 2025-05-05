using App;

namespace SpaceBattle
{
    public class CollisionDataGenerator : ICommand
    {
        private readonly int _requiredSamples;
        private readonly string _outputDirectory;
        private readonly Random _random;

        public CollisionDataGenerator(int samples, string outputDir, int? seed = null)
        {
            _requiredSamples = samples;
            _outputDirectory = outputDir;
            _random = seed.HasValue ? new Random(seed.Value) : new Random();

            Directory.CreateDirectory(outputDir);
        }

        public void Execute()
        {
            var checkers = Ioc.Resolve<IEnumerable<ICollisionChecker>>("Collision.Checkers");

            foreach (var checker in checkers)
            {
                GenerateCollisions(checker);
            }
        }

        private void GenerateCollisions(ICollisionChecker checker)
        {
            var collisions = new List<string>();
            while (collisions.Count < _requiredSamples)
            {
                var (x, y, vx, vy) = GenerateRandomParams();
                if (checker.IsCollision(x, y, vx, vy))
                {
                    collisions.Add($"{x},{y},{vx},{vy}");
                }
            }

            SaveToFile(checker.GetType().Name, collisions);
        }

        public (int x, int y, int vx, int vy) GenerateRandomParams()
        {
            return (
                _random.Next(-500, 500),
                _random.Next(-500, 500),
                _random.Next(-50, 50),
                _random.Next(-50, 50)
            );
        }

        private void SaveToFile(string checkerName, IEnumerable<string> data)
        {
            var filePath = Path.Combine(_outputDirectory, $"{checkerName}.collisions");
            File.WriteAllLines(filePath, data);
        }
    }
}
