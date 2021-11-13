using UnityEngine;
using System.Collections;
using MelonLoader;
using VRC;
using EvilEye.Module;
using EvilEye.SDK;
using EvilEye.Events;
using System;

namespace EvilEye.Module.Player
{
    class CapsuleEsp : BaseModule
    {
        public CapsuleEsp() : base("HeadFlipper", "Fuck your desktop neck", Main.Instance.playerButton, null, true) { }
        public override void OnEnable (bool state)
        {
            foreach (VRC.Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                CapsuleEsp.HighlightPlayer(player, state);
            }
        }

     
        public override void OnPlayerJoined(VRC.Player player)
        {
            MelonCoroutines.Start(this.Delay(player));
        }

        private IEnumerator Delay(VRC.Player player)
        {
            throw new NotImplementedException();
        }

        public IEnumerator Delay(VRC.Player player)
        {
            if (player == null)
            {
                yield break;
            }

            int timeout = 0;
            while (player.gameObject == null && timeout < 30)
            {
                yield return new WaitForSeconds(1f);
                int num = timeout;
                timeout = num + 1;
            }

            CapsuleEsp.HighlightPlayer(player, this.state);
            yield break;
        }

        public IEnumerator DelayRefresh()
        {
            yield return null;
            foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                Esp.HighlightPlayer(player, this.state);
            }

            yield break;
        }

        public static void HighlightPlayer(Player player, bool state)
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
                renderer2.sharedMaterial.color = Utils.Colors.primary;
            }
        }
    }
}