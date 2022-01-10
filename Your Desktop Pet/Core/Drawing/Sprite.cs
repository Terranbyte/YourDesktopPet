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
    class Sprite : Pet.LuaObject
    {
        public Forms.SpriteWindow window = null;
        public bool shouldExit = false;

        private string _spriteDirectory = "";
        public bool shouldHalt { get { return !_shown; } private set { } }
        private bool _shown = true;

        public Sprite(string spriteDirectory, Pet.AnchorPoint anchor = Pet.AnchorPoint.TopLeft) : base("New Object", Vector2.Zero, Pet.AnchorPoint.TopLeft)
        {
            window = Forms.FormManager.Current.CreateForm<Forms.SpriteWindow>();
            window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => shouldExit = true);
            window.Hide();
            _position = new Vector2(window.Left, window.Top);
            _size = new Vector2(window.Right, window.Bottom);

            //_spriteThread = new Thread(new ThreadStart(() =>
            //{
            //    window.ShowDialog();
            //}));
            //_spriteThread.SetApartmentState(ApartmentState.STA);
            //_spriteThread.Start();

            Hide();
        }

        public Sprite(string spriteDirectory, string defaultSprite, Pet.AnchorPoint anchor = Pet.AnchorPoint.TopLeft) : base("New Object", Vector2.Zero, Pet.AnchorPoint.TopLeft)
        {
            _spriteDirectory = spriteDirectory;

            window = Forms.FormManager.Current.CreateForm<Forms.SpriteWindow>();
            window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => shouldExit = true);
            window.Hide();

            SetPosition(window.Left, window.Top);
            SetSprite(defaultSprite);

            //_spriteThread = new Thread(new ThreadStart(() =>
            //{
            //    window.ShowDialog();
            //}));
            //_spriteThread.SetApartmentState(ApartmentState.STA);
            //_spriteThread.Start();

            Hide();
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

        public override void SetPosition(float x, float y)
        {
            base.SetPosition(x, y);
            window.Left = (int)_position.X;
            window.Top = (int)_position.Y;
        }

        public override void SetPosition(float x, float y, Pet.AnchorPoint overrideOffset)
        {
            base.SetPosition((float)x, (float)y, overrideOffset);
            window.ChangeSize((int)_size.X, (int)_size.Y);
        }

        public void SetSprite(string spriteName)
        {
            string[] temp = spriteName.Split('_', '.');
            window.BackgroundImage.Dispose();
            Image temp2 = SpriteSheetHelper.GetSpriteFromSpriteSheet(_spriteDirectory + "\\" + spriteName, Convert.ToInt32(temp[temp.Length - 2]), 0, window.InterpolationMode);
            window.BackgroundImage = temp2;
        }
    }
}

