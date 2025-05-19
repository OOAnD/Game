using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class CollisionDataGeneratorTests
    {
        private readonly object _scope;

        public CollisionDataGeneratorTests()
        {
            new InitCommand().Execute();
            _scope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", _scope).Execute();
        }

        [Fact]
        public void GenerateScaledParams_ReturnsValuesWithinGridProportions()
        {
            // Arrange
            var gridSize = 10;
            var generator = new CollisionDataGenerator(1, gridSize);

            // Act
            var (x, y, vx, vy) = generator.GenerateScaledParams();

            // Assert
            Assert.InRange(x, -2 * gridSize, 2 * gridSize);
            Assert.InRange(y, -2 * gridSize, 2 * gridSize);
            var velocityRange = Math.Max(1, gridSize / 10);
            Assert.InRange(vx, -velocityRange, velocityRange);
            Assert.InRange(vy, -velocityRange, velocityRange);
        }

        [Fact]
        public void Generate_ReturnsCorrectNumberOfSamplesPerChecker()
        {
            // Arrange
            var mockChecker = new Mock<ICollisionChecker>();
            mockChecker.Setup(c => c.IsCollision(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                       .Returns(true);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.Checkers",
                (object[] args) => new List<ICollisionChecker> { mockChecker.Object }
            ).Execute();

            var samples = 5;
            var generator = new CollisionDataGenerator(samples, 10);

            // Act
            var results = generator.Generate().Take(samples).ToList();

            // Assert
            Assert.Equal(samples, results.Count);
        }

        [Fact]
        public void Generate_RespectsMultipleCheckers()
        {
            // Arrange
            var mockChecker1 = new Mock<ICollisionChecker>();
            mockChecker1.Setup(c => c.IsCollision(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                       .Returns(true);
            var mockChecker2 = new Mock<ICollisionChecker>();
            mockChecker2.Setup(c => c.IsCollision(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                       .Returns(true);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.Checkers",
                (object[] args) => new List<ICollisionChecker> { mockChecker1.Object, mockChecker2.Object }
            ).Execute();

            var samples = 3;
            var generator = new CollisionDataGenerator(samples, 10);

            // Act
            var results = generator.Generate().Take(2 * samples).ToList();

            // Assert
            Assert.Equal(2 * samples, results.Count);
        }

        [Fact]
        public void Constructor_WithSeed_GeneratesDeterministicSequence()
        {
            // Arrange
            const int seed = 123;
            var generator1 = new CollisionDataGenerator(10, 10, seed);
            var generator2 = new CollisionDataGenerator(10, 10, seed);

            // Act
            var sequence1 = Enumerable.Range(0, 10).Select(_ => generator1.GenerateScaledParams()).ToList();
            var sequence2 = Enumerable.Range(0, 10).Select(_ => generator2.GenerateScaledParams()).ToList();

            // Assert
            Assert.Equal(sequence1, sequence2);
        }

        [Fact]
        public void Generate_FiltersNonCollidingParams()
        {
            // Arrange
            var mockChecker = new Mock<ICollisionChecker>();
            mockChecker.Setup(c => c.IsCollision(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                       .Returns<int, int, int, int>((x, y, vx, vy) => x % 2 == 0);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.Checkers",
                (object[] args) => new List<ICollisionChecker> { mockChecker.Object }
            ).Execute();

            var generator = new CollisionDataGenerator(5, 10);

            // Act
            var results = generator.Generate().ToList();

            // Assert
            Assert.All(results, param => Assert.True(param.x % 2 == 0));
        }

        [Fact]
        public void Generate_ReturnsEmptyWhenNoCheckersRegistered()
        {
            // Arrange
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.Checkers",
                (object[] args) => new List<ICollisionChecker>()
            ).Execute();

            var generator = new CollisionDataGenerator(5, 10);

            // Act
            var results = generator.Generate().ToList();

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void RegisterIoCDependency_SuccessfullyRegistersGenerator()
        {
            // Arrange
            new RegisterIoCDependencyCollisionGenerator().Execute();

            // Act
            var generator = Ioc.Resolve<CollisionDataGenerator>(
                "Collision.Generator",
                5,
                10
            );

            // Assert
            Assert.NotNull(generator);
            Assert.Equal(5, generator.RequiredSamples);
            Assert.Equal(10, generator.GridSize);
        }

        [Fact]
        public void RegisterGenerator_HandlesOptionalSeedParameter_WhenNotProvided()
        {
            // Arrange
            new RegisterIoCDependencyCollisionGenerator().Execute();

            // Act
            var generator = Ioc.Resolve<CollisionDataGenerator>(
                "Collision.Generator",
                5,
                10
            );

            // Assert
            Assert.NotNull(generator);
        }

        [Fact]
        public void RegisterGenerator_HandlesOptionalSeedParameter_WhenProvided()
        {
            // Arrange
            new RegisterIoCDependencyCollisionGenerator().Execute();
            const int testSeed = 123;

            // Act
            var generator = Ioc.Resolve<CollisionDataGenerator>(
                "Collision.Generator",
                5,
                10,
                testSeed
            );

            // Assert
            Assert.NotNull(generator);
        }
    }
}
