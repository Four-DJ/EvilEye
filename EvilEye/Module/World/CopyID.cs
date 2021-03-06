using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRC.Core;

namespace EvilEye.Module.World
{
    class CopyWID : BaseModule
    {
        public CopyWID() : base("Get World ID", "Copy the World + InstanceID", Main.Instance.worldButton, null, false) { }

        public override void OnEnable()
        {
           if(WorldWrapper.GetWorldID != "")
                Misc.SetClipboard(PlayerWrapper.GetAPIUser(PlayerWrapper.LocalPlayer).location);
	        LoggerUtill.Log("World ID: " + WorldWrapper.GetWorldID + " copied to clipboard.", ConsoleColor.Green);
        }
 
	}
}
