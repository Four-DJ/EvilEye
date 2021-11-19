using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class KillAll : BaseModule
    {
        public KillAll() : base("Kill Everyone", "Everyone Die's", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
