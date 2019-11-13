using FOFramework.CodeGeneration.Core;

using IF.CodeGeneration.CSharp;
using IF.Core.Data;
using IF.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Derin.Tools.CodeGenerator
{
    public partial class Form1 : Form
    {

        Assembiler assembiler;

        string path = @"C:\Temp";

        FileSystemCodeFormatProvider fileSystem = new FileSystemCodeFormatProvider(@"C:\Temp");

        public Form1()
        {
            this.assembiler = new Assembiler();

            InitializeComponent();

            this.modelTreeView.CheckBoxes = true;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionOnlyAssemblyResolve;

            this.textBoxName.Text = "Application";
            this.textBoxNameSpace.Text = "Gedik.SSO";
            this.textBoxTitle.Text = "Uygulama Yönetimi";
        }

        public static Assembly ReflectionOnlyAssemblyResolve(object sender,ResolveEventArgs args)
        {
            if(args.Name.Contains("Microsoft.EntityFrameworkCore"))
            {
                return Assembly.ReflectionOnlyLoadFrom(@"C:\Projects\InFramework\packages\Microsoft.EntityFrameworkCore.2.2.6\lib\netstandard2.0\Microsoft.EntityFrameworkCore.dll");
            }

            return Assembly.ReflectionOnlyLoad(args.Name);


        }

        private void buttonLoadModel_Click(object sender, EventArgs e)
        {
            assembiler.AddAssemly<Entity>(@"C:\Projects\Gedik.SSO\Gedik.SSO.Persistence.EF\bin\Debug\netstandard2.0\Gedik.SSO.Persistence.EF.dll");

            modelTreeView.Nodes.Clear();

            foreach (var assembly in assembiler.AllAssembilies().Keys)
            {
                var assName = assembly.GetName().Name;

                var node = modelTreeView.Nodes.Add(assName,assName);                

                foreach (var item in assembiler.AllAssembilies()[assembly])
                {
                    node.Nodes.Add(assName, item.Name);
                }
            }
        }

        private void modelTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (var item in this.assembiler.AllAssembilies().Keys)
            {
                if (item.GetName().Name == e.Node.Text) return;
            }

            e.Node.Nodes.Clear();

            GetChildNodes(e.Node);

            //treeView1.CollapseAll();
            e.Node.Expand();
        }

        private void GetChildNodes(TreeNode nodek)
        {
            PropertyInfo[] props;

            if (nodek.Text.Contains(":"))
            {
                props = assembiler.GetProperties(nodek.Name, nodek.Text.Split(':')[0]);
            }
            else if (nodek.Text.Contains("[]"))
            {
                props = assembiler.GetProperties(nodek.Name, nodek.Text.Split(new string[] { "[]" },StringSplitOptions.None)[0]);
            }
            else
            {
                props = assembiler.GetProperties(nodek.Name, nodek.Text);
            }

            foreach (var prop in props)
            {
                if (prop.PropertyType.IsSubclassOf(typeof(Entity)))
                {
                    var navigation = new TreeNode();
                    navigation.Text = prop.PropertyType.Name + ":" + prop.Name;
                    navigation.Name = nodek.Name;
                    navigation.Nodes.Add(new TreeNode());
                    nodek.Nodes.Add(navigation);

                }
                else if (prop.PropertyType.Name == "ICollection`1"
                    && prop.PropertyType.Namespace == "System.Collections.Generic"
                    && prop.PropertyType.GenericTypeArguments[0].IsSubclassOf(typeof(Entity))
                    )
                {
                    var navigation = new TreeNode();
                    navigation.Text = prop.PropertyType.GenericTypeArguments[0].Name + "[]" + prop.Name;
                    navigation.Name = nodek.Name;
                    navigation.Nodes.Add(new TreeNode());
                    nodek.Nodes.Add(navigation);
                }

                else
                {
                    nodek.Nodes.Add(prop.Name);
                }


            }
        }
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show(@"Please enter the Name.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxNameSpace.Text))
            {
                MessageBox.Show(@"Please enter the NameSpace.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var classTreeList = new List<ClassTree>();

            MakeClassTree(modelTreeView.Nodes,classTreeList);

            GenerateCSharp(classTreeList);
           
        }

        private void GenerateCSharp(List<ClassTree> classTreeList)
        {

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;

            string name = classTreeList.First().Childs.First().Name.Split('\\')[1];

            Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == name).SingleOrDefault();

            
            GenerateContractClasses(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            GenerateDataQueryHandlerClass(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            GenerateHandlerClass(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            GenerateControllerMethods(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            GenerateMvcModels(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            GenerateMvcIndexView(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            GenerateMvcGridView(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);

            fileSystem.ExploreFile(@"C:\Temp");
        }

        private void GenerateMvcGridView(string className, string namespaceName, ClassTree classTree, Type classType)
        {


            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"@model List<{namespaceName}.Models.{className}GridModel>");
            builder.AppendLine();


            builder.AppendLine("<table>");
            builder.AppendLine("<tr>");
            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{className}Create\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Ekle");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
            builder.AppendLine("</table>");


            builder.AppendLine("<table class=\"table table-striped table-sm\">");
            builder.AppendLine("<tr>");

            CSClass gridClass = GenerateClass(className, classTree, classType);

            foreach (var item in gridClass.Properties)
            {
                builder.AppendLine("<th>");
                builder.AppendLine(item.Name);
                builder.AppendLine("</th>");
            }


            builder.AppendLine("</tr>");

            builder.AppendLine("@if(Model != null && Model.Any())");
            builder.AppendLine("{");
            builder.AppendLine("@foreach (var item in Model)");
            builder.AppendLine("{");
            builder.AppendLine("<tr>");

            foreach (var item in gridClass.Properties)
            {
                builder.AppendLine("<td>");
                builder.AppendLine($"@Html.DisplayFor(modelItem => item.{item.Name})");
                builder.AppendLine("</td>");
            }


            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{className}Edit\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Düzenle");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");

            builder.AppendLine("</tr>");

            builder.AppendLine("}");
            builder.AppendLine("}");
            builder.AppendLine("else");
            builder.AppendLine("{");
            builder.AppendLine("@:Veri bulunamadı, Lütfen Kriter seçiniz");
            builder.AppendLine("}");
            builder.AppendLine("</table>");

            fileSystem.FormatCode(builder.ToString(), "cshtml", "_GridView");
        }

        private void GenerateMvcIndexView(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"@model List<{namespaceName}.Models.{className}GridModel>");
            builder.AppendLine();
            builder.AppendLine($"@{{ViewBag.Title = \"{textBoxTitle.Text}\"; }}");
            builder.AppendLine();
            builder.AppendLine("@{");
            builder.AppendLine("Layout = \"~/Views/Shared/_GridLayout.cshtml\";");
            builder.AppendLine();
            builder.AppendLine("@section GridView");
            builder.AppendLine("{");
            builder.AppendLine();
            builder.AppendLine($"@{{await Html.RenderPartialAsync(\"~/Views/Security/{className}/_GridView.cshtml\", Model);}}");
            builder.AppendLine();
            builder.AppendLine("}");

            //@model List<Gedik.SSO.Models.ApplicationGridModel>

            //@{ViewBag.Title = "Uygulama Yönetimi"; }

            //@{
            //    Layout = "~/Views/Shared/_GridLayout.cshtml";
            //}

            //@section GridView
            //{
            //    @{await Html.RenderPartialAsync("~/Views/Security/Application/_GridView.cshtml", Model);}
            //}

            fileSystem.FormatCode(builder.ToString(), "cshtml", "Index");

        }

        private void GenerateControllerMethods(string className, string namespaceName, ClassTree classTree, Type classType)
        {



            CSMethod method = new CSMethod(className + "Index", "ActionResult", "public");
            method.IsAsync = true;

            StringBuilder methodBody = new StringBuilder();


            //var list = await this.dispatcher.QueryAsync<ApplicationRequest, ApplicationResponse>(new ApplicationRequest());
            //var model = list.MapTo<ApplicationGridModel>();
            //return View("~/Views/Security/Application/Index.cshtml", model);

            methodBody.AppendLine($"var list = await this.dispatcher.QueryAsync<{className}Request, {className}Response>(new {className}Request());");
            methodBody.AppendLine($"var model = list.Data.MapTo<{className}GridModel>();");
            methodBody.AppendFormat($"return View(\"~/Views/Security/{className}/Index.cshtml\", model);");
            method.Body = methodBody.ToString();

            fileSystem.FormatCode(method.GenerateCode(), "cs");
        }


        private void GenerateMvcModels(string className,string namespaceName ,ClassTree classTree, Type classType)
        {
            CSClass gridClass = GenerateClass(className + "GridModel", classTree, classType);
            gridClass.NameSpace = namespaceName;
            fileSystem.FormatCode(gridClass.GenerateCode(), "cs");
        }

        private void GenerateHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();
            @class.Name = className + "Handler";
            @class.NameSpace = namespaceName + ".Queries.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{namespaceName}.Contract.Queries");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{namespaceName}.Persistence.EF.Queries");

            @class.InheritedInterfaces.Add($"IQueryHandlerAsync<{className}Request, {className}Response>");

            var repositoryProperty = new CSProperty("private", "query", false);
            repositoryProperty.PropertyTypeString = GetDataQueryIntarfaceName(className);
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "query", Type = GetDataQueryIntarfaceName(className) });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.query = query;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("Handle", className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = className + "Request" });
            handleMethod.Body += $"return await this.query.GetAsync(request);" + Environment.NewLine;           

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");

        }

        private void GenerateDataQueryHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataQueryClassName(className);
            @class.NameSpace = namespaceName + ".Persistence.EF.Queries";

            @class.Usings.Add($"{namespaceName}.Contract.Queries");
            @class.Usings.Add($"{namespaceName}.Persistence.EF.Models");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"IF.Persistence");
            @class.Usings.Add($"System.Linq");
            @class.Usings.Add($"Microsoft.EntityFrameworkCore");


            @class.InheritedInterfaces.Add(GetDataQueryIntarfaceName(className));

            var repositoryProperty = new CSProperty(typeof(IRepository), "private", "repository", false);
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "","public");
            constructorMethod.Parameters.Add(new CsMethodParameter() {Name = "repository",Type = "IRepository" });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.repository = repository;");
            methodBody.AppendLine();           
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);


            CSMethod handleMethod = new CSMethod("Get", className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = className + "Request" });
            handleMethod.Body += $"var data = await this.repository.GetQuery<{classType.Name}>()"+ Environment.NewLine;
            handleMethod.Body += $".Select(x => new {className}Dto" + Environment.NewLine;
            handleMethod.Body += $"{{" + Environment.NewLine;

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(classType, property.Name.Split('\\')[2]);
                handleMethod.Body += $"{classProperty.Name} = x.{classProperty.Name}," + Environment.NewLine;
            }

            handleMethod.Body += $"}}).ToListAsync();" + Environment.NewLine;

            handleMethod.Body += $"return new {className}Response {{ Data = data }};" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");
        }

        private void GenerateContractClasses(string className, string namespaceName, ClassTree classTree, Type classType)
        {

            CSClass @class = new CSClass();
            @class.Name = className + "Dto";
            //@class.NameSpace = namespaceName + ".Contract.Queries";
            @class.Properties = new List<CSProperty>();

            foreach (var property in classTree.Childs)
            {
                @class.Properties.Add(GetClassProperty(classType, property.Name.Split('\\')[2]));
            }



            CSClass requestClass = new CSClass();
            //requestClass.NameSpace = namespaceName + ".Contract.Queries";
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = className + "Request";          

            CSClass responseClass = new CSClass();
            //responseClass.NameSpace = namespaceName + ".Contract.Queries";
            responseClass.BaseClass = "BaseResponse";
            responseClass.Name = className + "Response";
            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = String.Format("List<{0}Dto>", className);
            responseClass.Properties.Add(dtoProperty);



            CSInterface @interface = new CSInterface();
            @interface.Name = GetDataQueryIntarfaceName(className);
            @interface.InheritedInterfaces.Add($"IDataGetQueryAsync<{className}Request,{className}Response>");

            string classes = "";
            classes += "using IF.Core.Data;";
            classes += Environment.NewLine;
            classes += "using System.Collections.Generic;";
            classes += Environment.NewLine;
            classes += Environment.NewLine;
            classes += "namespace " + namespaceName + ".Contract.Queries";
            classes += Environment.NewLine;
            classes += "{";
            classes += Environment.NewLine;
            classes += @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            fileSystem.FormatCode(classes, "cs", className);

        }

        private static string GetDataQueryIntarfaceName(string className)
        {
            return $"I{className}DataQueryAsync";
        }

        private static string GetDataQueryClassName(string className)
        {
            return $"{className}DataQueryAsync";
        }

        private CSClass GenerateClass(string className, ClassTree classTree,Type classType)
        {
            CSClass @class = new CSClass();

            @class.Name = className;

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(classType, property.Name.Split('\\')[2]);
                @class.Properties.Add(classProperty);
            }

            return @class;
        }

        private CSProperty GetClassProperty(Type classType, string propertyName)
        {
            var property = classType.GetProperty(propertyName);
            var classProperty = new CSProperty(property.PropertyType, "public", property.Name, property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
            return classProperty;
        }

        private void modelTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {          


            var node = e.Node;

            var parent = node.Parent;

            if (parent == null)
                return;

            if (parent.Name == node.Name && !node.IsExpanded)
            {

                parent.Nodes.Clear();
                TreeNode currentNode = new TreeNode();
                currentNode.Text = node.Text;
                currentNode.Name = parent.Name;              
                currentNode.Checked = true;
                GetChildNodes(currentNode);
                parent.Nodes.Add(currentNode);                
                currentNode.Expand();

            }

            SelectParents(node, node.Checked);
            
        }

        private void SelectParents(TreeNode node, Boolean isChecked)
        {

            var parent = node.Parent;

            if (parent == null)
                return;


            if (!isChecked && HasCheckedNode(parent))
                return;

            parent.Checked = isChecked;
            SelectParents(parent, isChecked);
        }

        private bool HasCheckedNode(TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>().Any(n => n.Checked);
        }

        void MakeClassTree(TreeNodeCollection nodes, List<ClassTree> list)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    ClassTree classTree = new ClassTree();
                    classTree.Name = node.FullPath;
                    list.Add(classTree);

                    if (node.Nodes.Count > 0)
                    {
                        MakeClassTree(node.Nodes, classTree.Childs);
                    }
                }


            }
        }


    }
}
