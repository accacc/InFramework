using DatabaseSchemaReader.DataSchema;
using FOFramework.CodeGeneration.Core;
using FOFramework.CodeGeneration.CSharp;
using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Derin.Tools.CodeGenerator
{
    public partial class Form1 : Form
    {

        DatabaseSchema schema;
        Assembiler assembiler;

        FileSystemCodeFormatProvider fileSystem = new FileSystemCodeFormatProvider(@"C:\Temp");

        public Form1()
        {
            this.assembiler = new Assembiler();

            InitializeComponent();

            this.modelTreeView.CheckBoxes = true;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionOnlyAssemblyResolve;

            
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

        //public void GetCheckedNodes(TreeNodeCollection nodes)
        //{
        //    foreach (System.Windows.Forms.TreeNode aNode in nodes)
        //    {
        //        //edit
        //        if (aNode.Checked)
        //            Console.WriteLine(aNode.Text);

        //        if (aNode.Nodes.Count != 0)
        //            GetCheckedNodes(aNode.Nodes);
        //    }
        //}



      
      

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

            //foreach (TreeNode assNode in modelTreeView.Nodes)
            //{
            //    if(assNode.IsExpanded)
            //    {
            //        foreach (TreeNode node in assNode.Nodes)
            //        {
            //            if(node.IsExpanded)
            //            {

            //                var type = ass.GetType(node.Name, node.Text);

            //                foreach (PropertyInfo prop in type.GetProperties())
            //                {

            //                }
            //            }

            //        }
            //    }
            //}
        }

        private void GenerateCSharp(List<ClassTree> classTreeList)
        {

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;
            GenerateMvcModels(textBoxName.Text, classTreeList.First().Childs.First(),assembly);
        }


        private void GenerateMvcModels(string className, ClassTree sp, Assembly assembly)
        {
            //CSClass gridFilterClass = new CSClass();

            //gridFilterClass.Name = tablePascalCaseName + "GridFilterModel";

            //foreach (var parameter in sp.Parameters)
            //{
            //    gridFilterClass.Properties.Add
            //        (
            //        new CSProperty(parameter.Type, "public", parameter.Alias, parameter.IsNullable)
            //        );
            //}



            //fileSystem.FormatCode(gridFilterClass.GenerateCode(), "cs");


            CSClass gridClass = GenerateClass(className+ "GridModel", sp, assembly);

            fileSystem.FormatCode(gridClass.GenerateCode(), "cs");



        }

        private CSClass GenerateClass(string className, ClassTree sp, Assembly assembly)
        {
            CSClass @class = new CSClass();

            @class.Name = className;

            foreach (var column in sp.Childs)
            {
                var classNames = column.Name.Split('\\');

                Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == classNames[1]).SingleOrDefault();

                var property = classType.GetProperty(classNames[2]);

                @class.Properties.Add(new CSProperty(property.PropertyType, "public", property.Name, property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)));
            }

            return @class;
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
