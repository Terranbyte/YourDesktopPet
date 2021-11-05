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
        public Window.SpriteWindow window = null;
        public PositionOffset offset = PositionOffset.TopLeft;
        public Animator animator;
        #region ohgod
        public bool shouldExit = false;
        public bool shouldHalt { get { return !_shown; } private set { } }
        #endregion

        private Thread _spriteThread = null;
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

        public Sprite(string spriteDirectory, PositionOffset offset = PositionOffset.TopLeft, bool keyboardHandler = false)
        {
            window = new Window.SpriteWindow(keyboardHandler);
            window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => shouldExit = true);
            this.offset = offset;
            this.animator = new Animator(ref window, spriteDirectory);

            _spriteThread = new Thread(new ThreadStart(() =>
            {
                window.ShowDialog();
            }));
            _spriteThread.SetApartmentState(ApartmentState.STA);
            _spriteThread.Start();
        }

        ~Sprite()
        {

        }

        public void Show()
        {
            if (_shown || !_spriteThread.IsAlive)
                return;

            if (window.InvokeRequired)
            {
                Action safeInvoke = delegate { Show(); };
                window.Invoke(safeInvoke);
            }
            else
            {
                _spriteThread.Resume();
                _shown = true;
            }
        }

        public void Hide()
        {
            if (!_shown || !_spriteThread.IsAlive)
                return;

            if (window.InvokeRequired)
            {
                Action safeInvoke = delegate { Hide(); };
                window.Invoke(safeInvoke);
                return;
            }
            else
            {
                _spriteThread.Suspend();
                _shown = false;
            }
        }

        public void Stop()
        {
            if (window.InvokeRequired)
            {
                Action safeInvoke = delegate { Stop(); };
                window.Invoke(safeInvoke);
            }
            else
            {
                window.Dispose();
                _spriteThread.Join();
            }
        }

        public void SetPosition(float x, float y, object offsetRef = null)
        {
            if (window.InvokeRequired)
            {
                Action safeInvoke = delegate { SetPosition(x, y, (object)_offsetLUT[(int)offset]); };
                window.Invoke(safeInvoke);
            }
            else
            {
                KeyValuePair<float, float> offset = new KeyValuePair<float, float>(0, 0);
                if (offsetRef != null)
                    offset = (KeyValuePair<float, float>)offsetRef;

                window.Left = (int)Math.Round(x - (window.Width * offset.Key));
                window.Top = (int)Math.Round(y - (window.Height * offset.Value));
            }
        }
    }
}

