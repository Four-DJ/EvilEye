using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogs2.Modules.Player
{
    class LoudMic : WDModule
    {
        public LoudMic() : base("LoudMic", "Scream", Categories.Player) { }

        public override void OnEnable()
        {
            USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
        }

        public override void OnDisable()
        {
            USpeaker.field_Internal_Static_Single_1 = 1;
        }
    }
}
