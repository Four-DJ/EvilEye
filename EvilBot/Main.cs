using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace EvilBot
{
    public class Main
    {
        public static void OnApplicationStart()
        {
            MelonCoroutines.Start(PacketLisiner());
        }

        private static IEnumerator PacketLisiner()
        {
            while (true)
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                byte[] buffer = new byte[1024];
                int length = client.GetStream().Read(buffer, 0, buffer.Length);
                string[] cmd = Encoding.UTF8.GetString(buffer, 0, length).Split('|');
                switch (cmd[0])
                {
                    case "0":
                        break;
                    case "1":
                        string[] worldID = cmd[1].Split(':');
                        VRCFlowManager.prop_VRCFlowManager_0.Method_Public_Void_String_String_WorldTransitionInfo_Action_1_String_Boolean_0(worldID[0], worldID[1]);
                        break;
                }

            }
            yield break;
        }

        public static void OnUpdate()
        {

        }
    }
}
