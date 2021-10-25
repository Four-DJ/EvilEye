using EvilEye.Events;
using EvilEye.SDK;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VRC;

namespace EvilEye.Module
{
    class CustomNameplate : MonoBehaviour
    {
        public Player player;
        private byte one;
        private byte zero;
        private int count = 0;
        private TextMeshProUGUI statsText;
        public CustomNameplate(IntPtr ptr) : base(ptr)
        {
        }

        void Start()
        {
            Transform stats = UnityEngine.Object.Instantiate<Transform>(this.gameObject.transform.Find("Contents/Quick Stats"), this.gameObject.transform.Find("Contents"));
            stats.parent = this.gameObject.transform.Find("Contents");
            stats.gameObject.SetActive(true);
            statsText = stats.Find("Trust Text").GetComponent<TextMeshProUGUI>();
            statsText.color = Color.white;
            stats.Find("Trust Icon").gameObject.SetActive(false);
            stats.Find("Performance Icon").gameObject.SetActive(false);
            stats.Find("Performance Text").gameObject.SetActive(false);
            stats.Find("Friend Anchor Stats").gameObject.SetActive(false);
            zero = player._playerNet.field_Private_Byte_0;
            one = player._playerNet.field_Private_Byte_1;
        }

        void Update()
        {
            if(zero == player._playerNet.field_Private_Byte_0 && one == player._playerNet.field_Private_Byte_1)
            {
                count++;
            }
            else{
                count = 0;
            }
            zero = player._playerNet.field_Private_Byte_0;
            one = player._playerNet.field_Private_Byte_1;
            string text = "<color=green>Stable</color>";
            if (count > 20)
                text = "<color=yellow>Lagging</color>";
            else if(count > 30)
                text = "<color=red>Crashed</color>";

            statsText.text = "[" + player.GetPlatform() + "] [<color=#ff8400>F</color>]: " + player.GetFrames() + " [<color=#ff8400>P</color>]: " + player.GetPing() + " [" + text + "]";
        }

    }
}
