using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World.World_Hacks.Murder
{
    class GameMurderWin : BaseModule
    {
        public GameMurderWin() : base("Murder Win", "Ends Game And Says The Murder Wins", Main.Instance.murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {

        }
    }
}
