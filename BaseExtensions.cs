using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Redemption
{
	public static class BaseExtensions
	{
		public static bool InZone(this Player p, string zoneName, ZoneInfo info = null)
		{
			if (info != null)
			{
				bool flag = info.InZone(p, zoneName);
				if (flag)
				{
					return true;
				}
			}
			switch (zoneName)
			{
			case "Space":
				return (double)(p.position.Y / 16f) < Main.worldSurface * 0.10000000149011612;
			case "Sky":
				return (double)(p.position.Y / 16f) > Main.worldSurface * 0.10000000149011612 && (double)(p.position.Y / 16f) < Main.worldSurface * 0.4000000059604645;
			case "Surface":
				return (double)(p.position.Y / 16f) > Main.worldSurface * 0.4000000059604645 && (double)(p.position.Y / 16f) < Main.worldSurface;
			case "DirtLayer":
			case "Underground":
				return (double)(p.position.Y / 16f) > Main.worldSurface && (double)(p.position.Y / 16f) < Main.rockLayer;
			case "RockLayer":
			case "Cavern":
				return (double)(p.position.Y / 16f) > Main.rockLayer && p.position.Y / 16f < (float)(Main.maxTilesY - 200);
			case "Hell":
				return p.position.Y / 16f > (float)(Main.maxTilesY - 200);
			case "BelowSurface":
				return (double)(p.position.Y / 16f) > Main.worldSurface;
			case "BelowDirtLayer":
			case "BelowUnderground":
				return (double)(p.position.Y / 16f) > Main.rockLayer;
			case "Rain":
				return p.ZoneRain;
			case "Desert":
				return p.ZoneDesert;
			case "UndergroundDesert":
			case "UGDesert":
				return p.ZoneUndergroundDesert;
			case "Sandstorm":
				return p.ZoneSandstorm;
			case "Ocean":
				return p.ZoneBeach;
			case "Jungle":
				return p.ZoneJungle;
			case "Snow":
				return p.ZoneSnow;
			case "Purity":
				return !p.ZoneTowerSolar && !p.ZoneTowerVortex && !p.ZoneTowerNebula && !p.ZoneTowerStardust && !p.ZoneBeach && !p.ZoneDesert && !p.ZoneUndergroundDesert && !p.ZoneSnow && !p.ZoneDungeon && !p.ZoneJungle && !p.ZoneCorrupt && !p.ZoneCrimson && !p.ZoneHoly && !p.ZoneMeteor && !p.ZoneGlowshroom;
			case "Meteor":
			case "Meteorite":
				return p.ZoneMeteor;
			case "GlowingMushroom":
			case "Glowshroom":
				return p.ZoneGlowshroom;
			case "Corrupt":
			case "Corruption":
				return p.ZoneCorrupt;
			case "Crim":
			case "Crimson":
				return p.ZoneCrimson;
			case "Hallow":
				return p.ZoneHoly;
			case "Dungeon":
				return p.ZoneDungeon;
			case "TowerAny":
				return p.ZoneTowerSolar || p.ZoneTowerVortex || p.ZoneTowerNebula || p.ZoneTowerStardust;
			case "TowerSolar":
				return p.ZoneTowerSolar;
			case "TowerVortex":
				return p.ZoneTowerVortex;
			case "TowerNebula":
				return p.ZoneTowerNebula;
			case "TowerStardust":
				return p.ZoneTowerStardust;
			}
			return false;
		}

		public static void AddRecipeGroup(this ModRecipe recipe, Mod mod, string groupName, int count)
		{
			Mod mod2 = (mod == null) ? recipe.mod : mod;
			recipe.AddRecipeGroup(mod2.Name + ":" + groupName, count);
		}

		public static void AddItem(this ModRecipe recipe, int itemID, int count)
		{
			recipe.AddIngredient(itemID, count);
		}

		public static void AddItem(this ModRecipe recipe, Mod mod, string itemName, int count)
		{
			recipe.AddIngredient((mod == null) ? recipe.mod : mod, itemName, count);
		}

		public static void ClearBuff(this Player player, Mod mod, string name)
		{
			player.ClearBuff(mod.BuffType(name));
		}

		public static void AddBuff(this Player player, Mod mod, string name, int time, bool sync = true)
		{
			player.AddBuff(mod.BuffType(name), time, sync);
		}

		public static int FindBuffIndex(this Player player, Mod mod, string name)
		{
			return player.FindBuffIndex(mod.BuffType(name));
		}

		public static int GoreType(this Mod mod, string name, IDictionary<string, int> gores = null)
		{
			return BaseUtility.CheckForGore(mod, name, gores);
		}

		public static int MusicType(this Mod mod, string name, string prefix = "Sounds/Music/")
		{
			return mod.GetSoundSlot(51, prefix + name);
		}

		public static LegacySoundStyle SoundCustom(this Mod mod, string name, string prefix = "Sounds/Custom/")
		{
			return mod.GetLegacySoundSlot(50, prefix + name);
		}

		public static LegacySoundStyle SoundItem(this Mod mod, string name, string prefix = "Sounds/Item/")
		{
			return mod.GetLegacySoundSlot(2, prefix + name);
		}

		public static LegacySoundStyle SoundNPCHit(this Mod mod, string name, string prefix = "Sounds/NPCHit/")
		{
			return mod.GetLegacySoundSlot(3, prefix + name);
		}

		public static LegacySoundStyle SoundNPCKilled(this Mod mod, string name, string prefix = "Sounds/NPCKilled/")
		{
			return mod.GetLegacySoundSlot(4, prefix + name);
		}

		public static int ProjType(this Mod mod, string name)
		{
			return mod.ProjectileType(name);
		}

		public static bool ReadBool(this BinaryReader w)
		{
			return w.ReadBoolean();
		}

		public static int ReadInt(this BinaryReader w)
		{
			return w.ReadInt32();
		}

		public static short ReadShort(this BinaryReader w)
		{
			return w.ReadInt16();
		}

		public static float ReadFloat(this BinaryReader w)
		{
			return w.ReadSingle();
		}

		public static bool IsBlank(this Item item)
		{
			return item.type <= 0 || item.stack <= 0 || string.IsNullOrEmpty(item.Name);
		}

		public static bool water(this Tile tile)
		{
			return tile.liquidType() == 0;
		}

		public static bool lava(this Tile tile)
		{
			return tile.liquidType() == 1;
		}

		public static bool honey(this Tile tile)
		{
			return tile.liquidType() == 2;
		}
	}
}
