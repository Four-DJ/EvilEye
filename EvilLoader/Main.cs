using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
			if (!Directory.Exists("Dependencies"))
			{
				Directory.CreateDirectory("Dependencies");
			}
			if (!File.Exists("Auth.txt"))
			{
				File.Create("Auth.txt").Close();
			}

			isBot = Application.isBatchMode;

			byte[] array = isBot ? File.ReadAllBytes("Dependencies/EvilBot.dll") : File.ReadAllBytes("Dependencies/EvilEye.dll");
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

		private MethodInfo onApplicationStartMethod;
		private MethodInfo onUpdateMethod;
		private MethodInfo onGUIMethod;
		private MethodInfo onSceneWasLoadedMethod;
		private MethodInfo onApplicationQuitMethod;
	}
}
