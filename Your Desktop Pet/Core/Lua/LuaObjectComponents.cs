using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Lua
{
    [Flags]
    public enum LuaObjectComponents
    {
        None = 0,
        LuaObject = 1,
        Sprite = 2,
        Animator = 4,
        PetObject = 8,
    }
}
