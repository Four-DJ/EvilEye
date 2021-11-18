using Il2CppSystem.Collections.Generic;
using System;
using UnityEngine;
using VRC.Core;

namespace EvilEye.SDK.AvatarFavorites
{
	internal static class AviObjects
	{
		public static string Names;
		internal static List<GameObject> MeshUwU = new List<GameObject>();
		internal static int listIndex = 0;
		internal static void ByePart(this GameObject avatar)
		{
			bool flag = avatar == null;
			if (!flag)
			{
				foreach (Transform transform in GetAllTransforms(avatar, true))
				{
					bool flag2 = transform.GetComponent<ParticleSystem>() || transform.GetComponent<ParticleSystemRenderer>();
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform.GetComponent<ParticleSystemRenderer>());
						UnityEngine.Object.Destroy(transform.GetComponent<ParticleSystem>());
					}
				}
			}
		}
		internal static void ByeDynBone(this GameObject avatar)
		{
			bool flag = avatar == null;
			if (!flag)
			{
				foreach (Transform transform in GetAllTransforms(avatar, true))
				{
					bool flag2 = transform.GetComponent<DynamicBone>() || transform.GetComponent<DynamicBoneCollider>() || transform.GetComponent<Cloth>();
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform.GetComponent<DynamicBone>());
						UnityEngine.Object.Destroy(transform.GetComponent<DynamicBoneCollider>());
						UnityEngine.Object.Destroy(transform.GetComponent<Cloth>());
					}
				}
			}
		}
		internal static void ByeDynBoneJustBones(this GameObject avatar)
		{
			bool flag = avatar == null;
			if (!flag)
			{
				foreach (Transform transform in GetAllTransforms(avatar, true))
				{
					bool flag2 = transform.GetComponent<DynamicBone>();
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform.GetComponent<DynamicBone>());
					}
				}
			}
		}
		internal static void ByeDynBoneJustColliders(this GameObject avatar)
		{
			bool flag = avatar == null;
			if (!flag)
			{
				foreach (Transform transform in GetAllTransforms(avatar, true))
				{
					bool flag2 = transform.GetComponent<DynamicBoneCollider>();
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform.GetComponent<DynamicBoneCollider>());
					}
				}
			}
		}
		internal static void byeCloth(this GameObject avatar)
		{
			bool flag = avatar == null;
			if (!flag)
			{
				foreach (Transform transform in GetAllTransforms(avatar, true))
				{
					bool flag2 = transform.GetComponent<Cloth>();
					if (flag2)
					{
						UnityEngine.Object.Destroy(transform.GetComponent<Cloth>());
					}
				}
			}
		}

		
		internal static void ListOFMesh(this GameObject avatar)
		{
			AviObjects.MeshUwU.Clear();
			bool flag = avatar == null;
			if (!flag)
			{
				foreach (Transform transform in GetAllTransforms(avatar, true))
				{
					bool flag2 = transform.GetComponent<MeshRenderer>() || transform.GetComponent<SkinnedMeshRenderer>();
					if (flag2)
					{
						AviObjects.MeshUwU.Insert(0, transform.gameObject);
					}
				}
			}
		}

		
		internal static void Toggler_And_Deleter(this GameObject Toggle, bool Delete, bool Isgoingon)
		{
			bool flag = Toggle == null;
			if (!flag)
			{
				if (Delete)
				{
					bool flag2 = Toggle.GetComponent<MeshRenderer>() || Toggle.GetComponent<SkinnedMeshRenderer>();
					if (flag2)
					{
						UnityEngine.Object.DestroyImmediate(Toggle.gameObject);
					}
				}
				else
				{
					bool flag3 = Toggle.GetComponent<MeshRenderer>() || Toggle.GetComponent<SkinnedMeshRenderer>();
					if (flag3)
					{
						Toggle.gameObject.SetActive(Isgoingon);
					}
				}
			}
		}
		internal static string MeshNeme()
		{
			bool flag = AviObjects.MeshUwU[AviObjects.listIndex].gameObject == null;
			string result;
			if (flag)
			{
				result = "Game obj is Null";
			}
			else
			{
				bool flag2 = AviObjects.MeshUwU.Count == 0;
				if (flag2)
				{
					result = "No Meshs";
				}
				else
				{
					result = string.Format("{0}. ", AviObjects.listIndex) + AviObjects.MeshUwU[AviObjects.listIndex].name;
				}
			}
			return result;
		}
		
        
		public static void ByeSound(this GameObject avatar)
		{
			bool flag = avatar == null;
			if (flag)
			{
			}
			foreach (Transform transform in GetAllTransforms(avatar, true))
			{
				bool flag2 = transform.GetComponent<AudioSource>();
				if (flag2)
				{
					UnityEngine.Object.DestroyImmediate(transform.GetComponent<AudioSource>());
				}
			}
		}
		public static List<Transform> GetAllTransforms(this GameObject g, bool getHidden = true)
		{
			List<Transform> list = new List<Transform>();
			Transform[] array = g.GetComponents<Transform>();
			Transform[] array2 = g.GetComponentsInChildren<Transform>(getHidden);
			int num = array.Length;
			int num2 = array2.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = !list.Contains(array[i]);
				if (flag)
				{
					list.Add(array[i]);
				}
			}
			for (int j = 0; j < num2; j++)
			{
				bool flag2 = !list.Contains(array2[j]);
				if (flag2)
				{
					list.Add(array2[j]);
				}
			}
			return list;
		}
	}
}
