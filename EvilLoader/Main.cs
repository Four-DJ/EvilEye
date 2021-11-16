﻿using Encyryption;
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
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[assembly: MelonLoader.MelonGame("VRChat", "VRChat")]
[assembly: MelonLoader.MelonInfo(typeof(EvilLoader.Main), "EvilLoader", "1.0", "Four_DJ")]


namespace EvilLoader
{
    public class Main : MelonMod
    {
		bool isBot = false;

		public override void OnApplicationStart()
		{
			
			if (!File.Exists("Auth.txt"))
			{
				File.Create("Auth.txt").Close();
			}

			Process.Start("EvilEyeAuth.exe", "48ec5a237aeff6fb6daab2e2bb1517952957c320390831f342ef04af80f748f3");

			TcpListener tcpListener = new TcpListener(8888);

			TcpClient tcpClient = tcpListener.AcceptTcpClient();

			byte[] reciveBytes = new byte[1024];

			int reciveLength = tcpClient.GetStream().Read(reciveBytes, 0, reciveBytes.Length);

			string key = Encoding.ASCII.GetString(reciveBytes, 0, reciveLength);

			byte[] encrypted = File.ReadAllBytes("EvilEye.dll");
			byte[] compressed = Convert.FromBase64String(StringCipher.Decrypt(Encoding.UTF8.GetString(encrypted, 0, encrypted.Length), key));

			MemoryStream from = new MemoryStream(compressed);
			MemoryStream to = new MemoryStream();
			GZipStream gZipStream = new GZipStream(from, CompressionMode.Decompress);
			gZipStream.CopyTo(to);

			byte[] array = to.ToArray();
			tcpClient.Close();
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
			File.Delete("EvilEye.dll");
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
