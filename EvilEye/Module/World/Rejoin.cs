using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.World
{
    class Rejoin : BaseModule
    {
        public Rejoin() : base("Rejoin", "Rejoin the World", Main.Instance.worldButton, Main.Instance.quickMenuStuff.Button_WorldsIcon.sprite) { }

        public override void OnEnable()
        {
            //VRCFlowManager.prop_VRCFlowManager_0.Method_Public_Void_String_WorldTransitionInfo_Action_1_String_Boolean_0(RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + RoomManager.field_Internal_Static_ApiWorldInstance_0.instanceId);
        }
    }
}
