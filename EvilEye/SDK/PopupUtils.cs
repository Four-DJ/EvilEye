using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.Core;

namespace WatchDogs.Utils
{
    static class PopupUtils
    {
        public static void Init()
        {
            popupV2 = typeof(VRCUiPopupManager).GetMethods().First(mb => mb.Name.StartsWith("Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_") && !mb.Name.Contains("PDM") && XrefUtils.CheckMethod(mb, "UserInterface/MenuContent/Popups/StandardPopupV2"));
        }

        public static void PopupV2(string title, string description, string leftButtonText, Action leftButtonClick, string rightButtonText, Action rightButtonClick, Action<VRCUiPopup> additionalSetup = null)
        {
            popupV2.Invoke(VRCUiPopupManager.prop_VRCUiPopupManager_0, new object[7] { title, description, leftButtonText, (Il2CppSystem.Action)leftButtonClick, rightButtonText, (Il2CppSystem.Action)rightButtonClick, (Il2CppSystem.Action<VRCUiPopup>)additionalSetup });
        }

		public static void SelectPlayer(this QuickMenu instance, Player player) => instance.Method_Public_Void_Player_0(player);

		public static void ClosePopup()
		{
			try
			{
				VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Private_Void_0();
			}
			catch { }
		}

		public static void ClearHudMessage()
		{
			VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0 = new Il2CppSystem.Collections.Generic.List<string>();
		}

		public static void QueHudMessage(string Message)
		{
			VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Add(Message);
		}

		public static void RenderElement(this UiVRCList uivrclist, Il2CppSystem.Collections.Generic.List<ApiAvatar> AvatarList)
		{
			if (!uivrclist.gameObject.activeInHierarchy || !uivrclist.isActiveAndEnabled || uivrclist.isOffScreen || !uivrclist.enabled)
			{
				return;
			}
			uivrclist.Method_Protected_Void_List_1_T_Int32_Boolean_VRCUiContentButton_0(AvatarList, 0, true);
		}


		private static MethodInfo popupV2;
    }
}
