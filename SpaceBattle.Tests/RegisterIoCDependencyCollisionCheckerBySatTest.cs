using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyCollisionCheckerBySatTest
    {
        [Fact]
        public void Execute_ShouldRegisterSatCollisionCheckerDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var polygon1 = new List<Point> { new Point(0, 0), new Point(1, 0), new Point(1, 1) };
            var polygon2 = new List<Point> { new Point(2, 2), new Point(3, 2), new Point(3, 3) };

            var registrator = new RegisterIoCDependencyCollisionCheckerBySat();

            // Act
            registrator.Execute();

            // Assert
            var checker = Ioc.Resolve<ICollisionChecker>(
                "PolygonCollisionChecker.BySAT",
                polygon1,
                polygon2
            );

            Assert.IsType<PolygonCollisionCheckerBySAT>(checker);
        }
    }
}
