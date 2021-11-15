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
    class CopyID : BaseModule
    {
        public CopyID() : base("CopyID", "Copy the InstanceID", Main.Instance.worldButton, null) { }

        public override void OnEnable()
        {
           if(WorldWrapper.GetWorldID != "")
                Misc.SetClipboard(WorldWrapper.GetWorldID);
	        LoggerUtill.Log("World ID: " + WorldWrapper.GetWorldID + "Copied to clipboard.", ConsoleColor.Green);
        }
 
	}
}
