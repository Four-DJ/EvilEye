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
        public static bool block9;

        public Anti9() : base("Event9\nDisabled", "Anti for the Event9 Exploit", Main.Instance.safetyButton, null, true, true)
        {
            Anti9.block9 = !Anti9.block9;
            this.name = $"Event9\n{(Anti9.block9 ? "Enabled" : "Disabled")}";
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


            if (eventData.Code == 9 && block9) return false;

            if (eventData.Code == 9)
            {
                if (eventData.Parameters[245].ToString().Length > 150)
                {
                    //LoggerUtill.Log("[Safety] Blocked Invalid 9");
                    return false;
                }
                
                return false;
            }

            return true;
        }
    }
}
