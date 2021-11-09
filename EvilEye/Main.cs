using EvilEye.Events;
using EvilEye.Module;
using EvilEye.Module.Exploit;
using EvilEye.Module.Movement;
using EvilEye.Module.Safety;
using EvilEye.Module.Settings;
using EvilEye.Module.World;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;

namespace EvilEye
{
    public class Main
    {
        public static Main Instance;
        public Config config = new Config();
        public QuickMenuStuff quickMenuStuff;

        public QMNestedButton worldButton;
        public QMNestedButton playerButton;
        public QMNestedButton movementButton;
        public QMNestedButton exploistButton;
        public QMNestedButton spoofersButton;
        public QMNestedButton safetyButton;
        public QMNestedButton botButton;
        public QMNestedButton settingsButton;

        private List<BaseModule> modules = new List<BaseModule>();
        public List<OnPlayerJoinEvent> onPlayerJoinEvents = new List<OnPlayerJoinEvent>();
        public List<OnPlayerLeaveEvent> onPlayerLeaveEvents = new List<OnPlayerLeaveEvent>();
        public List<OnUpdateEvent> onUpdateEvents = new List<OnUpdateEvent>();
        public List<OnEventEvent> onEventEvents = new List<OnEventEvent>();
        public List<OnRPCEvent> onRPCEvents = new List<OnRPCEvent>();
        public List<OnSendOPEvent> onSendOPEvents = new List<OnSendOPEvent>();

        public static void OnApplicationStart()
        {
            Main.Instance = new Main();
            ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
            LoggerUtill.DisplayLogo();

            Task.Run(() =>
            {
                Patches.Init();
            });
        }

        public static void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Process.Start("VRChat.exe", Environment.CommandLine);
                    OnApplicationQuit();
                }
            }
            foreach (OnUpdateEvent @event in Main.Instance.onUpdateEvents)
                @event.OnUpdate();
        }

        public static void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
        }

        [Obfuscation(Exclude = true)]
        private static void OnUIInit()
        {
            Main.Instance.quickMenuStuff = new QuickMenuStuff();
            GameObject devTab = Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools").gameObject;
            GameObject devMenu = Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons").gameObject;
            devTab.GetComponent<Button>().onClick.AddListener(new Action(() =>
            {
                for (int i = 0; i < devMenu.transform.transform.childCount; i++)
                    devMenu.transform.transform.GetChild(i).gameObject.SetActive(true);
            }));
            Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools/Header_DevTools/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().text = "EvilEye";
            for (int i = 0; i < devMenu.transform.transform.childCount; i++)
                GameObject.Destroy(devMenu.transform.transform.GetChild(i).gameObject);
            UnityEngine.Object.Destroy(devTab.transform.Find("Icon").GetComponent<StyleElement>());
            Image devTabImage = devTab.transform.Find("Icon").GetComponent<Image>();
            devTab.GetComponent<UiTooltip>().field_Public_String_0 = "EvilEye";
            devTabImage.sprite = Main.Instance.quickMenuStuff.Button_NameplateVisibleIcon.sprite;
            devTabImage.color = Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Notifications/Icon").GetComponent<Image>().color;
            devTab.SetActive(true);

            Main.Instance.worldButton = new QMNestedButton(devMenu.transform, "World", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.playerButton = new QMNestedButton(devMenu.transform, "Player", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.movementButton = new QMNestedButton(devMenu.transform, "Movement", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.exploistButton = new QMNestedButton(devMenu.transform, "Exploits", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.spoofersButton = new QMNestedButton(devMenu.transform, "Spoofers", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.safetyButton = new QMNestedButton(devMenu.transform, "Safety", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.botButton = new QMNestedButton(devMenu.transform, "Bot", Main.Instance.quickMenuStuff.StandIcon.sprite);
            Main.Instance.settingsButton = new QMNestedButton(devMenu.transform, "Settings", Main.Instance.quickMenuStuff.StandIcon.sprite);

            Main.Instance.modules.Add(new Fly());

            Main.Instance.modules.Add(new Event9());
            Main.Instance.modules.Add(new Event209());
            Main.Instance.modules.Add(new Event210());
            Main.Instance.modules.Add(new AssetBundleCrash());
            Main.Instance.modules.Add(new QuestCrash());

            Main.Instance.modules.Add(new RateLimit());
            Main.Instance.modules.Add(new Anti9());
            Main.Instance.modules.Add(new Anti209());
            Main.Instance.modules.Add(new Anti210());

            Main.Instance.modules.Add(new OPSendLogger());
            Main.Instance.modules.Add(new EventLogger());
            Main.Instance.modules.Add(new RpcLogger());
            Main.Instance.modules.Add(new UnityLogger());

            Main.Instance.modules.Add(new Rejoin());
            Main.Instance.modules.Add(new CopyID());

            foreach (BaseModule module in Main.Instance.modules)
                module.OnUIInit();

            new QMSingleButton(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, "ForceClone", "ForceClone", Main.Instance.quickMenuStuff.Button_AvatarsIcon.sprite, delegate
            {
                ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                if (avatar.releaseStatus == "public")
                    PlayerWrapper.ChangeAvatar(avatar.id);
            });

            LoggerUtill.Log("[UI] Done", ConsoleColor.Green);
        }

        public static void OnGUI()
        {

        }

        public static void OnApplicationQuit()
        {
            foreach(BaseModule module in Main.Instance.modules)
            {
                if (module.save)
                {
                    Main.Instance.config.setConfigBool(module.name, module.toggled);
                }
            }

            Process.GetCurrentProcess().Kill();
        }
    }
}
