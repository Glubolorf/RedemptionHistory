using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
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
			if (RedeWorld.downedInfectedEye && !RedeWorld.spawnXenoBiome)
			{
				RedeWorld.spawnXenoBiome = true;
				Main.NewText("The infection grows...", 50, 200, 75, false);
				for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.5E-06); k++)
				{
					WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), (double)WorldGen.genRand.Next(90, 130), WorldGen.genRand.Next(90, 170), (ushort)base.mod.TileType("DeadRockTile"));
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
			tasks.Insert(num + 4, new PassLegacy("Ancient House", delegate(GenerationProgress progress)
			{
				this.AncientHouse();
			}));
			tasks.Insert(num + 5, new PassLegacy("Furnishing Ancient House", delegate(GenerationProgress progress)
			{
				this.AncientHouseFurn();
			}));
		}

		public void AncientHouse()
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			dictionary[new Color(255, 100, 0)] = inst.TileType("AncientWoodTile");
			dictionary[new Color(0, 255, 0)] = inst.TileType("AncientDirtTile");
			dictionary[new Color(150, 150, 150)] = -2;
			dictionary[Color.Black] = -1;
			Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
			dictionary2[new Color(255, 0, 255)] = inst.WallType("AncientWoodWallTile");
			dictionary2[new Color(0, 255, 255)] = 63;
			dictionary2[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGen/AncientHouse"), dictionary, inst.GetTexture("WorldGen/AncientHouseWalls"), dictionary2, null, null);
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.07f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(point.X, point.Y, true, true);
		}

		public void AncientHouseFurn()
		{
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.07f) + 16, (int)((float)Main.maxTilesY * 0.45f) + 8, (int)((ushort)base.mod.TileType("AncientWoodDoorClosed")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.07f) + 16, (int)((float)Main.maxTilesY * 0.45f) + 8, (int)((ushort)base.mod.TileType("AncientWoodDoorClosed")), 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.07f) + 10, (int)((float)Main.maxTilesY * 0.45f) + 7, (int)((ushort)base.mod.TileType("BrothersPaintingTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.07f) + 10, (int)((float)Main.maxTilesY * 0.45f) + 7, (int)((ushort)base.mod.TileType("BrothersPaintingTile")), 0, 0, -1, -1);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.07f) + 3, (int)((float)Main.maxTilesY * 0.45f) + 9, (int)((ushort)base.mod.TileType("AncientWoodChestTile")), 0, null, null, null, false);
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
				base.mod.ItemType("GildedSeaAxe"),
				base.mod.ItemType("SeaNote")
			};
			int num2 = 0;
			for (int k = 0; k < 1000; k++)
			{
				Chest chest2 = Main.chest[k];
				if (chest2 != null && Main.tile[chest2.x, chest2.y].type == 21 && Main.tile[chest2.x, chest2.y].frameX == 612 && Main.rand.Next(10) == 0)
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
			for (int m = 1; m < 3; m++)
			{
				int[] array3 = new int[]
				{
					base.mod.ItemType("AntiXenomiteApplier"),
					base.mod.ItemType("CarbonMyofibre"),
					base.mod.ItemType("Starlite"),
					base.mod.ItemType("XenomiteShard"),
					73
				};
				int num3 = 0;
				for (int n = 0; n < 1000; n++)
				{
					Chest chest3 = Main.chest[n];
					if (chest3 != null && (int)Main.tile[chest3.x, chest3.y].type == base.mod.TileType("DeadWoodChestTile"))
					{
						for (int num4 = 0; num4 < 40; num4++)
						{
							if (chest3.item[num4].type == 0)
							{
								chest3.item[num4].SetDefaults(array3[num3], false);
								chest3.item[num4].stack = Main.rand.Next(2, 8);
								num3 = (num3 + 1) % array3.Length;
								break;
							}
						}
					}
				}
			}
			for (int num5 = 1; num5 < 2; num5++)
			{
				int[] array4 = new int[]
				{
					base.mod.ItemType("ScrapMetal"),
					base.mod.ItemType("AIChip"),
					base.mod.ItemType("Mk1Capacitator"),
					base.mod.ItemType("Mk2Capacitator"),
					base.mod.ItemType("Mk3Capacitator"),
					base.mod.ItemType("Mk1Plating"),
					base.mod.ItemType("Mk2Plating"),
					base.mod.ItemType("Mk3Plating")
				};
				int num6 = 0;
				for (int num7 = 0; num7 < 1000; num7++)
				{
					Chest chest4 = Main.chest[num7];
					if (chest4 != null && (int)Main.tile[chest4.x, chest4.y].type == base.mod.TileType("DeadWoodChestTile"))
					{
						for (int num8 = 0; num8 < 40; num8++)
						{
							if (chest4.item[num8].type == 0)
							{
								chest4.item[num8].SetDefaults(array4[num6], false);
								chest4.item[num8].stack = Main.rand.Next(1, 3);
								num6 = (num6 + 1) % array4.Length;
								break;
							}
						}
					}
				}
			}
			int[] array5 = new int[]
			{
				base.mod.ItemType("GasMask"),
				base.mod.ItemType("PlasmaShield"),
				base.mod.ItemType("MiniNuke"),
				base.mod.ItemType("XenoEye"),
				base.mod.ItemType("PlasmaSaber"),
				base.mod.ItemType("RadioactiveLauncher"),
				base.mod.ItemType("SludgeSpoon")
			};
			for (int num9 = 0; num9 < 1000; num9++)
			{
				Chest chest5 = Main.chest[num9];
				if (chest5 != null && (int)Main.tile[chest5.x, chest5.y].type == base.mod.TileType("DeadWoodChestTile"))
				{
					for (int num10 = 0; num10 < 40; num10++)
					{
						if (chest5.item[num10].type == 0)
						{
							int num11 = Main.rand.Next(array5.Length);
							chest5.item[0].SetDefaults(array5[num11], false);
							break;
						}
					}
				}
			}
			int[] array6 = new int[]
			{
				base.mod.ItemType("AncientWoodStave"),
				base.mod.ItemType("AncientWoodSword"),
				base.mod.ItemType("AncientWoodBow"),
				base.mod.ItemType("Falcon")
			};
			for (int num12 = 0; num12 < 1000; num12++)
			{
				Chest chest6 = Main.chest[num12];
				if (chest6 != null && (int)Main.tile[chest6.x, chest6.y].type == base.mod.TileType("AncientWoodChestTile"))
				{
					for (int num13 = 0; num13 < 40; num13++)
					{
						if (chest6.item[num13].type == 0)
						{
							int num14 = Main.rand.Next(array6.Length);
							chest6.item[0].SetDefaults(array6[num14], false);
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
			RedeWorld.spawnXenoBiome = false;
			RedeWorld.girusTalk1 = false;
			RedeWorld.girusTalk2 = false;
			RedeWorld.girusTalk3 = false;
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
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
				list.Add("OmegaOblitDamaged");
			}
			if (RedeWorld.downedSkullDigger)
			{
				list.Add("SkullDigger");
			}
			if (RedeWorld.downedSunkenCaptain)
			{
				list.Add("SunkenCaptain");
			}
			if (RedeWorld.spawnXenoBiome)
			{
				flag5 = true;
			}
			bool flag6 = RedeWorld.girusTalk1;
			bool flag7 = RedeWorld.girusTalk2;
			bool flag8 = RedeWorld.girusTalk3;
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("downed", list);
			tagCompound.Add("sapphiron", flag);
			tagCompound.Add("scarlion", flag2);
			tagCompound.Add("deathSlayer", flag3);
			tagCompound.Add("newbFound", flag4);
			tagCompound.Add("wasteland", flag5);
			tagCompound.Add("girTalk1", flag3);
			tagCompound.Add("girTalk2", flag3);
			tagCompound.Add("girTalk3", flag3);
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
			RedeWorld.downedVlitch3 = list.Contains("OmegaOblitDamaged");
			RedeWorld.downedSkullDigger = list.Contains("SkullDigger");
			RedeWorld.downedSunkenCaptain = list.Contains("SunkenCaptain");
			RedeWorld.spawnXenoBiome = tag.GetBool("wasteland");
			RedeWorld.girusTalk1 = tag.GetBool("girTalk1");
			RedeWorld.girusTalk2 = tag.GetBool("girTalk2");
			RedeWorld.girusTalk3 = tag.GetBool("girTalk3");
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

		public static bool spawnXenoBiome;

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

		public static bool girusTalk1;

		public static bool girusTalk2;

		public static bool girusTalk3;
	}
}
