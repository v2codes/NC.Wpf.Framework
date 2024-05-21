using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NC.Wpf.Framework.Localization;

[Generator("C#", new string[] { })]
public class AppSettingManagerSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //IL_0002: Unknown result type (might be due to invalid IL or missing references)
        //IL_0026: Unknown result type (might be due to invalid IL or missing references)
        //IL_002b: Unknown result type (might be due to invalid IL or missing references)
        //IL_002e: Unknown result type (might be due to invalid IL or missing references)
        IncrementalValueProvider<Compilation> val = IncrementalValueProviderExtensions.Select<Compilation, Compilation>(((IncrementalGeneratorInitializationContext)(ref context)).CompilationProvider, (Func<Compilation, CancellationToken, Compilation>)((Compilation compilation, CancellationToken cancellationToken) => compilation));
        ((IncrementalGeneratorInitializationContext)(ref context)).RegisterSourceOutput<Compilation>(val, (Action<SourceProductionContext, Compilation>)delegate (SourceProductionContext sourceProductionContext, Compilation compilation)
        {
            //IL_0063: Unknown result type (might be due to invalid IL or missing references)
            //IL_0068: Unknown result type (might be due to invalid IL or missing references)
            SyntaxTree val2 = compilation.SyntaxTrees.Where(delegate (SyntaxTree st)
            {
                string fileName = Path.GetFileName(st.FilePath);
                return fileName == "Resource.Designer.cs" || fileName == "Resources.Designer.cs";
            }).FirstOrDefault();
            if (val2 != null)
            {
                SemanticModel semanticModel = compilation.GetSemanticModel(val2, false);
                ClassDeclarationSyntax val3 = val2.GetRoot(((SourceProductionContext)(ref sourceProductionContext)).CancellationToken).DescendantNodes((Func<SyntaxNode, bool>)null, true).OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault();
                object obj;
                if (val3 == null)
                {
                    obj = null;
                }
                else
                {
                    SyntaxToken identifier = ((BaseTypeDeclarationSyntax)val3).Identifier;
                    obj = ((SyntaxToken)(ref identifier)).Text;
                }
                string text = (string)obj;
                if (!(text != "Resources") || !(text != "Resource"))
                {
                    AppSettingManagerSourceBuilder appSettingManagerSourceBuilder = new AppSettingManagerSourceBuilder(((object)ModelExtensions.GetDeclaredSymbol(semanticModel, (SyntaxNode)(object)val3, default(CancellationToken)).ContainingNamespace).ToString());
                    ((SourceProductionContext)(ref sourceProductionContext)).AddSource(appSettingManagerSourceBuilder.FileName, appSettingManagerSourceBuilder.Code);
                }
            }
        });
    }
}
