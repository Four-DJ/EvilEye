using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace EvilEye.SDK
{
    static class PlayerWrapper
    {
        public static Player[] GetAllPlayers() { return PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0; }
      
        public static void Teleport(this Player player) => LocalVRCPlayer.transform.position = player.prop_VRCPlayer_0.transform.position;

        public static string GetUserID => GetAPIUser(LocalPlayer).id; 
        
        public static Player GetByUsrID(string usrID)
        {
            return GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
        }
        
        public static Player LocalPlayer
        {
            get
            {
                return Player.prop_Player_0;
            }
        }

        public static VRCPlayer LocalVRCPlayer
        {
            get
            {
                return VRCPlayer.field_Internal_Static_VRCPlayer_0;
            }
        }

        public static APIUser GetAPIUser(this VRC.Player player)
        {
            return player.prop_APIUser_0;
        }

        public static string GetFrames(this Player player)
        {
            float fps = (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
            if(fps > 80)
                return "<color=green>" + fps + "</color>";
            else if (fps > 30)
                return "<color=yellow>" + fps + "</color>";
            else
                return "<color=red>" + fps + "</color>";
        }

        public static string GetPing(this Player player)
        {
            short ping = player._playerNet.field_Private_Int16_0;
            if (ping > 150)
                return "<color=red>" + ping + "</color>";
            else if (ping > 75)
                return "<color=yellow>" + ping + "</color>";
            else
                return "<color=green>" + ping + "</color>";
        }

        public static string GetPlatform(this Player player)
        {
            if (player.prop_APIUser_0.IsOnMobile)
            {
                return "<color=green>Q</color>";
            } else if (player.prop_VRCPlayerApi_0.IsUserInVR()) 
            {
                return "<color=yellow>V</color>";
            }
            else
            {
                return "<color=grey>PC</color>";
            }
        }
        public static int GetActorNumber(this Player player)
        {
            return player.GetVRCPlayerApi().playerId;
        }
        public static PlayerManager PManager
        {
            get
            {
                return PlayerManager.field_Private_Static_PlayerManager_0;
            }
        }
        public static List<Player> AllPlayers
        {
            get
            {
                return PlayerWrapper.PManager.field_Private_List_1_Player_0.ToArray().ToList<Player>();
            }
        }
        public static VRC.Player FetchPlayerWithNumber(int actorNumber)
        {
            foreach (VRC.Player player in PlayerManager.Method_Public_Static_ArrayOf_Player_0())
            {
                bool flag = player.Method_Public_Int32_0() == actorNumber; 
                if (flag)
                {
                    return player;
                }
            }
            return null;
        }
        public static Player GetPlayer2(int ActorNumber)
        {
            return (from p in PlayerWrapper.AllPlayers
                    where p.GetActorNumber() == ActorNumber
                    select p).FirstOrDefault<Player>();
        }
        public static IUser GetSelectedUser(this SelectedUserMenuQM selectMenu)
        {
            return selectMenu.field_Private_IUser_0;
        }

        public static void SetHide(this VRCPlayer Instance, bool State)
        {
            Instance._player.SetHide(State);
        }
        internal static VRCPlayer GetCurrentPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }
        public static Player GetPlayer(this VRCPlayer player)
        {
            return player.prop_Player_0;
        }
        public static APIUser GetAPIUser(this VRCPlayer Instance)
        {
            return Instance.GetPlayer().GetAPIUser();
        }
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance)
        {
            return (Instance == null) ? null : Instance.prop_VRCPlayerApi_0;
        }
        public static string UserID(this VRCPlayer Instance)
        {
            return Instance.GetAPIUser().id;
        }
        public static bool GetIsMaster(this Player Instance)
        {
            return Instance.GetVRCPlayerApi().isMaster;
        }
        public static string GetName(this Player player)
        {
            return player.GetAPIUser().displayName;
        }
        public static void DelegateSafeInvoke(this Delegate @delegate, params object[] args)
        {
            Delegate[] invocationList = @delegate.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                try
                {
                    invocationList[i].DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    LoggerUtill.Log("Error while executing delegate:\n" + ex.ToString());
                }
            }
        }
        public static void SetHide(this Player Instance, bool State)
        {
            Instance.transform.Find("ForwardDirection").gameObject.active = !State;
        }

        public static void ChangeAvatar(string AvatarID)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
            {
                id = AvatarID
            };
            component.ChangeToSelectedAvatar();
        }

        public static Player GetPlayerWithPlayerID(int playerID)
        {
            for(int i = 0; i < GetAllPlayers().Length; i++)
            {
                if(GetAllPlayers()[i].prop_VRCPlayerApi_0.playerId == playerID)
                {
                    return GetAllPlayers()[i];
                }
            }

            return null;
        }
    }
}
