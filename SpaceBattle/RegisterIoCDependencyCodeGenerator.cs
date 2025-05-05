using App;
using Scriban;

namespace SpaceBattle
{
    public class RegisterIoCDependencyGenerateAdapterCode : ICommand
    {
        public void Execute()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "Adapters.CodeGenerator", (object[] args) =>
            {
                var interfaceType = (Type)args[0];
                var pattern = @"class {{ Interface.Name }}Adapter : {{ Interface.Name }}
{
    private readonly IDictionary<string, object> _dict;

    public {{ Interface.Name }}Adapter(IDictionary<string, object> dict)
    {
        _dict = dict ?? throw new ArgumentNullException(nameof(dict));
    }

{{ for prop in InterfaceProperties }}
    public {{ prop.PropertyType.Name }} {{ prop.Name }}
    {
        get => ({{ prop.PropertyType.Name }})_dict[" + "\"{{ prop.Name }}\"" + @"];
    {{ if prop.CanWrite }}
        set => _dict[" + "\"{{ prop.Name }}\"" + @"] = value;
    {{ end }}
    }
{{ end }}
}";

                var template = Template.Parse(pattern);
                var model = new
                {
                    Interface = interfaceType,
                    InterfaceProperties = interfaceType.GetProperties()
                };

                // Рендерим шаблон
                var generatedCode = template.Render(model, member => member.Name);

                return generatedCode;

            }).Execute();
        }
    }
}
