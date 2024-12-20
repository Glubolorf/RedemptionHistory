﻿using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace Redemption
{
	public class RedeWorld : ModWorld
	{
		public override void TileCountsAvailable(int[] tileCounts)
		{
			RedeWorld.xenoBiome = tileCounts[base.mod.TileType("DeadRockTile")];
		}

		public override void ResetNearbyTileEffects()
		{
			RedeWorld.xenoBiome = 0;
		}

		public override void PostUpdate()
		{
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !RedeWorld.spawnSapphironOre && !RedeWorld.spawnScarlionOre)
			{
				if (!WorldGen.crimson)
				{
					RedeWorld.spawnSapphironOre = true;
					Main.NewText("The souls of the world taint it's own evil...", 100, 100, 200, false);
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						int num = WorldGen.genRand.Next(0, Main.maxTilesX);
						int num2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.45f));
						WorldGen.OreRunner(num, num2, (double)WorldGen.genRand.Next(4, 5), WorldGen.genRand.Next(5, 8), (ushort)base.mod.TileType("SapphironOreTile"));
					}
				}
				if (WorldGen.crimson)
				{
					RedeWorld.spawnScarlionOre = true;
					Main.NewText("The souls of the world taint it's own evil...", 200, 100, 100, false);
					for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); j++)
					{
						int num3 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int num4 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.45f));
						WorldGen.OreRunner(num3, num4, (double)WorldGen.genRand.Next(4, 5), WorldGen.genRand.Next(5, 8), (ushort)base.mod.TileType("ScarlionOreTile"));
					}
				}
			}
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
			if (num != -1)
			{
				tasks.Insert(num + 1, new PassLegacy("Redemption Mod Ores", delegate(GenerationProgress progress)
				{
					progress.Message = "Redemption Mod Ores";
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(6, 10), WorldGen.genRand.Next(2, 4), base.mod.TileType("KaniteOreTile"), false, 0f, 0f, false, true);
					}
				}));
			}
		}

		public override void PostWorldGen()
		{
			int[] array = new int[]
			{
				base.mod.ItemType("DonjonStave")
			};
			int num = 0;
			for (int i = 0; i < 1000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null && Main.tile[chest.x, chest.y].type == 21 && Main.tile[chest.x, chest.y].frameX == 72 && Main.rand.Next(3) == 0)
				{
					for (int j = 0; j < 40; j++)
					{
						if (chest.item[j].type == 0)
						{
							chest.item[j].SetDefaults(array[num], false);
							num = (num + 1) % array.Length;
							break;
						}
					}
				}
			}
			int[] array2 = new int[]
			{
				base.mod.ItemType("GildedSeaAxe")
			};
			int num2 = 0;
			for (int k = 0; k < 1000; k++)
			{
				Chest chest2 = Main.chest[k];
				if (chest2 != null && Main.tile[chest2.x, chest2.y].type == 21 && Main.tile[chest2.x, chest2.y].frameX == 612 && Main.rand.Next(5) == 0)
				{
					for (int l = 0; l < 40; l++)
					{
						if (chest2.item[l].type == 0)
						{
							chest2.item[l].SetDefaults(array2[num2], false);
							num2 = (num2 + 1) % array2.Length;
							break;
						}
					}
				}
			}
			int[] array3 = new int[]
			{
				base.mod.ItemType("SeaNote")
			};
			int num3 = 0;
			for (int m = 0; m < 1000; m++)
			{
				Chest chest3 = Main.chest[m];
				if (chest3 != null && Main.tile[chest3.x, chest3.y].type == 21 && Main.tile[chest3.x, chest3.y].frameX == 612 && Main.rand.Next(10) == 0)
				{
					for (int n = 0; n < 40; n++)
					{
						if (chest3.item[n].type == 0)
						{
							chest3.item[n].SetDefaults(array3[num3], false);
							num3 = (num3 + 1) % array3.Length;
							break;
						}
					}
				}
			}
		}

		public override void Initialize()
		{
			RedeWorld.downedKingChicken = false;
			RedeWorld.downedTheKeeper = false;
			RedeWorld.downedXenomiteCrystal = false;
			RedeWorld.downedInfectedEye = false;
			RedeWorld.downedStrangePortal = false;
			RedeWorld.downedVlitch1 = false;
			RedeWorld.downedVlitch2 = false;
			RedeWorld.downedDarkSlime = false;
			RedeWorld.downedSlayer = false;
			RedeWorld.spawnSapphironOre = false;
			RedeWorld.spawnScarlionOre = false;
			RedeWorld.deathBySlayer = false;
			RedeWorld.foundNewb = false;
			RedeWorld.downedVlitch3 = false;
			RedeWorld.downedSkullDigger = false;
			RedeWorld.downedSunkenCaptain = false;
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (RedeWorld.downedKingChicken)
			{
				list.Add("KingChicken");
			}
			if (RedeWorld.downedTheKeeper)
			{
				list.Add("TheKeeper");
			}
			if (RedeWorld.downedXenomiteCrystal)
			{
				list.Add("XenomiteCrystalPhase2");
			}
			if (RedeWorld.downedInfectedEye)
			{
				list.Add("InfectedEye");
			}
			if (RedeWorld.downedStrangePortal)
			{
				list.Add("StrangePortal");
			}
			if (RedeWorld.downedVlitch1)
			{
				list.Add("VlitchCleaver");
			}
			if (RedeWorld.downedVlitch2)
			{
				list.Add("VlitchWormHead");
			}
			if (RedeWorld.downedDarkSlime)
			{
				list.Add("DarkSlime");
			}
			if (RedeWorld.downedSlayer)
			{
				list.Add("KSEntrance");
			}
			if (RedeWorld.spawnSapphironOre)
			{
				flag = true;
			}
			if (RedeWorld.spawnScarlionOre)
			{
				flag2 = true;
			}
			if (RedeWorld.deathBySlayer)
			{
				flag3 = true;
			}
			if (RedeWorld.foundNewb)
			{
				flag4 = true;
			}
			if (RedeWorld.downedVlitch3)
			{
				list.Add("OmegaOblitIdle");
			}
			if (RedeWorld.downedSkullDigger)
			{
				list.Add("SkullDigger");
			}
			if (RedeWorld.downedSunkenCaptain)
			{
				list.Add("SunkenCaptain");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("downed", list);
			tagCompound.Add("sapphiron", flag);
			tagCompound.Add("scarlion", flag2);
			tagCompound.Add("deathSlayer", flag3);
			tagCompound.Add("newbFound", flag4);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("downed");
			RedeWorld.downedKingChicken = list.Contains("KingChicken");
			RedeWorld.downedTheKeeper = list.Contains("TheKeeper");
			RedeWorld.downedXenomiteCrystal = list.Contains("XenomiteCrystalPhase2");
			RedeWorld.downedInfectedEye = list.Contains("InfectedEye");
			RedeWorld.downedStrangePortal = list.Contains("StrangePortal");
			RedeWorld.downedVlitch1 = list.Contains("VlitchCleaver");
			RedeWorld.downedVlitch2 = list.Contains("VlitchWormHead");
			RedeWorld.downedDarkSlime = list.Contains("DarkSlime");
			RedeWorld.downedSlayer = list.Contains("KSEntrance");
			RedeWorld.spawnSapphironOre = tag.GetBool("sapphiron");
			RedeWorld.spawnScarlionOre = tag.GetBool("scarlion");
			RedeWorld.deathBySlayer = tag.GetBool("deathSlayer");
			RedeWorld.foundNewb = tag.GetBool("newbFound");
			RedeWorld.downedVlitch3 = list.Contains("OmegaOblitIdle");
			RedeWorld.downedSkullDigger = list.Contains("SkullDigger");
			RedeWorld.downedSunkenCaptain = list.Contains("SunkenCaptain");
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte bitsByte = default(BitsByte);
			bitsByte[0] = RedeWorld.downedKingChicken;
			bitsByte[1] = RedeWorld.downedTheKeeper;
			bitsByte[2] = RedeWorld.downedXenomiteCrystal;
			bitsByte[3] = RedeWorld.downedInfectedEye;
			bitsByte[4] = RedeWorld.downedStrangePortal;
			bitsByte[5] = RedeWorld.downedVlitch1;
			bitsByte[6] = RedeWorld.downedVlitch2;
			bitsByte[7] = RedeWorld.downedDarkSlime;
			writer.Write(bitsByte);
			BitsByte bitsByte2 = default(BitsByte);
			bitsByte2[0] = RedeWorld.downedSlayer;
			bitsByte2[1] = RedeWorld.downedVlitch3;
			bitsByte2[2] = RedeWorld.downedSkullDigger;
			bitsByte2[3] = RedeWorld.downedSunkenCaptain;
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte bitsByte = default(BitsByte);
			RedeWorld.downedKingChicken = bitsByte[0];
			RedeWorld.downedTheKeeper = bitsByte[1];
			RedeWorld.downedXenomiteCrystal = bitsByte[2];
			RedeWorld.downedInfectedEye = bitsByte[3];
			RedeWorld.downedStrangePortal = bitsByte[4];
			RedeWorld.downedVlitch1 = bitsByte[5];
			RedeWorld.downedVlitch2 = bitsByte[6];
			RedeWorld.downedDarkSlime = bitsByte[7];
			BitsByte bitsByte2 = default(BitsByte);
			RedeWorld.downedSlayer = bitsByte2[0];
			RedeWorld.downedVlitch3 = bitsByte2[1];
			RedeWorld.downedSkullDigger = bitsByte2[2];
			RedeWorld.downedSunkenCaptain = bitsByte2[3];
		}

		private const int saveVersion = 0;

		public static bool spawnOre;

		public static bool spawnDragonOre;

		public static bool spawnSapphironOre;

		public static bool spawnScarlionOre;

		public static int xenoBiome;

		public static bool downedKingChicken;

		public static bool downedTheKeeper;

		public static bool downedXenomiteCrystal;

		public static bool downedInfectedEye;

		public static bool downedStrangePortal;

		public static bool downedVlitch1;

		public static bool downedVlitch2;

		public static bool downedDarkSlime;

		public static bool downedSlayer;

		public static bool deathBySlayer;

		public static bool foundNewb;

		public static bool downedVlitch3;

		public static bool downedSkullDigger;

		public static bool downedSunkenCaptain;
	}
}
