using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Runtime.InteropServices;

namespace Your_Desktop_Pet.Forms
{
    public partial class Launcher : Form
    {
        private Core.Pet.PetObject _pet;
        private System.Drawing.Drawing2D.InterpolationMode _interpMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        private Queue<float> _fpsSamples = new Queue<float>();
        private string[] _petDirectories = new string[0];
        private IntPtr _hWnd;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            ApplyAppSettings();

            _hWnd = GetConsoleWindow();
            if (!Core.Globals.debugMode)
                ShowWindow(_hWnd, SW_HIDE);

            GetPetList();

            for (int i = 0; i < 5; i++)
            {
                _fpsSamples.Enqueue(1f / 60f);
            }

            Core.Helpers.Time.Start();
        }

        private void ExtractPetArchive(string path)
        {
            ZipFile zipFile = new ZipFile(path);
            string installPath = Core.Globals.dataPath + @"temp\";

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

        private void ClearPetList()
        {
            if (list_petList.SmallImageList != null)
            {
                list_petList.SmallImageList.Images.Clear();
                list_petList.SmallImageList.Dispose();
            }

            list_petList.Items.Clear();
        }

        private void GetPetList()
        {
            ClearPetList();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            imageList.ColorDepth = ColorDepth.Depth16Bit;

            _petDirectories = Directory.GetDirectories(Core.Globals.dataPath);
            for (int i = 0; i < _petDirectories.Length; i++)
            {
                string directory = _petDirectories[i];

                if (!Core.Helpers.PetHelper.CheckPetFileStructure(directory, out string e))
                {
                    Core.Helpers.Log.WriteLine("Launcher", $"Error: Tried to access incomplete pet {directory}, skipping...\nException: {e}");
                    continue;
                }

                imageList.Images.Add(new Bitmap(Core.Helpers.SpriteSheetHelper.GetSpriteFromSpriteSheet(directory + "\\icon.png", 1, 0, _interpMode)));

                Ini.IniFile iniFile = new Ini.IniFile(directory + @"\pet.ini");
                list_petList.Items.Add(iniFile.IniReadValue("PetInfo", "Name"), i);
            }

            list_petList.SmallImageList = imageList;
        }

        private void SpawnPet(string petPath)
        {
            if (_pet != null)
            {
                _pet.Stop();
                _pet.Dispose();
            }

            _pet = new Core.Pet.PetObject(petPath);
            Core.Pet.LuaObjectManager.Current.AddObject(_pet);
            _pet.Start();
        }

        private void ApplyAppSettings()
        {
            Ini.IniFile settingsFile = new Ini.IniFile(@".\settings.ini");
            Core.Globals.debugMode = Convert.ToBoolean(settingsFile.IniReadValue("AppSettings", "DebugMode"));
            Core.Globals.luaTraceback = Convert.ToBoolean(settingsFile.IniReadValue("AppSettings", "LuaTraceback"));
        }

        private void btn_spawn_Click(object sender, EventArgs e)
        {
            if (list_petList.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Looks like you don't have any pet selected.", "No pet selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SpawnPet(_petDirectories[list_petList.SelectedIndices[0]]);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Add new pet";
                dialog.Filter = "Pet archive (*.pet)|*.pet|Zip archive(*.zip)|*.zip";
                dialog.RestoreDirectory = true;

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ExtractPetArchive(dialog.FileName);
                }
            }

            GetPetList();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (list_petList.Items.Count == 0 || list_petList.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Looks like you don't have any pet selected.", "No pet selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to remove this pet?", "Removing pet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int index = list_petList.SelectedIndices[0];
                ClearPetList();
                string removePath = _petDirectories[index];

                if (Directory.Exists(removePath))
                    Directory.Delete(removePath, true);

                GetPetList();
            }
        }

        private void btn_details_Click(object sender, EventArgs e)
        {
            if (list_petList.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Looks like you don't have any pet selected.", "No pet selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dir = _petDirectories[list_petList.SelectedIndices[0]];

            PetDetails details = new PetDetails(
                dir,
                Core.Helpers.SpriteSheetHelper.GetSpriteFromSpriteSheet(dir + "\\icon.png", 1, 0, _interpMode));
            FormManager.Current.RegisterForm(details);
            details.Show();
        }

        private void btn_options_Click(object sender, EventArgs e)
        {
            MessageBox.Show("That feature isn't implemented yet, sorry!", "Missing feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_sdk_Click(object sender, EventArgs e)
        {
            updateTimer.Stop();
            Hide();

            SDK.SDKWindow w = new SDK.SDKWindow();
            w.ShowDialog();
            
            updateTimer.Start();
            Show();
        }

        private void btn_activePets_Click(object sender, EventArgs e)
        {
            MessageBox.Show("That feature isn't implemented yet, sorry!", "Missing feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pet != null)
            {
                _pet.Stop();
                _pet = null;
            }

            Core.Helpers.Log.WriteLine("Main", "Debug end");
            Core.Helpers.Log.Destroy();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            Core.Helpers.Time.Update();
            Core.API.Input.InputProvider.Update();

            _fpsSamples.Dequeue();
            _fpsSamples.Enqueue(Core.Helpers.Time.deltaTime);

            Console.Title = $"FPS: {Math.Round(1f / _fpsSamples.Average())}";
            //Console.Title = Core.Helpers.Time.deltaTime.ToString();

            if (_pet != null && _pet.ready)
            {
                if (_pet.shouldExit)
                {
                    _pet = null;
                    return;
                }

                _pet.currentTime += Core.Helpers.Time.deltaTime;
                _pet.totalTime += Core.Helpers.Time.deltaTime;

                if (_pet.currentTime < _pet.updateInterval)
                    return;

                _pet.animationTime += 1;
                _pet.Update();

                if (_pet.animationTime >= _pet.animationInterval)
                {
                    _pet.Draw();
                    _pet.animationTime -= _pet.animationInterval;
                }

                _pet.currentTime -= _pet.updateInterval;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = _interpMode;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            base.OnPaintBackground(e);
        }
    }
}
