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
                PlayerWrapper.ChangeAvatar(GetClipboard());
            }
            else
            {
               LoggerUtill.Log("ID not a Avatar", ConsoleColor.Red);
            }
        }
        private string GetClipboard()
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
    }
}
