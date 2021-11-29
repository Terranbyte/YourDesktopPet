using MoonSharp.Interpreter;
using System.Drawing;

namespace Your_Desktop_Pet.Core.Lua
{
    public static class LuaHelper
    {
        public static Table RectToTable(Rectangle rect, Script owner)
        {
            Table table = new Table(owner);

            table["x"] = rect.X;
            table["y"] = rect.Y;
            table["w"] = rect.Width;
            table["h"] = rect.Height;

            return table;
        }

        public static Table PointToTable(Point p, Script owner)
        {
            Table table = new Table(owner);

            table["x"] = p.X;
            table["y"] = p.Y;

            return table;
        }

        public static Rectangle TableToRect(Table table)
        {
            Rectangle rect = new Rectangle();

            rect.X = (int)table["x"];
            rect.Y = (int)table["y"];
            rect.Width = (int)table["w"];
            rect.Height = (int)table["h"];

            return rect;
        }

        public static Point TableToPoint(Table table)
        {
            Point p = new Point();

            p.X = (int)table["x"];
            p.Y = (int)table["y"];

            return p;
        }
    }
}
