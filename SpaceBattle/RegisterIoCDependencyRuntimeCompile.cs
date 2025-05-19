using System.Reflection;
using App;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SpaceBattle;

public class RegisterIoCDependencyRuntimeCompile : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            "RuntimeCompiler",
            (object[] args) =>
            {
                var code = (string)args[0];
                var fullClassName = (string)args[1];

                var references = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                    .Select(a => MetadataReference.CreateFromFile(a.Location))
                    .ToList();

                references.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
                references.Add(MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location));

                var syntaxTree = CSharpSyntaxTree.ParseText(code);

                var compilation = CSharpCompilation.Create(
                    assemblyName: Guid.NewGuid().ToString(),
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                using var ms = new MemoryStream();
                var result = compilation.Emit(ms);

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                var type = assembly.GetType(fullClassName)!;

                return type;
            }
        ).Execute();
    }
}
