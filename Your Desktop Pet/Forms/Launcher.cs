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

namespace Your_Desktop_Pet.Forms
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            GetPetList();
        }

        private void ExtractPetArchive(string path)
        {
            ZipFile zipFile = new ZipFile(path);
            string installPath = Core.Globals.dataPath + @"temp\";

            list_petList.Items.Clear();

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
                Ini.IniFile iniFile = new Ini.IniFile(installPath + @"pet.ini");
                string id = iniFile.IniReadValue("PetInfo", "Guid");

                if (Directory.Exists(Core.Globals.dataPath + id))
                    Directory.Delete(Core.Globals.dataPath + id, true);

                Directory.Move(installPath, Core.Globals.dataPath + id);

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
            imageList.ImageSize = new Size(64, 65);
            imageList.ColorDepth = ColorDepth.Depth16Bit;

            string[] directories = Directory.GetDirectories(Core.Globals.dataPath);
            for (int i = 0; i < directories.Length; i++)
            {
                string directory = directories[i];
                Bitmap bitmap = new Bitmap(64, 64);

                try
                {
                    Image image = Image.FromFile(directory + @"\icon.png");
                    
                    using (Graphics g =  Graphics.FromImage(bitmap))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
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

        }

        private void btn_spawn_Click(object sender, EventArgs e)
        {
            if (list_petList.Items.Count == 0)
            {
                MessageBox.Show("Looks like you don't have any pets added/selected.", "No pet to spawn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            base.OnPaintBackground(e);
        }
    }
}
