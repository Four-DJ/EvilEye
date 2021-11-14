using EvilEye.Events;
using EvilEye.SDK;
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
        //everything is added i just dont know how to link the join and leave log to this shit lol Four you can fix it its all coded and patches are done and fixed
        public bool OnPlayerJoin(VRC.Player player)
        {
            LoggerUtill.Log($"Player Joined ~> Username: {player.prop_APIUser_0.displayName} | Photon ID: {player.prop_VRCPlayerApi_0.playerId} | UserID: {player.prop_APIUser_0.id}");
            return true;    
        }
        public bool OnPlayerLeave(VRC.Player player)
        {
            LoggerUtill.Log($"Player Left ~> Username: {player.prop_APIUser_0.displayName} | UserID: {player.prop_APIUser_0.id}");
            return true;
        }
    }
}
