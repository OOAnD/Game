using App;
using App.Scopes;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyCodeGeneratorTest
    {
        [Fact]
        public void Execute_ShouldCorrectRegistry()
        {
            //Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            //Act
            var registrator = new RegisterIoCDependencyGenerateAdapterCode();
            registrator.Execute();

            //Assert
            var code = Ioc.Resolve<string>("Adapters.CodeGenerator", typeof(ITest1));
            Assert.IsType<string>(code);
        }

        [Fact]
        public void Execute_ShouldReturnCorrectCode()
        {
            //Arrange
            new InitCommand().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
            var correctCode = @"class ITest1Adapter : ITest1
{
    private readonly IDictionary<string, object> _dict;

    public ITest1Adapter(IDictionary<string, object> dict)
    {
        _dict = dict ?? throw new ArgumentNullException(nameof(dict));
    }


    public String Prop1
    {
        get => (String)_dict[" + "\"Prop1\"" + @"];
    
            set => _dict[" + "\"Prop1\"" + @"] = value;
        
    }

    public Int32 Prop2
    {
        get => (Int32)_dict[" + "\"Prop2\"" + @"];
    
    }

}";

            //Act
            var registrator = new RegisterIoCDependencyGenerateAdapterCode();
            registrator.Execute();

            //Assert
            var code = Ioc.Resolve<string>("Adapters.CodeGenerator", typeof(ITest1));
            Assert.Equal(correctCode, code);
        }
    }

    internal interface ITest1
    {
        string Prop1 { get; set; }
        int Prop2 { get; }
    }
}
