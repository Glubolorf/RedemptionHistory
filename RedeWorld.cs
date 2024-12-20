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
		public static int GetWorldSize()
		{
			if (Main.maxTilesX <= 4200)
			{
				return 1;
			}
			if (Main.maxTilesX <= 6400)
			{
				return 2;
			}
			if (Main.maxTilesX <= 8400)
			{
				return 3;
			}
			return 1;
		}

		public static int GetBelowFloatingIslandY()
		{
			int size = RedeWorld.GetWorldSize();
			return ((size == 1) ? 1200 : ((size == 2) ? 1600 : ((size == 3) ? 2000 : 1200))) + 1;
		}

		public override void TileCountsAvailable(int[] tileCounts)
		{
			Main.sandTiles += tileCounts[ModContent.TileType<RadioactiveSandTile>()] + tileCounts[ModContent.TileType<HardenedRadioactiveSandTile>()] + tileCounts[ModContent.TileType<RadioactiveSandstoneTile>()];
			Main.snowTiles += tileCounts[ModContent.TileType<RadioactiveIceTile>()];
			RedeWorld.xenoBiome = tileCounts[base.mod.TileType("DeadRockTile")] + tileCounts[base.mod.TileType("DeadGrassTile")] + tileCounts[base.mod.TileType("RadioactiveSandTile")] + tileCounts[base.mod.TileType("RadioactiveSandstoneTile")] + tileCounts[base.mod.TileType("RadioactiveIceTile")];
			RedeWorld.evilXenoBiome = tileCounts[base.mod.TileType("DeadGrassTileCorruption")] + tileCounts[base.mod.TileType("DeadGrassTileCrimson")] + tileCounts[base.mod.TileType("IrradiatedCrimstoneTile")] + tileCounts[base.mod.TileType("IrradiatedEbonstoneTile")];
			RedeWorld.labBiome = tileCounts[base.mod.TileType("LabTileUnsafe")];
			RedeWorld.slayerBiome = tileCounts[base.mod.TileType("SlayerShipPanelTile")];
		}

		public override void ResetNearbyTileEffects()
		{
			RedeWorld.xenoBiome = 0;
			RedeWorld.evilXenoBiome = 0;
			RedeWorld.labBiome = 0;
			RedeWorld.slayerBiome = 0;
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
					string key = "Mods.Redemption.DruidicOre";
					Color messageColor = Color.LightBlue;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, new object[0]), messageColor, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor, false);
					}
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						int num = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num, j2, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)base.mod.TileType("SapphironOreTile"));
					}
				}
				if (WorldGen.crimson)
				{
					RedeWorld.spawnScarlionOre = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					string key2 = "Mods.Redemption.DruidicOre";
					Color messageColor2 = Color.PaleVioletRed;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(key2, new object[0]), messageColor2, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key2), messageColor2, false);
					}
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
					{
						int num2 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j3 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num2, j3, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)base.mod.TileType("ScarlionOreTile"));
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
				string key3 = "Mods.Redemption.DragonLeadMessage";
				Color messageColor3 = Color.Orange;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key3, new object[0]), messageColor3, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key3), messageColor3, false);
				}
				for (int l = 0; l < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); l++)
				{
					int num3 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int j4 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.6f), (int)((float)Main.maxTilesY * 0.8f));
					WorldGen.OreRunner(num3, j4, (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(4, 15), (ushort)base.mod.TileType("DragonLeadOreTile"));
				}
			}
			if (RedeWorld.downedXenomiteCrystal && !RedeWorld.infectionBegin)
			{
				RedeWorld.infectionBegin = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key4 = "Mods.Redemption.InfectionMessage1";
				Color messageColor4 = Color.Green;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key4, new object[0]), messageColor4, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key4), messageColor4, false);
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
					string key5 = "Mods.Redemption.LabIsSafe";
					Color messageColor5 = Color.Cyan;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(key5, new object[0]), messageColor5, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key5), messageColor5, false);
					}
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/LabSafeS").WithVolume(0.6f), -1, -1);
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
				string key6 = "Mods.Redemption.GrowingInfection";
				Color messageColor6 = Color.Green;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key6, new object[0]), messageColor6, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key6), messageColor6, false);
				}
				for (int m = 0; m < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.5E-06); m++)
				{
					WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), (double)WorldGen.genRand.Next(60, 80), WorldGen.genRand.Next(60, 90), (ushort)base.mod.TileType("DeadRockTile"));
				}
				int worldSize = RedeWorld.GetWorldSize();
				int nukeY = RedeWorld.GetBelowFloatingIslandY();
				int nukePos = (worldSize == 3) ? 4940 : ((worldSize == 2) ? 4240 : 3470);
				Point origin = new Point(Main.maxTilesX + nukePos, Main.maxTilesY + nukeY);
				origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
				int p = Projectile.NewProjectile((float)origin.X, (float)origin.Y, 0f, 0f, base.mod.ProjectileType("WastelandNuke"), 0, 1f, Main.myPlayer, 0f, 0f);
				Main.projectile[p].netUpdate = true;
			}
			if (RedeWorld.downedPatientZero && !RedeWorld.patientZeroMessages)
			{
				RedeWorld.patientZeroMessages = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key7 = "Mods.Redemption.PatientZeroMessage1";
				string key8 = "Mods.Redemption.PatientZeroMessage2";
				string key9 = "Mods.Redemption.PatientZeroMessage3";
				string key10 = "Mods.Redemption.PatientZeroMessage4";
				string key11 = "Mods.Redemption.PatientZeroMessage5";
				Color messageColor7 = Color.Gold;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key7, new object[0]), messageColor7, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key7), messageColor7, false);
				}
				Color messageColor8 = Color.DarkRed;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key8, new object[0]), messageColor8, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key8), messageColor8, false);
				}
				Color messageColor9 = Color.HotPink;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key9, new object[0]), messageColor9, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key9), messageColor9, false);
				}
				Color messageColor10 = Color.ForestGreen;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key10, new object[0]), messageColor10, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key10), messageColor10, false);
				}
				Color messageColor11 = Color.Orange;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key11, new object[0]), messageColor11, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key11), messageColor11, false);
				}
				int x = Main.maxTilesX;
				int y = Main.maxTilesY;
				for (int n = 0; n < (int)((double)(x * y) * 0.00015); n++)
				{
					int tilesX = WorldGen.genRand.Next(0, x);
					int tilesY = WorldGen.genRand.Next(y - 200, y);
					if (Main.tile[tilesX, tilesY].type == 57)
					{
						WorldGen.TileRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (int)((ushort)base.mod.TileType("ShinkiteTile")), false, 0f, 0f, false, true);
					}
				}
			}
			Player player = Main.player[Main.myPlayer];
			if (NPC.AnyNPCs(ModContent.NPCType<TheSoulless>()) || NPC.AnyNPCs(ModContent.NPCType<TheSoulless2>()))
			{
				if (!Filters.Scene["MoonLordShake"].IsActive())
				{
					Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
				}
				Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
			}
			if (RedeWorld.girusCloaked && !player.HasBuff(base.mod.BuffType("GirusCloakBuff")))
			{
				RedeWorld.girusCloakTimer++;
				if (RedeWorld.girusCloakTimer >= 120)
				{
					if (Main.rand.Next(3) == 0)
					{
						string key12 = "Mods.Redemption.GirusHide";
						Color messageColor12 = new Color(255, 32, 32);
						if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromKey(key12, new object[0]), messageColor12, -1);
						}
						else if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key12), messageColor12, false);
						}
						Redemption.SpawnBoss(player, "CorruptedCopter1", false, 0, 0, "", false);
					}
					RedeWorld.girusCloaked = false;
					RedeWorld.girusCloakTimer = 0;
				}
			}
			if (player.HasBuff(base.mod.BuffType("IntruderAlertDebuff")) && Main.rand.Next(200) == 0 && NPC.CountNPCS(base.mod.NPCType("LabSentryDrone")) <= 3)
			{
				int A = Main.rand.Next(-200, 200) * 6;
				int drone = NPC.NewNPC((int)player.Center.X + A, (int)player.Center.Y + A, base.mod.NPCType("LabSentryDrone"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[drone].netUpdate = true;
			}
			if (RedeWorld.golemLure && !player.HasBuff(base.mod.BuffType("GolemSpelltomeBuff")))
			{
				RedeWorld.golemLure = false;
			}
			if (RedeWorld.darkSlimeLure && !player.HasBuff(base.mod.BuffType("EvilJellyBuff")))
			{
				RedeWorld.darkSlimeLure = false;
			}
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int ShiniesIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
			int ShiniesIndex2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Final Cleanup"));
			if (ShiniesIndex != -1)
			{
				tasks.Insert(ShiniesIndex + 1, new PassLegacy("Redemption Mod Ores", delegate(GenerationProgress progress)
				{
					progress.Message = "Redemption Mod Ores";
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(6, 10), WorldGen.genRand.Next(2, 4), base.mod.TileType("KaniteOreTile"), false, 0f, 0f, false, true);
					}
					for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 4E-05); j++)
					{
						int num = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.7f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num, j2, (double)WorldGen.genRand.Next(1, 2), WorldGen.genRand.Next(2, 4), (ushort)base.mod.TileType("UraniumTile"));
					}
				}));
				tasks.Insert(ShiniesIndex + 2, new PassLegacy("Generating P L A N T", delegate(GenerationProgress progress)
				{
					progress.Message = "Generating P L A N T";
					for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
					{
						int x = Main.maxTilesX;
						int y = Main.maxTilesY;
						int tilesX = WorldGen.genRand.Next(0, x);
						int tilesY = WorldGen.genRand.Next((int)((float)y * 0.05f), (int)((double)y * 0.3));
						if (Main.tile[tilesX, tilesY].type == 0)
						{
							WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 6), (ushort)base.mod.TileType("PlantMatterTile"));
						}
					}
					for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 7E-05); j++)
					{
						int x2 = Main.maxTilesX;
						int y2 = Main.maxTilesY;
						int tilesX2 = WorldGen.genRand.Next(0, x2);
						int tilesY2 = WorldGen.genRand.Next((int)((float)y2 * 0.05f), (int)((double)y2 * 0.5));
						if (Main.tile[tilesX2, tilesY2].type == 59)
						{
							WorldGen.OreRunner(tilesX2, tilesY2, (double)WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 8), (ushort)base.mod.TileType("PlantMatterTile"));
						}
					}
				}));
			}
			tasks.Insert(ShiniesIndex + 4, new PassLegacy("Ancient House", delegate(GenerationProgress progress)
			{
				this.AncientHouse();
			}));
			tasks.Insert(ShiniesIndex + 5, new PassLegacy("Furnishing Ancient House", delegate(GenerationProgress progress)
			{
				this.AncientHouseFurn();
			}));
			tasks.Insert(ShiniesIndex2 + 4, new PassLegacy("Clearing Space for ???", delegate(GenerationProgress progress)
			{
				this.HeroHallClear();
			}));
			tasks.Insert(ShiniesIndex2 + 5, new PassLegacy("???", delegate(GenerationProgress progress)
			{
				this.HeroHall();
			}));
			tasks.Insert(ShiniesIndex2 + 6, new PassLegacy("??? Furniture", delegate(GenerationProgress progress)
			{
				this.HeroHallStuff();
			}));
		}

		public void AncientHouse()
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(255, 100, 0)] = mod.TileType("AncientWoodTile");
			colorToTile[new Color(0, 255, 0)] = mod.TileType("AncientDirtTile");
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(255, 0, 255)] = mod.WallType("AncientWoodWallTile");
			colorToWall[new Color(0, 255, 255)] = 63;
			colorToWall[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/AncientHouse"), colorToTile, mod.GetTexture("WorldGeneration/AncientHouseWalls"), colorToWall, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.07f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
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
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/TempleOfHeroesClear"), colorToTile, null, null, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
		}

		public void HeroHall()
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 0)] = mod.TileType("AncientHallBrickTile");
			colorToTile[new Color(255, 255, 0)] = mod.TileType("AncientStoneTile");
			colorToTile[new Color(0, 0, 255)] = mod.TileType("AncientWoodTile");
			colorToTile[new Color(255, 0, 255)] = 51;
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(255, 0, 255)] = mod.WallType("AncientHallPillarWall");
			colorToWall[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/TempleOfHeroes"), colorToTile, mod.GetTexture("WorldGeneration/TempleOfHeroesWalls"), colorToWall, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
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

		public override void PostWorldGen()
		{
			int[] itemsToPlaceInDungeonChests = new int[]
			{
				base.mod.ItemType("DonjonStave")
			};
			int itemsToPlaceInDungeonChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				if (chest != null && Main.tile[chest.x, chest.y].type == 21 && Main.tile[chest.x, chest.y].frameX == 72 && Main.rand.Next(3) == 0)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == 0)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInDungeonChests[itemsToPlaceInDungeonChestsChoice], false);
							itemsToPlaceInDungeonChestsChoice = (itemsToPlaceInDungeonChestsChoice + 1) % itemsToPlaceInDungeonChests.Length;
							break;
						}
					}
				}
			}
			int[] itemsToPlaceInWaterChests = new int[]
			{
				base.mod.ItemType("GildedSeaAxe"),
				base.mod.ItemType("SeaNote")
			};
			int itemsToPlaceInWaterChestsChoice = 0;
			for (int chestIndex2 = 0; chestIndex2 < 1000; chestIndex2++)
			{
				Chest chest2 = Main.chest[chestIndex2];
				if (chest2 != null && Main.tile[chest2.x, chest2.y].type == 21 && Main.tile[chest2.x, chest2.y].frameX == 612 && Main.rand.Next(10) == 0)
				{
					for (int inventoryIndex2 = 0; inventoryIndex2 < 40; inventoryIndex2++)
					{
						if (chest2.item[inventoryIndex2].type == 0)
						{
							chest2.item[inventoryIndex2].SetDefaults(itemsToPlaceInWaterChests[itemsToPlaceInWaterChestsChoice], false);
							itemsToPlaceInWaterChestsChoice = (itemsToPlaceInWaterChestsChoice + 1) % itemsToPlaceInWaterChests.Length;
							break;
						}
					}
				}
			}
			int[] itemsToPlaceInWoodChests = new int[]
			{
				base.mod.ItemType("AcornBomb")
			};
			int itemsToPlaceInWoodChestsChoice = 0;
			for (int chestIndex3 = 0; chestIndex3 < 1000; chestIndex3++)
			{
				Chest chest3 = Main.chest[chestIndex3];
				if (chest3 != null && Main.tile[chest3.x, chest3.y].type == 21 && Main.tile[chest3.x, chest3.y].frameX == 0 && Main.rand.Next(3) == 0)
				{
					for (int inventoryIndex3 = 0; inventoryIndex3 < 40; inventoryIndex3++)
					{
						if (chest3.item[inventoryIndex3].type == 0)
						{
							chest3.item[inventoryIndex3].SetDefaults(itemsToPlaceInWoodChests[itemsToPlaceInWoodChestsChoice], false);
							chest3.item[inventoryIndex3].stack = Main.rand.Next(3, 5);
							itemsToPlaceInWoodChestsChoice = (itemsToPlaceInWoodChestsChoice + 1) % itemsToPlaceInWoodChests.Length;
							break;
						}
					}
				}
			}
			int[] itemsToPlaceInAncientChests = new int[]
			{
				base.mod.ItemType("AncientWoodStave"),
				base.mod.ItemType("AncientWoodSword"),
				base.mod.ItemType("AncientWoodBow"),
				base.mod.ItemType("Falcon")
			};
			for (int chestIndex4 = 0; chestIndex4 < 1000; chestIndex4++)
			{
				Chest chest4 = Main.chest[chestIndex4];
				if (chest4 != null && (int)Main.tile[chest4.x, chest4.y].type == base.mod.TileType("AncientWoodChestTile"))
				{
					for (int inventoryIndex4 = 0; inventoryIndex4 < 40; inventoryIndex4++)
					{
						if (chest4.item[inventoryIndex4].type == 0)
						{
							int itemsToPlaceInAncientChestsChoice = Main.rand.Next(itemsToPlaceInAncientChests.Length);
							chest4.item[0].SetDefaults(itemsToPlaceInAncientChests[itemsToPlaceInAncientChestsChoice], false);
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
			RedeWorld.girusCloaked = false;
			RedeWorld.girusCloakTimer = 0;
			RedeWorld.slayerRep = 0;
			RedeWorld.downedJanitor = false;
			RedeWorld.downedVolt = false;
			RedeWorld.voltBegin = false;
			RedeWorld.pzUS = false;
			RedeWorld.maceUS = false;
		}

		public override TagCompound Save()
		{
			List<string> downed = new List<string>();
			bool sapp = false;
			bool scar = false;
			bool drago = false;
			bool dbs = false;
			bool fn = false;
			bool waste = false;
			bool starlite = false;
			bool gir = false;
			bool gir2 = false;
			bool gir3 = false;
			bool unsafelab = false;
			bool infect = false;
			bool labAcc = false;
			bool labAcc2 = false;
			bool labAcc3 = false;
			bool labAcc4 = false;
			bool labAcc5 = false;
			bool labAcc6 = false;
			bool labAcc7 = false;
			bool pzmess = false;
			bool keepSave = false;
			bool tbotLab = false;
			bool dbn = false;
			bool voltB = false;
			if (RedeWorld.downedKingChicken)
			{
				downed.Add("KingChicken");
			}
			if (RedeWorld.downedTheKeeper)
			{
				downed.Add("TheKeeper");
			}
			if (RedeWorld.downedXenomiteCrystal)
			{
				downed.Add("XenomiteCrystalPhase2");
			}
			if (RedeWorld.downedInfectedEye)
			{
				downed.Add("InfectedEye");
			}
			if (RedeWorld.downedStrangePortal)
			{
				downed.Add("StrangePortal");
			}
			if (RedeWorld.downedVlitch1)
			{
				downed.Add("VlitchCleaver");
			}
			if (RedeWorld.downedVlitch2)
			{
				downed.Add("VlitchWormHead");
			}
			if (RedeWorld.downedDarkSlime)
			{
				downed.Add("DarkSlime");
			}
			if (RedeWorld.downedSlayer)
			{
				downed.Add("KSEntrance");
			}
			if (RedeWorld.spawnSapphironOre)
			{
				sapp = true;
			}
			if (RedeWorld.spawnScarlionOre)
			{
				scar = true;
			}
			if (RedeWorld.spawnDragonOre)
			{
				drago = true;
			}
			if (RedeWorld.deathBySlayer)
			{
				dbs = true;
			}
			if (RedeWorld.foundNewb)
			{
				fn = true;
			}
			if (RedeWorld.downedVlitch3)
			{
				downed.Add("OmegaOblitDamaged");
			}
			if (RedeWorld.downedSkullDigger)
			{
				downed.Add("SkullDigger");
			}
			if (RedeWorld.downedSunkenCaptain)
			{
				downed.Add("SunkenCaptain");
			}
			if (RedeWorld.spawnXenoBiome)
			{
				waste = true;
			}
			if (RedeWorld.starliteGenned)
			{
				starlite = true;
			}
			if (RedeWorld.girusTalk1)
			{
				gir = true;
			}
			if (RedeWorld.girusTalk2)
			{
				gir2 = true;
			}
			if (RedeWorld.girusTalk3)
			{
				gir3 = true;
			}
			if (RedeWorld.labSafe)
			{
				unsafelab = true;
			}
			if (RedeWorld.infectionBegin)
			{
				infect = true;
			}
			if (RedeWorld.downedBlisterface)
			{
				downed.Add("Blisterface");
			}
			if (RedeWorld.downedStage2Scientist)
			{
				downed.Add("Stage2ScientistBoss");
			}
			if (RedeWorld.downedStage3Scientist)
			{
				downed.Add("Stage3Scientist");
			}
			if (RedeWorld.labAccess1)
			{
				labAcc = true;
			}
			if (RedeWorld.labAccess2)
			{
				labAcc2 = true;
			}
			if (RedeWorld.labAccess3)
			{
				labAcc3 = true;
			}
			if (RedeWorld.labAccess4)
			{
				labAcc4 = true;
			}
			if (RedeWorld.labAccess5)
			{
				labAcc5 = true;
			}
			if (RedeWorld.labAccess6)
			{
				labAcc6 = true;
			}
			if (RedeWorld.labAccess7)
			{
				labAcc7 = true;
			}
			if (RedeWorld.patientZeroMessages)
			{
				pzmess = true;
			}
			if (RedeWorld.keeperSaved)
			{
				keepSave = true;
			}
			if (RedeWorld.tbotLabAccess)
			{
				tbotLab = true;
			}
			if (RedeWorld.downedIBehemoth)
			{
				downed.Add("IrradiatedBehemoth");
			}
			if (RedeWorld.downedMACE)
			{
				downed.Add("MACEProjectHead");
			}
			if (RedeWorld.downedPatientZero)
			{
				downed.Add("PatientZero");
			}
			if (RedeWorld.downedNebuleus)
			{
				downed.Add("Nebuleus");
			}
			if (RedeWorld.deathByNeb)
			{
				dbn = true;
			}
			if (RedeWorld.downedEaglecrestGolem)
			{
				downed.Add("EaglecrestGolem");
			}
			if (RedeWorld.downedEaglecrestGolemPZ)
			{
				downed.Add("EaglecrestGolemPZ");
			}
			if (RedeWorld.downedThorn)
			{
				downed.Add("Thorn");
			}
			if (RedeWorld.downedThornPZ)
			{
				downed.Add("ThornPZ");
			}
			if (RedeWorld.downedJanitor)
			{
				downed.Add("JanitorBot");
			}
			if (RedeWorld.downedVolt)
			{
				downed.Add("TbotMiniboss");
			}
			if (RedeWorld.voltBegin)
			{
				voltB = true;
			}
			if (RedeWorld.pzUS)
			{
				downed.Add("PUS");
			}
			if (RedeWorld.maceUS)
			{
				downed.Add("MPUS");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("downed", downed);
			tagCompound.Add("sapphiron", sapp);
			tagCompound.Add("scarlion", scar);
			tagCompound.Add("dragore", drago);
			tagCompound.Add("deathSlayer", dbs);
			tagCompound.Add("newbFound", fn);
			tagCompound.Add("wasteland", waste);
			tagCompound.Add("starliteGen", starlite);
			tagCompound.Add("girTalk1", gir);
			tagCompound.Add("girTalk2", gir2);
			tagCompound.Add("girTalk3", gir3);
			tagCompound.Add("labSafe1", unsafelab);
			tagCompound.Add("infection1", infect);
			tagCompound.Add("labA1", labAcc);
			tagCompound.Add("labA2", labAcc2);
			tagCompound.Add("labA3", labAcc3);
			tagCompound.Add("labA4", labAcc4);
			tagCompound.Add("labA5", labAcc5);
			tagCompound.Add("labA6", labAcc6);
			tagCompound.Add("labA7", labAcc7);
			tagCompound.Add("pzMessage", pzmess);
			tagCompound.Add("tbotLabLasers", tbotLab);
			tagCompound.Add("keeperS", keepSave);
			tagCompound.Add("deathNeb", dbn);
			tagCompound.Add("downedChickenInv", RedeWorld.downedChickenInv);
			tagCompound.Add("downedChickenInvPZ", RedeWorld.downedChickenInvPZ);
			tagCompound.Add("redePoints", RedeWorld.redemptionPoints);
			tagCompound.Add("slayRep", RedeWorld.slayerRep);
			tagCompound.Add("voltBeginFight", voltB);
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
			RedeWorld.slayerRep = tag.GetInt("slayRep");
			RedeWorld.downedJanitor = list.Contains("JanitorBot");
			RedeWorld.downedVolt = list.Contains("TbotMiniboss");
			RedeWorld.voltBegin = tag.GetBool("voltBeginFight");
			RedeWorld.pzUS = list.Contains("PUS");
			RedeWorld.maceUS = list.Contains("MPUS");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				RedeWorld.downedKingChicken = flags[0];
				RedeWorld.downedTheKeeper = flags[1];
				RedeWorld.downedXenomiteCrystal = flags[2];
				RedeWorld.downedInfectedEye = flags[3];
				RedeWorld.downedStrangePortal = flags[4];
				RedeWorld.downedVlitch1 = flags[5];
				RedeWorld.downedVlitch2 = flags[6];
				RedeWorld.downedDarkSlime = flags[7];
				BitsByte flags2 = reader.ReadByte();
				RedeWorld.downedSlayer = flags2[0];
				RedeWorld.downedVlitch3 = flags2[1];
				RedeWorld.downedSkullDigger = flags2[2];
				RedeWorld.downedSunkenCaptain = flags2[3];
				RedeWorld.downedBlisterface = flags2[4];
				RedeWorld.downedStage2Scientist = flags2[5];
				RedeWorld.downedStage3Scientist = flags2[6];
				RedeWorld.downedIBehemoth = flags2[7];
				BitsByte flags3 = reader.ReadByte();
				RedeWorld.downedMACE = flags3[0];
				RedeWorld.downedPatientZero = flags3[1];
				RedeWorld.labSafe = flags3[2];
				RedeWorld.deathBySlayer = flags3[3];
				RedeWorld.girusTalk1 = flags3[4];
				RedeWorld.girusTalk2 = flags3[5];
				RedeWorld.girusTalk3 = flags3[6];
				RedeWorld.keeperSaved = flags3[7];
				BitsByte flags4 = reader.ReadByte();
				RedeWorld.tbotLabAccess = flags4[0];
				RedeWorld.downedPatientZero = flags4[1];
				RedeWorld.labSafe = flags4[2];
				RedeWorld.deathBySlayer = flags4[3];
				RedeWorld.labAccess1 = flags4[4];
				RedeWorld.labAccess2 = flags4[5];
				RedeWorld.labAccess3 = flags4[6];
				RedeWorld.labAccess4 = flags4[7];
				BitsByte flags5 = reader.ReadByte();
				RedeWorld.labAccess5 = flags5[0];
				RedeWorld.labAccess6 = flags5[1];
				RedeWorld.labAccess7 = flags5[2];
				RedeWorld.infectionBegin = flags5[3];
				RedeWorld.patientZeroMessages = flags5[4];
				RedeWorld.downedNebuleus = flags5[5];
				RedeWorld.deathByNeb = flags5[6];
				RedeWorld.downedEaglecrestGolem = flags5[7];
				BitsByte flags6 = reader.ReadByte();
				RedeWorld.downedChickenInv = flags6[0];
				RedeWorld.downedChickenInvPZ = flags6[1];
				RedeWorld.downedEaglecrestGolemPZ = flags6[2];
				RedeWorld.downedThorn = flags6[3];
				RedeWorld.downedThornPZ = flags6[4];
				RedeWorld.downedJanitor = flags6[5];
				RedeWorld.downedVolt = flags6[6];
				RedeWorld.voltBegin = flags6[7];
				return;
			}
			base.mod.Logger.Debug("Redemption: Unknown loadVersion: " + loadVersion);
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = default(BitsByte);
			flags[0] = RedeWorld.downedKingChicken;
			flags[1] = RedeWorld.downedTheKeeper;
			flags[2] = RedeWorld.downedXenomiteCrystal;
			flags[3] = RedeWorld.downedInfectedEye;
			flags[4] = RedeWorld.downedStrangePortal;
			flags[5] = RedeWorld.downedVlitch1;
			flags[6] = RedeWorld.downedVlitch2;
			flags[7] = RedeWorld.downedDarkSlime;
			writer.Write(flags);
			BitsByte flags2 = default(BitsByte);
			flags2[0] = RedeWorld.downedSlayer;
			flags2[1] = RedeWorld.downedVlitch3;
			flags2[2] = RedeWorld.downedSkullDigger;
			flags2[3] = RedeWorld.downedSunkenCaptain;
			flags2[4] = RedeWorld.downedBlisterface;
			flags2[5] = RedeWorld.downedStage2Scientist;
			flags2[6] = RedeWorld.downedStage3Scientist;
			flags2[7] = RedeWorld.downedIBehemoth;
			writer.Write(flags2);
			BitsByte flags3 = default(BitsByte);
			flags3[0] = RedeWorld.downedMACE;
			flags3[1] = RedeWorld.downedPatientZero;
			flags3[2] = RedeWorld.labSafe;
			flags3[3] = RedeWorld.deathBySlayer;
			flags3[4] = RedeWorld.girusTalk1;
			flags3[5] = RedeWorld.girusTalk2;
			flags3[6] = RedeWorld.girusTalk3;
			flags3[7] = RedeWorld.keeperSaved;
			writer.Write(flags3);
			BitsByte flags4 = default(BitsByte);
			flags4[0] = RedeWorld.tbotLabAccess;
			flags4[1] = RedeWorld.downedPatientZero;
			flags4[2] = RedeWorld.labSafe;
			flags4[3] = RedeWorld.deathBySlayer;
			flags4[4] = RedeWorld.labAccess1;
			flags4[5] = RedeWorld.labAccess2;
			flags4[6] = RedeWorld.labAccess3;
			flags4[7] = RedeWorld.labAccess4;
			writer.Write(flags4);
			BitsByte flags5 = default(BitsByte);
			flags5[0] = RedeWorld.labAccess5;
			flags5[1] = RedeWorld.labAccess6;
			flags5[2] = RedeWorld.labAccess7;
			flags5[3] = RedeWorld.infectionBegin;
			flags5[4] = RedeWorld.patientZeroMessages;
			flags5[5] = RedeWorld.downedNebuleus;
			flags5[6] = RedeWorld.deathByNeb;
			flags5[7] = RedeWorld.downedEaglecrestGolem;
			writer.Write(flags5);
			BitsByte flags6 = default(BitsByte);
			flags6[0] = RedeWorld.downedChickenInv;
			flags6[1] = RedeWorld.downedChickenInvPZ;
			flags6[2] = RedeWorld.downedEaglecrestGolemPZ;
			flags6[3] = RedeWorld.downedThorn;
			flags6[4] = RedeWorld.downedThornPZ;
			flags6[5] = RedeWorld.downedJanitor;
			flags6[6] = RedeWorld.downedVolt;
			flags6[7] = RedeWorld.voltBegin;
			writer.Write(flags6);
			BitsByte flags7 = default(BitsByte);
			flags7[0] = RedeWorld.pzUS;
			flags7[1] = RedeWorld.maceUS;
			writer.Write(flags7);
			writer.Write(RedeWorld.redemptionPoints);
			writer.Write(RedeWorld.girusCloakTimer);
			writer.Write(RedeWorld.slayerRep);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			RedeWorld.downedKingChicken = flags[0];
			RedeWorld.downedTheKeeper = flags[1];
			RedeWorld.downedXenomiteCrystal = flags[2];
			RedeWorld.downedInfectedEye = flags[3];
			RedeWorld.downedStrangePortal = flags[4];
			RedeWorld.downedVlitch1 = flags[5];
			RedeWorld.downedVlitch2 = flags[6];
			RedeWorld.downedDarkSlime = flags[7];
			BitsByte flags2 = reader.ReadByte();
			RedeWorld.downedSlayer = flags2[0];
			RedeWorld.downedVlitch3 = flags2[1];
			RedeWorld.downedSkullDigger = flags2[2];
			RedeWorld.downedSunkenCaptain = flags2[3];
			RedeWorld.downedBlisterface = flags2[4];
			RedeWorld.downedStage2Scientist = flags2[5];
			RedeWorld.downedStage3Scientist = flags2[6];
			RedeWorld.downedIBehemoth = flags2[7];
			BitsByte flags3 = reader.ReadByte();
			RedeWorld.downedMACE = flags3[0];
			RedeWorld.downedPatientZero = flags3[1];
			RedeWorld.labSafe = flags3[2];
			RedeWorld.deathBySlayer = flags3[3];
			RedeWorld.girusTalk1 = flags3[4];
			RedeWorld.girusTalk2 = flags3[5];
			RedeWorld.girusTalk3 = flags3[6];
			RedeWorld.keeperSaved = flags3[7];
			BitsByte flags4 = reader.ReadByte();
			RedeWorld.tbotLabAccess = flags4[0];
			RedeWorld.downedPatientZero = flags4[1];
			RedeWorld.labSafe = flags4[2];
			RedeWorld.deathBySlayer = flags4[3];
			RedeWorld.labAccess1 = flags4[4];
			RedeWorld.labAccess2 = flags4[5];
			RedeWorld.labAccess3 = flags4[6];
			RedeWorld.labAccess4 = flags4[7];
			BitsByte flags5 = reader.ReadByte();
			RedeWorld.labAccess5 = flags5[0];
			RedeWorld.labAccess6 = flags5[1];
			RedeWorld.labAccess7 = flags5[2];
			RedeWorld.infectionBegin = flags5[3];
			RedeWorld.patientZeroMessages = flags5[4];
			RedeWorld.downedNebuleus = flags5[5];
			RedeWorld.deathByNeb = flags5[6];
			RedeWorld.downedEaglecrestGolem = flags5[7];
			BitsByte flags6 = reader.ReadByte();
			RedeWorld.downedChickenInv = flags6[0];
			RedeWorld.downedChickenInvPZ = flags6[1];
			RedeWorld.downedEaglecrestGolemPZ = flags6[2];
			RedeWorld.downedThorn = flags6[3];
			RedeWorld.downedThornPZ = flags6[4];
			RedeWorld.downedJanitor = flags6[5];
			RedeWorld.downedVolt = flags6[6];
			RedeWorld.voltBegin = flags6[7];
			BitsByte flags7 = reader.ReadByte();
			RedeWorld.pzUS = flags7[0];
			RedeWorld.maceUS = flags7[1];
			RedeWorld.redemptionPoints = reader.ReadInt32();
			RedeWorld.girusCloakTimer = reader.ReadInt32();
			RedeWorld.slayerRep = reader.ReadInt32();
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

		public static int evilXenoBiome;

		public static int labBiome;

		public static int slayerBiome;

		private int labSafeMessageTimer;

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

		public static int girusCloakTimer;

		public static bool girusCloaked;

		public static int slayerRep;

		public static bool golemLure;

		public static bool darkSlimeLure;

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

		public static bool downedJanitor;

		public static bool downedVolt;

		public static bool voltBegin;

		public static bool pzUS;

		public static bool maceUS;
	}
}
