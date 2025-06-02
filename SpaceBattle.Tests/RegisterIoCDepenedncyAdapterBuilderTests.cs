using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDepenedncyAdapterBuilderTests
    {
        private static void SetupIoc()
        {
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        }

        [Fact]
        public void Execute_ShouldRegisterAdapterInstanceDependency()
        {
            // Arrange
            SetupIoc();
            var interfaceTypeMock = new Mock<Type>();
            interfaceTypeMock.Setup(t => t.FullName).Returns("TestInterface");

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

            var registrator = new RegisterIoCDepenedncyAdapterBuilder();

            // Act
            registrator.Execute();

            // Assert
            var adapter = Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceTypeMock.Object,
                new Dictionary<string, object>()
            );

            Assert.NotNull(adapter);
            Assert.IsType<MockAdapter>(adapter);
        }

        [Fact]
        public void Execute_ShouldPassCorrectParametersToAdapterConstructor()
        {
            // Arrange
            SetupIoc();
            var interfaceTypeMock = new Mock<Type>();
            interfaceTypeMock.Setup(t => t.FullName).Returns("TestInterface");

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
            var registrator = new RegisterIoCDepenedncyAdapterBuilder();
            registrator.Execute();

            // Act
            var adapter = (MockAdapter)Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceTypeMock.Object,
                expectedObj
            );

            // Assert
            Assert.Same(expectedObj, adapter._obj);
        }

        [Fact]
        public void Execute_ShouldCacheAdapterTypes()
        {
            // Arrange
            SetupIoc();
            var interfaceTypeMock = new Mock<Type>();
            interfaceTypeMock.Setup(t => t.FullName).Returns("TestInterface");

            var generateCounter = 0;
            var compileCounter = 0;

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.CodeGenerator",
                (object[] args) =>
                {
                    generateCounter++;
                    return "public class MockAdapter {}";
                }
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Compile",
                (object[] args) =>
                {
                    compileCounter++;
                    return typeof(MockAdapter);
                }
            ).Execute();

            var registrator = new RegisterIoCDepenedncyAdapterBuilder();
            registrator.Execute();

            // Act - First call
            var adapter1 = Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceTypeMock.Object,
                new Dictionary<string, object>()
            );
            // Act - Second call (same interface)
            var adapter2 = Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceTypeMock.Object,
                new Dictionary<string, object>()
            );

            // Assert
            Assert.Equal(1, generateCounter);
            Assert.Equal(1, compileCounter);
            Assert.IsType<MockAdapter>(adapter1);
        }

        [Fact]
        public void Execute_ShouldNotCacheDifferentInterfaces()
        {
            // Arrange
            SetupIoc();
            var interfaceType1 = new Mock<Type>();
            interfaceType1.Setup(t => t.FullName).Returns("Interface1");

            var interfaceType2 = new Mock<Type>();
            interfaceType2.Setup(t => t.FullName).Returns("Interface2");

            var generateCounter = 0;

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.CodeGenerator",
                (object[] args) =>
                {
                    generateCounter++;
                    return $"public class Adapter{generateCounter} {{}}";
                }
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Compile",
                (object[] args) =>
                {
                    return typeof(MockAdapter);
                }
            ).Execute();

            var registrator = new RegisterIoCDepenedncyAdapterBuilder();
            registrator.Execute();

            // Act - First interface
            var adapter1 = Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceType1.Object,
                new Dictionary<string, object>()
            );

            // Act - Second interface
            var adapter2 = Ioc.Resolve<object>(
                "Adapter.Instance",
                interfaceType2.Object,
                new Dictionary<string, object>()
            );

            // Assert
            Assert.Equal(2, generateCounter);
        }

        public class MockAdapter(object obj)
        {
            public object _obj = obj;
        }
    }
}
