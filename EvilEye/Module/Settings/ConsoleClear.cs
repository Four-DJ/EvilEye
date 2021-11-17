using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class ConsoleClear : BaseModule
    {
        public ConsoleClear() : base("Clear Console", "Clears Melon Loader Console", Main.Instance.settingsButton, null, false)
        {
        }
        public override void OnEnable()
        {
            Console.Clear();
            LoggerUtill.Log("Cleared Console!", Console.ForegroundColor);
        }
    }
}
