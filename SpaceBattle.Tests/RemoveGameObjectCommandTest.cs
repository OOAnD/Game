using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RemoveGameObjectCommandTest
    {
        [Fact]
        public void Execute_ShouldRemoveGameObject_WhenObjectExists()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = "object1";
            var gameObject = new Dictionary<string, object> { { "id", gameObjectId } };

            var objects = new Dictionary<string, IDictionary<string, object>>
            {
                { gameObjectId, gameObject }
            };

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll",
                (Func<object[], IDictionary<string, IDictionary<string, object>>>)(_ => objects)).Execute();

            var command = new RemoveGameObjectCommand(gameObject);

            // Act
            command.Execute();

            // Assert
            Assert.False(objects.ContainsKey(gameObjectId));
        }

        [Fact]
        public void Execute_ShouldDoNothing_WhenObjectDoesNotExist()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = "object1";
            var gameObject = new Dictionary<string, object> { { "id", gameObjectId } };

            var objects = new Dictionary<string, IDictionary<string, object>>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll",
                (Func<object[], IDictionary<string, IDictionary<string, object>>>)(_ => objects)).Execute();

            var command = new RemoveGameObjectCommand(gameObject);

            // Act
            command.Execute();

            // Assert
            Assert.Empty(objects);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenObjectIdIsMissing()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObject = new Dictionary<string, object>();

            var objects = new Dictionary<string, IDictionary<string, object>>();

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll",
                (Func<object[], IDictionary<string, IDictionary<string, object>>>)(_ => objects)).Execute();

            var command = new RemoveGameObjectCommand(gameObject);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => command.Execute());
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenObjectsCollectionIsNotInitialized()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var gameObjectId = "object1";
            var gameObject = new Dictionary<string, object> { { "id", gameObjectId } };

            Ioc.Resolve<ICommand>("IoC.Register", "Game.Objects.GetAll",
                (Func<object[], IDictionary<string, IDictionary<string, object>>?>)(_ => null)).Execute();

            var command = new RemoveGameObjectCommand(gameObject);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => command.Execute());
        }
    }
}
