using IF.CodeGeneration.Application;
using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Application.Generator.Get;
using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.Application.Generator.Update;
using IF.CodeGeneration.Core;
using IF.Core.Data;
using IF.Tools.CodeGenerator;
using IF.Tools.CodeGenerator.VsAutomation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Derin.Tools.CodeGenerator
{
    public partial class MainForm : Form
    {

        private readonly Assembiler assembiler;
        private readonly FileSystemCodeFormatProvider fileSystem;
        

        private readonly string basePath = @"C:\temp";
        private readonly string solutionPath = @"C:\Projects";
        private string solutionName;
        

        

        public MainForm()
        {            

            InitializeComponent();

            this.fileSystem = new FileSystemCodeFormatProvider(basePath);
            this.assembiler = new Assembiler();

            this.modelTreeView.CheckBoxes = true;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionOnlyAssemblyResolve;

            List<NameValueDto> publishModes = new List<NameValueDto>
            {
                new NameValueDto( "Gedik.SSO","Gedik.SSO"),
                new NameValueDto( "Gedik.Resource","Gedik.Resource")
            };

            bindingSourceProjects.DataSource = publishModes;

            comboBoxProjects.DataSource = bindingSourceProjects;

            comboBoxProjects.DisplayMember = "Name";
            comboBoxProjects.ValueMember = "Value";


            //this.textBoxName.Text = "UserUpdate";
            //this.textBoxNameSpace.Text = "Gedik.SSO";
            //this.textBoxTitle.Text = "Kullanıcı Yönetimi";
        }

        public  Assembly ReflectionOnlyAssemblyResolve(object sender,ResolveEventArgs args)
        {
            if(args.Name.Contains("Microsoft.EntityFrameworkCore"))
            {
                return Assembly.ReflectionOnlyLoadFrom(@"C:\Projects\InFramework\packages\Microsoft.EntityFrameworkCore.2.2.6\lib\netstandard2.0\Microsoft.EntityFrameworkCore.dll");
            }

            if (args.Name.Contains($"{solutionName}.Contract"))
            {
                return Assembly.ReflectionOnlyLoadFrom($@"{solutionPath}\{solutionName}\{solutionName}.Contract\bin\Debug\netstandard2.0\{solutionName}.Contract.dll");
            }

            if (args.Name.Contains("IF.Persistence.EF"))
            {
                return Assembly.ReflectionOnlyLoadFrom($@"{solutionPath}\{solutionName}\packages\InFramework\IF.Persistence.EF.dll");
            }

            return Assembly.ReflectionOnlyLoad(args.Name);


        }

        private void buttonLoadModel_Click(object sender, EventArgs e)
        {

            this.solutionName = comboBoxProjects.SelectedValue.ToString();

            assembiler.AddAssemly<Entity>($@"{solutionPath}\{solutionName}\{solutionName}.Persistence.EF\bin\Debug\netstandard2.0\{solutionName}.Persistence.EF.dll");

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

        private void buttonGenerateList_Click(object sender, EventArgs e)
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

            MakeClassTree(modelTreeView.Nodes, classTreeList);

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;

            string name = classTreeList.First().Childs.First().Name.Split('\\')[1];

            Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == name).SingleOrDefault();

            var classTree = classTreeList.First().Childs.First();

            var vsManager = new VsManager(solutionName, solutionPath, basePath);

            var context = new GeneratorContext(fileSystem, textBoxName.Text, textBoxNameSpace.Text, classTree, classType, vsManager);

            context.Title = textBoxTitle.Text;

            

            if (checkBoxApiCode.Checked)
            {
                ApiCsListGenerator codeGenerator = new ApiCsListGenerator(context);
                ApiListGeneratorForm generatorForm = new ApiListGeneratorForm(codeGenerator);
                generatorForm.Show();
            }
            else
            {
                CSListGenerator codeGenerator = new CSListGenerator(context);
                MvcListGeneratorForm listGenerator = new MvcListGeneratorForm(codeGenerator);
                listGenerator.Show();
            }


            //fileSystem.ExploreDirectory(basePath);

        }

        private void buttonGenerateCreate_Click(object sender, EventArgs e)
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

            MakeClassTree(modelTreeView.Nodes, classTreeList);

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;

            string name = classTreeList.First().Childs.First().Name.Split('\\')[1];

            Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == name).SingleOrDefault();

            var classTree = classTreeList.First().Childs.First();

            var vsManager = new VsManager(solutionName, solutionPath, basePath);

            var context = new GeneratorContext(fileSystem, textBoxName.Text, textBoxNameSpace.Text, classTree, classType, vsManager);

            context.Title = textBoxTitle.Text;

            CSInsertGenerator codeGenerator = new CSInsertGenerator(context);

            MvcAddGeneratorForm listGenerator = new MvcAddGeneratorForm(codeGenerator);
            listGenerator.Show();
            //fileSystem.ExploreDirectory(basePath);
        }

        private void buttonGenerateUpdate_Click(object sender, EventArgs e)
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

            MakeClassTree(modelTreeView.Nodes, classTreeList);

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;

            string name = classTreeList.First().Childs.First().Name.Split('\\')[1];

            Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == name).SingleOrDefault();

            var classTree = classTreeList.First().Childs.First();

            var vsManager = new VsManager(solutionName, solutionPath, basePath);

            var context = new GeneratorContext(fileSystem, textBoxName.Text, textBoxNameSpace.Text, classTree, classType, vsManager);

            context.Title = textBoxTitle.Text;

            

            if (checkBoxApiCode.Checked)
            {

                ApiCsUpdateGenerator codeGenerator = new ApiCsUpdateGenerator(context);
                ApiUpdateGeneratorForm listGenerator = new ApiUpdateGeneratorForm(codeGenerator);
                listGenerator.Show();
            }
            else
            {
                CSUpdateGenerator codeGenerator = new CSUpdateGenerator(context);
                MvcUpdateGeneratorForm listGenerator = new MvcUpdateGeneratorForm(codeGenerator);
                listGenerator.Show();
            }
            //fileSystem.ExploreDirectory(basePath);
        }

        private void buttonGenerateGet_Click(object sender, EventArgs e)
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

            MakeClassTree(modelTreeView.Nodes, classTreeList);

            Assembly assembly = assembiler.AllAssembilies().Where(s => s.Key.GetName().Name == classTreeList.First().Name).SingleOrDefault().Key;

            string name = classTreeList.First().Childs.First().Name.Split('\\')[1];

            Type classType = assembiler.AllAssembilies()[assembly].Where(t => t.Name == name).SingleOrDefault();

            var classTree = classTreeList.First().Childs.First();

            var vsManager = new VsManager(solutionName, solutionPath, basePath);

            var context = new GeneratorContext(fileSystem, textBoxName.Text, textBoxNameSpace.Text, classTree, classType, vsManager);

            context.Title = textBoxTitle.Text;

            if(checkBoxApiCode.Checked)
            {
                ApiCsGetGenerator codeGenerator = new ApiCsGetGenerator(context);
                ApiGetGeneratorForm generatorForm = new ApiGetGeneratorForm(codeGenerator);
                generatorForm.Show();
            }
            else
            {
                CSGetGenerator codeGenerator = new CSGetGenerator(context);
                MvcGetGeneratorForm generatorForm = new MvcGetGeneratorForm(codeGenerator);
                generatorForm.Show();
            }

            


            
        }

        private void comboBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBoxProjects.SelectedItem as NameValueDto) != null)
            {
                textBoxNameSpace.Text = (comboBoxProjects.SelectedItem as NameValueDto).Value.ToString();
            }
        }
    }
}
