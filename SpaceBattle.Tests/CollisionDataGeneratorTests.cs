using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class CollisionDataGeneratorTests
    {
        private readonly object _scope;
        private class MockCollisionChecker : ICollisionChecker
        {
            public bool IsCollision(int xPosition, int yPosition, int xVelocity, int yVelocity) => true;
        }
        public CollisionDataGeneratorTests()
        {
            new InitCommand().Execute();
            _scope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", _scope).Execute();
        }

        [Fact]
        public void Constructor_CreatesOutputDirectory()
        {
            // Arrange
            var outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            // Act
            _ = new CollisionDataGenerator(10, outputDir);

            // Assert
            Assert.True(Directory.Exists(outputDir));

            // Cleanup
            Directory.Delete(outputDir);
        }

        [Fact]
        public void Execute_GeneratesRequiredNumberOfSamples()
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

            var outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var generator = new CollisionDataGenerator(5, outputDir);

            // Act
            generator.Execute();

            // Assert
            var files = Directory.GetFiles(outputDir);
            Assert.Single(files);
            var lines = File.ReadAllLines(files[0]);
            Assert.Equal(5, lines.Length);

            // Cleanup
            Directory.Delete(outputDir, true);
        }

        [Fact]
        public void GenerateCollisions_OnlySavesCollidingCases()
        {
            // Arrange
            var mockChecker = new Mock<ICollisionChecker>();
            mockChecker.SetupSequence(c => c.IsCollision(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                      .Returns(false)
                      .Returns(false)
                      .Returns(true)
                      .Returns(false)
                      .Returns(true);

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Collision.Checkers",
                (object[] args) => new List<ICollisionChecker> { mockChecker.Object }
            ).Execute();

            var outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var generator = new CollisionDataGenerator(2, outputDir);

            // Act
            generator.Execute();

            // Assert
            var files = Directory.GetFiles(outputDir);
            var lines = File.ReadAllLines(files[0]);
            Assert.Equal(2, lines.Length);

            // Cleanup
            Directory.Delete(outputDir, true);
        }

        [Fact]
        public void RegisterIoCDependencyCollisionGenerator_RegistersGeneratorCorrectly()
        {
            // Arrange
            var registrator = new RegisterIoCDependencyCollisionGenerator();

            // Act
            registrator.Execute();

            // Assert
            var generator = Ioc.Resolve<ICommand>(
                "Collision.Generator",
                10,
                "test_dir"
            );

            Assert.IsType<CollisionDataGenerator>(generator);
        }
    }
}

