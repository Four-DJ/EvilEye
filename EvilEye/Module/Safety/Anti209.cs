using EvilEye.Events;
using EvilEye.SDK;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Safety
{
    class Anti209 : BaseModule, OnEventEvent
    {
        public Anti209() : base("Event209", "Anti for the Event209 Exploit", Main.Instance.safetyButton, Main.Instance.quickMenuStuff.Button_SafetyIcon.sprite, true, true)
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
            if (eventData.Code == 209)
            {
                LoggerUtill.Log("[Safety] Blocked Invalid 209");
                return false;
            }

            return true;
        }
    }
}
