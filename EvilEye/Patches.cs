using AmplitudeSDKWrapper;
using EvilEye.Events;
using EvilEye.Module;
using EvilEye.Module.Settings;
using EvilEye.SDK;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transmtn;
using UnityEngine;
using UnityEngine.Networking;
using VRC.SDKBase;

namespace EvilEye
{
    class Patches
    {
        private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("EvilEye");
        private static string newHWID = "";

        public static void Init()
        {
            
            try
            {
                Instance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(FakeHWID))));
                Instance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(VoidPatch))));
                Instance.Patch(typeof(AmplitudeWrapper).GetMethods().First((MethodInfo x) => x.Name == "LogEvent" && x.GetParameters().Length == 4), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(VoidPatch))));
                LoggerUtill.Log("[Patches] Patched Analystics", ConsoleColor.Green);
            }
            catch(Exception ex)
            {
                LoggerUtill.Log("[Patches] Could not patch Analystics failed\n" + ex, ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(LoadBalancingClient).GetMethod("OnEvent"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnEvent))));
                Instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnRPC))));
                Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0", null, null), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OpRaiseEvent))));
                LoggerUtill.Log("[Patch] Networking", ConsoleColor.Green);
            }
            catch
            {
                LoggerUtill.Log("[Patch] [Error] Networking", ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "Log" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Debug))));
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogError" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(DebugError))));
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogWarning" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(DebugWarning))));

                LoggerUtill.Log("[Patch] Logger", ConsoleColor.Green);
            }
            catch
            {
                LoggerUtill.Log("[Patch] [Error] Logger", ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod("Awake"),null, new HarmonyMethod(AccessTools.Method(typeof(Main), ("OnUIInit"))));
            }
            catch
            {

            }

            while (NetworkManager.field_Internal_Static_NetworkManager_0 == null)
            {
                Thread.Sleep(25);
            }
            try
            {
                NetworkManager
                    .field_Internal_Static_NetworkManager_0
                    .field_Internal_VRCEventDelegate_1_Player_0
                    .field_Private_HashSet_1_UnityAction_1_T_0
                    .Add(new Action<VRC.Player>(player => OnPlayerJoined(ref player)));
                LoggerUtill.Log("[Patches] Patched PlayerJoin", ConsoleColor.Green);
            }
            catch { LoggerUtill.Log("[Patches] Could not patch PlayerJoin", ConsoleColor.Red); }

            try
            {
                NetworkManager
                    .field_Internal_Static_NetworkManager_0
                    .field_Internal_VRCEventDelegate_1_Player_1
                    .field_Private_HashSet_1_UnityAction_1_T_0
                    .Add(new Action<VRC.Player>(player => OnPlayerLeft(ref player)));
                LoggerUtill.Log("[Patches] Patched OnPlayerLeft", ConsoleColor.Green);
            }
            catch { LoggerUtill.Log("[Patches] Could not patch PlayerLeave", ConsoleColor.Red); }
            LoggerUtill.Log("[Patch] Done", ConsoleColor.Green);
        }

        private static void OnPlayerJoined(ref VRC.Player __0)
        {
            if(VRC.Player.prop_Player_0 == __0)
                WorldWrapper.Init();
            CustomNameplate nameplate = __0.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
            nameplate.player = __0;
            foreach (OnPlayerJoinEvent wdEvent in Main.Instance.onPlayerJoinEvents)
                wdEvent.OnPlayerJoin(__0);
        }

        private static void OnPlayerLeft(ref VRC.Player __0)
        {
            foreach (OnPlayerLeaveEvent wdEvent in Main.Instance.onPlayerLeaveEvents)
                wdEvent.PlayerLeave(__0);
        }

        [Obfuscation(Exclude = true)]
        private static bool DebugError(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                LoggerUtill.Log("[UnityError] " + Il2CppSystem.Convert.ToString(__0));
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool DebugWarning(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                LoggerUtill.Log("[UnityWarning] " + Il2CppSystem.Convert.ToString(__0));
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool Debug(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                LoggerUtill.Log("[Unity] " + Il2CppSystem.Convert.ToString(__0));
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool FakeHWID(ref string __result)
        {
            if (Patches.newHWID == "")
            {
                Patches.newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9)
                }))).Select(delegate (byte x)
                {
                    byte b = x;
                    return b.ToString("x2");
                }).Aggregate((string x, string y) => x + y);
                LoggerUtill.Log("[HWID] new " + Patches.newHWID);
            }
            __result = Patches.newHWID;
            return false;
        }

        [Obfuscation(Exclude = true)]
        private static bool OnEvent(EventData __0)
        {
            if (__0 == null)
                return false;
            foreach (OnEventEvent @event in Main.Instance.onEventEvents)
                if (!@event.OnEvent(__0))
                    return false;
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool OpRaiseEvent(byte __0, Il2CppSystem.Object __1, RaiseEventOptions __2)
        {
            foreach (OnSendOPEvent wdEvent in Main.Instance.onSendOPEvents)
                wdEvent.OnSendOP(__0, __1, __2);

            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool OnRPC(VRC.Player __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
        {
            foreach (OnRPCEvent wdEvent in Main.Instance.onRPCEvents)
                if (!wdEvent.OnRPC(__0, __1, __2, __3, __4))
                    return false;

            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool VoidPatch()
        {
            return false;
        }

        [Obfuscation(Exclude = true)]
        private static bool VoidPatchTrue(bool __result)
        {
            __result = true;
            return false;
        }

        [Obfuscation(Exclude = true)]
        private static bool VoidPatchFalse(bool __result)
        {
            __result = false;
            return false;
        }
    }
}
