using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class AssignMurder : BaseModule
    {
        public AssignMurder() : base("Assign Murder", "Makes You A Murder", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {
          
        }
    }
}
