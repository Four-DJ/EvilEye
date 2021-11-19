using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class GameAbort : BaseModule
    {
        public GameAbort() : base("Abort Game", "Ends Game And Says Nobody Wins", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
