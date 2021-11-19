using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class BringShotgun : BaseModule
    {
        public BringShotgun() : base("Give Shotgun", "Teleports Shotgun To Your Position", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
