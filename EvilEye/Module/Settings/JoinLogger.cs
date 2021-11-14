using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class JoinLogger : BaseModule
    {
        public JoinLogger() : base("Join/Leave Log", "Logs Players Joining And Leaving", Main.Instance.settingsButton, null, true, true)
        {
        }

        public override void OnEnable()
        { 
        }

        public override void OnDisable()
        { 
        }
    }
}
