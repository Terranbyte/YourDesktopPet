using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Drawing
{
    class Animator
    {
        public bool flipX = false;
        public string currentAnimation = "";

        private Image[] _frames;
        private string[] _spriteFiles;
        private Forms.SpriteWindow _window;
        private int _numFrames = 0;
        private int _currentFrame = 0;
        private string _spriteDirectory = "";

        public Animator(ref Forms.SpriteWindow window, string spriteDirectory)
        {
            _frames = new Image[0];
            _spriteFiles = Directory
                .EnumerateFiles(spriteDirectory, "*", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .ToArray();
            _window = window;
            _spriteDirectory = spriteDirectory;
        }

        ~Animator()
        {
            for (int i = 0; i < _frames.Length; i++)
            {
                _frames[i].Dispose();
            }

            _window.Dispose();
        }

        public void FlipSprite(bool flipX)
        {
            if (this.flipX == flipX)
                return;

            this.flipX = flipX;
            foreach (Image image in _frames)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            _window.Invalidate();
        }

        public void ChangeAnimation(string animation)
        {
            Image source;
            string animationName = _spriteFiles.Where(s => s.Split('_')[0] == animation).First().ToString();

            try
            {
                source = Image.FromFile(_spriteDirectory + "\\" + animationName);
            }
            catch (FileNotFoundException)
            {
                Helpers.Log.WriteLine("Animator", $"Error! Couldn't find the animation called \"{animation}\".");
                return;
            }

            string[] temp = animationName.Split('_', '.');
            _numFrames = Convert.ToInt32(temp[temp.Length - 2]);
            _window.BackgroundImage.Dispose();
            _window.ChangeSize(source.Width / _numFrames, source.Height);

            currentAnimation = animation;
            _currentFrame = 0;

            for (int i = 0; i < _frames.Length; i++)
            {
                _frames[i].Dispose();
            }

            _frames = SpriteSheetHelper.GetSpriteSheetSprites(source, _numFrames, _window.InterpolationMode);

            if (flipX)
            {
                foreach (Image image in _frames)
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }
        }

        public void Tick()
        {
            _currentFrame = (_currentFrame + 1) % _numFrames;
            _window.BackgroundImage = _frames[_currentFrame];
        }
    }
}
