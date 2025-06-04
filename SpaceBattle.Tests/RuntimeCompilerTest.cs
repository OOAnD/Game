
using App;
using App.Scopes;

namespace SpaceBattle.Tests;

public class CompilerTests
{
    [Fact]
    public void CompileAndRun_DynamicClass_ReturnsExpectedMessage()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        var code = @"
using System;


namespace TestNamespace
{
    public class TestClass
    {
        public string GetMessage()
        {
            return ""ABC123!"";
        }
    }
}";

        new RegisterIoCDependencyRuntimeCompile().Execute();
        var type = Ioc.Resolve<Type>("RuntimeCompiler", code, "TestNamespace.TestClass");

        var methodInfo = type.GetMethod("GetMessage")!;
        var instance = Activator.CreateInstance(type)!;
        var result = methodInfo.Invoke(instance, null) as string;

        Assert.NotNull(result);
        Assert.Equal("ABC123!", result);
    }
}
