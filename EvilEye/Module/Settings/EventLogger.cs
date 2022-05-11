using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using EvilEye.SDK.PhotonSDK;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.SDK.PhotonSDK;

namespace EvilEye.Module.Settings
{
    class EventLogger : BaseModule, OnEventEvent
    {
        public EventLogger() : base("EventLogger", "Logs Photon Events", Main.Instance.settingsButton, null, true, true)
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
            if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8)
                return true;
            Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;
            byte code = eventData.Code;
            int sender = eventData.Sender;
            VRC.Player player = PlayerWrapper.GetPlayerByActorID(sender);
            string arg = player != null ? player.prop_APIUser_0.displayName : "Server";
            string arg2 = "";
            if (parameters != null)
                arg2 = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(parameters), Formatting.Indented);
            LoggerUtill.Log($"[OPLog] {arg} sended {code} {arg2}");

            return true;
        }
    }
}
