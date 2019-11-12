using FOFramework.CodeGeneration.Core;

using IF.CodeGeneration.CSharp;
using IF.Core.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Derin.Tools.CodeGenerator
{
    public partial class Form1 : Form
    {

        Assembiler assembiler;

        FileSystemCodeFormatProvider fileSystem = new FileSystemCodeFormatProvider(@"C:\Temp");

        public Form1()
        {
            this.assembiler = new Assembiler();

            InitializeComponent();

            this.modelTreeView.CheckBoxes = true;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionOnlyAssemblyResolve;

            this.textBoxName.Text = "Test";
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

            var classTreeList = new List<ClassTree>();

            MakeClassTree(modelTreeView.Nodes,classTreeList);

            GenerateCSharp(classTreeList);
           
        }

        private void GenerateCSharp(List<ClassTree> classTreeList)
        {

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;

            string name = classTreeList.First().Childs.First().Name.Split('\\')[1];

            Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == name).SingleOrDefault();

            GenerateMvcModels(textBoxName.Text, classTreeList.First().Childs.First(),classType);
            GenerateContractClasses(textBoxName.Text, classTreeList.First().Childs.First(), classType);
        }


        private void GenerateMvcModels(string className, ClassTree classTree, Type classType)
        {
            CSClass gridClass = GenerateClass(className + "GridModel", classTree, classType);
            fileSystem.FormatCode(gridClass.GenerateCode(), "cs");
        }

        private void GenerateContractClasses(string className, ClassTree classTree, Type classType)
        {

            CSClass @class = new CSClass();
            @class.Name = className + "Dto";
            @class.Properties = new List<CSProperty>();

            foreach (var property in classTree.Childs)
            {
                @class.Properties.Add(GetClassProperty(classType, property.Name.Split('\\')[2]));
            }



            CSClass requestClass = new CSClass();
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = className + "Request";


            requestClass.Properties = new List<CSProperty>();

            foreach (var property in classTree.Childs)
            {
                requestClass.Properties.Add(GetClassProperty(classType, property.Name.Split('\\')[2]));
            }
            CSClass responseClass = new CSClass();
            responseClass.BaseClass = "BaseResponse";
            responseClass.Name = className + "Response";



            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = String.Format("List<{0}Dto>", className);

            responseClass.Properties.Add(dtoProperty);

            

            CSInterface @interface = new CSInterface();
            @interface.Name = $"I{className}Query";
            @interface.InheritedInterfaces.Add($"IDataGetQueryAsync<{className}Request,{className}Response>");



            var classes = @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;

            fileSystem.FormatCode(classes, "cs", className);



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
