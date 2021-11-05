using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Input;

namespace Your_Desktop_Pet.Core.Window
{
    class SpriteWindow : Form
    {
        public InterpolationMode InterpolationMode = InterpolationMode.NearestNeighbor;
        public System.Windows.IInputElement Input;

        protected override bool ShowWithoutActivation { get { return true; } }
        private const int WS_EX_TOPMOST = 0x00000008;

        public SpriteWindow(bool keyboardHandler)
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

            Image image = Image.FromFile(@"./default.png");
            BackgroundImageLayout = ImageLayout.Zoom;
            BackgroundImage = image;

            KeyPreview = false;

            Size = new Size((int)(image.Width * Globals.ScaleFactor), (int)(image.Height * Globals.ScaleFactor));

            if (keyboardHandler)
            {
                KeyPreview = true;
                Input = new System.Windows.Controls.Button();
                Input.Focusable = true;
                Keyboard.Focus(Input);
            }

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
            base.OnPaintBackground(e);
        }

        public void ChangeSize(float width, int height)
        {

            if (InvokeRequired)
            {
                Action safeInvoke = delegate { ChangeSize(width, height); };
                Invoke(safeInvoke);
            }
            else
            {
                Size = new Size((int)(width * Globals.ScaleFactor), (int)(height * Globals.ScaleFactor));
                BackgroundImage = new Bitmap((int)(width * Globals.ScaleFactor), (int)(height * Globals.ScaleFactor));
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SpriteWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SpriteWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
    }
}
