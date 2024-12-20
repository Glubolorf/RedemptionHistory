using System;
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
				if (chest != null && Main.tile[chest.x, chest.y].type == 21 && Main.tile[chest.x, chest.y].frameX == 72 && Main.rand.Next(4) == 1)
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
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
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
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("downed", list);
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
		}

		private const int saveVersion = 0;

		public static bool spawnOre;

		public static int xenoBiome;

		public static bool downedKingChicken;

		public static bool downedTheKeeper;

		public static bool downedXenomiteCrystal;

		public static bool downedInfectedEye;

		public static bool downedStrangePortal;

		public static bool downedVlitch1;

		public static bool downedVlitch2;

		public static bool downedDarkSlime;
	}
}
