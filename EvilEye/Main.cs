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

            QMTab mainTab = new QMTab("EvilEye","EvilEye", "Watching You", Main.Instance.quickMenuStuff.Button_NameplateVisibleIcon.sprite);
            
            Main.Instance.worldButton = new QMNestedButton(mainTab.menuTransform, "World", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.playerButton = new QMNestedButton(mainTab.menuTransform, "Player", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.movementButton = new QMNestedButton(mainTab.menuTransform, "Movement", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.exploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.spoofersButton = new QMNestedButton(mainTab.menuTransform, "Spoofers", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.safetyButton = new QMNestedButton(mainTab.menuTransform, "Safety", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.botButton = new QMNestedButton(mainTab.menuTransform, "Bot", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);
            Main.Instance.settingsButton = new QMNestedButton(mainTab.menuTransform, "Settings", Main.Instance.quickMenuStuff.StandIcon.sprite, mainTab.menu.page.field_Public_String_0);

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
