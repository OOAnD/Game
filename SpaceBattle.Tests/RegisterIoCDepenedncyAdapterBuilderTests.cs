using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDepenedncyAdapterBuilderTests
    {
        [Fact]
        public void Execute_ShouldRegisterAdapterInstanceDependency()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
            var interfaceTypeMock = new Mock<Type>();

            var mockAdapterCode = "public class MockAdapter {}";
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.CodeGenerator",
                (object[] args) => mockAdapterCode
            ).Execute();

            var mockAdapterType = typeof(MockAdapter);
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Compile",
                (object[] args) => mockAdapterType
            ).Execute();

            var mockCustomBehaviorWrapper = new Dictionary<string, object>();
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "CustomBehaviorWrapper",
                (object[] args) => mockCustomBehaviorWrapper
            ).Execute();

            var registrator = new RegisterIoCDepenedncyAdapterBuilder();

            // Act
            registrator.Execute();

            // Assert
            var adapter = Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceTypeMock.Object,
                new Dictionary<string, object>(),
                new Dictionary<string, Func<object>>()
            );

            Assert.NotNull(adapter);
            Assert.IsType<MockAdapter>(adapter);
        }

        [Fact]
        public void Execute_ShouldPassCorrectParametersToAdapterConstructor()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
            var interfaceTypeMock = new Mock<Type>();

            var mockAdapterCode = "public class MockAdapter { public object Obj; public MockAdapter(object obj) { Obj = obj; } }";
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.CodeGenerator",
                (object[] args) => mockAdapterCode
            ).Execute();

            var mockAdapterType = typeof(MockAdapter);
            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Compile",
                (object[] args) => mockAdapterType
            ).Execute();

            var expectedObj = new Dictionary<string, object>();
            var customBehavior = new Dictionary<string, Func<object>>();
            var mockCustomBehaviorWrapper = expectedObj;

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "CustomBehaviorWrapper",
                (object[] args) =>
                {
                    Assert.Same(expectedObj, args[0]);
                    Assert.Same(customBehavior, args[1]);
                    return mockCustomBehaviorWrapper;
                }
            ).Execute();

            var registrator = new RegisterIoCDepenedncyAdapterBuilder();
            registrator.Execute();

            // Act
            var adapter = (MockAdapter)Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceTypeMock.Object,
                expectedObj,
                customBehavior
            );

            // Assert
            Assert.Same(mockCustomBehaviorWrapper, adapter._obj);
        }

        public class MockAdapter(object obj)
        {
            public object _obj = obj;
        }
    }
}
