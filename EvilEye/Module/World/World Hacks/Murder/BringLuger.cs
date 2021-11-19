using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class BringLuger : BaseModule
    {
        public BringLuger() : base("Give Luger", "Teleports Luger To Your Position", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
