using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

namespace Redemption
{
	public class SubworldCache
	{
		public static void InitCache()
		{
			SubworldCache.cache = new List<SubworldCacheClass>();
			SubworldCache.mod = Redemption.Inst;
		}

		public static void UnloadCache()
		{
			SubworldCache.cache = null;
			SubworldCache.mod = null;
		}

		public static void UpdateCache()
		{
			if (!SubworldCache.postCacheFields || SubworldCache.cache.Count < 1)
			{
				return;
			}
			for (int i = 0; i < SubworldCache.cache.Count; i++)
			{
				SubworldCacheClass cachee = SubworldCache.cache[i];
				FieldInfo fild = SubworldCache.mod.GetModWorld(cachee.modwld).GetType().GetField(cachee.field, BindingFlags.Static | BindingFlags.Public);
				if (cachee.myint != null)
				{
					fild.SetValue(SubworldCache.mod.GetModWorld(cachee.modwld), cachee.myint.Value);
				}
				else
				{
					fild.SetValue(SubworldCache.mod.GetModWorld(cachee.modwld), cachee.mybool.Value);
				}
			}
			SubworldCache.postCacheFields = false;
			SubworldCache.cache.Clear();
		}

		public static bool AddCache(string mod, string modWorld, string field, bool? mybool, int? myint)
		{
			SubworldCacheClass newone = new SubworldCacheClass(mod, modWorld, field, mybool, myint);
			SubworldCache.cache.Add(newone);
			SubworldCache.postCacheFields = true;
			return true;
		}

		public static List<SubworldCacheClass> cache;

		public static bool postCacheFields;

		public static Mod mod;
	}
}
