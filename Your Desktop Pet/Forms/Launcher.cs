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
            foreach (string s in Directory.GetFiles("./"))
            {
                Console.WriteLine(s);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

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
    }
}
