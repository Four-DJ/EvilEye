using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.SDK;
using EvilEye.Module;
using EvilEye.Events;

namespace EvilEye.Module.Player
{
    class XBoxMic : BaseModule
    {
        public XBoxMic() : base("XboxMic", "1v1 in COD bro", Main.Instance.playerButton, null, true) { }
          
        

        public override void OnEnable()
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_8K;
        }

        public override void OnDisable()
        {
        
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }

    
    }
}
