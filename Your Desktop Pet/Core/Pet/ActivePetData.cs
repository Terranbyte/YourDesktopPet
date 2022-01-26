using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Pet
{
    internal class ActivePetData
    {
        public Image icon;
        public string name;
        public Vector2 position;

        public ActivePetData(Image icon, string name, Vector2 position)
        {
            this.icon = icon;
            this.name = name;
            this.position = position;
        }
    }
}
