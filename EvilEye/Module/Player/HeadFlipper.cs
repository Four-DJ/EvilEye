//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using UnityEngine;
using EvilEye.SDK;
using EvilEye.Module;
using EvilEye.Events;
using VRC.DataModel;

namespace EvilEye.Module.Player
{
    class HeadFlipper : BaseModule
    {

        public HeadFlipper() : base("HeadFlipper", "Fuck your desktop neck", Main.Instance.playerButton, null, true) { }
        NeckRange original;
        MonoBehaviourPublicObSiBoSiVeBoQuVeBoSiUnique neck;
        public override void OnEnable()
        {
            neck = GameObject.Find("/_Application/TrackingVolume/TrackingSteam(Clone)/SteamCamera/[CameraRig]/Neck").GetComponent<MonoBehaviourPublicObSiBoSiVeBoQuVeBoSiUnique>();
            original = neck.field_Public_NeckRange_0;
            neck.field_Public_NeckRange_0 = new NeckRange(-360, 360, 0);
        }

        public override void OnDisable()
        {
            neck.field_Public_NeckRange_0 = original;
        }
    }
}
