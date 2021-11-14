using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using EvilEye.SDK.Photon;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Safety
{
    class Anti9 : BaseModule, OnEventEvent
    {
        public Anti9() : base("Event9", "Anti for the Event9 Exploit", Main.Instance.safetyButton, null, true, true)
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
            if (eventData.Code == 9)
            {
                if (eventData.Parameters[245].ToString().Length > 150)
                {
                    LoggerUtill.Log("[Safety] Blocked Invalid 9");
                    return false;
                }
                
                return false;
            }

            return true;
        }
    }
}
