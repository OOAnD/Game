using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyRayCollisionCheckerTest
    {
        [Fact]
        public void Execute_ShouldRegisterRayCollisionCheckerDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var polygon1 = new List<Point> { new Point(0, 0), new Point(1, 0), new Point(1, 1) };
            var polygon2 = new List<Point> { new Point(2, 2), new Point(3, 2), new Point(3, 3) };

            var registrator = new RegisterIoCDependencyRayCollisionChecker();

            // Act
            registrator.Execute();

            // Assert
            var checker = Ioc.Resolve<ICollisionChecker>(
                "PolygonCollisionChecker.ByRays",
                polygon1,
                polygon2
            );

            Assert.IsType<PolygonCollisionCheckerByRays>(checker);
        }
    }
}
