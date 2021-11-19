using EvilEye.Events;
using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class UdonLogger : BaseModule, OnUdonEvent
    {
        public UdonLogger() : base("UdonLogger", "Logs Udon Events", Main.Instance.settingsButton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onUdonEvents.Remove(this);
        }
        public bool OnUdon(string __0, VRC.Player __1)
        {
            LoggerUtill.Log("Type: " + __0 + " | From: " + __1.field_Private_APIUser_0.displayName);
            return true;
        }
    }
}
