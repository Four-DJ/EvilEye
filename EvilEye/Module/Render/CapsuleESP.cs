using UnityEngine;
using System.Collections;
using MelonLoader;
using VRC;
using EvilEye.Module;
using EvilEye.SDK;
using EvilEye.Events;
using System;

namespace EvilEye.Module.Render
{
    class CapsuleEsp : BaseModule, OnPlayerJoinEvent
    {
        public CapsuleEsp() : base("CapsuleEsp", "See Players n shit", Main.Instance.rendererButton, null, true, true) { }

        public override void OnEnable ()
        {
            for(int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
            {
                CapsuleEsp.HighlightPlayer(PlayerWrapper.GetAllPlayers()[i], true);
            }
            Main.Instance.onPlayerJoinEvents.Add(this);
        }

        public override void OnDisable()
        {
            for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
            {
                CapsuleEsp.HighlightPlayer(PlayerWrapper.GetAllPlayers()[i], false);
            }
            Main.Instance.onPlayerJoinEvents.Remove(this);
        }

        public static void HighlightPlayer(VRC.Player player, bool state)
        {
            Renderer renderer;
            if (player == null)
            {
                renderer = null;
            }
            else
            {
                Transform transform = player.transform.Find("SelectRegion");
                renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
            }

            Renderer renderer2 = renderer;
            if (renderer2)
            {
                HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, state);
            }
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            HighlightPlayer(player, true);
        }
    }
}