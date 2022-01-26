using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Helpers
{
    public class SpriteHelper
    {
        public static Image GetSprite(string source, InterpolationMode interpMode = InterpolationMode.NearestNeighbor)
        {
            // Draw to a new image to enable releasing the handle to the sprite's file
            Image image = Image.FromFile(source);
            Image sprite = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(sprite))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.DrawImage(image, new Rectangle(0, 0, sprite.Width, sprite.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                g.Save();
            }

            image.Dispose();

            return sprite;
        }

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
                graphics.DrawImage(source, spriteSize,
                    new Rectangle(i * spriteWidth, 0, spriteWidth, source.Height),
                    GraphicsUnit.Pixel);
                graphics.Save();
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
            if (totalSprites == 1)
                return source;

            int spriteWidth = source.Width / totalSprites;
            Image sprite = new Bitmap(spriteWidth, source.Height);

            Graphics graphics = Graphics.FromImage(sprite);
            graphics.InterpolationMode = interpMode;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.DrawImage(source, new Rectangle(0, 0, spriteWidth, source.Height),
                new Rectangle(spriteIndex * spriteWidth, 0, spriteWidth, source.Height),
                GraphicsUnit.Pixel);
            graphics.Save();
            graphics.Dispose();

            source.Dispose();

            return sprite;
        }
    }
}
