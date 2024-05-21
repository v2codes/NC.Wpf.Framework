using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NC.Wpf.Framework.Localization;

[Generator("C#", new string[] { })]
public class ResourceExtensionSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //IL_0002: Unknown result type (might be due to invalid IL or missing references)
        //IL_0026: Unknown result type (might be due to invalid IL or missing references)
        //IL_002b: Unknown result type (might be due to invalid IL or missing references)
        //IL_002e: Unknown result type (might be due to invalid IL or missing references)
        try
        {
            IncrementalValueProvider<Compilation> val = IncrementalValueProviderExtensions.Select<Compilation, Compilation>(((IncrementalGeneratorInitializationContext)(ref context)).CompilationProvider, (Func<Compilation, CancellationToken, Compilation>)((Compilation compilation, CancellationToken cancellationToken) => compilation));
            ((IncrementalGeneratorInitializationContext)(ref context)).RegisterSourceOutput<Compilation>(val, (Action<SourceProductionContext, Compilation>)delegate (SourceProductionContext sourceProductionContext, Compilation compilation)
            {
                //IL_0063: Unknown result type (might be due to invalid IL or missing references)
                //IL_0068: Unknown result type (might be due to invalid IL or missing references)
                //IL_0178: Unknown result type (might be due to invalid IL or missing references)
                //IL_017d: Unknown result type (might be due to invalid IL or missing references)
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
                        IEnumerable<PropertyDeclarationSyntax> enumerable = (from node in val2.GetRoot(default(CancellationToken)).DescendantNodes((Func<SyntaxNode, bool>)null, false)
                                                                             where CSharpExtensions.IsKind(node, (SyntaxKind)8892)
                                                                             select node).Select((Func<SyntaxNode, PropertyDeclarationSyntax>)((SyntaxNode node) => (PropertyDeclarationSyntax)node));
                        List<PropertyInfo> list = new List<PropertyInfo>();
                        foreach (PropertyDeclarationSyntax item2 in enumerable)
                        {
                            IPropertySymbol declaredSymbol = CSharpExtensions.GetDeclaredSymbol(semanticModel, item2, default(CancellationToken));
                            if (((ISymbol)declaredSymbol).IsStatic && ((ISymbol)declaredSymbol).Name != "Culture" && ((ISymbol)declaredSymbol).Name != "ResourceManager")
                            {
                                PropertyInfo item = default(PropertyInfo);
                                item.Name = ((ISymbol)declaredSymbol).Name;
                                item.Type = ((ISymbol)declaredSymbol.Type).Name;
                                Accessibility declaredAccessibility = ((ISymbol)declaredSymbol).DeclaredAccessibility;
                                item.Accessibility = ((object)(Accessibility)(ref declaredAccessibility)).ToString().ToLower();
                                list.Add(item);
                            }
                        }
                        ResourceExtensionSourceBuilder resourceExtensionSourceBuilder = new ResourceExtensionSourceBuilder(((object)((ISymbol)CSharpExtensions.GetDeclaredSymbol(semanticModel, (BaseTypeDeclarationSyntax)(object)val3, default(CancellationToken))).ContainingNamespace).ToString(), text, list.ToArray());
                        ((SourceProductionContext)(ref sourceProductionContext)).AddSource(resourceExtensionSourceBuilder.FileName, resourceExtensionSourceBuilder.Code);
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Debugger.Log(1, ex.Message, ex.ToString());
        }
    }
}
