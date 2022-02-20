using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Your_Desktop_Pet.Core.Lua;

namespace Your_Desktop_Pet.Core.Drawing
{
    class Sprite : Lua.LuaObject
    {
        public Forms.SpriteWindow window = null;
        public bool flipX = false;
        public bool shouldExit = false;
        public bool shouldHalt { get { return !_shown; } private set { } }

        private string _spriteName = "";
        private string _spriteDirectory = "";
        private string[] _spriteFiles;
        private bool _shown = true;

        public Sprite(string spriteDirectory, AnchorPoint anchor = AnchorPoint.TopLeft) : base("New Object", Vector2.Zero, AnchorPoint.TopLeft)
        {
            window = Forms.FormManager.Current.CreateForm<Forms.SpriteWindow>();
            window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => shouldExit = true);
            window.Hide();

            _position = new Vector2(window.Left, window.Top);
            _size = new Vector2(window.Right, window.Bottom);

            components |= LuaObjectComponents.Sprite;

            _spriteFiles = Directory
                .EnumerateFiles(spriteDirectory, "*", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .ToArray();
            Hide();
        }

        public Sprite(string spriteDirectory, string defaultSprite, AnchorPoint anchor = AnchorPoint.TopLeft) : base("New Object", Vector2.Zero, AnchorPoint.TopLeft)
        {
            _spriteDirectory = spriteDirectory;

            window = Forms.FormManager.Current.CreateForm<Forms.SpriteWindow>();
            window.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) => shouldExit = true);
            window.Hide();

            _spriteFiles = Directory
                .EnumerateFiles(spriteDirectory, "*", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .ToArray();

            SetPosition(window.Left, window.Top);

            if (string.IsNullOrEmpty(defaultSprite))
            {
                Exception e = new ArgumentException("Default sprite was not set in pet.ini");
                Helpers.Log.WriteLine("Sprite", "Warning: " + e.Message);
            }
            else
            {
                SetSprite(defaultSprite);
            }

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

        public void FlipSprite(bool flipX)
        {
            if (this.flipX == flipX)
                return;

            this.flipX = flipX;
            window.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            window.Invalidate();
        }

        public override void SetPosition(float x, float y)
        {
            base.SetPosition(x, y);
            window.Left = (int)_position.X;
            window.Top = (int)_position.Y;
        }

        public override void SetPosition(float x, float y, AnchorPoint overrideOffset)
        {
            base.SetPosition((float)x, (float)y, overrideOffset);
            window.ChangeSize((int)_size.X, (int)_size.Y);
        }

        public void SetSprite(string spriteName)
        {
            _spriteName = spriteName;
            string spriteFileName = _spriteFiles.Where(s => s.Split('_')[0] == spriteName).First().ToString();
            string[] temp = spriteFileName.Split('_', '.');

            Image temp2 = Helpers.SpriteSheetHelper.GetSpriteFromSpriteSheet(_spriteDirectory + "\\" + spriteFileName, Convert.ToInt32(temp[temp.Length - 2]), 0, window.InterpolationMode);
            window.BackgroundImage = temp2;

            _size = new Vector2(temp2.Size.Width * window.scaleFactor, temp2.Size.Height * window.scaleFactor);
        }

        public void SetSprite(Image sprite, string spriteName)
        {
            _spriteName = spriteName;
            window.BackgroundImage = sprite;
            _size = new Vector2(sprite.Size.Width * window.scaleFactor, sprite.Size.Height * window.scaleFactor);
        }

        public Forms.SDK.SpriteDebugInfo GetSpriteDebugInfo()
        {
            return new Forms.SDK.SpriteDebugInfo(_spriteName, flipX, _shown);
        }
    }
}

