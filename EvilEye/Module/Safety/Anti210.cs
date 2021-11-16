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
using UnhollowerBaseLib;

namespace EvilEye.Module.Safety
{
    class Anti210 : BaseModule, OnEventEvent
    {
        public Anti210() : base ("Event210", "Anti for the Event210 Exploit", Main.Instance.safetyButton, null, true, true)
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
            if (eventData.Code == 210) 
            {
                VRC.Player player = PlayerWrapper.GetPlayerWithPlayerID(eventData.sender);
                if(player == null)
                {
                    //LoggerUtill.Log("[Safety] Blocked Invalid 210");
                    return false;
                }
                Il2CppStructArray<int> il2CppStructArray = eventData.Parameters[245].TryCast<Il2CppStructArray<int>>();
                if (il2CppStructArray[1] != player.prop_VRCPlayerApi_0.playerId)
                {
                    //LoggerUtill.Log("[Safety] Blocked Invalid 210");
                    return false;
                }
            }
            
            return true;
        }

    }
}
