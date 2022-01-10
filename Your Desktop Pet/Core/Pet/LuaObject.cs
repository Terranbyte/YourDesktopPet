using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Pet
{
    public enum AnchorPoint
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

    public class LuaObject
    {
        public string name;
        public AnchorPoint anchor = AnchorPoint.TopLeft;

        protected Vector2 _position = Vector2.Zero;
        protected Vector2 _size = Vector2.One;
        protected readonly string _guid;

        protected readonly KeyValuePair<float, float>[] _offsetLUT = new KeyValuePair<float, float>[9]
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

        public LuaObject(string name, Vector2 position, AnchorPoint anchor)
        {
            this.name = name;
            _position = position;
            this.anchor = anchor;

            _guid = Guid.NewGuid().ToString();
        }

        public virtual void SetPosition(float x, float y)
        {
            KeyValuePair<float, float> o = _offsetLUT[(int)anchor];

            _position = new Vector2((int)Math.Round(x - (_size.X * o.Key)), (int)Math.Round(y - (_size.Y * o.Value)));
        }

        public virtual void SetPosition(float x, float y, AnchorPoint overrideAnchor)
        {
            KeyValuePair<float, float> o = _offsetLUT[(int)overrideAnchor];

            _position = new Vector2(x - (int)(_size.X * o.Key), y - (int)(_size.Y * o.Value));
        }

        public virtual void SetSize(int x, int y)
        {
            _size = new Vector2(x, y);
        }
    }
}
