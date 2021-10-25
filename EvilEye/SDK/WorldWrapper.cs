using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace EvilEye.SDK
{
    class WorldWrapper
    {
        public static VRC_Pickup[] vrc_Pickups;
        public static PhotonView[] photonViews;

        public static void Init()
        {
            vrc_Pickups = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
            photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
        }
    }
}
