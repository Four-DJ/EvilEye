using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using ExitGames.Client.Photon;
using UnhollowerBaseLib;

namespace EvilEye.Module.Safety
{
    class Anti7 : BaseModule, OnEventEvent
    {
        public Anti7() : base("Anti7", "Prevents Menu Exploit", Main.Instance.safetyButton, null, true, true)
        {
        }
        public override void OnEnable()
        {
            Main.Instance.onEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onEventEvents.Remove(this);
        }
        public bool OnEvent(EventData eventData)
        {
            if (eventData.Code == 7)
            {
                var bytes = eventData.CustomData.Cast<Il2CppArrayBase<byte>>();
                if (bytes.Length > 300)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
