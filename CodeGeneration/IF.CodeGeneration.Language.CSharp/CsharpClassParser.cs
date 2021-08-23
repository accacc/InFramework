using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;
using System.Text.RegularExpressions;

namespace IF.CodeGeneration.Language.CSharp
{
    public static class CsharpClassParser
    {
        public static CSClass Parse(string content)
        {
            var cls = new CSClass();
            var tree = CSharpSyntaxTree.ParseText(content);
            var members = tree.GetRoot().DescendantNodes().OfType<MemberDeclarationSyntax>();

            foreach (var member in members)
            {
                if (member is PropertyDeclarationSyntax property)
                {
                    CSProperty cSProperty = new CSProperty("public",
                         property.Identifier.ValueText,
                         false);

                    cSProperty.PropertyTypeString = property.Type.ToString();

                    cls.Properties.Add(cSProperty);
                }

                if (member is NamespaceDeclarationSyntax namespaceDeclaration)
                {
                    cls.NameSpace = namespaceDeclaration.Name.ToString();
                }

                if (member is ClassDeclarationSyntax classDeclaration)
                {
                    cls.Name = classDeclaration.Identifier.ValueText;

                    //cls.PrimaryKeyType = FindPrimaryKeyType(classDeclaration);
                }

                //if (member is MethodDeclarationSyntax method)
                //{
                //    Console.WriteLine("Method: " + method.Identifier.ValueText);
                //}
            }


            return cls;
        }

        private static string FindPrimaryKeyType(ClassDeclarationSyntax classDeclaration)
        {
            if (classDeclaration == null)
            {
                return null;
            }

            if (classDeclaration.BaseList == null)
            {
                return null;
            }

            foreach (var baseClass in classDeclaration.BaseList.Types)
            {
                var match = Regex.Match(baseClass.Type.ToString(), @"<(.*?)>");
                if (match.Success)
                {
                    var primaryKey = match.Groups[1].Value;

                    //if (AppConsts.PrimaryKeyTypes.Any(x => x.Value == primaryKey))
                    //{
                    //    return primaryKey;
                    //}
                }
            }

            return null;
        }
    }
}
