using EvilEye.SDK;
using System;

namespace EvilEye.Module.World
{
    class CopyUserID : BaseModule
    {
        public CopyUserID() : base("Get User ID", "Copy the UserID to clipboard", Main.Instance.playerButton, null) { }

        public override void OnEnable()
        {
            if (PlayerWrapper.GetUserID != "")
                Misc.SetClipboard(PlayerWrapper.GetUserID);
            LoggerUtill.Log("User ID: " + PlayerWrapper.GetUserID + " Copied to clipboard.", ConsoleColor.Green);
        }

    }
}
