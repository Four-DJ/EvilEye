using EvilEye.Events;
using EvilEye.Module;
using EvilEye.Module.Exploit;
using EvilEye.Module.Movement;
using EvilEye.Module.Player;
using EvilEye.Module.Render;
using EvilEye.Module.Safety;
using EvilEye.Module.Settings;
using EvilEye.Module.World;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using EvilEye.Modules.Exploits;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC.Core;

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
        public QMNestedButton worldhacksButton;
        public QMNestedButton murderbutton;
        public QMNestedButton rendererButton;
        public QMNestedButton botButton;
        public QMNestedButton settingsButton;

        private List<BaseModule> modules = new List<BaseModule>();
        public List<OnPlayerJoinEvent> onPlayerJoinEvents = new List<OnPlayerJoinEvent>();
        public List<OnPlayerLeaveEvent> onPlayerLeaveEvents = new List<OnPlayerLeaveEvent>();
        public List<OnUpdateEvent> onUpdateEvents = new List<OnUpdateEvent>();
        public List<OnUdonEvent> onUdonEvents = new List<OnUdonEvent>();
        public List<OnEventEvent> onEventEvents = new List<OnEventEvent>();
        public List<OnRPCEvent> onRPCEvents = new List<OnRPCEvent>();
        public List<OnSendOPEvent> onSendOPEvents = new List<OnSendOPEvent>();

        public static void OnApplicationStart()
        {
            Main.Instance = new Main();
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
            QMTab mainTab = new QMTab("EvilEye", "EvilEye", "Watching You", Main.Instance.quickMenuStuff.Button_NameplateVisibleIcon.sprite);

            
            Main.Instance.worldButton = new QMNestedButton(mainTab.menuTransform, "World");
            Main.Instance.playerButton = new QMNestedButton(mainTab.menuTransform, "Player");
            Main.Instance.movementButton = new QMNestedButton(mainTab.menuTransform, "Movement");
            Main.Instance.exploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits");
            Main.Instance.safetyButton = new QMNestedButton(mainTab.menuTransform, "Safety");
            Main.Instance.rendererButton = new QMNestedButton(mainTab.menuTransform, "Renderer");
            Main.Instance.settingsButton = new QMNestedButton(mainTab.menuTransform, "Settings");

            Main.Instance.modules.Add(new Fly());
            Main.Instance.modules.Add(new Speed());

            Main.Instance.modules.Add(new AviToID());
            Main.Instance.modules.Add(new FPSUnlocker());
            Main.Instance.modules.Add(new XBoxMic());
            Main.Instance.modules.Add(new LoudMic());
            Main.Instance.modules.Add(new HeadFlipper());
            Main.Instance.modules.Add(new CopyUserID());

            
            Main.Instance.modules.Add(new UdonSpam());
            Main.Instance.modules.Add(new AssetBundleCrash());
            Main.Instance.modules.Add(new QuestCrash());
            Main.Instance.modules.Add(new FreezePlayer());
            Main.Instance.modules.Add(new VRCA());
            Main.Instance.modules.Add(new VRCW());
            Main.Instance.modules.Add(new GhostWalk());   

            Main.Instance.modules.Add(new RateLimit());
            Main.Instance.modules.Add(new AntiInvalidEvent());

            Main.Instance.modules.Add(new OPSendLogger());
            Main.Instance.modules.Add(new UdonLogger());
            Main.Instance.modules.Add(new EventLogger());
            Main.Instance.modules.Add(new RpcLogger());
            Main.Instance.modules.Add(new UnityLogger());
            Main.Instance.modules.Add(new ConsoleClear());
            Main.Instance.modules.Add(new QuickRestart());

            Main.Instance.modules.Add(new CapsuleEsp());
            Main.Instance.modules.Add(new PlayerList());

            Main.Instance.modules.Add(new JoinByID());
            Main.Instance.modules.Add(new Rejoin());
            Main.Instance.modules.Add(new CopyWID());
            Main.Instance.modules.Add(new ItemOrbit());
            Main.Instance.modules.Add(new MasterLock());

            foreach (BaseModule module in Main.Instance.modules)
                module.OnUIInit();

            new QMSingleButton(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, "VRCA", "Download Users VRCA", null, delegate
            {
                ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                WebClient webClient = new WebClient
                {
                    Headers =
                    {
                        "User-Agent: Other"
                    }
                };
                webClient.DownloadFileAsync(new Uri(avatar.assetUrl), "EvilEye/VRCA/" + avatar.name);
                LoggerUtill.Log("Downloaded Selected User VRCA Completed");
            });

            new QMSingleButton(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, "ForceClone", "ForceClone", null, delegate
            {
                ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                if (avatar.releaseStatus == "public")
                    PlayerWrapper.ChangeAvatar(avatar.id);
            });
            
            new QMSingleButton(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, "Get UserID", "UserID", null, delegate
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                if (SelectedPlayer.id != "")
                    SDK.Misc.SetClipboard(SelectedPlayer.id);
            });

            new QMSingleButton(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, "Teleport", "Teleport to selected user.", null, delegate
            {
             
                    PlayerWrapper.Teleport(PlayerWrapper.GetByUsrID(Main.Instance.quickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0));
                    
            });
             
            LoggerUtill.Log("[UI] Done", ConsoleColor.Green);
            try
            {

                //Avatar_Favorites.startAvis();
            }
            catch (Exception ex)
            {
                LoggerUtill.Log(ex.ToString(), ConsoleColor.Red);
            }
        }



        public static void OnGUI()
        {

        }

        public static void OnApplicationQuit()
        {
            foreach (BaseModule module in Main.Instance.modules)
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
