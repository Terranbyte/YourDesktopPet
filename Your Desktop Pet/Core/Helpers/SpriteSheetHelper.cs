using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Helpers
{
    public class SpriteSheetHelper
    {
        public static Image[] GetSpriteSheetSprites(string sourceSprite, int totalSprites, InterpolationMode interpMode)
        {
            Image source = Image.FromFile(sourceSprite);
            return GetSpriteSheetSprites(source, totalSprites, interpMode);
        }

        public static Image[] GetSpriteSheetSprites(Image source, int totalSprites, InterpolationMode interpMode)
        {
            Image[] frames = new Image[totalSprites];
            int spriteWidth = source.Width / totalSprites;
            Rectangle spriteSize = new Rectangle(0, 0, spriteWidth, source.Height);

            for (int i = 0; i < totalSprites; i++)
            {
                frames[i] = new Bitmap(spriteWidth, source.Height);

                Graphics graphics = Graphics.FromImage(frames[i]);
                graphics.InterpolationMode = interpMode;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.SmoothingMode = SmoothingMode.None;

                graphics.DrawImage(source, spriteSize,
                    new Rectangle(i * spriteWidth, 0, spriteWidth, source.Height),
                    GraphicsUnit.Pixel);
                graphics.Dispose();
            }

            source.Dispose();

            return frames;
        }

        public static Image GetSpriteFromSpriteSheet(string sourceSprite, int totalSprites, int spriteIndex, InterpolationMode interpMode)
        {
            Image source = Image.FromFile(sourceSprite);
            return GetSpriteFromSpriteSheet(source, totalSprites, spriteIndex, interpMode);
        }

        public static Image GetSpriteFromSpriteSheet(Image source, int totalSprites, int spriteIndex, InterpolationMode interpMode)
        {
            int spriteWidth = source.Width / totalSprites;
            Image sprite = new Bitmap(spriteWidth, source.Height);

            Graphics graphics = Graphics.FromImage(sprite);
            graphics.InterpolationMode = interpMode;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.SmoothingMode = SmoothingMode.None;

            if (totalSprites == 1 && false)
            {
                Rectangle bounds = new Rectangle(new Point(0, 0), source.Size);
                graphics.DrawImage(source, bounds, bounds, GraphicsUnit.Pixel);
            }
            else
            {
                graphics.DrawImage(source, new Rectangle(0, 0, spriteWidth, source.Height),
                new Rectangle(spriteIndex * spriteWidth, 0, spriteWidth, source.Height),
                GraphicsUnit.Pixel);
            }

            graphics.Dispose();
            source.Dispose();

            return sprite;
        }
    }
}
