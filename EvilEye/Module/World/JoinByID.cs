using EvilEye.SDK.ButtonAPI;
using System;
using System.Windows.Forms;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;

namespace EvilEye.Module.World
{
    class JoinByID : BaseModule
    {
        public JoinByID() : base("JoinByID", "Make Sure To Copy A World ID To Your Clipboard Before Clicking", Main.Instance.worldButton, null) { }

        public override void OnEnable()
        {
			string id = GetClipboard();
			bool flag = !Networking.GoToRoom(id);
			if (flag)
			{
				string[] array = id.Split(new char[]
				{
					':'
				});
				bool flag2 = array.Length != 2;
				if (flag2)
				{ 
				}
				new PortalInternal().Method_Private_Void_String_String_PDM_0(array[0], array[1]);
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
	}
}
