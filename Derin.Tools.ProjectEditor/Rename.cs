using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FOFramework.Tools.ProjectEditor
{
    public partial class Rename : Form
    {

        string oldSolutionNamespaceName = "Derin.Campaign";
        string oldSolutionName;
        string newSolutionNamespaceName;
        string newSolutionName;
        string oldSolutionDirectory;


        public Rename()
        {
            InitializeComponent();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            
            oldSolutionName = Normalize(textBoxOldSolutionName.Text.Replace(".sln", ""));
            newSolutionName = Normalize(textBoxNewSolutionName.Text);
            newSolutionNamespaceName = textBoxNewSolutionNamespaceName.Text;
            oldSolutionDirectory = textBoxOldSolutionDirectory.Text;

            if (string.IsNullOrWhiteSpace(oldSolutionName))
            {
                MessageBox.Show(@"Lütfen yeni solution adını giriniz.",@"Zorunlu Alan",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                return;
            }

            if (string.IsNullOrWhiteSpace(newSolutionName))
            {
                MessageBox.Show(@"Lütfen  yeni solution adını giriniz.",@"Zorunlu Alan",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                return;
            }

            if (string.IsNullOrWhiteSpace(newSolutionNamespaceName))
            {
                MessageBox.Show(@"Lütfen  yeni solution namespace adını giriniz.",@"Zorunlu Alan",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                return;
            }

            var confirmResult = MessageBox.Show(@"Exe'nin çalıştığı klasör ve tüm alt klasörlerde bulunan dosyaların adları ve içeriklerinde değişiklik yapılacaktır. Bu işlemin geri dönüşü bulunmamaktadır. Devam etmek istediğinize emin misiniz?",
                                     @"Dikkat!",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                MoveOldSolutionFiles();

                ReplaceNamespaceNames();

                RenameOldSolutionFiles();


                try
                {
                    Directory.Delete(@"C:\xGeneratorOutput\Temp\", true);
                }
                catch (Exception)
                {

                }

                Process.Start("explorer.exe", @"c:\xGeneratorOutput");
            }
        }

        private void ReplaceNamespaceNames()
        {
            
            string directory = @"C:\\xGeneratorOutput\\Temp\\";
           

            Replacer replacer = new Replacer();
            replacer.Dir = directory;
            replacer.FileMask = @"*.*";
            replacer.ExcludeFileMask = @"*.dll, *.exe, *.pdm, *.txt, *.swf, *.nupkg";
            replacer.IncludeSubDirectories = true;
            replacer.IsCaseSensitive = true;
            replacer.FindText = oldSolutionNamespaceName;
            replacer.ReplaceText = newSolutionNamespaceName;

            var resultItems = replacer.Replace().ResultItems;

            // Remove TFS Bindigs
            var removeBinding = new RemoveBinding();
            removeBinding.Process();
        }

        private int GenerateRandomPortNumber()
        {
            Random r = new Random();
            return r.Next(49152, 65535);
        }

      

        private static string Normalize(string text)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9ıİöÖçÇüÜğĞşŞ -]");
            text = rgx.Replace(text, "");
            text = text.Replace("  ", " ");
            text = text.Replace("  ", " ");
            text = ConvertToEnglish(text);
            return text;
        }
        private static string ConvertToEnglish(string text)
        {
            Encoding srcEncoding = Encoding.UTF8;
            Encoding destEncoding = Encoding.GetEncoding(1252); // Latin alphabet

            text = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(text)));

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                if (!CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]).Equals(UnicodeCategory.NonSpacingMark))
                {
                    result.Append(normalizedString[i]);
                }
            }

            return result.ToString();
        }
        private void MoveOldSolutionFiles()
        {
            

            try { Directory.Delete(@"C:\xGeneratorOutput\", true); } catch {}
            if ( !Directory.Exists(@"C:\xGeneratorOutput\")) { Directory.CreateDirectory(@"C:\xGeneratorOutput\"); }
            if ( !Directory.Exists(@"C:\xGeneratorOutput\Temp\")) { Directory.CreateDirectory(@"C:\xGeneratorOutput\Temp\"); }

            foreach (string dirPath in Directory.GetDirectories(oldSolutionDirectory, "*", SearchOption.AllDirectories))
            {
                var neww = dirPath.Replace(oldSolutionDirectory, @"C:\\xGeneratorOutput\\Temp\\");
                Directory.CreateDirectory(neww);
            }

            foreach (string newPath in Directory.GetFiles(oldSolutionDirectory, "*.*", SearchOption.AllDirectories))
            {
                var neww = newPath.Replace(oldSolutionDirectory, @"C:\\xGeneratorOutput\\Temp\\");
                File.Copy(newPath, neww, true);
            }

            var tempFolders = Directory.GetDirectories(@"C:\\xGeneratorOutput\\Temp\\");

            foreach (var item in tempFolders)
            {
                var _split = item.Split('\\');
                string _folder =  _split[_split.Length - 1];
                if (!clbFolders.CheckedItems.Contains(_folder))
                {
                    Directory.Delete(item, true);
                }
            }

        }

        private void RenameOldSolutionFiles()
        {
            

            string directory = @"C:\xGeneratorOutput\Temp";

            foreach (string dirPath in Directory.GetDirectories(directory, "*", SearchOption.AllDirectories))
            {
                var neww = dirPath.Replace(directory, @"C:\\xGeneratorOutput\\");
                var newFileName = neww.Replace(oldSolutionNamespaceName,newSolutionNamespaceName);

                Directory.CreateDirectory(newFileName);
            }

            foreach (string newPath in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
            {
                if (Path.GetExtension(newPath) == ".sln")
                {
                    var neww = newPath.Replace(directory, @"C:\\xGeneratorOutput\\");
                    var newFileName = neww.Replace(oldSolutionName, newSolutionName);
                    File.Copy(newPath, newFileName, true);
                }
                else
                {
                    var neww = newPath.Replace(directory, @"C:\\xGeneratorOutput\\");
                    var newFileName = neww.Replace(oldSolutionNamespaceName, newSolutionNamespaceName);
                    File.Copy(newPath, newFileName, true);
                }
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Solution files (*.sln)|*.sln|All files (*.*)|*.*";
            var fileDialogResult = openFileDialog1.ShowDialog();

            if (fileDialogResult == DialogResult.OK)
            {
                var fileName = openFileDialog1.SafeFileName;
                var fileDir = openFileDialog1.FileName;

                textBoxOldSolutionName.Text = fileName;
                textBoxOldSolutionDirectory.Text = fileDir.Replace(fileName, string.Empty)+ "IF.Templates";

                var directories = Directory.GetDirectories(textBoxOldSolutionDirectory.Text);

                clbFolders.Items.Clear();

                foreach (var path in directories)
                {
                    var _folder = path.Split('\\');
                    clbFolders.Items.Add(_folder[_folder.Length - 1], true);
                }

              
            }

        }

        //private void changeConfigs()
        //{
        //    string devConfigPath = @"C:\xGeneratorOutput\" + newSolutionName + ".WebUI" + @"\Security\DEV.config";
        //    string uiConfigPath = @"C:\xGeneratorOutput\" + newSolutionName.Text + ".WebUI" + @"\Web.config";

        //    // Web UI Config
        //    try
        //    {
        //        string configPath = uiConfigPath;
        //        var configFile = new FileInfo(configPath);
        //        var vdm = new VirtualDirectoryMapping(configFile.DirectoryName, true, configFile.Name);
        //        var wcfm = new WebConfigurationFileMap();
        //        wcfm.VirtualDirectories.Add("/", vdm);
        //        var test = WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");

        //        for (int i = 0; i < dgvUi.Rows.Count; i++)
        //        {
        //            test.AppSettings.Settings[dgvUi[0, i].Value.ToString()].Value = dgvUi[1, i].Value.ToString();
        //            test.Save();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    //DEV Config -> generic değil
        //    try
        //    {
        //        string DbKey = "";
        //        string ServerName = "";
        //        string DbSchema = "";
        //        string DbPwd = "";

        //        for (int i = 0; i < dgvDev.Rows.Count; i++)
        //        {
        //            if (dgvDev[0, i].Value.ToString() == "DbKey")
        //                DbKey = dgvDev[1, i].Value.ToString();
        //            if (dgvDev[0, i].Value.ToString() == "ServerName")
        //                ServerName = dgvDev[1, i].Value.ToString();
        //            if (dgvDev[0, i].Value.ToString() == "DbSchema")
        //                DbSchema = dgvDev[1, i].Value.ToString();
        //            if (dgvDev[0, i].Value.ToString() == "DbPwd")
        //                DbPwd = dgvDev[1, i].Value.ToString();
        //        }

        //        System.IO.File.WriteAllText(devConfigPath, string.Empty);

        //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(devConfigPath, true))
        //        {
        //            file.WriteLine("<? xml version = \"1.0\" standalone = \"yes\" ?>");
        //            file.WriteLine("<DbConnectionSettingsCollection>");
        //            file.WriteLine("<DbConnectionSettings>");
        //            file.WriteLine("<ROW>");
        //            file.WriteLine("<DbKey>" + DbKey + "</DbKey>");
        //            file.WriteLine("<ServerName>" + ServerName + "</ServerName>");
        //            file.WriteLine("<DbSchema>" + DbSchema + "</DbSchema>");
        //            file.WriteLine("<DbPwd>" + DbPwd + "</DbPwd>");
        //            file.WriteLine("</ROW>");
        //            file.WriteLine("</DbConnectionSettings>");
        //            file.WriteLine("</DbConnectionSettingsCollection>");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //private void btnChangeConfig_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        changeConfigs();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}


    }
}