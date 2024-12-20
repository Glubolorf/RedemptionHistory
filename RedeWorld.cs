using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Redemption.NPCs;
using Redemption.NPCs.v08;
using Redemption.Tiles.Wasteland;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace Redemption
{
	public class RedeWorld : ModWorld
	{
		public override void TileCountsAvailable(int[] tileCounts)
		{
			Main.sandTiles = Main.sandTiles + tileCounts[base.mod.TileType<RadioactiveSandTile>()] + tileCounts[base.mod.TileType<HardenedRadioactiveSandTile>()] + tileCounts[base.mod.TileType<RadioactiveSandstoneTile>()];
			Main.snowTiles += tileCounts[base.mod.TileType<RadioactiveIceTile>()];
			RedeWorld.xenoBiome = tileCounts[base.mod.TileType("DeadRockTile")] + tileCounts[base.mod.TileType("DeadGrassTile")] + tileCounts[base.mod.TileType("RadioactiveSandTile")] + tileCounts[base.mod.TileType("RadioactiveSandstoneTile")] + tileCounts[base.mod.TileType("RadioactiveIceTile")];
			RedeWorld.labBiome = tileCounts[base.mod.TileType("LabTileUnsafe")];
		}

		public override void ResetNearbyTileEffects()
		{
			RedeWorld.xenoBiome = 0;
			RedeWorld.labBiome = 0;
		}

		public override void PostUpdate()
		{
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !RedeWorld.spawnSapphironOre && !RedeWorld.spawnScarlionOre)
			{
				if (!WorldGen.crimson)
				{
					RedeWorld.spawnSapphironOre = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					string text = "Mods.Redemption.DruidicOre";
					Color lightBlue = Color.LightBlue;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(text, new object[0]), lightBlue, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(text), lightBlue, false);
					}
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						int num = WorldGen.genRand.Next(0, Main.maxTilesX);
						int num2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num, num2, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)base.mod.TileType("SapphironOreTile"));
					}
				}
				if (WorldGen.crimson)
				{
					RedeWorld.spawnScarlionOre = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					string text2 = "Mods.Redemption.DruidicOre";
					Color paleVioletRed = Color.PaleVioletRed;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(text2, new object[0]), paleVioletRed, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(text2), paleVioletRed, false);
					}
					for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); j++)
					{
						int num3 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int num4 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num3, num4, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)base.mod.TileType("ScarlionOreTile"));
					}
				}
			}
			if (NPC.downedBoss3 && !RedeWorld.spawnDragonOre)
			{
				RedeWorld.spawnDragonOre = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string text3 = "Mods.Redemption.DragonLeadMessage";
				Color orange = Color.Orange;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text3, new object[0]), orange, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text3), orange, false);
				}
				for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
				{
					int num5 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int num6 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.6f), (int)((float)Main.maxTilesY * 0.8f));
					WorldGen.OreRunner(num5, num6, (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(4, 15), (ushort)base.mod.TileType("DragonLeadOreTile"));
				}
			}
			if (RedeWorld.downedXenomiteCrystal && !RedeWorld.infectionBegin)
			{
				RedeWorld.infectionBegin = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string text4 = "Mods.Redemption.InfectionMessage1";
				Color green = Color.Green;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text4, new object[0]), green, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text4), green, false);
				}
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !RedeWorld.labSafe)
			{
				this.labSafeMessageTimer++;
				if (this.labSafeMessageTimer >= 300)
				{
					RedeWorld.labSafe = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					string text5 = "Mods.Redemption.LabIsSafe";
					Color cyan = Color.Cyan;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(text5, new object[0]), cyan, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(text5), cyan, false);
					}
				}
			}
			if (RedeWorld.downedInfectedEye && !RedeWorld.spawnXenoBiome)
			{
				RedeWorld.spawnXenoBiome = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string text6 = "Mods.Redemption.GrowingInfection";
				Color green2 = Color.Green;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text6, new object[0]), green2, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text6), green2, false);
				}
				for (int l = 0; l < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.5E-06); l++)
				{
					WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), (double)WorldGen.genRand.Next(90, 130), WorldGen.genRand.Next(90, 170), (ushort)base.mod.TileType("DeadRockTile"));
				}
			}
			if (RedeWorld.downedPatientZero && !RedeWorld.patientZeroMessages)
			{
				RedeWorld.patientZeroMessages = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string text7 = "Mods.Redemption.PatientZeroMessage1";
				string text8 = "Mods.Redemption.PatientZeroMessage2";
				string text9 = "Mods.Redemption.PatientZeroMessage3";
				string text10 = "Mods.Redemption.PatientZeroMessage4";
				string text11 = "Mods.Redemption.PatientZeroMessage5";
				Color gold = Color.Gold;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text7, new object[0]), gold, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text7), gold, false);
				}
				Color darkRed = Color.DarkRed;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text8, new object[0]), darkRed, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text8), darkRed, false);
				}
				Color hotPink = Color.HotPink;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text9, new object[0]), hotPink, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text9), hotPink, false);
				}
				Color forestGreen = Color.ForestGreen;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text10, new object[0]), forestGreen, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text10), forestGreen, false);
				}
				Color orange2 = Color.Orange;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(text11, new object[0]), orange2, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(text11), orange2, false);
				}
				int maxTilesX = Main.maxTilesX;
				int maxTilesY = Main.maxTilesY;
				for (int m = 0; m < (int)((double)(maxTilesX * maxTilesY) * 0.00015); m++)
				{
					int num7 = WorldGen.genRand.Next(0, maxTilesX);
					int num8 = WorldGen.genRand.Next(maxTilesY - 200, maxTilesY);
					if (Main.tile[num7, num8].type == 57)
					{
						WorldGen.TileRunner(num7, num8, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (int)((ushort)base.mod.TileType("ShinkiteTile")), false, 0f, 0f, false, true);
					}
				}
			}
			Player player = Main.player[Main.myPlayer];
			if (NPC.AnyNPCs(base.mod.NPCType<TheSoulless>()) || NPC.AnyNPCs(base.mod.NPCType<TheSoulless2>()))
			{
				if (!Filters.Scene["MoonLordShake"].IsActive())
				{
					Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
				}
				Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
			}
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
			int num2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Final Cleanup"));
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
				tasks.Insert(num + 2, new PassLegacy("Generating P L A N T", delegate(GenerationProgress progress)
				{
					progress.Message = "Generating P L A N T";
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						int maxTilesX = Main.maxTilesX;
						int maxTilesY = Main.maxTilesY;
						int num3 = WorldGen.genRand.Next(0, maxTilesX);
						int num4 = WorldGen.genRand.Next((int)((float)maxTilesY * 0.05f), (int)((double)maxTilesY * 0.3));
						if (Main.tile[num3, num4].type == 0)
						{
							WorldGen.OreRunner(num3, num4, (double)WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 6), (ushort)base.mod.TileType("PlantMatterTile"));
						}
					}
					for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 7E-05); j++)
					{
						int maxTilesX2 = Main.maxTilesX;
						int maxTilesY2 = Main.maxTilesY;
						int num5 = WorldGen.genRand.Next(0, maxTilesX2);
						int num6 = WorldGen.genRand.Next((int)((float)maxTilesY2 * 0.05f), (int)((double)maxTilesY2 * 0.5));
						if (Main.tile[num5, num6].type == 59)
						{
							WorldGen.OreRunner(num5, num6, (double)WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 8), (ushort)base.mod.TileType("PlantMatterTile"));
						}
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
			tasks.Insert(num2 + 4, new PassLegacy("Clearing Space for ???", delegate(GenerationProgress progress)
			{
				this.HeroHallClear();
			}));
			tasks.Insert(num2 + 5, new PassLegacy("???", delegate(GenerationProgress progress)
			{
				this.HeroHall();
			}));
			tasks.Insert(num2 + 6, new PassLegacy("??? Furniture", delegate(GenerationProgress progress)
			{
				this.HeroHallStuff();
			}));
			tasks.Insert(num2 + 7, new PassLegacy("Clearing Space for Lab", delegate(GenerationProgress progress)
			{
				this.LabClear();
			}));
			tasks.Insert(num2 + 8, new PassLegacy("Clearing Liquids for Lab", delegate(GenerationProgress progress)
			{
				this.PreLab();
			}));
			tasks.Insert(num2 + 9, new PassLegacy("Abandoned Lab", delegate(GenerationProgress progress)
			{
				this.Lab();
			}));
			tasks.Insert(num2 + 10, new PassLegacy("Furnishing Lab", delegate(GenerationProgress progress)
			{
				this.LabChests();
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
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/AncientHouse"), dictionary, inst.GetTexture("WorldGeneration/AncientHouseWalls"), dictionary2, null, null);
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

		public void HeroHallClear()
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			dictionary[new Color(150, 150, 150)] = -2;
			dictionary[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/TempleOfHeroesClear"), dictionary, null, null, null, null);
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(point.X, point.Y, true, true);
		}

		public void HeroHall()
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			dictionary[new Color(0, 255, 0)] = inst.TileType("AncientHallBrickTile");
			dictionary[new Color(255, 255, 0)] = inst.TileType("AncientStoneTile");
			dictionary[new Color(0, 0, 255)] = inst.TileType("AncientWoodTile");
			dictionary[new Color(255, 0, 255)] = 51;
			dictionary[new Color(150, 150, 150)] = -2;
			dictionary[Color.Black] = -1;
			Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
			dictionary2[new Color(255, 0, 255)] = inst.WallType("AncientHallPillarWall");
			dictionary2[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/TempleOfHeroes"), dictionary, inst.GetTexture("WorldGeneration/TempleOfHeroesWalls"), dictionary2, null, null);
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(point.X, point.Y, true, true);
		}

		public void HeroHallStuff()
		{
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 17, (int)((float)Main.maxTilesY * 0.45f) + 11, (int)((ushort)base.mod.TileType("HKStatueTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 25, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)base.mod.TileType("JStatueTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 6, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)base.mod.TileType("KSStatueTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 36, (int)((float)Main.maxTilesY * 0.45f) + 20, (int)((ushort)base.mod.TileType("NStatueTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 4, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 43, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 14, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 33, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 24, (int)((float)Main.maxTilesY * 0.45f) + 27, (int)((ushort)base.mod.TileType("AncientAltarTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 17, (int)((float)Main.maxTilesY * 0.45f) + 11, (int)((ushort)base.mod.TileType("HKStatueTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 25, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)base.mod.TileType("JStatueTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 6, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)base.mod.TileType("KSStatueTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 36, (int)((float)Main.maxTilesY * 0.45f) + 20, (int)((ushort)base.mod.TileType("NStatueTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 4, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 43, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 14, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 33, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)base.mod.TileType("ArchclothBannerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 24, (int)((float)Main.maxTilesY * 0.45f) + 27, (int)((ushort)base.mod.TileType("AncientAltarTile")), 0, 0, -1, -1);
		}

		public void LabClear()
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			dictionary[new Color(150, 150, 150)] = -2;
			dictionary[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/LabClear"), dictionary, null, null, null, null);
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.6f), (int)((float)Main.maxTilesY * 0.65f));
			texGenerator.Generate(point.X, point.Y, true, true);
		}

		public void PreLab()
		{
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.6f), (int)((float)Main.maxTilesY * 0.65f));
			WorldUtils.Gen(point, new Shapes.Rectangle(289, 217), Actions.Chain(new GenAction[]
			{
				new Actions.SetLiquid(0, 0)
			}));
		}

		public void Lab()
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			dictionary[new Color(0, 255, 0)] = inst.TileType("LabTileUnsafe");
			dictionary[new Color(255, 0, 255)] = 51;
			dictionary[new Color(0, 255, 255)] = 214;
			dictionary[new Color(0, 0, 255)] = inst.TileType("XenomiteOreBlock");
			dictionary[new Color(255, 255, 0)] = inst.TileType("HardenedSludgeTile");
			dictionary[new Color(187, 255, 90)] = inst.TileType("HardenedSludge2Tile");
			dictionary[new Color(17, 54, 17)] = inst.TileType("HardenedSludge3Tile");
			dictionary[new Color(120, 255, 255)] = inst.TileType("LabTubeTile");
			dictionary[new Color(220, 255, 255)] = inst.TileType("HalogenLampTile");
			dictionary[new Color(255, 100, 100)] = inst.TileType("RedLaserTile");
			dictionary[new Color(100, 255, 100)] = inst.TileType("GreenLaserTile");
			dictionary[new Color(255, 0, 0)] = 54;
			dictionary[new Color(255, 100, 0)] = 325;
			dictionary[new Color(150, 150, 150)] = -2;
			dictionary[Color.Black] = -1;
			Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
			dictionary2[new Color(0, 255, 0)] = inst.WallType("LabWallTileUnsafe");
			dictionary2[new Color(0, 0, 255)] = inst.WallType("HardenedSludgeWallTile");
			dictionary2[new Color(100, 0, 0)] = inst.WallType("HardenedlyHardenedSludgeWallTile");
			dictionary2[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/TrueLab"), dictionary, inst.GetTexture("WorldGeneration/TrueLabWalls"), dictionary2, inst.GetTexture("WorldGeneration/TrueLabLiquids"), null);
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.6f), (int)((float)Main.maxTilesY * 0.65f));
			texGenerator.Generate(point.X, point.Y, true, true);
		}

		public void LabChests()
		{
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 131, (int)((float)Main.maxTilesY * 0.65f) + 7, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 119, (int)((float)Main.maxTilesY * 0.65f) + 34, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 113, (int)((float)Main.maxTilesY * 0.65f) + 72, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 50, (int)((float)Main.maxTilesY * 0.65f) + 73, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 94, (int)((float)Main.maxTilesY * 0.65f) + 104, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 198, (int)((float)Main.maxTilesY * 0.65f) + 146, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 126, (int)((float)Main.maxTilesY * 0.65f) + 120, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 222, (int)((float)Main.maxTilesY * 0.65f) + 48, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 204, (int)((float)Main.maxTilesY * 0.65f) + 28, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 264, (int)((float)Main.maxTilesY * 0.65f) + 81, (int)((ushort)base.mod.TileType("DeadWoodChestTile")), 0, null, null, null, false);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 213, (int)((float)Main.maxTilesY * 0.65f) + 64, (int)((ushort)base.mod.TileType("BotanistStationTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 117, (int)((float)Main.maxTilesY * 0.65f) + 138, (int)((ushort)base.mod.TileType("XenoForgeTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 118, (int)((float)Main.maxTilesY * 0.65f) + 138, (int)((ushort)base.mod.TileType("XenoForgeTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 116, (int)((float)Main.maxTilesY * 0.65f) + 138, (int)((ushort)base.mod.TileType("XenoForgeTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 256, (int)((float)Main.maxTilesY * 0.65f) + 99, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 269, (int)((float)Main.maxTilesY * 0.65f) + 99, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 282, (int)((float)Main.maxTilesY * 0.65f) + 118, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 282, (int)((float)Main.maxTilesY * 0.65f) + 127, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 282, (int)((float)Main.maxTilesY * 0.65f) + 136, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 229, (int)((float)Main.maxTilesY * 0.65f) + 101, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 220, (int)((float)Main.maxTilesY * 0.65f) + 107, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 250, (int)((float)Main.maxTilesY * 0.65f) + 117, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 228, (int)((float)Main.maxTilesY * 0.65f) + 131, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 235, (int)((float)Main.maxTilesY * 0.65f) + 146, (int)((ushort)base.mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 82, (int)((float)Main.maxTilesY * 0.65f) + 27, (int)((ushort)base.mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 141, (int)((float)Main.maxTilesY * 0.65f) + 31, (int)((ushort)base.mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 122, (int)((float)Main.maxTilesY * 0.65f) + 5, (int)((ushort)base.mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 69, (int)((float)Main.maxTilesY * 0.65f) + 69, (int)((ushort)base.mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 92, (int)((float)Main.maxTilesY * 0.65f) + 98, (int)((ushort)base.mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 171, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 247, (int)((float)Main.maxTilesY * 0.65f) + 87, (int)((ushort)base.mod.TileType("SignBoiTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 235, (int)((float)Main.maxTilesY * 0.65f) + 36, (int)((ushort)base.mod.TileType("SignElectricTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 105, (int)((float)Main.maxTilesY * 0.65f) + 32, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 50, (int)((float)Main.maxTilesY * 0.65f) + 83, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 150, (int)((float)Main.maxTilesY * 0.65f) + 45, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 197, (int)((float)Main.maxTilesY * 0.65f) + 102, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 174, (int)((float)Main.maxTilesY * 0.65f) + 105, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 45, (int)((float)Main.maxTilesY * 0.65f) + 122, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 38, (int)((float)Main.maxTilesY * 0.65f) + 144, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 35, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 40, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 45, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 185, (int)((float)Main.maxTilesY * 0.65f) + 152, (int)((ushort)base.mod.TileType("LabForgeTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 176, (int)((float)Main.maxTilesY * 0.65f) + 36, (int)((ushort)base.mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 64, (int)((float)Main.maxTilesY * 0.65f) + 125, (int)((ushort)base.mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 130, (int)((float)Main.maxTilesY * 0.65f) + 29, (int)((ushort)base.mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 100, (int)((float)Main.maxTilesY * 0.65f) + 85, (int)((ushort)base.mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 135, (int)((float)Main.maxTilesY * 0.65f) + 143, (int)((ushort)base.mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 213, (int)((float)Main.maxTilesY * 0.65f) + 54, (int)((ushort)base.mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 129, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 147, (int)((float)Main.maxTilesY * 0.65f) + 152, (int)((ushort)base.mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 195, (int)((float)Main.maxTilesY * 0.65f) + 74, (int)((ushort)base.mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 175, (int)((float)Main.maxTilesY * 0.65f) + 17, (int)((ushort)base.mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 32, (int)((float)Main.maxTilesY * 0.65f) + 83, (int)((ushort)base.mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 173, (int)((float)Main.maxTilesY * 0.65f) + 58, (int)((ushort)base.mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 267, (int)((float)Main.maxTilesY * 0.65f) + 66, (int)((ushort)base.mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 22, (int)((float)Main.maxTilesY * 0.65f) + 111, (int)((ushort)base.mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 174, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BlisterfaceSummon")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 173, (int)((float)Main.maxTilesY * 0.65f) + 120, (int)((ushort)base.mod.TileType("BlisterHoleTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 90, (int)((float)Main.maxTilesY * 0.65f) + 33, (int)((ushort)base.mod.TileType("Stage2ScientistSummon")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 86, (int)((float)Main.maxTilesY * 0.65f) + 29, (int)((ushort)base.mod.TileType("ScientistVentTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 24, (int)((float)Main.maxTilesY * 0.65f) + 85, (int)((ushort)base.mod.TileType("Stage3ScientistSummon")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 158, (int)((float)Main.maxTilesY * 0.65f) + 38, (int)((ushort)base.mod.TileType("BehemothSummon")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 56, (int)((float)Main.maxTilesY * 0.65f) + 154, (int)((ushort)base.mod.TileType("MACESummon")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 55, (int)((float)Main.maxTilesY * 0.65f) + 127, (int)((ushort)base.mod.TileType("TBotConsole")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 144, (int)((float)Main.maxTilesY * 0.65f) + 208, (int)((ushort)base.mod.TileType("PatientZeroSummon")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 70, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 65, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 60, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 55, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 50, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 176, (int)((float)Main.maxTilesY * 0.65f) + 17, (int)((ushort)base.mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 170, (int)((float)Main.maxTilesY * 0.65f) + 85, (int)((ushort)base.mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 127, (int)((float)Main.maxTilesY * 0.65f) + 38, (int)((ushort)base.mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 154, (int)((float)Main.maxTilesY * 0.65f) + 48, (int)((ushort)base.mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 156, (int)((float)Main.maxTilesY * 0.65f) + 48, (int)((ushort)base.mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 265, (int)((float)Main.maxTilesY * 0.65f) + 92, (int)((ushort)base.mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 126, (int)((float)Main.maxTilesY * 0.65f) + 20, (int)((ushort)base.mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 128, (int)((float)Main.maxTilesY * 0.65f) + 20, (int)((ushort)base.mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 123, (int)((float)Main.maxTilesY * 0.65f) + 167, (int)((ushort)base.mod.TileType("ComputerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 86, (int)((float)Main.maxTilesY * 0.65f) + 60, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 174, (int)((float)Main.maxTilesY * 0.65f) + 61, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 250, (int)((float)Main.maxTilesY * 0.65f) + 57, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 128, (int)((float)Main.maxTilesY * 0.65f) + 104, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 112, (int)((float)Main.maxTilesY * 0.65f) + 163, (int)((ushort)base.mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 117, (int)((float)Main.maxTilesY * 0.65f) + 170, (int)((ushort)base.mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 119, (int)((float)Main.maxTilesY * 0.65f) + 170, (int)((ushort)base.mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 104, (int)((float)Main.maxTilesY * 0.65f) + 164, (int)((ushort)base.mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 99, (int)((float)Main.maxTilesY * 0.65f) + 174, (int)((ushort)base.mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 107, (int)((float)Main.maxTilesY * 0.65f) + 166, (int)((ushort)base.mod.TileType("SignBoiTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 110, (int)((float)Main.maxTilesY * 0.65f) + 166, (int)((ushort)base.mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.6f) + 257, (int)((float)Main.maxTilesY * 0.65f) + 45, (int)((ushort)base.mod.TileType("CorruptorTile")), false, 0, 0, -1, -1);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 181, (int)((float)Main.maxTilesY * 0.65f) + 79, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 128, (int)((float)Main.maxTilesY * 0.65f) + 74, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 270, (int)((float)Main.maxTilesY * 0.65f) + 57, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 280, (int)((float)Main.maxTilesY * 0.65f) + 81, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 271, (int)((float)Main.maxTilesY * 0.65f) + 129, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 274, (int)((float)Main.maxTilesY * 0.65f) + 129, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 135, (int)((float)Main.maxTilesY * 0.65f) + 104, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 21, (int)((float)Main.maxTilesY * 0.65f) + 158, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 24, (int)((float)Main.maxTilesY * 0.65f) + 158, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 27, (int)((float)Main.maxTilesY * 0.65f) + 158, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 118, (int)((float)Main.maxTilesY * 0.65f) + 53, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 283, (int)((float)Main.maxTilesY * 0.65f) + 81, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 201, (int)((float)Main.maxTilesY * 0.65f) + 123, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 172, (int)((float)Main.maxTilesY * 0.65f) + 7, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 51, (int)((float)Main.maxTilesY * 0.65f) + 59, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 90, (int)((float)Main.maxTilesY * 0.65f) + 155, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 178, (int)((float)Main.maxTilesY * 0.65f) + 79, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 262, (int)((float)Main.maxTilesY * 0.65f) + 129, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 218, (int)((float)Main.maxTilesY * 0.65f) + 146, (int)((ushort)base.mod.TileType("LabChestTileLocked2")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 222, (int)((float)Main.maxTilesY * 0.65f) + 149, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 141, (int)((float)Main.maxTilesY * 0.65f) + 165, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 143, (int)((float)Main.maxTilesY * 0.65f) + 165, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 145, (int)((float)Main.maxTilesY * 0.65f) + 165, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 147, (int)((float)Main.maxTilesY * 0.65f) + 165, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.6f) + 149, (int)((float)Main.maxTilesY * 0.65f) + 165, (int)((ushort)base.mod.TileType("LabChestTileLocked")), 0, null, null, null, false);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 213, (int)((float)Main.maxTilesY * 0.65f) + 64, (int)((ushort)base.mod.TileType("BotanistStationTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 117, (int)((float)Main.maxTilesY * 0.65f) + 138, (int)((ushort)base.mod.TileType("XenoForgeTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 118, (int)((float)Main.maxTilesY * 0.65f) + 138, (int)((ushort)base.mod.TileType("XenoForgeTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 116, (int)((float)Main.maxTilesY * 0.65f) + 138, (int)((ushort)base.mod.TileType("XenoForgeTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 256, (int)((float)Main.maxTilesY * 0.65f) + 99, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 269, (int)((float)Main.maxTilesY * 0.65f) + 99, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 282, (int)((float)Main.maxTilesY * 0.65f) + 118, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 282, (int)((float)Main.maxTilesY * 0.65f) + 127, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 282, (int)((float)Main.maxTilesY * 0.65f) + 136, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 229, (int)((float)Main.maxTilesY * 0.65f) + 101, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 220, (int)((float)Main.maxTilesY * 0.65f) + 107, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 250, (int)((float)Main.maxTilesY * 0.65f) + 117, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 228, (int)((float)Main.maxTilesY * 0.65f) + 131, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 235, (int)((float)Main.maxTilesY * 0.65f) + 146, (int)((ushort)base.mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 82, (int)((float)Main.maxTilesY * 0.65f) + 27, (int)((ushort)base.mod.TileType("IntercomTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 141, (int)((float)Main.maxTilesY * 0.65f) + 31, (int)((ushort)base.mod.TileType("IntercomTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 122, (int)((float)Main.maxTilesY * 0.65f) + 5, (int)((ushort)base.mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 69, (int)((float)Main.maxTilesY * 0.65f) + 69, (int)((ushort)base.mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 92, (int)((float)Main.maxTilesY * 0.65f) + 98, (int)((ushort)base.mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 171, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 247, (int)((float)Main.maxTilesY * 0.65f) + 87, (int)((ushort)base.mod.TileType("SignBoiTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 235, (int)((float)Main.maxTilesY * 0.65f) + 36, (int)((ushort)base.mod.TileType("SignElectricTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 105, (int)((float)Main.maxTilesY * 0.65f) + 32, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 50, (int)((float)Main.maxTilesY * 0.65f) + 83, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 150, (int)((float)Main.maxTilesY * 0.65f) + 45, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 197, (int)((float)Main.maxTilesY * 0.65f) + 102, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 174, (int)((float)Main.maxTilesY * 0.65f) + 105, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 45, (int)((float)Main.maxTilesY * 0.65f) + 122, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 38, (int)((float)Main.maxTilesY * 0.65f) + 144, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 35, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("XenoTank1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 40, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("XenoTank1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 45, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("XenoTank1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 185, (int)((float)Main.maxTilesY * 0.65f) + 152, (int)((ushort)base.mod.TileType("XenoTank1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 176, (int)((float)Main.maxTilesY * 0.65f) + 36, (int)((ushort)base.mod.TileType("XenoTank1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 64, (int)((float)Main.maxTilesY * 0.65f) + 125, (int)((ushort)base.mod.TileType("XenoTank1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 130, (int)((float)Main.maxTilesY * 0.65f) + 29, (int)((ushort)base.mod.TileType("VentTile4")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 100, (int)((float)Main.maxTilesY * 0.65f) + 85, (int)((ushort)base.mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 135, (int)((float)Main.maxTilesY * 0.65f) + 143, (int)((ushort)base.mod.TileType("VentTile4")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 213, (int)((float)Main.maxTilesY * 0.65f) + 54, (int)((ushort)base.mod.TileType("VentTile4")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 129, (int)((float)Main.maxTilesY * 0.65f) + 56, (int)((ushort)base.mod.TileType("VentTile4")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 147, (int)((float)Main.maxTilesY * 0.65f) + 152, (int)((ushort)base.mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 195, (int)((float)Main.maxTilesY * 0.65f) + 74, (int)((ushort)base.mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 175, (int)((float)Main.maxTilesY * 0.65f) + 17, (int)((ushort)base.mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 32, (int)((float)Main.maxTilesY * 0.65f) + 83, (int)((ushort)base.mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 173, (int)((float)Main.maxTilesY * 0.65f) + 58, (int)((ushort)base.mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 267, (int)((float)Main.maxTilesY * 0.65f) + 66, (int)((ushort)base.mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 22, (int)((float)Main.maxTilesY * 0.65f) + 111, (int)((ushort)base.mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 174, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BlisterfaceSummon")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 173, (int)((float)Main.maxTilesY * 0.65f) + 120, (int)((ushort)base.mod.TileType("BlisterHoleTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 90, (int)((float)Main.maxTilesY * 0.65f) + 33, (int)((ushort)base.mod.TileType("Stage2ScientistSummon")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 86, (int)((float)Main.maxTilesY * 0.65f) + 29, (int)((ushort)base.mod.TileType("ScientistVentTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 24, (int)((float)Main.maxTilesY * 0.65f) + 85, (int)((ushort)base.mod.TileType("Stage3ScientistSummon")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 158, (int)((float)Main.maxTilesY * 0.65f) + 38, (int)((ushort)base.mod.TileType("BehemothSummon")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 56, (int)((float)Main.maxTilesY * 0.65f) + 154, (int)((ushort)base.mod.TileType("MACESummon")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 55, (int)((float)Main.maxTilesY * 0.65f) + 127, (int)((ushort)base.mod.TileType("TBotConsole")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 144, (int)((float)Main.maxTilesY * 0.65f) + 208, (int)((ushort)base.mod.TileType("PatientZeroSummon")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 70, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 65, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 60, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 55, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 50, (int)((float)Main.maxTilesY * 0.65f) + 109, (int)((ushort)base.mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 176, (int)((float)Main.maxTilesY * 0.65f) + 17, (int)((ushort)base.mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 170, (int)((float)Main.maxTilesY * 0.65f) + 85, (int)((ushort)base.mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 127, (int)((float)Main.maxTilesY * 0.65f) + 38, (int)((ushort)base.mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 154, (int)((float)Main.maxTilesY * 0.65f) + 48, (int)((ushort)base.mod.TileType("LabTableTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 152, (int)((float)Main.maxTilesY * 0.65f) + 48, (int)((ushort)base.mod.TileType("LabChairTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 265, (int)((float)Main.maxTilesY * 0.65f) + 92, (int)((ushort)base.mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 126, (int)((float)Main.maxTilesY * 0.65f) + 20, (int)((ushort)base.mod.TileType("LabTableTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 123, (int)((float)Main.maxTilesY * 0.65f) + 20, (int)((ushort)base.mod.TileType("LabChairTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 123, (int)((float)Main.maxTilesY * 0.65f) + 167, (int)((ushort)base.mod.TileType("ComputerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 86, (int)((float)Main.maxTilesY * 0.65f) + 60, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 174, (int)((float)Main.maxTilesY * 0.65f) + 61, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 250, (int)((float)Main.maxTilesY * 0.65f) + 57, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 128, (int)((float)Main.maxTilesY * 0.65f) + 104, (int)((ushort)base.mod.TileType("HiveSpawnerTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 112, (int)((float)Main.maxTilesY * 0.65f) + 163, (int)((ushort)base.mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 117, (int)((float)Main.maxTilesY * 0.65f) + 170, (int)((ushort)base.mod.TileType("LabTableTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 119, (int)((float)Main.maxTilesY * 0.65f) + 170, (int)((ushort)base.mod.TileType("LabChairTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 104, (int)((float)Main.maxTilesY * 0.65f) + 164, (int)((ushort)base.mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 99, (int)((float)Main.maxTilesY * 0.65f) + 174, (int)((ushort)base.mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 107, (int)((float)Main.maxTilesY * 0.65f) + 166, (int)((ushort)base.mod.TileType("SignBoiTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 110, (int)((float)Main.maxTilesY * 0.65f) + 166, (int)((ushort)base.mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.6f) + 257, (int)((float)Main.maxTilesY * 0.65f) + 45, (int)((ushort)base.mod.TileType("CorruptorTile")), 0, 0, -1, -1);
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
				base.mod.ItemType("SludgeSpoon"),
				base.mod.ItemType("FloppyDisk1"),
				base.mod.ItemType("FloppyDisk3")
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
			for (int num15 = 1; num15 < 3; num15++)
			{
				int[] array7 = new int[]
				{
					base.mod.ItemType("Starlite"),
					base.mod.ItemType("XenomiteShard"),
					base.mod.ItemType("Electronade"),
					3460
				};
				int num16 = 0;
				for (int num17 = 0; num17 < 1000; num17++)
				{
					Chest chest7 = Main.chest[num17];
					if (chest7 != null && (int)Main.tile[chest7.x, chest7.y].type == base.mod.TileType("LabChestTileLocked"))
					{
						for (int num18 = 0; num18 < 40; num18++)
						{
							if (chest7.item[num18].type == 0)
							{
								chest7.item[num18].SetDefaults(array7[num16], false);
								chest7.item[num18].stack = Main.rand.Next(8, 12);
								num16 = (num16 + 1) % array7.Length;
								break;
							}
						}
					}
				}
			}
			for (int num19 = 1; num19 < 2; num19++)
			{
				int[] array8 = new int[]
				{
					base.mod.ItemType("ScrapMetal"),
					base.mod.ItemType("AIChip"),
					base.mod.ItemType("Mk3Capacitator"),
					base.mod.ItemType("Mk3Plating"),
					base.mod.ItemType("RawXenium")
				};
				int num20 = 0;
				for (int num21 = 0; num21 < 1000; num21++)
				{
					Chest chest8 = Main.chest[num21];
					if (chest8 != null && (int)Main.tile[chest8.x, chest8.y].type == base.mod.TileType("LabChestTileLocked"))
					{
						for (int num22 = 0; num22 < 40; num22++)
						{
							if (chest8.item[num22].type == 0)
							{
								chest8.item[num22].SetDefaults(array8[num20], false);
								chest8.item[num22].stack = Main.rand.Next(1, 3);
								num20 = (num20 + 1) % array8.Length;
								break;
							}
						}
					}
				}
			}
			int[] array9 = new int[]
			{
				base.mod.ItemType("HazmatSuit"),
				base.mod.ItemType("SuspiciousXenomiteShard"),
				base.mod.ItemType("Petridish"),
				base.mod.ItemType("DNAgger"),
				base.mod.ItemType("EmptyMutagen"),
				base.mod.ItemType("TeslaManipulatorPrototype"),
				base.mod.ItemType("FloppyDisk5"),
				base.mod.ItemType("FloppyDisk5_1"),
				base.mod.ItemType("FloppyDisk5_2"),
				base.mod.ItemType("FloppyDisk5_3"),
				base.mod.ItemType("TerraBombaPart1"),
				base.mod.ItemType("TerraBombaPart2"),
				base.mod.ItemType("TerraBombaPart3")
			};
			for (int num23 = 0; num23 < 1000; num23++)
			{
				Chest chest9 = Main.chest[num23];
				if (chest9 != null && (int)Main.tile[chest9.x, chest9.y].type == base.mod.TileType("LabChestTileLocked"))
				{
					for (int num24 = 0; num24 < 40; num24++)
					{
						if (chest9.item[num24].type == 0)
						{
							int num25 = Main.rand.Next(array9.Length);
							chest9.item[0].SetDefaults(array9[num25], false);
							break;
						}
					}
				}
			}
			int[] array10 = new int[]
			{
				base.mod.ItemType("NanoAxe")
			};
			for (int num26 = 0; num26 < 1000; num26++)
			{
				Chest chest10 = Main.chest[num26];
				if (chest10 != null && (int)Main.tile[chest10.x, chest10.y].type == base.mod.TileType("LabChestTileLocked2"))
				{
					for (int num27 = 0; num27 < 40; num27++)
					{
						if (chest10.item[num27].type == 0)
						{
							int num28 = Main.rand.Next(array10.Length);
							chest10.item[0].SetDefaults(array10[num28], false);
							break;
						}
					}
				}
			}
			for (int num29 = 1; num29 < 2; num29++)
			{
				int[] array11 = new int[]
				{
					base.mod.ItemType("RawXenium")
				};
				int num30 = 0;
				for (int num31 = 0; num31 < 1000; num31++)
				{
					Chest chest11 = Main.chest[num31];
					if (chest11 != null && (int)Main.tile[chest11.x, chest11.y].type == base.mod.TileType("LabChestTileLocked2"))
					{
						for (int num32 = 0; num32 < 40; num32++)
						{
							if (chest11.item[num32].type == 0)
							{
								chest11.item[num32].SetDefaults(array11[num30], false);
								chest11.item[num32].stack = Main.rand.Next(42, 62);
								num30 = (num30 + 1) % array11.Length;
								break;
							}
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
			RedeWorld.spawnDragonOre = false;
			RedeWorld.deathBySlayer = false;
			RedeWorld.foundNewb = false;
			RedeWorld.downedVlitch3 = false;
			RedeWorld.downedSkullDigger = false;
			RedeWorld.downedSunkenCaptain = false;
			RedeWorld.spawnXenoBiome = false;
			RedeWorld.starliteGenned = false;
			RedeWorld.girusTalk1 = false;
			RedeWorld.girusTalk2 = false;
			RedeWorld.girusTalk3 = false;
			RedeWorld.labSafe = false;
			RedeWorld.infectionBegin = false;
			RedeWorld.downedBlisterface = false;
			RedeWorld.downedStage2Scientist = false;
			RedeWorld.downedStage3Scientist = false;
			RedeWorld.labAccess1 = false;
			RedeWorld.labAccess2 = false;
			RedeWorld.labAccess3 = false;
			RedeWorld.labAccess4 = false;
			RedeWorld.labAccess5 = false;
			RedeWorld.labAccess6 = false;
			RedeWorld.labAccess7 = false;
			RedeWorld.keeperSaved = false;
			RedeWorld.downedIBehemoth = false;
			RedeWorld.downedMACE = false;
			RedeWorld.tbotLabAccess = false;
			RedeWorld.downedPatientZero = false;
			RedeWorld.patientZeroMessages = false;
			RedeWorld.downedNebuleus = false;
			RedeWorld.deathByNeb = false;
			RedeWorld.downedEaglecrestGolem = false;
			RedeWorld.downedChickenInvPZ = false;
			RedeWorld.downedChickenInv = false;
			RedeWorld.downedEaglecrestGolemPZ = false;
			RedeWorld.downedThorn = false;
			RedeWorld.downedThornPZ = false;
			RedeWorld.redemptionPoints = 0;
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			bool flag16 = false;
			bool flag17 = false;
			bool flag18 = false;
			bool flag19 = false;
			bool flag20 = false;
			bool flag21 = false;
			bool flag22 = false;
			bool flag23 = false;
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
			if (RedeWorld.spawnDragonOre)
			{
				flag3 = true;
			}
			if (RedeWorld.deathBySlayer)
			{
				flag4 = true;
			}
			if (RedeWorld.foundNewb)
			{
				flag5 = true;
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
				flag6 = true;
			}
			if (RedeWorld.starliteGenned)
			{
				flag7 = true;
			}
			if (RedeWorld.girusTalk1)
			{
				flag8 = true;
			}
			if (RedeWorld.girusTalk2)
			{
				flag9 = true;
			}
			if (RedeWorld.girusTalk3)
			{
				flag10 = true;
			}
			if (RedeWorld.labSafe)
			{
				flag11 = true;
			}
			if (RedeWorld.infectionBegin)
			{
				flag12 = true;
			}
			if (RedeWorld.downedBlisterface)
			{
				list.Add("Blisterface");
			}
			if (RedeWorld.downedStage2Scientist)
			{
				list.Add("Stage2ScientistBoss");
			}
			if (RedeWorld.downedStage3Scientist)
			{
				list.Add("Stage3Scientist");
			}
			if (RedeWorld.labAccess1)
			{
				flag13 = true;
			}
			if (RedeWorld.labAccess2)
			{
				flag14 = true;
			}
			if (RedeWorld.labAccess3)
			{
				flag15 = true;
			}
			if (RedeWorld.labAccess4)
			{
				flag16 = true;
			}
			if (RedeWorld.labAccess5)
			{
				flag17 = true;
			}
			if (RedeWorld.labAccess6)
			{
				flag18 = true;
			}
			if (RedeWorld.labAccess7)
			{
				flag19 = true;
			}
			if (RedeWorld.patientZeroMessages)
			{
				flag20 = true;
			}
			if (RedeWorld.keeperSaved)
			{
				flag21 = true;
			}
			if (RedeWorld.tbotLabAccess)
			{
				flag22 = true;
			}
			if (RedeWorld.downedIBehemoth)
			{
				list.Add("IrradiatedBehemoth");
			}
			if (RedeWorld.downedMACE)
			{
				list.Add("MACEProjectHead");
			}
			if (RedeWorld.downedPatientZero)
			{
				list.Add("PatientZero");
			}
			if (RedeWorld.downedNebuleus)
			{
				list.Add("Nebuleus");
			}
			if (RedeWorld.deathByNeb)
			{
				flag23 = true;
			}
			if (RedeWorld.downedEaglecrestGolem)
			{
				list.Add("EaglecrestGolem");
			}
			if (RedeWorld.downedEaglecrestGolemPZ)
			{
				list.Add("EaglecrestGolemPZ");
			}
			if (RedeWorld.downedThorn)
			{
				list.Add("Thorn");
			}
			if (RedeWorld.downedThornPZ)
			{
				list.Add("ThornPZ");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("downed", list);
			tagCompound.Add("sapphiron", flag);
			tagCompound.Add("scarlion", flag2);
			tagCompound.Add("dragore", flag3);
			tagCompound.Add("deathSlayer", flag4);
			tagCompound.Add("newbFound", flag5);
			tagCompound.Add("wasteland", flag6);
			tagCompound.Add("starliteGen", flag7);
			tagCompound.Add("girTalk1", flag8);
			tagCompound.Add("girTalk2", flag9);
			tagCompound.Add("girTalk3", flag10);
			tagCompound.Add("labSafe1", flag11);
			tagCompound.Add("infection1", flag12);
			tagCompound.Add("labA1", flag13);
			tagCompound.Add("labA2", flag14);
			tagCompound.Add("labA3", flag15);
			tagCompound.Add("labA4", flag16);
			tagCompound.Add("labA5", flag17);
			tagCompound.Add("labA6", flag18);
			tagCompound.Add("labA7", flag19);
			tagCompound.Add("pzMessage", flag20);
			tagCompound.Add("tbotLabLasers", flag22);
			tagCompound.Add("keeperS", flag21);
			tagCompound.Add("deathNeb", flag23);
			tagCompound.Add("downedChickenInv", RedeWorld.downedChickenInv);
			tagCompound.Add("downedChickenInvPZ", RedeWorld.downedChickenInvPZ);
			tagCompound.Add("redePoints", RedeWorld.redemptionPoints);
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
			RedeWorld.spawnDragonOre = tag.GetBool("dragore");
			RedeWorld.deathBySlayer = tag.GetBool("deathSlayer");
			RedeWorld.foundNewb = tag.GetBool("newbFound");
			RedeWorld.downedVlitch3 = list.Contains("OmegaOblitDamaged");
			RedeWorld.downedSkullDigger = list.Contains("SkullDigger");
			RedeWorld.downedSunkenCaptain = list.Contains("SunkenCaptain");
			RedeWorld.spawnXenoBiome = tag.GetBool("wasteland");
			RedeWorld.starliteGenned = tag.GetBool("starliteGen");
			RedeWorld.girusTalk1 = tag.GetBool("girTalk1");
			RedeWorld.girusTalk2 = tag.GetBool("girTalk2");
			RedeWorld.girusTalk3 = tag.GetBool("girTalk3");
			RedeWorld.labSafe = tag.GetBool("labSafe1");
			RedeWorld.infectionBegin = tag.GetBool("infection1");
			RedeWorld.downedBlisterface = list.Contains("Blisterface");
			RedeWorld.downedStage2Scientist = list.Contains("Stage2ScientistBoss");
			RedeWorld.downedStage3Scientist = list.Contains("Stage3Scientist");
			RedeWorld.labAccess1 = tag.GetBool("labA1");
			RedeWorld.labAccess2 = tag.GetBool("labA2");
			RedeWorld.labAccess3 = tag.GetBool("labA3");
			RedeWorld.labAccess4 = tag.GetBool("labA4");
			RedeWorld.labAccess5 = tag.GetBool("labA5");
			RedeWorld.labAccess6 = tag.GetBool("labA6");
			RedeWorld.labAccess7 = tag.GetBool("labA7");
			RedeWorld.patientZeroMessages = tag.GetBool("pzMessage");
			RedeWorld.keeperSaved = tag.GetBool("keeperS");
			RedeWorld.tbotLabAccess = tag.GetBool("tbotLabLasers");
			RedeWorld.downedIBehemoth = list.Contains("IrradiatedBehemoth");
			RedeWorld.downedMACE = list.Contains("MACEProjectHead");
			RedeWorld.downedPatientZero = list.Contains("PatientZero");
			RedeWorld.downedNebuleus = list.Contains("Nebuleus");
			RedeWorld.deathByNeb = tag.GetBool("deathNeb");
			RedeWorld.downedEaglecrestGolem = list.Contains("EaglecrestGolem");
			RedeWorld.downedChickenInv = tag.GetBool("downedChickenInv");
			RedeWorld.downedChickenInvPZ = tag.GetBool("downedChickenInvPZ");
			RedeWorld.downedEaglecrestGolemPZ = list.Contains("EaglecrestGolemPZ");
			RedeWorld.downedThorn = list.Contains("Thorn");
			RedeWorld.downedThornPZ = list.Contains("ThornPZ");
			RedeWorld.redemptionPoints = tag.GetInt("redePoints");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num == 0)
			{
				BitsByte bitsByte = reader.ReadByte();
				RedeWorld.downedKingChicken = bitsByte[0];
				RedeWorld.downedTheKeeper = bitsByte[1];
				RedeWorld.downedXenomiteCrystal = bitsByte[2];
				RedeWorld.downedInfectedEye = bitsByte[3];
				RedeWorld.downedStrangePortal = bitsByte[4];
				RedeWorld.downedVlitch1 = bitsByte[5];
				RedeWorld.downedVlitch2 = bitsByte[6];
				RedeWorld.downedDarkSlime = bitsByte[7];
				BitsByte bitsByte2 = reader.ReadByte();
				RedeWorld.downedSlayer = bitsByte2[0];
				RedeWorld.downedVlitch3 = bitsByte2[1];
				RedeWorld.downedSkullDigger = bitsByte2[2];
				RedeWorld.downedSunkenCaptain = bitsByte2[3];
				RedeWorld.downedBlisterface = bitsByte2[4];
				RedeWorld.downedStage2Scientist = bitsByte2[5];
				RedeWorld.downedStage3Scientist = bitsByte2[6];
				RedeWorld.downedIBehemoth = bitsByte2[7];
				BitsByte bitsByte3 = reader.ReadByte();
				RedeWorld.downedMACE = bitsByte3[0];
				RedeWorld.downedPatientZero = bitsByte3[1];
				RedeWorld.labSafe = bitsByte3[2];
				RedeWorld.deathBySlayer = bitsByte3[3];
				RedeWorld.girusTalk1 = bitsByte3[4];
				RedeWorld.girusTalk2 = bitsByte3[5];
				RedeWorld.girusTalk3 = bitsByte3[6];
				RedeWorld.keeperSaved = bitsByte3[7];
				BitsByte bitsByte4 = reader.ReadByte();
				RedeWorld.tbotLabAccess = bitsByte4[0];
				RedeWorld.downedPatientZero = bitsByte4[1];
				RedeWorld.labSafe = bitsByte4[2];
				RedeWorld.deathBySlayer = bitsByte4[3];
				RedeWorld.labAccess1 = bitsByte4[4];
				RedeWorld.labAccess2 = bitsByte4[5];
				RedeWorld.labAccess3 = bitsByte4[6];
				RedeWorld.labAccess4 = bitsByte4[7];
				BitsByte bitsByte5 = reader.ReadByte();
				RedeWorld.labAccess5 = bitsByte5[0];
				RedeWorld.labAccess6 = bitsByte5[1];
				RedeWorld.labAccess7 = bitsByte5[2];
				RedeWorld.infectionBegin = bitsByte5[3];
				RedeWorld.patientZeroMessages = bitsByte5[4];
				RedeWorld.downedNebuleus = bitsByte5[5];
				RedeWorld.deathByNeb = bitsByte5[6];
				RedeWorld.downedEaglecrestGolem = bitsByte5[7];
				BitsByte bitsByte6 = reader.ReadByte();
				RedeWorld.downedChickenInv = bitsByte6[0];
				RedeWorld.downedChickenInvPZ = bitsByte6[1];
				RedeWorld.downedEaglecrestGolemPZ = bitsByte6[2];
				RedeWorld.downedThorn = bitsByte6[3];
				RedeWorld.downedThornPZ = bitsByte6[4];
				return;
			}
			ErrorLogger.Log("Redemption: Unknown loadVersion: " + num);
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
			bitsByte2[4] = RedeWorld.downedBlisterface;
			bitsByte2[5] = RedeWorld.downedStage2Scientist;
			bitsByte2[6] = RedeWorld.downedStage3Scientist;
			bitsByte2[7] = RedeWorld.downedIBehemoth;
			writer.Write(bitsByte2);
			BitsByte bitsByte3 = default(BitsByte);
			bitsByte3[0] = RedeWorld.downedMACE;
			bitsByte3[1] = RedeWorld.downedPatientZero;
			bitsByte3[2] = RedeWorld.labSafe;
			bitsByte3[3] = RedeWorld.deathBySlayer;
			bitsByte3[4] = RedeWorld.girusTalk1;
			bitsByte3[5] = RedeWorld.girusTalk2;
			bitsByte3[6] = RedeWorld.girusTalk3;
			bitsByte3[7] = RedeWorld.keeperSaved;
			writer.Write(bitsByte3);
			BitsByte bitsByte4 = default(BitsByte);
			bitsByte4[0] = RedeWorld.tbotLabAccess;
			bitsByte4[1] = RedeWorld.downedPatientZero;
			bitsByte4[2] = RedeWorld.labSafe;
			bitsByte4[3] = RedeWorld.deathBySlayer;
			bitsByte4[4] = RedeWorld.labAccess1;
			bitsByte4[5] = RedeWorld.labAccess2;
			bitsByte4[6] = RedeWorld.labAccess3;
			bitsByte4[7] = RedeWorld.labAccess4;
			writer.Write(bitsByte4);
			BitsByte bitsByte5 = default(BitsByte);
			bitsByte5[0] = RedeWorld.labAccess5;
			bitsByte5[1] = RedeWorld.labAccess6;
			bitsByte5[2] = RedeWorld.labAccess7;
			bitsByte5[3] = RedeWorld.infectionBegin;
			bitsByte5[4] = RedeWorld.patientZeroMessages;
			bitsByte5[5] = RedeWorld.downedNebuleus;
			bitsByte5[6] = RedeWorld.deathByNeb;
			bitsByte5[7] = RedeWorld.downedEaglecrestGolem;
			writer.Write(bitsByte5);
			BitsByte bitsByte6 = default(BitsByte);
			bitsByte6[0] = RedeWorld.downedChickenInv;
			bitsByte6[1] = RedeWorld.downedChickenInvPZ;
			bitsByte6[2] = RedeWorld.downedEaglecrestGolemPZ;
			bitsByte6[3] = RedeWorld.downedThorn;
			bitsByte6[4] = RedeWorld.downedThornPZ;
			writer.Write(bitsByte6);
			writer.Write(RedeWorld.redemptionPoints);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte bitsByte = reader.ReadByte();
			RedeWorld.downedKingChicken = bitsByte[0];
			RedeWorld.downedTheKeeper = bitsByte[1];
			RedeWorld.downedXenomiteCrystal = bitsByte[2];
			RedeWorld.downedInfectedEye = bitsByte[3];
			RedeWorld.downedStrangePortal = bitsByte[4];
			RedeWorld.downedVlitch1 = bitsByte[5];
			RedeWorld.downedVlitch2 = bitsByte[6];
			RedeWorld.downedDarkSlime = bitsByte[7];
			BitsByte bitsByte2 = reader.ReadByte();
			RedeWorld.downedSlayer = bitsByte2[0];
			RedeWorld.downedVlitch3 = bitsByte2[1];
			RedeWorld.downedSkullDigger = bitsByte2[2];
			RedeWorld.downedSunkenCaptain = bitsByte2[3];
			RedeWorld.downedBlisterface = bitsByte2[4];
			RedeWorld.downedStage2Scientist = bitsByte2[5];
			RedeWorld.downedStage3Scientist = bitsByte2[6];
			RedeWorld.downedIBehemoth = bitsByte2[7];
			BitsByte bitsByte3 = reader.ReadByte();
			RedeWorld.downedMACE = bitsByte3[0];
			RedeWorld.downedPatientZero = bitsByte3[1];
			RedeWorld.labSafe = bitsByte3[2];
			RedeWorld.deathBySlayer = bitsByte3[3];
			RedeWorld.girusTalk1 = bitsByte3[4];
			RedeWorld.girusTalk2 = bitsByte3[5];
			RedeWorld.girusTalk3 = bitsByte3[6];
			RedeWorld.keeperSaved = bitsByte3[7];
			BitsByte bitsByte4 = reader.ReadByte();
			RedeWorld.tbotLabAccess = bitsByte4[0];
			RedeWorld.downedPatientZero = bitsByte4[1];
			RedeWorld.labSafe = bitsByte4[2];
			RedeWorld.deathBySlayer = bitsByte4[3];
			RedeWorld.labAccess1 = bitsByte4[4];
			RedeWorld.labAccess2 = bitsByte4[5];
			RedeWorld.labAccess3 = bitsByte4[6];
			RedeWorld.labAccess4 = bitsByte4[7];
			BitsByte bitsByte5 = reader.ReadByte();
			RedeWorld.labAccess5 = bitsByte5[0];
			RedeWorld.labAccess6 = bitsByte5[1];
			RedeWorld.labAccess7 = bitsByte5[2];
			RedeWorld.infectionBegin = bitsByte5[3];
			RedeWorld.patientZeroMessages = bitsByte5[4];
			RedeWorld.downedNebuleus = bitsByte5[5];
			RedeWorld.deathByNeb = bitsByte5[6];
			RedeWorld.downedEaglecrestGolem = bitsByte5[7];
			BitsByte bitsByte6 = reader.ReadByte();
			RedeWorld.downedChickenInv = bitsByte6[0];
			RedeWorld.downedChickenInvPZ = bitsByte6[1];
			RedeWorld.downedEaglecrestGolemPZ = bitsByte6[2];
			RedeWorld.downedThorn = bitsByte6[3];
			RedeWorld.downedThornPZ = bitsByte6[4];
			RedeWorld.redemptionPoints = reader.ReadInt32();
		}

		private const int saveVersion = 0;

		public static bool spawnOre;

		public static bool spawnDragonOre;

		public static bool spawnSapphironOre;

		public static bool spawnScarlionOre;

		public static bool spawnXenoBiome;

		public static bool starliteGenned;

		public static bool labSafe;

		public static bool infectionBegin;

		public static int xenoBiome;

		public static int labBiome;

		private int labSafeMessageTimer;

		private int starliteGenTimer;

		public static bool labAccess1;

		public static bool labAccess2;

		public static bool labAccess3;

		public static bool labAccess4;

		public static bool labAccess5;

		public static bool labAccess6;

		public static bool labAccess7;

		public static bool patientZeroMessages;

		public static bool KSRajahInteraction;

		public static int redemptionPoints;

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

		public static bool downedBlisterface;

		public static bool downedStage2Scientist;

		public static bool downedStage3Scientist;

		public static bool keeperSaved;

		public static bool downedIBehemoth;

		public static bool downedMACE;

		public static bool tbotLabAccess;

		public static bool downedPatientZero;

		public static bool downedNebuleus;

		public static bool deathByNeb;

		public static bool downedEaglecrestGolem;

		public static bool downedChickenInvPZ;

		public static bool downedChickenInv;

		public static bool downedEaglecrestGolemPZ;

		public static bool downedThorn;

		public static bool downedThornPZ;
	}
}
