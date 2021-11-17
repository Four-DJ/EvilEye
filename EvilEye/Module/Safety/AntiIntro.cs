using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.AvatarFavorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EvilEye.Module.Safety
{
    class AntiIntro : BaseModule, OnPlayerJoinEvent
	{
        public AntiIntro() : base("Anti Intro", "No Avatar Intro Music", Main.Instance.safetyButton, null, true, true)
        {
        }

        public override void OnEnable()
        {
        }

        public override void OnDisable()
        {
        }
		 
        public void OnPlayerJoin(VRC.Player player)
        {
            //just put this together you can fix it if you want idk it wont call to byesound but i added other shit too like by cloth and partcle and mesh
   //         bool flag = player.UserID() == player.prop_APIUser_0.id;

   //         if (flag)
			//{
			//	GameObject.ByeSound();
			//}
		}
    }
}
