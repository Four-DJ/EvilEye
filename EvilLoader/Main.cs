using Encyryption;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[assembly: MelonLoader.MelonGame("VRChat", "VRChat")]
[assembly: MelonLoader.MelonInfo(typeof(EvilLoader.Main), "EvilLoader", "1.0", "Four_DJ")]


namespace EvilLoader
{
    public class Main : MelonMod
    {
		public override void OnApplicationStart()
		{
			TcpClient client = new TcpClient("www.pandagaming.se", 8888);

			if (!File.Exists("Auth.txt"))
				File.Create("Auth.txt");

			NetworkStream networkStream = client.GetStream();
			byte[] bufferBytes = new byte[1024];

			int reciveLength = networkStream.Read(bufferBytes, 0, bufferBytes.Length);
			string encryptionKey = StringCipher.Decrypt(Encoding.ASCII.GetString(bufferBytes, 0, reciveLength), "48ec5a237aeff6fb6daab2e2bb1517952957c320390831f342ef04af80f748f3");

			var crypt = new SHA256Managed();
			string hash = String.Empty;
			byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(File.ReadAllText("Auth.txt")));
			foreach (byte theByte in crypto)
			{
				hash += theByte.ToString("x2");
			}

			byte[] bytesToSend = Encoding.ASCII.GetBytes(StringCipher.Encrypt("0 " + hash, encryptionKey));
			networkStream.Write(bytesToSend, 0, bytesToSend.Length);

			networkStream.Read(bufferBytes, 0, bufferBytes.Length);

			int size = BitConverter.ToInt32(bufferBytes, 0);

			byte[] array =  new byte[size];
			byte[] smallbuffer = new byte[1];
			for (int i = 0; i < array.Length; i++)
			{
				int bytesread = networkStream.Read(smallbuffer, 0, smallbuffer.Length);
				array[i] = smallbuffer[0];
			}

			if (array == null)
			{  
					Process.GetCurrentProcess().Kill();
			}
			foreach (Type type in Assembly.Load(array).GetTypes())
			{
				if (type.Name == "Main")
				{
					foreach (MethodInfo methodInfo in type.GetMethods())
					{
						if (methodInfo.Name.Equals("OnApplicationStart"))
						{
							this.onApplicationStartMethod = methodInfo;
						}
						if (methodInfo.Name.Equals("OnApplicationQuit"))
						{
							this.onApplicationQuitMethod = methodInfo;
						}
						if (methodInfo.Name.Equals("OnUpdate"))
						{
							this.onUpdateMethod = methodInfo;
						}
						if (methodInfo.Name.Equals("OnGUI"))
						{
							this.onGUIMethod = methodInfo;
						}
						if (methodInfo.Name.Equals("OnSceneWasLoaded"))
						{
							this.onSceneWasLoadedMethod = methodInfo;
						}
					}
					break;
				}
			}
			MethodInfo methodInfo2 = this.onApplicationStartMethod;
			if (methodInfo2 == null)
			{
				return;
			}
			methodInfo2.Invoke(null, new object[0]);
			client.Close();
		}

		public override void OnUpdate()
		{
			MethodInfo methodInfo = this.onUpdateMethod;
			if (methodInfo == null)
			{
				return;
			}
			methodInfo.Invoke(null, new object[0]);
		}

		public override void OnApplicationQuit()
		{
			MethodInfo methodInfo = this.onApplicationQuitMethod;
			if (methodInfo == null)
			{
				return;
			}
			methodInfo.Invoke(null, new object[0]);
		}

		public override void OnGUI()
		{
			MethodInfo methodInfo = this.onGUIMethod;
			if (methodInfo == null)
			{
				return;
			}
			methodInfo.Invoke(null, new object[0]);
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			MethodInfo methodInfo = this.onSceneWasLoadedMethod;
			if (methodInfo == null)
			{
				return;
			}
			methodInfo.Invoke(null, new object[]
			{
				buildIndex,
				sceneName
			});
		}
		//public static void CheckAndUpdate()
		//{
		//	WebClient webClient = new WebClient();
		//	string dowloadlink = webClient.DownloadString("https://pastebin.com/raw/0JaZkVzq");
		//	bool flag = File.Exists("Dependencies/EvilEye.dll");
		//	if (flag)
		//	{
		//		string fileVersion = FileVersionInfo.GetVersionInfo("Dependencies/EvilEye.dll").FileVersion;
		//		string text = new WebClient().DownloadString("https://pastebin.com/raw/jC8w0XGG");
		//		bool flag2 = fileVersion != text;
		//		if (flag2)
		//		{
		//			MelonLogger.Msg(string.Concat(new string[]
		//			{
		//				"Downloading New Evil Eye version. You running Version ",
		//				fileVersion,
		//				" the current Version is ",
		//				text,
		//				" !"
		//			}), false, ConsoleColor.Red);
		//			byte[] bytes = new WebClient().DownloadData($"{dowloadlink}");
		//			File.WriteAllBytes("Dependencies/EvilEye.dll", bytes);
		//			MelonLogger.Msg("Downloaded the latest Version Restarting... ~ EvilEye", false, ConsoleColor.Green);
		//			Process.Start("vrchat.exe", Environment.CommandLine.ToString());
		//			Process.GetCurrentProcess().Kill();
		//		}
		//		else
		//		{
		//			MelonLogger.Msg("You have the Latest Evil Eye Version", false, ConsoleColor.Green);
		//		}
		//	}
		//	else
		//	{
		//		MelonLogger.Msg("Downloading New Update", false, ConsoleColor.White);
		//		byte[] bytes2 = new WebClient().DownloadData($"{dowloadlink}");
		//		File.WriteAllBytes("Dependencies/EvilEye.dll", bytes2);
		//	}
		//}
		private MethodInfo onApplicationStartMethod;
		private MethodInfo onUpdateMethod;
		private MethodInfo onGUIMethod;
		private MethodInfo onSceneWasLoadedMethod;
		private MethodInfo onApplicationQuitMethod;
	}
}
