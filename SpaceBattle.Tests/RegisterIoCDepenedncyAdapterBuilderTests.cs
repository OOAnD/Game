using App;
using App.Scopes;

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

            var interfaceType = typeof(ITestInterface);
            var obj = new Dictionary<string, object>();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.CodeGenerator",
                (object[] args) => "public class TestAdapter { public TestAdapter(IDictionary<string, object> obj) {} }"
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Compile",
                (object[] args) => typeof(TestAdapter)
            ).Execute();

            var builder = new RegisterIoCDepenedncyAdapterBuilder();

            // Act
            builder.Execute();

            // Assert
            var adapter = Ioc.Resolve<object>("Adapter.Instance", interfaceType, obj);
            Assert.NotNull(adapter);
            Assert.IsType<TestAdapter>(adapter);
        }

        [Fact]
        public void Execute_ShouldPassCorrectObjectToAdapterConstructor()
        {
            // Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            var interfaceType = typeof(ITestInterface);
            var expectedObj = new Dictionary<string, object> { ["test"] = "value" };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapters.CodeGenerator",
                (object[] args) => "public class TestAdapter { public IDictionary<string, object> Obj; public TestAdapter(IDictionary<string, object> obj) { Obj = obj; } }"
            ).Execute();

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "Adapter.Compile",
                (object[] args) => typeof(TestAdapter)
            ).Execute();

            var builder = new RegisterIoCDepenedncyAdapterBuilder();
            builder.Execute();

            // Act
            var adapter = (TestAdapter)Ioc.Resolve<object>("Adapter.Instance", interfaceType, expectedObj);

            // Assert
            Assert.Same(expectedObj, adapter._obj);
        }

        public interface ITestInterface { }

        public class TestAdapter(IDictionary<string, object> obj)
        {
            public IDictionary<string, object> _obj = obj;
        }
    }
}
