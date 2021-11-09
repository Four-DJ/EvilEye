using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace EvilEye.SDK
{
    static class PlayerWrapper
    {
        public static Player[] GetAllPlayers() { return PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0; }

        public static Player GetByUsrID(string usrID)
        {
            return GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
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

        public static IUser GetSelectedUser(this SelectedUserMenuQM selectMenu)
        {
            return selectMenu.field_Private_IUser_0;
        }

        public static void SetHide(this VRCPlayer Instance, bool State)
        {
            Instance._player.SetHide(State);
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
