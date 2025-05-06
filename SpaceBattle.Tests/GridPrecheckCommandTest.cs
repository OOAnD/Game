using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class GridPrecheckCommandTests
    {
        [Fact]
        public void Execute_ShouldNotCheckCollisions_WhenObjectsInDifferentCells()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var movingObjectMock = new Mock<IMoving>();
            movingObjectMock.SetupGet(m => m.Position).Returns(new Vector(10, 10));

            var otherObject = new object();
            var otherMovingMock = new Mock<IMoving>();
            otherMovingMock.SetupGet(m => m.Position).Returns(new Vector(16, 16));

            var grid = new Grid(0, 0, 5);

            var objectsDict = new Dictionary<string, object> { { "obj1", otherObject } };

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Grids", (object[] args) =>
                new List<Grid> { grid }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects", (object[] args) =>
                objectsDict).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.IMovingObject", (object[] args) =>
                otherMovingMock.Object).Execute();

            var collisionCmdMock = new Mock<ICommand>();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.CheckCollision", (object[] args) =>
                collisionCmdMock.Object).Execute();

            var command = new GridPrecheckCommand(movingObjectMock.Object);

            command.Execute();

            collisionCmdMock.Verify(c => c.Execute(), Times.Never());
        }

        [Fact]
        public void Execute_ShouldHandleMultipleGrids()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var movingObjectMock = new Mock<IMoving>();
            movingObjectMock.SetupGet(m => m.Position).Returns(new Vector(10, 10));

            var otherObject = new object();
            var otherMovingMock = new Mock<IMoving>();
            otherMovingMock.SetupGet(m => m.Position).Returns(new Vector(11, 11));

            var grid1 = new Grid(0, 0, 20);
            var grid2 = new Grid(0, 0, 5);

            var objectsDict = new Dictionary<string, object> { { "obj1", otherObject } };

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Grids", (object[] args) =>
                new List<Grid> { grid1, grid2 }).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects", (object[] args) =>
                objectsDict).Execute();

            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.IMovingObject", (object[] args) =>
                otherMovingMock.Object).Execute();

            var collisionCmdMock = new Mock<ICommand>();
            Ioc.Resolve<ICommand>("IoC.Register", "Commands.CheckCollision", (object[] args) =>
                collisionCmdMock.Object).Execute();

            var command = new GridPrecheckCommand(movingObjectMock.Object);

            command.Execute();

            collisionCmdMock.Verify(c => c.Execute(), Times.Once());
        }
    }
}
