using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using EvilEye.SDK;

namespace EvilEye.SDK
{
    class WorldWrapper
    {
        public static VRC_Pickup[] vrc_Pickups;
        public static PhotonView[] photonViews;
       
        public static string GetWorldID => PlayerWrapper.GetAPIUser(PlayerWrapper.LocalPlayer).location; //four expression body https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
       
        public static void Init()
        {
            vrc_Pickups = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
            photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
        }
    }
}
