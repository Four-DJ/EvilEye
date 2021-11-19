using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class GameBystanderWin : BaseModule
    {
        public GameBystanderWin() : base("Bystander Win", "Ends Game And Says Bystander's Win", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
