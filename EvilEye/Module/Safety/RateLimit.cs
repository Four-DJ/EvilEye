using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.SDKBase;

namespace EvilEye.Module.Safety
{
    class RateLimit : BaseModule, OnEventEvent, OnRPCEvent
    {
        public RateLimit() : base("RateLimit", "Anti for EventSpamm related Exploits", Main.Instance.safetyButton, Main.Instance.quickMenuStuff.Button_SafetyIcon.sprite, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onEventEvents.Add(this);
            Main.Instance.onRPCEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onEventEvents.Remove(this);
            Main.Instance.onRPCEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8 || eventData.Code == 33 || eventData.Code == 253 || eventData.Code == 254)
                return true;
            if (lastCode == eventData.Code)
            {
                if (count > 50)
                {
                    if (!blockedEvents.Contains(eventData.Code))
                    {
                        blockedEvents.Add(eventData.Code);
                    }
                }
                else
                {
                    count++;
                }
            }
            else
            {
                if (blockedEvents.Contains(lastCode))
                {
                    blockedEvents.Remove(lastCode);
                }
                lastCode = eventData.Code;
                count = 0;
            }
            if (blockedEvents.Contains(eventData.Code))
            {
                LoggerUtill.Log("[Safety] Blocked spammed event " + eventData.Code + " from " + PlayerWrapper.GetPlayerWithPlayerID(eventData.sender)?.prop_APIUser_0.displayName);
                return false;
            }

            return true;
        }

        public bool OnRPC(Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            if (sender == VRC.Player.prop_Player_0)
                return true;
            if (lastEvent == vrcEvent)
            {
                if (eventCount > 50)
                {
                    if (!blockedRPCEvents.Contains(vrcEvent))
                    {
                        blockedRPCEvents.Add(vrcEvent);
                    }
                }
                else
                {
                    eventCount++;
                }
            }
            else
            {
                if (blockedRPCEvents.Contains(vrcEvent))
                {
                    blockedRPCEvents.Remove(vrcEvent);
                }
                lastEvent = vrcEvent;
                eventCount = 0;
            }
            if (blockedRPCEvents.Contains(vrcEvent))
            {
                LoggerUtill.Log("[Safety] Blocked spammed rpc " + vrcEvent.ParameterString + " from " + sender.prop_APIUser_0.displayName);
                return false;
            }
            return true;
        }

        List<VRC_EventHandler.VrcEvent> blockedRPCEvents = new List<VRC_EventHandler.VrcEvent>();
        VRC_EventHandler.VrcEvent lastEvent;
        int eventCount;

        List<byte> blockedEvents = new List<byte>();
        byte lastCode;
        int count;
    }
}
