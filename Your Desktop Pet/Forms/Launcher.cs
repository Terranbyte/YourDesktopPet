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
        private Core.Pet.PetObject pet;
        private Queue<float> fpsSamples = new Queue<float>();
        private string[] petDirectories = new string[0];
        private IntPtr hWnd;

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

            hWnd = GetConsoleWindow();
            if (!Core.Globals.debugMode)
                ShowWindow(hWnd, SW_HIDE);

            GetPetList();

            for (int i = 0; i < 25; i++)
            {
                fpsSamples.Enqueue(1f / 60f);
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
                if (CheckPetFileStructure(installPath))
                {
                    Ini.IniFile iniFile = new Ini.IniFile(installPath + @"pet.ini");
                    string id = iniFile.IniReadValue("PetInfo", "Guid");
                    bool deleted = false;

                    if (Directory.Exists(Core.Globals.dataPath + id))
                    {
                        DialogResult result = MessageBox.Show("That pet already exists, would you like to replace it?\nDOING THIS WILL COMPLETELY RESET THE PET!", "Pet already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                            Directory.Delete(Core.Globals.dataPath + id, true);
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
                }

                if (zipFile != null)
                {
                    zipFile.IsStreamOwner = true;
                    zipFile.Close();
                }
            }
        }

        private void GetPetList()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            imageList.ColorDepth = ColorDepth.Depth16Bit;

            list_petList.Items.Clear();

            petDirectories = Directory.GetDirectories(Core.Globals.dataPath);
            for (int i = 0; i < petDirectories.Length; i++)
            {
                string directory = petDirectories[i];
                Bitmap bitmap = new Bitmap(64, 64);

                try
                {
                    Image image = Image.FromFile(directory + @"\icon.png");
                    
                    using (Graphics g =  Graphics.FromImage(bitmap))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                        g.DrawImage(image, new Rectangle(0, 0, 64, 64), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                        g.Save();
                    }

                    image.Dispose();
                    imageList.Images.Add(bitmap);
                }
                catch (Exception e)
                {
                    imageList.Images.Add(bitmap);
                    Core.Helpers.Log.WriteLine("Launcher", e.Message);
                }

                Ini.IniFile iniFile = new Ini.IniFile(directory + @"\pet.ini");
                list_petList.Items.Add(iniFile.IniReadValue("PetInfo", "Name"), i);
            }

            list_petList.SmallImageList = imageList;
        }

        private void SpawnPet(string petPath)
        {
            if (pet != null)
                pet.Stop();

            Console.WriteLine("Test");
            pet = new Core.Pet.PetObject(petPath);
            pet.Start();
        }

        private bool CheckPetFileStructure(string petPath)
        {
            string[] filesToCheck = new string[]
            {
                @"\pet.ini",
                @"icon.png",
                @"\Scripts\pet.lua",
            };

            foreach (string s in filesToCheck)
            {
                if (!File.Exists(petPath + s) && !Directory.Exists(petPath + s))
                    return false;
            }

            return true;
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
                MessageBox.Show("Looks like you don't have any pet selected.", "No pet to spawn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SpawnPet(petDirectories[list_petList.SelectedIndices[0]]);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Add new pet";
                dialog.Filter = "Pet archive (*.pet)|*.pet";
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
                MessageBox.Show("Looks like you don't have any pet selected.", "No pet to spawn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to remove this pet?", "Removing pet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string removePath = petDirectories[list_petList.SelectedIndices[0]];
                if (Directory.Exists(removePath))
                    Directory.Delete(removePath, true);

                GetPetList();
            }
        }

        private void btn_details_Click(object sender, EventArgs e)
        {

        }

        private void btn_options_Click(object sender, EventArgs e)
        {

        }

        private void btn_sdk_Click(object sender, EventArgs e)
        {

        }

        private void btn_activePets_Click(object sender, EventArgs e)
        {
            
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Helpers.Log.WriteLine("Main", "Debug end");
            Core.Helpers.Log.Destroy();
            Environment.Exit(0);
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            Core.Helpers.Time.Update();

            if (pet == null)
                return;

            pet.currentTime += Core.Helpers.Time.deltaTime;
            pet.totalTime += Core.Helpers.Time.deltaTime;

            if (pet.currentTime < pet.updateInterval)
                return;

            fpsSamples.Dequeue();
            fpsSamples.Enqueue(Core.Helpers.Time.deltaTime);

            Console.Title = $"FPS: {1f / fpsSamples.Average()}";

            pet.animationTime += 1;

            Core.API.Input.InputProvider.Update();
            pet.Update();

            if (pet.animationTime >= pet.animationInterval)
            {
                pet.Draw();
                pet.animationTime -= pet.animationInterval;
            }

            pet.currentTime -= pet.updateInterval;
        }
    }
}
