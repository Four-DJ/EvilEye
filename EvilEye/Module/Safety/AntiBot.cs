using EvilEye.Events;
using ExitGames.Client.Photon;
using System;
using MelonLoader;
using UnityEngine; 
using EvilEye.SDK;

namespace EvilEye.Module.Safety
{
    class AntiBot : BaseModule, OnEventEvent
    {
        public AntiBot() : base("Anti PhotonBot", "Detects PhotonBots In Lobby And Blocks There Events", Main.Instance.safetyButton, null, true, true)
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
            bool flag = eventData.Code == 1 || eventData.Code == 6 || eventData.Code == 9 || eventData.Code == 209 || eventData.Code == 210;
            if (flag)
            {
                VRC.Player player = PlayerWrapper.FetchPlayerWithNumber(eventData.Sender);
                bool flag2 = player._playerNet.field_Private_Int16_0 <= 0 && 1000 / (int)player._playerNet.field_Private_Byte_0 <= 0;
                if (flag2)
                {
                    LoggerUtill.Log($"Blocked Event {eventData.Code} from {player.field_Private_APIUser_0.displayName} | Reason: Player Detected As Photon Bot", ConsoleColor.DarkRed);
                    return false;
                }
                bool flag3 = player.transform.position == Vector3.zero;
                if (flag3)
                {
                    LoggerUtill.Log($"Blocked Event {eventData.Code} from {player.field_Private_APIUser_0.displayName} | Reason: Player Detected As Photon Bot", ConsoleColor.DarkRed);
                    return false;
                }
            }
            return true;
        }
    }
}
