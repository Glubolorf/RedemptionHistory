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
			if (info != null && info.InZone(p, zoneName))
			{
				return true;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(zoneName);
			if (num > 2049690417U)
			{
				if (num > 3508074612U)
				{
					if (num <= 3811646694U)
					{
						if (num <= 3729410372U)
						{
							if (num != 3692573838U)
							{
								if (num != 3729410372U)
								{
									return false;
								}
								if (!(zoneName == "Jungle"))
								{
									return false;
								}
								return p.ZoneJungle;
							}
							else if (!(zoneName == "Crimson"))
							{
								return false;
							}
						}
						else if (num != 3769326657U)
						{
							if (num != 3811646694U)
							{
								return false;
							}
							if (!(zoneName == "Underground"))
							{
								return false;
							}
							goto IL_5F3;
						}
						else
						{
							if (!(zoneName == "BelowSurface"))
							{
								return false;
							}
							return (double)(p.position.Y / 16f) > Main.worldSurface;
						}
					}
					else if (num <= 3917187825U)
					{
						if (num != 3907416818U)
						{
							if (num != 3917187825U)
							{
								return false;
							}
							if (!(zoneName == "TowerNebula"))
							{
								return false;
							}
							return p.ZoneTowerNebula;
						}
						else
						{
							if (!(zoneName == "Corrupt"))
							{
								return false;
							}
							goto IL_772;
						}
					}
					else if (num != 4159201133U)
					{
						if (num != 4173769292U)
						{
							if (num != 4294611794U)
							{
								return false;
							}
							if (!(zoneName == "Crim"))
							{
								return false;
							}
						}
						else
						{
							if (!(zoneName == "UGDesert"))
							{
								return false;
							}
							goto IL_6C5;
						}
					}
					else
					{
						if (!(zoneName == "Dungeon"))
						{
							return false;
						}
						return p.ZoneDungeon;
					}
					return p.ZoneCrimson;
				}
				if (num <= 3227030328U)
				{
					if (num > 2802400880U)
					{
						if (num != 2831101335U)
						{
							if (num != 3227030328U)
							{
								return false;
							}
							if (!(zoneName == "BelowDirtLayer"))
							{
								return false;
							}
						}
						else if (!(zoneName == "BelowUnderground"))
						{
							return false;
						}
						return (double)(p.position.Y / 16f) > Main.rockLayer;
					}
					if (num != 2627585030U)
					{
						if (num != 2802400880U)
						{
							return false;
						}
						if (!(zoneName == "Cavern"))
						{
							return false;
						}
						goto IL_628;
					}
					else
					{
						if (!(zoneName == "Hell"))
						{
							return false;
						}
						return p.position.Y / 16f > (float)(Main.maxTilesY - 200);
					}
				}
				else if (num <= 3250860581U)
				{
					if (num != 3242021712U)
					{
						if (num != 3250860581U)
						{
							return false;
						}
						if (!(zoneName == "Space"))
						{
							return false;
						}
						return (double)(p.position.Y / 16f) < Main.worldSurface * 0.10000000149011612;
					}
					else
					{
						if (!(zoneName == "Hallow"))
						{
							return false;
						}
						return p.ZoneHoly;
					}
				}
				else if (num != 3290431975U)
				{
					if (num != 3482222667U)
					{
						if (num != 3508074612U)
						{
							return false;
						}
						if (!(zoneName == "Sandstorm"))
						{
							return false;
						}
						return p.ZoneSandstorm;
					}
					else
					{
						if (!(zoneName == "TowerSolar"))
						{
							return false;
						}
						return p.ZoneTowerSolar;
					}
				}
				else if (!(zoneName == "UndergroundDesert"))
				{
					return false;
				}
				IL_6C5:
				return p.ZoneUndergroundDesert;
			}
			if (num <= 670448652U)
			{
				if (num <= 344094026U)
				{
					if (num <= 141479077U)
					{
						if (num != 78706450U)
						{
							if (num != 141479077U)
							{
								return false;
							}
							if (!(zoneName == "DirtLayer"))
							{
								return false;
							}
						}
						else
						{
							if (!(zoneName == "Snow"))
							{
								return false;
							}
							return p.ZoneSnow;
						}
					}
					else if (num != 164626931U)
					{
						if (num != 344094026U)
						{
							return false;
						}
						if (!(zoneName == "TowerStardust"))
						{
							return false;
						}
						return p.ZoneTowerStardust;
					}
					else
					{
						if (!(zoneName == "Rain"))
						{
							return false;
						}
						return p.ZoneRain;
					}
				}
				else if (num <= 437214172U)
				{
					if (num != 416514354U)
					{
						if (num != 437214172U)
						{
							return false;
						}
						if (!(zoneName == "Desert"))
						{
							return false;
						}
						return p.ZoneDesert;
					}
					else
					{
						if (!(zoneName == "TowerVortex"))
						{
							return false;
						}
						return p.ZoneTowerVortex;
					}
				}
				else if (num != 620006529U)
				{
					if (num != 670448652U)
					{
						return false;
					}
					if (!(zoneName == "Corruption"))
					{
						return false;
					}
					goto IL_772;
				}
				else
				{
					if (!(zoneName == "RockLayer"))
					{
						return false;
					}
					goto IL_628;
				}
			}
			else
			{
				if (num <= 1308329636U)
				{
					if (num <= 925215865U)
					{
						if (num != 800466646U)
						{
							if (num != 925215865U)
							{
								return false;
							}
							if (!(zoneName == "Meteor"))
							{
								return false;
							}
						}
						else
						{
							if (!(zoneName == "Sky"))
							{
								return false;
							}
							return (double)(p.position.Y / 16f) > Main.worldSurface * 0.10000000149011612 && (double)(p.position.Y / 16f) < Main.worldSurface * 0.4000000059604645;
						}
					}
					else if (num != 1256897355U)
					{
						if (num != 1308329636U)
						{
							return false;
						}
						if (!(zoneName == "TowerAny"))
						{
							return false;
						}
						return p.ZoneTowerSolar || p.ZoneTowerVortex || p.ZoneTowerNebula || p.ZoneTowerStardust;
					}
					else if (!(zoneName == "Meteorite"))
					{
						return false;
					}
					return p.ZoneMeteor;
				}
				if (num <= 1802051888U)
				{
					if (num != 1498322830U)
					{
						if (num != 1802051888U)
						{
							return false;
						}
						if (!(zoneName == "Purity"))
						{
							return false;
						}
						return !p.ZoneTowerSolar && !p.ZoneTowerVortex && !p.ZoneTowerNebula && !p.ZoneTowerStardust && !p.ZoneBeach && !p.ZoneDesert && !p.ZoneUndergroundDesert && !p.ZoneSnow && !p.ZoneDungeon && !p.ZoneJungle && !p.ZoneCorrupt && !p.ZoneCrimson && !p.ZoneHoly && !p.ZoneMeteor && !p.ZoneGlowshroom;
					}
					else if (!(zoneName == "GlowingMushroom"))
					{
						return false;
					}
				}
				else if (num != 1906012040U)
				{
					if (num != 1984122524U)
					{
						if (num != 2049690417U)
						{
							return false;
						}
						if (!(zoneName == "Ocean"))
						{
							return false;
						}
						return p.ZoneBeach;
					}
					else
					{
						if (!(zoneName == "Surface"))
						{
							return false;
						}
						return (double)(p.position.Y / 16f) > Main.worldSurface * 0.4000000059604645 && (double)(p.position.Y / 16f) < Main.worldSurface;
					}
				}
				else if (!(zoneName == "Glowshroom"))
				{
					return false;
				}
				return p.ZoneGlowshroom;
			}
			IL_5F3:
			return (double)(p.position.Y / 16f) > Main.worldSurface && (double)(p.position.Y / 16f) < Main.rockLayer;
			IL_628:
			return (double)(p.position.Y / 16f) > Main.rockLayer && p.position.Y / 16f < (float)(Main.maxTilesY - 200);
			IL_772:
			return p.ZoneCorrupt;
		}

		public static void AddRecipeGroup(this ModRecipe recipe, Mod mod, string groupName, int count)
		{
			Mod i = (mod == null) ? recipe.mod : mod;
			recipe.AddRecipeGroup(i.Name + ":" + groupName, count);
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

		public static ushort ReadUShort(this BinaryReader w)
		{
			return w.ReadUInt16();
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
