using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WatchDogs2.Events;

namespace WatchDogs2.Modules.Player
{
    class HeadFlipper : WDModule, OnUpdateEvent
    {

        public HeadFlipper() : base("HeadFlipper", "Rotate", Categories.Player) { }
        Vector3 original;
        NeckMouseRotator neck;
        public override void OnEnable()
        {
            Main.Instance.onUpdateEvents.Add(this);
            neck = GameObject.Find("/_Application/TrackingVolume/TrackingSteam(Clone)/SteamCamera/[CameraRig]/Neck").GetComponent<NeckMouseRotator>();
            original = neck.field_Public_Vector3_0;
        }

        public override void OnDisable()
        {
            Main.Instance.onUpdateEvents.Remove(this);
            neck.field_Public_Vector3_0 = new Vector3(-70, 0, 80);
        }

        public void OnUpdate()
        {
            neck.field_Public_Vector3_0 = original * 4;
        }

    }
}
