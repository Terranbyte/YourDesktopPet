using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Your_Desktop_Pet.Core.Pet;

namespace Your_Desktop_Pet
{
    public class test : LuaObject
    {
        public test(string name, Vector2 position, AnchorPoint anchor = AnchorPoint.TopLeft) : base(name, position, anchor)
        {
        }
    }
}
