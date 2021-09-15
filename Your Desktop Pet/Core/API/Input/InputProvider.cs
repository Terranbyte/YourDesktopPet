using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

// Mouse input: https://stackoverflow.com/questions/1316681/getting-mouse-position-in-c-sharp

namespace Your_Desktop_Pet.Core.API.Input
{
    class InputProvider
    {
        private static Key _lastKey = Key.None;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public static void Update()
        {
            if (_lastKey != Key.None && Keyboard.IsKeyUp(_lastKey))
                _lastKey = Key.None;
        }

        public static Point GetMousePos()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }

        public static bool MouseButtonDown(int side)
        {
            if (side == 0)
                return Mouse.LeftButton.HasFlag(MouseButtonState.Pressed);
            
            return Mouse.RightButton.HasFlag(MouseButtonState.Pressed);
        }

        public static bool MouseButtonUp(int side)
        {
            if (side == 0)
                return Mouse.LeftButton.HasFlag(MouseButtonState.Released);

            return Mouse.RightButton.HasFlag(MouseButtonState.Released);
        }

        public static bool IsKeyDown(string key)
        {
            Key currentKey = GetKey(key);
            bool result = Keyboard.IsKeyDown(currentKey) && currentKey != _lastKey;

            if (result)
                _lastKey = currentKey;
            
            return result;
        }

        public static bool IsKeyHeld(string key)
        {
            return Keyboard.IsKeyDown(GetKey(key));
        }

        public static bool IsKeyUp(string key)
        {
            return Keyboard.IsKeyUp(GetKey(key));
        }

        private static Key GetKey(string key)
        {
            if (key.Length == 1)
            {
                if (char.IsLetter(Convert.ToChar(key)))
                    return (Key)Enum.Parse(typeof(Key), key.ToString().ToUpper());
                else if (char.IsNumber(Convert.ToChar(key)))
                    return (Key)Enum.Parse(typeof(Key), (Convert.ToInt32(key) - 14).ToString());
            }

            if (key == "Space")
                return Key.Space;

            throw new ArgumentException($"Tried to get input from the key \"{key}\". Input is only allowed from alphanumeric and whitespace characters.");
        }
    }
}
