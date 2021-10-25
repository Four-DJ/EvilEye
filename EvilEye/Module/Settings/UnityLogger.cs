using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class UnityLogger : BaseModule
    {
        public static UnityLogger Instance;

        public UnityLogger() : base("UnityLogger", "Logs Unity Debug", Main.Instance.settingsButton, Main.Instance.quickMenuStuff.Button_RespawnIcon.sprite, true, true)
        {
            Instance = this;
        }
    }
}
