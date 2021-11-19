using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class BringKnifes : BaseModule
    {
        public BringKnifes() : base("Give Knifes", "Teleports Knifes To Your Position", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
