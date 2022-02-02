using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Your_Desktop_Pet.Forms.SDK
{
    public partial class SDKWindow : Form
    {
        private string _projectDir = "";
        private string[] _projectDirs = new string[0];

        public SDKWindow()
        {
            InitializeComponent();
        }

        private void SDKWindow_Load(object sender, EventArgs e)
        {
            Ini.IniFile settingsFile = new Ini.IniFile(@".\settings.ini");

            _projectDir = settingsFile.IniReadValue("AppSettings", "ProjectFolder");

            while (!Directory.Exists(_projectDir))
            {
                SetProjectFolder();
            }

            UpdateProjectList();
        }

        private void SetProjectFolder()
        {
            Ini.IniFile settingsFile = new Ini.IniFile(@".\settings.ini");
            OpenFileDialog folderBrowser = new OpenFileDialog();

            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Select project folder";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                _projectDir = Path.GetDirectoryName(folderBrowser.FileName);
                settingsFile.IniWriteValue("AppSettings", "ProjectFolder", _projectDir);
            }

            folderBrowser.Dispose();
        }

        private void ClearProjectList()
        {
            projectDataSourceBindingSource.Clear();
        }

        private void UpdateProjectList()
        {
            _projectDirs = Directory.GetDirectories(_projectDir).Where(x => Core.Helpers.PetHelper.CheckPetFileStructure(x, out _)).ToArray();

            foreach (string s in _projectDirs)
            {
                Ini.IniFile pet = new Ini.IniFile(s + "\\pet.ini");
                projectDataSourceBindingSource.Add(new ProjectDataSource(
                    s.Split('\\').Last()
                    ));
            }
        }

        private void CreateNewProject(string path)
        {
            try
            {
                Core.Helpers.Log.WriteLine("SDK", "Creating new project...");
                Core.Helpers.Log.IndentationLevel += 1;

                Core.Helpers.Log.WriteLine("SDK", "Creating directories...");
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + "\\Sprites");
                Directory.CreateDirectory(path + "\\Scripts");
                Directory.CreateDirectory(path + "\\Assets");
                Core.Helpers.Log.WriteLine("SDK", "Done!");

                Core.Helpers.Log.WriteLine("SDK", "Creating template ini file...");
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamWriter sw = new StreamWriter(File.Create(path + "\\pet.ini")))
                using (Stream stream = assembly.GetManifestResourceStream(
                    assembly.GetManifestResourceNames().Where(x => x.Contains("PetIniTemplate.txt")).ToArray()[0]
                    ))
                using (StreamReader reader = new StreamReader(stream))
                {
                    sw.Write(reader.ReadToEnd());
                }
                Core.Helpers.Log.WriteLine("SDK", "Done!");

                Core.Helpers.Log.WriteLine("SDK", "Creating default images...");
                using (FileStream icon = new FileStream(path + "\\icon.png", FileMode.Create, FileAccess.Write))
                using (FileStream defaultImg = new FileStream(path + "\\Sprites\\default_1.png", FileMode.Create, FileAccess.Write))
                using (Stream stream = assembly.GetManifestResourceStream(
                    assembly.GetManifestResourceNames().Where(x => x.Contains("TemplateIcon.png")).ToArray()[0]
                    ))
                using (StreamReader reader = new StreamReader(stream))
                {
                    stream.CopyTo(icon);
                    stream.Position = 0;
                    stream.CopyTo(defaultImg);
                }
                Core.Helpers.Log.WriteLine("SDK", "Done!");

                Core.Helpers.Log.WriteLine("SDK", "Creating template script file...");
                using (StreamWriter sw = new StreamWriter(File.Create(path + "\\Scripts\\pet.lua")))
                using (Stream stream = assembly.GetManifestResourceStream(
                    assembly.GetManifestResourceNames().Where(x => x.Contains("PetScriptTemplate.txt")).ToArray()[0]
                    ))
                using (StreamReader reader = new StreamReader(stream))
                {
                    sw.Write(reader.ReadToEnd());
                }
                Core.Helpers.Log.WriteLine("SDK", "Done!");
            }
            catch (Exception e)
            {
                Directory.Delete(path, true);
                Core.Helpers.Log.WriteLine("SDK", e.Message);
                Core.Helpers.Log.IndentationLevel = 0;
                return;
            }
            
            Core.Helpers.Log.IndentationLevel = 0;
            Core.Helpers.Log.WriteLine("SDK", "Done!");

            ClearProjectList();
            UpdateProjectList();
        }

        private void ImportPetArchive(string path)
        {
            ZipFile zipFile = new ZipFile(path);
            string installPath = _projectDir + "\\temp\\";

            try
            {
                foreach (ZipEntry zipEntry in zipFile)
                {
                    string entryFileName = zipEntry.Name;
                    byte[] buffer = new byte[4096];
                    Stream zipStream = zipFile.GetInputStream(zipEntry);

                    string fullZipToPath = Path.Combine(installPath, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);

                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    using (FileStream streamWriter = File.Open(fullZipToPath, FileMode.Create))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (Core.Helpers.PetHelper.CheckPetFileStructure(installPath, out string e))
                {
                    Ini.IniFile iniFile = new Ini.IniFile(installPath + @"pet.ini");
                    string id = iniFile.IniReadValue("PetInfo", "Guid");
                    bool deleted = false;

                    if (Directory.Exists(Core.Globals.dataPath + id))
                    {
                        DialogResult result = MessageBox.Show("That pet already exists, would you like to replace it?\nDOING THIS WILL COMPLETELY RESET THE PET!", "Pet already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Directory.Delete(Core.Globals.dataPath + id, true);
                        }
                        else
                        {
                            Directory.Delete(installPath, true);
                            deleted = true;
                        }
                    }

                    if (!deleted)
                        Directory.Move(installPath, Core.Globals.dataPath + id);
                }
                else
                {
                    if (Directory.Exists(installPath))
                        Directory.Delete(installPath, true);

                    MessageBox.Show("The given pet archive was not a valid pet.\nUndoing changes.", "Error when adding new pet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Core.Helpers.Log.WriteLine("Launcher", $"Error: {e}");
                }

                if (zipFile != null)
                {
                    zipFile.IsStreamOwner = true;
                    zipFile.Close();
                }
            }
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_projectDir + "\\New pet"))
            {
                int a = 1;

                while (Directory.Exists(_projectDir + $"\\New pet {a}"))
                {
                    a += 1;
                }

                CreateNewProject(_projectDir + $"\\New pet {a}");
                return;
            }

            CreateNewProject(_projectDir + "\\New pet");
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            ClearProjectList();

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Import pet";
                dialog.Filter = "Pet archive (*.pet)|*.pet|Zip archive(*.zip)|*.zip";
                dialog.RestoreDirectory = true;

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ImportPetArchive(dialog.FileName);
                }
            }

            UpdateProjectList();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (projectDataSourceBindingSource.Count == 0 || projectView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Looks like you don't have any projects selected.", "No project selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this project?", "Delete project", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int index = projectView.SelectedCells[0].RowIndex;
                ClearProjectList();
                string removePath = _projectDirs[index];

                if (Directory.Exists(removePath))
                    Directory.Delete(removePath, true);

                UpdateProjectList();
            }
        }

        private void btn_projects_Click(object sender, EventArgs e)
        {
            SetProjectFolder();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            ClearProjectList();
            UpdateProjectList();
        }

        private void projectView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_projectDirs.Length == 0)
                return;

            Directory.Move(_projectDirs[e.RowIndex], _projectDir + $"\\{projectView.Rows[e.RowIndex].Cells[0].Value}");
        }

        private void projectView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string projectPath = _projectDir + $"\\{projectView.Rows[e.RowIndex].Cells[0].Value}";

            switch (projectView.Columns[e.ColumnIndex].Name)
            {
                case "metadata":
                    EditMetadata(projectPath);
                    break;
                case "test":
                    TestProject(projectPath);
                    break;
                case "compile":
                    BuildProject(projectPath);
                    break;
                default:
                    break;
            }
        }

        private void EditMetadata(string path)
        {
            MessageBox.Show("That feature isn't implemented yet, sorry!", "Missing feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TestProject(string path)
        {
            Core.Helpers.Log.WriteLine("SDK", "Launching pet tester...");
            PetTestingWindow w = new PetTestingWindow(path);
            FormManager.Current.RegisterForm(w);
            w.Show();
        }

        private void BuildProject(string path)
        {
            MessageBox.Show("That feature isn't implemented yet, sorry!", "Missing feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
