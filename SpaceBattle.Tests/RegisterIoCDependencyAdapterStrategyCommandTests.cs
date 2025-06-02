using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyAdapterStrategyCommandTests
    {
        [Fact]
        public void Execute_ShouldRegisterAdapterStrategy()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var sourceData = new Dictionary<string, object>();
            var customBehaviors = new Dictionary<string, Func<object>>();

            var registrator = new RegisterIoCDependencyAdapterStrategyCommand();

            // Act
            registrator.Execute();

            // Assert
            var adapter = Ioc.Resolve<IDictionary<string, object>>(
                "Adapter.Strategy",
                sourceData,
                customBehaviors
            );

            Assert.IsType<CustomAdapter>(adapter);
        }
    }
}
