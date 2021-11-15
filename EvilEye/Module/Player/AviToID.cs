using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;
using EvilEye.SDK;

namespace EvilEye.Module.Player
{
    class AviToID : BaseModule
    {
        public AviToID() : base("Change By\nAvi ID", "From Clipboard", Main.Instance.playerButton, null, false) { }
        public override void OnEnable()
        {
            bool flag = GetClipboard().StartsWith("avtr");
            if (flag)
            {
                avatarbyid(GetClipboard());
            }
            else
            {
               LoggerUtill.Log("ID not a Avatar", ConsoleColor.Red);
            }
        }
        internal static string GetClipboard()
        {
            bool flag = Clipboard.ContainsText();
            string result;
            if (flag)
            {
                result = Clipboard.GetText();
            }
            else
            {
                result = null;
            }
            return result;
        }
        internal static void avatarbyid(string AVI)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
            {
                id = AVI
            };
            component.ChangeToSelectedAvatar();
        }
    }
}
