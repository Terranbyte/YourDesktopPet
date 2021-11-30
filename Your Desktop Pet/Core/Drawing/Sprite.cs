using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Your_Desktop_Pet.Core.Drawing
{
    enum PositionOffset
    {
        TopLeft = 0,
        Top = 1,
        TopRight = 2,
        Right = 3,
        BottomRight = 4,
        Bottom = 5,
        BottomLeft = 6,
        Left = 7,
        Center = 8,
    }

    class Sprite
    {
        public Forms.SpriteWindow window = null;
        public PositionOffset offset = PositionOffset.TopLeft;
        public Animator animator;
        public bool shouldExit = false;

        public bool shouldHalt { get { return !_shown; } private set { } }

        private bool _shown = true;

        private readonly KeyValuePair<float, float>[] _offsetLUT = new KeyValuePair<float, float>[9]
        {
            new KeyValuePair<float, float>(0f, 0f),
            new KeyValuePair<float, float>(0.5f, 0f),
            new KeyValuePair<float, float>(1f, 0f),
            new KeyValuePair<float, float>(1f, 0.5f),
            new KeyValuePair<float, float>(1f, 1f),
            new KeyValuePair<float, float>(0.5f, 1f),
            new KeyValuePair<float, float>(0f, 1f),
            new KeyValuePair<float, float>(0f, 0.5f),
            new KeyValuePair<float, float>(0.5f, 0.5f)
        };

        public Sprite(string spriteDirectory, PositionOffset offset = PositionOffset.TopLeft)
        {
            window = Forms.FormManager.Current.CreateForm<Forms.SpriteWindow>();
            window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => shouldExit = true);
            window.Hide();
            this.offset = offset;

            //_spriteThread = new Thread(new ThreadStart(() =>
            //{
            //    window.ShowDialog();
            //}));
            //_spriteThread.SetApartmentState(ApartmentState.STA);
            //_spriteThread.Start();

            Hide();
        }

        ~Sprite()
        {

        }

        public void Show()
        {
            if (_shown || window.IsDisposed)
                return;

            window.Show();
            _shown = true;
        }

        public void Hide()
        {
            if (!_shown || window.IsDisposed)
                return;

            window.Hide();
            _shown = false;
        }

        public void SetPosition(float x, float y)
        {
            KeyValuePair<float, float> o = _offsetLUT[(int)offset];

            window.Left = (int)Math.Round(x - (window.Width * o.Key));
            window.Top = (int)Math.Round(y - (window.Height * o.Value));
        }

        public void SetPosition(float x, float y, PositionOffset overrideOffset)
        {
            KeyValuePair<float, float> offset = _offsetLUT[(int)overrideOffset];

            window.Left = (int)Math.Round(x - (window.Width * offset.Key));
            window.Top = (int)Math.Round(y - (window.Height * offset.Value));
        }
    }
}

