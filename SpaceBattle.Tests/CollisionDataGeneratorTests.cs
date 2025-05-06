using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class CollisionDataGeneratorTests
    {
        private readonly object _scope;
        private const int TestGridSize = 10;

        public CollisionDataGeneratorTests()
        {
            new InitCommand().Execute();
            _scope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", _scope).Execute();
        }

        [Fact]
        public void GenerateScaledParams_ReturnsGridProportionalValues()
        {
            // Arrange
            var generator = new CollisionDataGenerator(10, TestGridSize);

            // Act
            var (x, y, vx, vy) = generator.GenerateScaledParams();

            // Assert
            Assert.InRange(x, -TestGridSize * 2, TestGridSize * 2);
            Assert.InRange(y, -TestGridSize * 2, TestGridSize * 2);
            Assert.InRange(vx, -TestGridSize / 10, TestGridSize / 10);
            Assert.InRange(vy, -TestGridSize / 10, TestGridSize / 10);
        }

        [Fact]
        public void Execute_GeneratesGridProportionalCollisions()
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

            var generator = new CollisionDataGenerator(5, TestGridSize);

            // Act
            generator.Execute();
            var collisions = generator.GetGeneratedCollisions();

            // Assert
            Assert.Equal(5, collisions.Count());

            foreach (var collision in collisions)
            {
                var parts = collision.Split(',');
                var x = int.Parse(parts[0]);
                var y = int.Parse(parts[1]);
                var vx = int.Parse(parts[2]);
                var vy = int.Parse(parts[3]);

                Assert.InRange(x, -TestGridSize * 2, TestGridSize * 2);
                Assert.InRange(y, -TestGridSize * 2, TestGridSize * 2);
                Assert.InRange(vx, -TestGridSize / 10, TestGridSize / 10);
                Assert.InRange(vy, -TestGridSize / 10, TestGridSize / 10);
            }
        }

        [Fact]
        public void RegisterIoCDependency_RegistersWithGridSize()
        {
            // Arrange
            new RegisterIoCDependencyCollisionGenerator().Execute();

            // Act
            var generator = Ioc.Resolve<ICommand>(
                "Collision.Generator",
                10,
                20
            );

            // Assert
            Assert.IsType<CollisionDataGenerator>(generator);
        }

        [Fact]
        public void Constructor_WithSeed_ProducesDeterministicResults()
        {
            // Arrange
            const int seed = 123;
            var generator1 = new CollisionDataGenerator(10, TestGridSize, seed);
            var generator2 = new CollisionDataGenerator(10, TestGridSize, seed);

            // Act
            var params1 = Enumerable.Range(0, 10).Select(_ => generator1.GenerateScaledParams()).ToList();
            var params2 = Enumerable.Range(0, 10).Select(_ => generator2.GenerateScaledParams()).ToList();

            // Assert
            Assert.Equal(params1, params2);
        }

        [Fact]
        public void GetGeneratedCollisions_ReturnsEmptyList_BeforeExecution()
        {
            // Arrange
            var generator = new CollisionDataGenerator(5, TestGridSize);

            // Act
            var collisions = generator.GetGeneratedCollisions();

            // Assert
            Assert.Empty(collisions);
        }

        [Fact]
        public void Execute_GeneratesCorrectNumberOfSamples_ForMultipleCheckers()
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

            var generator = new CollisionDataGenerator(3, TestGridSize);

            // Act
            generator.Execute();
            var collisions = generator.GetGeneratedCollisions();

            // Assert
            Assert.Equal(6, collisions.Count());
        }
    }
}
