using MoonSharp.Interpreter;
using System;
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

        public static Rectangle TableToRect(Table table)
        {
            Rectangle rect = new Rectangle();

            rect.X = Convert.ToInt32(table["x"]);
            rect.Y = Convert.ToInt32(table["y"]);
            rect.Width = Convert.ToInt32(table["w"]);
            rect.Height = Convert.ToInt32(table["h"]);

            return rect;
        }

        public static Rectangle CircleToRect(int x, int y, int w, int h)
        {
            return new Rectangle(x - (w / 2), y - (h / 2), w, h);
        }

        public static Rectangle CircleToRect(Rectangle r)
        {
            return new Rectangle(r.X - (r.Width / 2), r.Y - (r.Height / 2), r.Width, r.Height);
        }

        public static Table PointToTable(Point p, Script owner)
        {
            Table table = new Table(owner);

            table["x"] = p.X;
            table["y"] = p.Y;

            return table;
        }

        public static Point TableToPoint(Table table)
        {
            Point p = new Point();

            p.X = (int)table["x"];
            p.Y = (int)table["y"];

            return p;
        }

        public static Table RGBToTable(int r, int g, int b, Script owner)
        {
            Table table = new Table(owner);

            table["r"] = r;
            table["g"] = g;
            table["b"] = b;

            return table;
        }

        public static Table RGBToTable(Color c, Script owner)
        {
            Table table = new Table(owner);

            table["r"] = c.R;
            table["g"] = c.G;
            table["b"] = c.B;

            return table;
        }

        public static Color TableToRGB(Table a)
        {
            return Color.FromArgb(Convert.ToInt32(a["r"]), Convert.ToInt32(a["g"]), Convert.ToInt32(a["b"]));
        }

        public static Table AABBFromXYWH(int x, int y, int w, int h, Script owner)
        {
            Rectangle rect = new Rectangle();

            rect.X = x;
            rect.Y = y;
            rect.Width = w;
            rect.Height = h;

            return RectToTable(rect, owner);
        }

        public static bool AABBColliding(Table a, Table b)
        {
            return (double)a["x"] < (double)b["x"] + (double)b["w"] &&
                    (double)a["x"] + (double)a["w"] > (double)b["x"] &&
                    (double)a["y"] < (double)b["y"] + (double)b["h"] &&
                    (double)a["y"] + (double)a["h"] > (double)b["y"];
        }
    }
}
