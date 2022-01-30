using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Your_Desktop_Pet.Controls;

namespace Your_Desktop_Pet.Forms
{
    public partial class PetDetails : Form
    {
        private string ini;
        private Image icon;

        public PetDetails(string petPath, Image icon)
        {
            InitializeComponent();
            ini = petPath;
            this.icon = icon;
        }

        private void PetDetails_Load(object sender, EventArgs e)
        {
            Ini.IniFile pet = new Ini.IniFile(ini + "//pet.ini");

            iconBox.Image = icon;
            name.Text = pet.IniReadValue("PetInfo", "Name");
            author.Text = pet.IniReadValue("PetInfo", "Author");
            version.Text = pet.IniReadValue("PetInfo", "Version");
            description.Text = pet.IniReadValue("PetInfo", "Description");
        }
    }
}
