using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyCollisionCheckerTest
    {
        [Fact]
        public void Execute_ShouldRegisterCollisionCheckersDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var mockPolygon = new List<Point> { new Point(0, 0), new Point(1, 0), new Point(0, 1) };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Shapes.Get",
                (object[] args) => mockPolygon
            ).Execute();

            var mockRayChecker = new Mock<ICollisionChecker>().Object;
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "PolygonCollisionChecker.ByRays",
                (object[] args) => mockRayChecker
            ).Execute();

            var mockSATChecker = new Mock<ICollisionChecker>().Object;
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "PolygonCollisionChecker.BySAT",
                (object[] args) => mockSATChecker
            ).Execute();

            var registrator = new RegisterIoCDependencyCollisionChecker();

            // Act
            registrator.Execute();

            // Assert
            var checkers = Ioc.Resolve<IEnumerable<ICollisionChecker>>(
                "PolygonCollisionCheckers",
                "shape1",
                "shape2"
            );

            Assert.Collection(checkers,
                checker => Assert.Same(mockSATChecker, checker),
                checker => Assert.Same(mockRayChecker, checker)
            );
        }
    }
}
