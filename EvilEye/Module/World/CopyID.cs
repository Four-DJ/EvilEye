using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvilEye.Module.World
{
    class CopyID : BaseModule
    {
        public CopyID() : base("CopyID","Copy the InstanceID", Main.Instance.worldButton, null) { }

        public override void OnEnable()
        {

        }
    }
}
