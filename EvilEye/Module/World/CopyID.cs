using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvilEye.Module.World
{
    class CopyID : BaseModule
    {
        public CopyID() : base("CopyID","Copy the InstanceID", Main.Instance.worldButton, null) { }

        public override void OnEnable()
        {
			SetClipboard(SDK.PlayerWrapper.GetAPIUser(SDK.PlayerWrapper.LocalPlayer).location);
		}
		internal static void SetClipboard(string Set)
		{
			bool flag = Clipboard.ContainsText();
			if (flag)
			{
				Clipboard.Clear();
				Clipboard.SetText(Set);
			}
			else
			{
				Clipboard.SetText(Set);
			}
		}
	}
}
