using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class AssignBystander : BaseModule
    {
        public AssignBystander() : base("Assign Bystander", "Makes You A Bystander", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {
            
        }
    }
}
