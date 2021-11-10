using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogs2.Events;
using WatchDogs2.Settings;

namespace WatchDogs2.Modules.Player
{
    class XBoxMic : WDModule, OnUpdateEvent
    {
        public XBoxMic() : base("XBoxMic", "CoD lobby", Categories.Player, new Setting[] { new Setting("Bitrate",new object[] {
            BitRate.BitRate_8K,
            BitRate.BitRate_10K,
            BitRate.BitRate_16K,
            BitRate.BitRate_18K,
            BitRate.BitRate_20K,
            BitRate.BitRate_24K,
            BitRate.BitRate_32K,
            BitRate.BitRate_48K,
            BitRate.BitRate_64k,
            BitRate.BitRate_96k,
            BitRate.BitRate_128k,
            BitRate. BitRate_256k,
            BitRate.BitRate_384k,
            BitRate.BitRate_512k
        },0) }) { }

        public override void OnEnable()
        {
            Main.Instance.onUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onUpdateEvents.Remove(this);
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }

        public void OnUpdate()
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = (BitRate)settings[0].o_val[settings[0].selected];
        }
    }
}
