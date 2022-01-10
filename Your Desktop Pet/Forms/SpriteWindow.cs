using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Input;

namespace Your_Desktop_Pet.Forms
{
    class SpriteWindow : Form
    {
        public InterpolationMode InterpolationMode = InterpolationMode.NearestNeighbor;
        public float scaleFactor;

        protected override bool ShowWithoutActivation { get { return true; } }
        private const int WS_EX_TOPMOST = 0x00000008;

        public SpriteWindow()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);

            TransparencyKey = ColorTranslator.FromHtml("#000001");
            BackColor = TransparencyKey;
            AllowTransparency = true;
            AllowDrop = true;

            FormBorderStyle = FormBorderStyle.None;
            Bounds = new Rectangle(0, 0, Width, Height);
            TopMost = true;
            ShowInTaskbar = false;

            BackgroundImageLayout = ImageLayout.Zoom;
            BackgroundImage = Image.FromFile("./default.png");
            Size = new Size(0, 0);

            Activate();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = this.InterpolationMode;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            base.OnPaintBackground(e);
        }

        public void ChangeSize(float width, int height)
        {
            Size = new Size((int)(width * scaleFactor), (int)(height * scaleFactor));
            BackgroundImage = new Bitmap(Size.Width, Size.Height);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SpriteWindow
            // 
            this.ClientSize = new Size(284, 261);
            this.Name = "SpriteWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }
    }
}
