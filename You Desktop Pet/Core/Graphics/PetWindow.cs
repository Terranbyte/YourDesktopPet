using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;

namespace Your_Desktop_Pet.Core.Graphics
{
    class PetWindow : Form
    {
        public InterpolationMode InterpolationMode = InterpolationMode.NearestNeighbor;

        private Vector2 _positon;
        private int _width;
        private int _height;

        public PetWindow(int width, int height)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            TransparencyKey = ColorTranslator.FromHtml("#000001");
            BackColor = TransparencyKey;
            AllowTransparency = true;
            AllowDrop = false;

            FormBorderStyle = FormBorderStyle.None;
            Bounds = new Rectangle(0, 0, Width, Height);
            TopMost = true;

            Image image = Image.FromFile(@"C:\Users\fredr\OneDrive\Documents\Sprite-0001.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = image;
            //Hide();

            Application.EnableVisualStyles();
            Application.Run(this);

            _positon = new Vector2(Left, Top);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = this.InterpolationMode;
            base.OnPaintBackground(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                _positon += new Vector2(0, -5);
            else if (e.KeyCode == Keys.Left)
                _positon += new Vector2(-5, 0);
            else if (e.KeyCode == Keys.Down)
                _positon += new Vector2(0, 5);
            else if (e.KeyCode == Keys.Right)
                _positon += new Vector2(5, 0);

            Left = (int) _positon.X;
            Top = (int) _positon.Y;
        }
    }
}
