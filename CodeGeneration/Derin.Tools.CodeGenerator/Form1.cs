
using Derin.Tools.CodeGenerator.Generator;
using IF.CodeGeneration.Core;
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

        private readonly Assembiler assembiler;

        private readonly string path = @"C:\Temp";

        FileSystemCodeFormatProvider fileSystem;

        public Form1()
        {
            fileSystem = new FileSystemCodeFormatProvider(path);

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

            if (args.Name.Contains("Gedik.SSO.Contract"))
            {
                return Assembly.ReflectionOnlyLoadFrom(@"C:\Projects\Gedik.SSO\Gedik.SSO.Contract\bin\Debug\netstandard2.0\Gedik.SSO.Contract.dll");
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

            CSGenerator codeGenerator = new CSGenerator(fileSystem);


            codeGenerator.GenerateContractClasses(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            codeGenerator.GenerateDataQueryHandlerClass(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            codeGenerator.GenerateHandlerClass(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            codeGenerator.GenerateControllerMethods(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            codeGenerator.GenerateMvcModels(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);
            codeGenerator.GenerateMvcIndexView(textBoxName.Text, textBoxNameSpace.Text, textBoxTitle.Text,classTreeList.First().Childs.First(), classType);
            codeGenerator.GenerateMvcGridView(textBoxName.Text, textBoxNameSpace.Text, classTreeList.First().Childs.First(), classType);

            fileSystem.ExploreFile(path);
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

        private void buttonGenerateMvcForm_Click(object sender, EventArgs e)
        {

        }
    }
}
