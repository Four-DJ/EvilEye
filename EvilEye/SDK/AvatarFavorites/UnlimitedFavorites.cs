using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogs.Utils;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.UI;
using WatchDogs.Modules.Events;

namespace WatchDogs.Modules.UI
{
    class UnlimitedFavorites : OnUpdateEvent, OnUIEvent
    {
        private GameObject avatarPage;
        private UiAvatarList newFavList;
        private Il2CppSystem.Collections.Generic.List<ApiAvatar> favedAvatars = new Il2CppSystem.Collections.Generic.List<ApiAvatar>();
        bool JustOpened;

        public UnlimitedFavorites()
        {
            Main.Instance.onUIEvents.Add(this);
            Main.Instance.onUpdateEvents.Add(this);
        }

        public void OnUpdate()
        {
            if (RoomManager.field_Internal_Static_ApiWorldInstance_0 == null)
                return;

            if (avatarPage.activeSelf && !JustOpened)
            {
                JustOpened = true;
                MelonCoroutines.Start(RefreshMenu(1f));
            }
            else if (!avatarPage.activeSelf && JustOpened)
                JustOpened = false;
        }

        public void UI()
        {
            if (!File.Exists("WATCHDOGS/Favourite.txt"))
                File.Create("WATCHDOGS/Favourite.txt");

            avatarPage = GameObject.Find("UserInterface/MenuContent/Screens/Avatar");
            newFavList = VRCUiManager.prop_VRCUiManager_0.field_Public_GameObject_0.transform.Find("Screens/Avatar/Vertical Scroll View/Viewport/Content/Legacy Avatar List").gameObject.GetComponent<UiAvatarList>();
            newFavList.transform.SetAsFirstSibling();
            newFavList.clearUnseenListOnCollapse = false;
            newFavList.field_Public_EnumNPublicSealedvaInPuMiFaSpClPuLiCrUnique_0 = UiAvatarList.EnumNPublicSealedvaInPuMiFaSpClPuLiCrUnique.SpecificList;
            newFavList.GetComponentInChildren<Text>().text = "WATCHDOGS";

            GameObject NewFavButton = UnityEngine.Object.Instantiate<GameObject>(VRCUiManager.prop_VRCUiManager_0.field_Public_GameObject_0.transform.Find("Screens/Avatar/Favorite Button").gameObject, VRCUiManager.prop_VRCUiManager_0.field_Public_GameObject_0.transform.Find("Screens/Avatar/"));
            NewFavButton.GetComponentInChildren<RectTransform>().localPosition += new Vector3(0f, 165f);
            PageAvatar pageAvatar = VRCUiManager.prop_VRCUiManager_0.field_Public_GameObject_0.transform.Find("Screens/Avatar/").GetComponentInChildren<PageAvatar>();

            Button NewFavButtonButton = NewFavButton.GetComponent<Button>();

            NewFavButtonButton.transform.Find("Horizontal/FavoritesCountSpacingText").gameObject.SetActive(false);
            NewFavButtonButton.transform.Find("Horizontal/FavoritesCurrentCountText").gameObject.SetActive(false);
            NewFavButtonButton.transform.Find("Horizontal/FavoritesCountDividerText").gameObject.SetActive(false);
            NewFavButtonButton.transform.Find("Horizontal/FavoritesMaxAvailableText").gameObject.SetActive(false);
            NewFavButtonButton.GetComponentInChildren<Text>().text = "Fav/UnFav";
            NewFavButtonButton.gameObject.SetActive(true);
            NewFavButtonButton.onClick.RemoveAllListeners();
            NewFavButtonButton.onClick.AddListener(new System.Action(() => {
                ApiAvatar apiAvatar = pageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0;
                if (favedAvatars.Contains(apiAvatar))
                {
                    PopupUtils.PopupV2("Unfav", "Do you really want to unfavour?", "Yes", delegate {
                        favedAvatars.Remove(apiAvatar);
                        string[] arrLine = File.ReadAllLines("WATCHDOGS/Favourite.txt");
                        string avText = "";
                        for (int i = 0; i < arrLine.Length; i++)
                        {
                            if (!arrLine[i].Contains(apiAvatar.id))
                            {
                                avText += arrLine[i];
                            }
                        }
                        File.WriteAllText("WATCHDOGS/Favourite.txt", avText);
                        newFavList.RenderElement(favedAvatars);
                        PopupUtils.ClosePopup();
                    }, "No", delegate {
                        newFavList.RenderElement(favedAvatars);
                        PopupUtils.ClosePopup();
                    });
                }
                else
                {
                    favedAvatars.Add(apiAvatar);
                    MelonCoroutines.Start(RefreshMenu(1f));
                    File.AppendAllText("WATCHDOGS/Favourite.txt", apiAvatar.id + "|" + apiAvatar.name + "|" + apiAvatar.thumbnailImageUrl + "\n");
                    newFavList.RenderElement(favedAvatars);
                }
            }));

            string[] avatars = File.ReadAllLines("WATCHDOGS/Favourite.txt");
            for (int i = 0; i < avatars.Length; i++)
            {
                string[] args = avatars[i].Split('|');
                favedAvatars.Add(new ApiAvatar { id = args[0], name = args[1], thumbnailImageUrl = args[2] });
            }

            MelonCoroutines.Start(RefreshMenu(1f));
        }

        IEnumerator RefreshMenu(float v)
        {
            yield return new WaitForSeconds(v);
            newFavList.RenderElement(favedAvatars);
            yield break;
        }
    }
}
