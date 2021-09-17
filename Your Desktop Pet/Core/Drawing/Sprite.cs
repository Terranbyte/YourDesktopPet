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
        public Window.SpriteWindow Window = null;
        public PositionOffset Offset = PositionOffset.TopLeft;
        public Animator Animator;
        #region ohgod
        public bool ShouldExit = false;
        public bool ShouldHalt { get { return !_shown; } private set { } }
        //public bool DdEnter = false;
        //public bool DdLeave = false;
        //public bool DdTrigger = false;
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
            Window = new Window.SpriteWindow(keyboardHandler);
            Window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => ShouldExit = true);
            Offset = offset;
            this.Animator = new Animator(ref Window, spriteDirectory);

            _spriteThread = new Thread(new ThreadStart(() =>
            {
                Window.ShowDialog();
            }));
            _spriteThread.SetApartmentState(ApartmentState.STA);
            _spriteThread.Start();
        }

        public void Show()
        {
            if (_shown || !_spriteThread.IsAlive)
                return;

            if (Window.InvokeRequired)
            {
                Action safeInvoke = delegate { Show(); };
                Window.Invoke(safeInvoke);
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

            if (Window.InvokeRequired)
            {
                Action safeInvoke = delegate { Hide(); };
                Window.Invoke(safeInvoke);
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
            if (Window.InvokeRequired)
            {
                Action safeInvoke = delegate { Stop(); };
                Window.Invoke(safeInvoke);
            }
            else
            {
                Window.Dispose();
                _spriteThread.Join();
            }
        }

        public void SetPosition(float x, float y, object offsetRef = null)
        {
            if (Window.InvokeRequired)
            {
                Action safeInvoke = delegate { SetPosition(x, y, (object)_offsetLUT[(int)Offset]); };
                Window.Invoke(safeInvoke);
            }
            else
            {
                KeyValuePair<float, float> offset = new KeyValuePair<float, float>(0, 0);
                if (offsetRef != null)
                    offset = (KeyValuePair<float, float>)offsetRef;

                Window.Left = (int)Math.Round(x - (Window.Width * offset.Key));
                Window.Top = (int)Math.Round(y - (Window.Height * offset.Value));
            }
        }
    }
}

