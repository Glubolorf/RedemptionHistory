using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items;
using Redemption.Items.DruidDamageClass;
using Redemption.Items.DruidDamageClass.v08;
using Redemption.Items.Weapons;
using Redemption.NPCs;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Redemption.NPCs.LabNPCs.New;
using Redemption.NPCs.Minibosses;
using Redemption.NPCs.v08;
using Redemption.Tiles;
using Redemption.Tiles.SlayerShip;
using Redemption.Tiles.Wasteland;
using Redemption.Walls;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.Graphics.Effects;
using Terraria.ID;
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
			RedeWorld.xenoBiome = tileCounts[ModContent.TileType<DeadRockTile>()] + tileCounts[ModContent.TileType<DeadGrassTile>()] + tileCounts[ModContent.TileType<RadioactiveSandTile>()] + tileCounts[ModContent.TileType<RadioactiveSandstoneTile>()] + tileCounts[ModContent.TileType<RadioactiveIceTile>()];
			RedeWorld.evilXenoBiome = tileCounts[ModContent.TileType<DeadGrassTileCorruption>()] + tileCounts[ModContent.TileType<IrradiatedEbonstoneTile>()];
			RedeWorld.evilXenoBiome2 = tileCounts[ModContent.TileType<DeadGrassTileCrimson>()] + tileCounts[ModContent.TileType<IrradiatedCrimstoneTile>()];
			RedeWorld.labBiome = tileCounts[ModContent.TileType<LabTileUnsafe>()];
			RedeWorld.slayerBiome = tileCounts[ModContent.TileType<SlayerShipPanelTile>()];
		}

		public override void ResetNearbyTileEffects()
		{
			RedeWorld.xenoBiome = 0;
			RedeWorld.evilXenoBiome = 0;
			RedeWorld.evilXenoBiome2 = 0;
			RedeWorld.labBiome = 0;
			RedeWorld.slayerBiome = 0;
		}

		public override void PostUpdate()
		{
			if (!Main.dayTime && NPC.downedBoss1 && !Main.hardMode && !Main.fastForwardTime)
			{
				if (Main.time == 1.0 && !WorldGen.spawnEye && !RedeWorld.downedTheKeeper && Main.netMode != 1)
				{
					bool flag3 = false;
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active && Main.player[i].statLifeMax >= 200 && Main.player[i].statDefense > 10)
						{
							flag3 = true;
							break;
						}
					}
					if (flag3 && Main.rand.Next(3) == 0)
					{
						int num8 = 0;
						for (int num9 = 0; num9 < 200; num9++)
						{
							if (Main.npc[num9].active && Main.npc[num9].townNPC)
							{
								num8++;
							}
						}
						if (num8 >= 4)
						{
							RedeWorld.spawnKeeper = true;
							if (Main.netMode == 0)
							{
								Main.NewText("Shrieks echo through the night...", 180, 112, 240, false);
							}
							else if (Main.netMode == 2)
							{
								NetMessage.SendData(25, -1, -1, null, 255, 50f, 255f, 130f, 0, 0, 0);
							}
						}
					}
				}
				if (RedeWorld.spawnKeeper && Main.netMode != 1 && Main.time > 4860.0)
				{
					for (int j = 0; j < 255; j++)
					{
						if (Main.player[j].active && !Main.player[j].dead && (double)Main.player[j].position.Y < Main.worldSurface * 16.0)
						{
							if (Main.netMode == 0)
							{
								if (Main.netMode != 1)
								{
									BaseUtility.Chat("The Keeper has awoken!", 175, 75, byte.MaxValue, false);
								}
							}
							else if (Main.netMode == 2)
							{
								if (Main.netMode == 0)
								{
									if (Main.netMode != 1)
									{
										BaseUtility.Chat("The Keeper has awoken!", 175, 75, byte.MaxValue, false);
									}
								}
								else if (Main.netMode == 2)
								{
									NetMessage.BroadcastChatMessage(NetworkText.FromKey("Mods.Redemption.KeeperAwoken", new object[0]), new Color(175, 75, 255), -1);
								}
							}
							Redemption.SpawnBoss(Main.player[j], "TheKeeper", false, new Vector2(Main.player[j].position.X + (float)Main.rand.Next(400, 500), Main.player[j].position.Y - 0f), "The Keeper", false);
							RedeWorld.spawnKeeper = false;
							break;
						}
					}
				}
			}
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
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
					{
						int num10 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num10, j2, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)ModContent.TileType<SapphironOreTile>());
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
					for (int l = 0; l < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); l++)
					{
						int num11 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j3 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num11, j3, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)ModContent.TileType<ScarlionOreTile>());
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
				for (int m = 0; m < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); m++)
				{
					int num12 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int j4 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.6f), (int)((float)Main.maxTilesY * 0.8f));
					WorldGen.OreRunner(num12, j4, (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(4, 15), (ushort)ModContent.TileType<DragonLeadOreTile>());
				}
			}
			if (NPC.downedMoonlord && !RedeWorld.messageKingSlayer)
			{
				RedeWorld.messageKingSlayer = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				if (RedeWorld.slayerRep > 0 && RedeWorld.downedSlayer)
				{
					string key4 = "Mods.Redemption.KingSlayerMoonlord";
					Color messageColor4 = Colors.RarityCyan;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(key4, new object[0]), messageColor4, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key4), messageColor4, false);
					}
				}
			}
			if (RedeWorld.downedXenomiteCrystal && !RedeWorld.infectionBegin)
			{
				RedeWorld.infectionBegin = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key5 = "Mods.Redemption.InfectionMessage1";
				string key6 = "Mods.Redemption.SoIChecklist";
				Color messageColor5 = Color.Green;
				Color messageColor6 = Color.White;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key5, new object[0]), messageColor5, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key5), messageColor5, false);
				}
				if (Redemption.checklistLoaded)
				{
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(key6, new object[0]), messageColor6, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key6), messageColor6, false);
					}
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
					string key7 = "Mods.Redemption.LabIsSafe";
					Color messageColor7 = Color.Cyan;
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey(key7, new object[0]), messageColor7, -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key7), messageColor7, false);
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
				string key8 = "Mods.Redemption.GrowingInfection";
				Color messageColor8 = Color.Green;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key8, new object[0]), messageColor8, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key8), messageColor8, false);
				}
				if (Main.dungeonX < Main.maxTilesX / 2)
				{
					ConversionHandler.ConvertDown((int)((float)Main.maxTilesX * 0.25f), 0, 100, ConversionType.WASTELAND);
				}
				else
				{
					ConversionHandler.ConvertDown((int)((float)Main.maxTilesX * 0.75f), 0, 100, ConversionType.WASTELAND);
				}
			}
			if (RedeWorld.downedPatientZero && !RedeWorld.patientZeroMessages)
			{
				RedeWorld.patientZeroMessages = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key9 = "Mods.Redemption.PatientZeroMessage1";
				string key10 = "Mods.Redemption.PatientZeroMessage2";
				string key11 = "Mods.Redemption.PatientZeroMessage3";
				Color messageColor9 = Color.Gold;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key9, new object[0]), messageColor9, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key9), messageColor9, false);
				}
				Color messageColor10 = Color.DarkRed;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key10, new object[0]), messageColor10, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key10), messageColor10, false);
				}
				Color messageColor11 = Color.ForestGreen;
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
						WorldGen.TileRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (int)((ushort)ModContent.TileType<ShinkiteTile>()), false, 0f, 0f, false, true);
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
			if (RedeWorld.girusCloaked && !player.HasBuff(ModContent.BuffType<GirusCloakBuff>()))
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
			if (player.HasBuff(ModContent.BuffType<IntruderAlertDebuff>()) && Main.rand.Next(200) == 0 && NPC.CountNPCS(ModContent.NPCType<LabSentryDrone>()) <= 3)
			{
				int A = Main.rand.Next(-200, 200) * 6;
				int drone = NPC.NewNPC((int)player.Center.X + A, (int)player.Center.Y + A, ModContent.NPCType<LabSentryDrone>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[drone].netUpdate = true;
			}
			if ((RedeWorld.golemLure && !player.HasBuff(ModContent.BuffType<GolemSpelltomeBuff>())) || NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolem>()))
			{
				RedeWorld.golemLure = false;
				player.ClearBuff(ModContent.BuffType<GolemSpelltomeBuff>());
			}
			if ((RedeWorld.darkSlimeLure && !player.HasBuff(ModContent.BuffType<EvilJellyBuff>())) || NPC.AnyNPCs(ModContent.NPCType<EvilJellyBoss>()))
			{
				RedeWorld.darkSlimeLure = false;
				player.ClearBuff(ModContent.BuffType<EvilJellyBuff>());
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
						WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(6, 10), WorldGen.genRand.Next(2, 4), ModContent.TileType<KaniteOreTile>(), false, 0f, 0f, false, true);
					}
					for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 5E-05); j++)
					{
						int num = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.7f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num, j2, (double)WorldGen.genRand.Next(1, 2), WorldGen.genRand.Next(2, 4), (ushort)ModContent.TileType<UraniumTile>());
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
							WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 6), (ushort)ModContent.TileType<PlantMatterTile>());
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
							WorldGen.OreRunner(tilesX2, tilesY2, (double)WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 8), (ushort)ModContent.TileType<PlantMatterTile>());
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
			colorToTile[new Color(255, 100, 0)] = ModContent.TileType<AncientWoodTile>();
			colorToTile[new Color(0, 255, 0)] = ModContent.TileType<AncientDirtTile>();
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(255, 0, 255)] = ModContent.WallType<AncientWoodWallTile>();
			colorToWall[new Color(0, 255, 255)] = 63;
			colorToWall[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/AncientHouse"), colorToTile, mod.GetTexture("WorldGeneration/AncientHouseWalls"), colorToWall, null, null, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.07f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
		}

		public void AncientHouseFurn()
		{
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.07f) + 16, (int)((float)Main.maxTilesY * 0.45f) + 8, (int)((ushort)ModContent.TileType<AncientWoodDoorClosed>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.07f) + 16, (int)((float)Main.maxTilesY * 0.45f) + 8, (int)((ushort)ModContent.TileType<AncientWoodDoorClosed>()), 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.07f) + 10, (int)((float)Main.maxTilesY * 0.45f) + 7, (int)((ushort)ModContent.TileType<BrothersPaintingTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.07f) + 10, (int)((float)Main.maxTilesY * 0.45f) + 7, (int)((ushort)ModContent.TileType<BrothersPaintingTile>()), 0, 0, -1, -1);
			BaseWorldGen.GenerateChest((int)((float)Main.maxTilesX * 0.07f) + 3, (int)((float)Main.maxTilesY * 0.45f) + 9, (int)((ushort)ModContent.TileType<AncientWoodChestTile>()), 0, null, null, null, false);
		}

		public void HeroHallClear()
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/TempleOfHeroesClear"), colorToTile, null, null, null, null, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
		}

		public void HeroHall()
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 0)] = ModContent.TileType<AncientHallBrickTile>();
			colorToTile[new Color(255, 255, 0)] = ModContent.TileType<AncientStoneTile>();
			colorToTile[new Color(0, 0, 255)] = ModContent.TileType<AncientWoodTile>();
			colorToTile[new Color(255, 0, 255)] = 51;
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(255, 0, 255)] = ModContent.WallType<AncientHallPillarWall>();
			colorToWall[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/TempleOfHeroes"), colorToTile, mod.GetTexture("WorldGeneration/TempleOfHeroesWalls"), colorToWall, null, null, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
		}

		public void HeroHallStuff()
		{
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 17, (int)((float)Main.maxTilesY * 0.45f) + 11, (int)((ushort)ModContent.TileType<HKStatueTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 25, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)ModContent.TileType<JStatueTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 6, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)ModContent.TileType<KSStatueTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 36, (int)((float)Main.maxTilesY * 0.45f) + 20, (int)((ushort)ModContent.TileType<NStatueTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 4, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 43, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 14, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 33, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), false, 0, 0, -1, -1);
			WorldGen.PlaceObject((int)((float)Main.maxTilesX * 0.4f) + 24, (int)((float)Main.maxTilesY * 0.45f) + 27, (int)((ushort)ModContent.TileType<AncientAltarTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 17, (int)((float)Main.maxTilesY * 0.45f) + 11, (int)((ushort)ModContent.TileType<HKStatueTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 25, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)ModContent.TileType<JStatueTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 6, (int)((float)Main.maxTilesY * 0.45f) + 16, (int)((ushort)ModContent.TileType<KSStatueTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 36, (int)((float)Main.maxTilesY * 0.45f) + 20, (int)((ushort)ModContent.TileType<NStatueTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 4, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 43, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 14, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 33, (int)((float)Main.maxTilesY * 0.45f) + 13, (int)((ushort)ModContent.TileType<ArchclothBannerTile>()), 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, (int)((float)Main.maxTilesX * 0.4f) + 24, (int)((float)Main.maxTilesY * 0.45f) + 27, (int)((ushort)ModContent.TileType<AncientAltarTile>()), 0, 0, -1, -1);
		}

		public override void PostWorldGen()
		{
			int[] itemsToPlaceInDungeonChests = new int[]
			{
				ModContent.ItemType<DonjonStave>()
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
				ModContent.ItemType<GildedSeaAxe>(),
				ModContent.ItemType<SeaNote>()
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
				ModContent.ItemType<AcornBomb>()
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
				ModContent.ItemType<AncientWoodStave>(),
				ModContent.ItemType<AncientWoodSword>(),
				ModContent.ItemType<AncientWoodBow>(),
				ModContent.ItemType<Falcon>()
			};
			for (int chestIndex4 = 0; chestIndex4 < 1000; chestIndex4++)
			{
				Chest chest4 = Main.chest[chestIndex4];
				if (chest4 != null && (int)Main.tile[chest4.x, chest4.y].type == ModContent.TileType<AncientWoodChestTile>())
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
			RedeWorld.downedTheKeeper = false;
			RedeWorld.redemptionPoints = 0;
			RedeWorld.downedTheWarden = false;
			RedeWorld.downedMansionWraith = false;
			RedeWorld.wardenSaved = false;
			RedeWorld.keeperSaved = false;
			RedeWorld.slayerRep = 0;
			RedeWorld.downedJanitor = false;
			RedeWorld.downedVolt = false;
			RedeWorld.voltBegin = false;
			RedeWorld.downedMossyGoliath = false;
			RedeWorld.downedXenomiteCrystal = false;
			RedeWorld.downedInfectedEye = false;
			RedeWorld.downedStrangePortal = false;
			RedeWorld.downedVlitch1 = false;
			RedeWorld.downedVlitch2 = false;
			RedeWorld.downedDarkSlime = false;
			RedeWorld.downedSlayer = false;
			RedeWorld.downedVlitch3 = false;
			RedeWorld.downedSkullDigger = false;
			RedeWorld.downedSunkenCaptain = false;
			RedeWorld.downedBlisterface = false;
			RedeWorld.downedStage3Scientist = false;
			RedeWorld.labAccess1 = false;
			RedeWorld.labAccess2 = false;
			RedeWorld.labAccess3 = false;
			RedeWorld.labAccess4 = false;
			RedeWorld.labAccess5 = false;
			RedeWorld.labAccess6 = false;
			RedeWorld.labAccess7 = false;
			RedeWorld.labSafe = false;
			RedeWorld.downedIBehemoth = false;
			RedeWorld.downedMACE = false;
			RedeWorld.downedPatientZero = false;
			RedeWorld.patientZeroMessages = false;
			RedeWorld.downedNebuleus = false;
			RedeWorld.downedEaglecrestGolem = false;
			RedeWorld.downedChickenInvPZ = false;
			RedeWorld.downedChickenInv = false;
			RedeWorld.downedEaglecrestGolemPZ = false;
			RedeWorld.downedThorn = false;
			RedeWorld.downedThornPZ = false;
			RedeWorld.downedKingChicken = false;
			RedeWorld.spawnSapphironOre = false;
			RedeWorld.spawnScarlionOre = false;
			RedeWorld.spawnDragonOre = false;
			RedeWorld.deathBySlayer = false;
			RedeWorld.foundNewb = false;
			RedeWorld.spawnXenoBiome = false;
			RedeWorld.starliteGenned = false;
			RedeWorld.girusTalk1 = false;
			RedeWorld.girusTalk2 = false;
			RedeWorld.girusTalk3 = false;
			RedeWorld.infectionBegin = false;
			RedeWorld.deathByNeb = false;
			RedeWorld.girusCloaked = false;
			RedeWorld.girusCloakTimer = 0;
			RedeWorld.pzUS = false;
			RedeWorld.maceUS = false;
			RedeWorld.zephosDownedTimer = 0;
			RedeWorld.daerelDownedTimer = 0;
			RedeWorld.tbotDownedTimer = 0;
			RedeWorld.spawnKeeper = false;
			RedeWorld.oblitDeath = 0;
			RedeWorld.messageKingSlayer = false;
		}

		public override TagCompound Save()
		{
			List<string> downed = new List<string>();
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
				downed.Add("sapphiron");
			}
			if (RedeWorld.spawnScarlionOre)
			{
				downed.Add("scarlion");
			}
			if (RedeWorld.spawnDragonOre)
			{
				downed.Add("dragore");
			}
			if (RedeWorld.deathBySlayer)
			{
				downed.Add("deathSlayer");
			}
			if (RedeWorld.foundNewb)
			{
				downed.Add("newbFound");
			}
			if (RedeWorld.downedVlitch3)
			{
				downed.Add("OO");
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
				downed.Add("wasteland");
			}
			if (RedeWorld.starliteGenned)
			{
				downed.Add("starliteGen");
			}
			if (RedeWorld.girusTalk1)
			{
				downed.Add("girTalk1");
			}
			if (RedeWorld.girusTalk2)
			{
				downed.Add("girTalk2");
			}
			if (RedeWorld.girusTalk3)
			{
				downed.Add("girTalk3");
			}
			if (RedeWorld.labSafe)
			{
				downed.Add("labSafe1");
			}
			if (RedeWorld.infectionBegin)
			{
				downed.Add("infection1");
			}
			if (RedeWorld.downedBlisterface)
			{
				downed.Add("Blisterface");
			}
			if (RedeWorld.downedStage3Scientist)
			{
				downed.Add("Stage3Scientist");
			}
			if (RedeWorld.labAccess1)
			{
				downed.Add("labA1");
			}
			if (RedeWorld.labAccess2)
			{
				downed.Add("labA2");
			}
			if (RedeWorld.labAccess3)
			{
				downed.Add("labA3");
			}
			if (RedeWorld.labAccess4)
			{
				downed.Add("labA4");
			}
			if (RedeWorld.labAccess5)
			{
				downed.Add("labA5");
			}
			if (RedeWorld.labAccess6)
			{
				downed.Add("labA6");
			}
			if (RedeWorld.labAccess7)
			{
				downed.Add("labA7");
			}
			if (RedeWorld.patientZeroMessages)
			{
				downed.Add("pzMessage");
			}
			if (RedeWorld.keeperSaved)
			{
				downed.Add("keeperS");
			}
			if (RedeWorld.wardenSaved)
			{
				downed.Add("wardenS");
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
				downed.Add("deathNeb");
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
				downed.Add("voltBeginFight");
			}
			if (RedeWorld.pzUS)
			{
				downed.Add("PUS");
			}
			if (RedeWorld.maceUS)
			{
				downed.Add("MPUS");
			}
			if (RedeWorld.downedMossyGoliath)
			{
				downed.Add("MossyGoliath");
			}
			if (RedeWorld.downedTheWarden)
			{
				downed.Add("TheWarden");
			}
			if (RedeWorld.downedMansionWraith)
			{
				downed.Add("MansionWraith");
			}
			if (RedeWorld.downedChickenInv)
			{
				downed.Add("downedChickenInv");
			}
			if (RedeWorld.downedChickenInvPZ)
			{
				downed.Add("downedChickenInvPZ");
			}
			if (RedeWorld.messageKingSlayer)
			{
				downed.Add("messageKingSlayer");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("downed", downed);
			tagCompound.Add("redePoints", RedeWorld.redemptionPoints);
			tagCompound.Add("slayRep", RedeWorld.slayerRep);
			tagCompound.Add("tbotDowned", RedeWorld.tbotDownedTimer);
			tagCompound.Add("zephosDowned", RedeWorld.zephosDownedTimer);
			tagCompound.Add("daerelDowned", RedeWorld.daerelDownedTimer);
			tagCompound.Add("oblitDeath", RedeWorld.oblitDeath);
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
			RedeWorld.spawnSapphironOre = list.Contains("sapphiron");
			RedeWorld.spawnScarlionOre = list.Contains("scarlion");
			RedeWorld.spawnDragonOre = list.Contains("dragore");
			RedeWorld.deathBySlayer = list.Contains("deathSlayer");
			RedeWorld.foundNewb = list.Contains("newbFound");
			RedeWorld.downedVlitch3 = list.Contains("OO");
			RedeWorld.downedSkullDigger = list.Contains("SkullDigger");
			RedeWorld.downedSunkenCaptain = list.Contains("SunkenCaptain");
			RedeWorld.spawnXenoBiome = list.Contains("wasteland");
			RedeWorld.starliteGenned = list.Contains("starliteGen");
			RedeWorld.girusTalk1 = list.Contains("girTalk1");
			RedeWorld.girusTalk2 = list.Contains("girTalk2");
			RedeWorld.girusTalk3 = list.Contains("girTalk3");
			RedeWorld.labSafe = list.Contains("labSafe1");
			RedeWorld.infectionBegin = list.Contains("infection1");
			RedeWorld.downedBlisterface = list.Contains("Blisterface");
			RedeWorld.downedStage3Scientist = list.Contains("Stage3Scientist");
			RedeWorld.labAccess1 = list.Contains("labA1");
			RedeWorld.labAccess2 = list.Contains("labA2");
			RedeWorld.labAccess3 = list.Contains("labA3");
			RedeWorld.labAccess4 = list.Contains("labA4");
			RedeWorld.labAccess5 = list.Contains("labA5");
			RedeWorld.labAccess6 = list.Contains("labA6");
			RedeWorld.labAccess7 = list.Contains("labA7");
			RedeWorld.patientZeroMessages = list.Contains("pzMessage");
			RedeWorld.keeperSaved = list.Contains("keeperS");
			RedeWorld.downedIBehemoth = list.Contains("IrradiatedBehemoth");
			RedeWorld.downedMACE = list.Contains("MACEProjectHead");
			RedeWorld.downedPatientZero = list.Contains("PatientZero");
			RedeWorld.downedNebuleus = list.Contains("Nebuleus");
			RedeWorld.deathByNeb = list.Contains("deathNeb");
			RedeWorld.downedEaglecrestGolem = list.Contains("EaglecrestGolem");
			RedeWorld.downedChickenInv = list.Contains("downedChickenInv");
			RedeWorld.downedChickenInvPZ = list.Contains("downedChickenInvPZ");
			RedeWorld.downedEaglecrestGolemPZ = list.Contains("EaglecrestGolemPZ");
			RedeWorld.downedThorn = list.Contains("Thorn");
			RedeWorld.downedThornPZ = list.Contains("ThornPZ");
			RedeWorld.redemptionPoints = tag.GetInt("redePoints");
			RedeWorld.slayerRep = tag.GetInt("slayRep");
			RedeWorld.downedJanitor = list.Contains("JanitorBot");
			RedeWorld.downedVolt = list.Contains("TbotMiniboss");
			RedeWorld.voltBegin = list.Contains("voltBeginFight");
			RedeWorld.pzUS = list.Contains("PUS");
			RedeWorld.maceUS = list.Contains("MPUS");
			RedeWorld.daerelDownedTimer = tag.GetInt("daerelDowned");
			RedeWorld.tbotDownedTimer = tag.GetInt("tbotDowned");
			RedeWorld.zephosDownedTimer = tag.GetInt("zephosDowned");
			RedeWorld.downedMossyGoliath = list.Contains("MossyGoliath");
			RedeWorld.downedTheWarden = list.Contains("TheWarden");
			RedeWorld.downedMansionWraith = list.Contains("MansionWraith");
			RedeWorld.wardenSaved = list.Contains("wardenS");
			RedeWorld.messageKingSlayer = list.Contains("messageKingSlayer");
			RedeWorld.oblitDeath = tag.GetInt("oblitDeath");
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
			flags2[5] = RedeWorld.downedStage3Scientist;
			flags2[6] = RedeWorld.downedIBehemoth;
			flags2[7] = RedeWorld.downedMACE;
			writer.Write(flags2);
			BitsByte flags3 = default(BitsByte);
			flags3[0] = RedeWorld.downedPatientZero;
			flags3[1] = RedeWorld.labSafe;
			flags3[2] = RedeWorld.deathBySlayer;
			flags3[3] = RedeWorld.girusTalk1;
			flags3[4] = RedeWorld.girusTalk2;
			flags3[5] = RedeWorld.girusTalk3;
			flags3[6] = RedeWorld.keeperSaved;
			flags3[7] = RedeWorld.downedPatientZero;
			writer.Write(flags3);
			BitsByte flags4 = default(BitsByte);
			flags4[0] = RedeWorld.labSafe;
			flags4[1] = RedeWorld.deathBySlayer;
			flags4[2] = RedeWorld.labAccess1;
			flags4[3] = RedeWorld.labAccess2;
			flags4[4] = RedeWorld.labAccess3;
			flags4[5] = RedeWorld.labAccess4;
			flags4[6] = RedeWorld.labAccess5;
			flags4[7] = RedeWorld.labAccess6;
			writer.Write(flags4);
			BitsByte flags5 = default(BitsByte);
			flags5[0] = RedeWorld.labAccess7;
			flags5[1] = RedeWorld.infectionBegin;
			flags5[2] = RedeWorld.patientZeroMessages;
			flags5[3] = RedeWorld.downedNebuleus;
			flags5[4] = RedeWorld.deathByNeb;
			flags5[5] = RedeWorld.downedEaglecrestGolem;
			flags5[6] = RedeWorld.downedChickenInv;
			flags5[7] = RedeWorld.downedChickenInvPZ;
			writer.Write(flags5);
			BitsByte flags6 = default(BitsByte);
			flags6[0] = RedeWorld.downedEaglecrestGolemPZ;
			flags6[1] = RedeWorld.downedThorn;
			flags6[2] = RedeWorld.downedThornPZ;
			flags6[3] = RedeWorld.downedJanitor;
			flags6[4] = RedeWorld.downedVolt;
			flags6[5] = RedeWorld.voltBegin;
			flags6[6] = RedeWorld.pzUS;
			flags6[7] = RedeWorld.maceUS;
			writer.Write(flags6);
			BitsByte flags7 = default(BitsByte);
			flags7[0] = RedeWorld.downedMossyGoliath;
			flags7[1] = RedeWorld.downedTheWarden;
			flags7[2] = RedeWorld.wardenSaved;
			flags7[3] = RedeWorld.spawnXenoBiome;
			flags7[4] = RedeWorld.downedMansionWraith;
			writer.Write(flags7);
			writer.Write(RedeWorld.redemptionPoints);
			writer.Write(RedeWorld.girusCloakTimer);
			writer.Write(RedeWorld.slayerRep);
			writer.Write(RedeWorld.zephosDownedTimer);
			writer.Write(RedeWorld.daerelDownedTimer);
			writer.Write(RedeWorld.tbotDownedTimer);
			writer.Write(RedeWorld.oblitDeath);
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
			RedeWorld.downedStage3Scientist = flags2[5];
			RedeWorld.downedIBehemoth = flags2[6];
			RedeWorld.downedMACE = flags2[7];
			BitsByte flags3 = reader.ReadByte();
			RedeWorld.downedPatientZero = flags3[0];
			RedeWorld.labSafe = flags3[1];
			RedeWorld.deathBySlayer = flags3[2];
			RedeWorld.girusTalk1 = flags3[3];
			RedeWorld.girusTalk2 = flags3[4];
			RedeWorld.girusTalk3 = flags3[5];
			RedeWorld.keeperSaved = flags3[6];
			RedeWorld.downedPatientZero = flags3[7];
			BitsByte flags4 = reader.ReadByte();
			RedeWorld.labSafe = flags4[0];
			RedeWorld.deathBySlayer = flags4[1];
			RedeWorld.labAccess1 = flags4[2];
			RedeWorld.labAccess2 = flags4[3];
			RedeWorld.labAccess3 = flags4[4];
			RedeWorld.labAccess4 = flags4[5];
			RedeWorld.labAccess5 = flags4[6];
			RedeWorld.labAccess6 = flags4[7];
			BitsByte flags5 = reader.ReadByte();
			RedeWorld.labAccess7 = flags5[0];
			RedeWorld.infectionBegin = flags5[1];
			RedeWorld.patientZeroMessages = flags5[2];
			RedeWorld.downedNebuleus = flags5[3];
			RedeWorld.deathByNeb = flags5[4];
			RedeWorld.downedEaglecrestGolem = flags5[5];
			RedeWorld.downedChickenInv = flags5[6];
			RedeWorld.downedChickenInvPZ = flags5[7];
			BitsByte flags6 = reader.ReadByte();
			RedeWorld.downedEaglecrestGolemPZ = flags6[0];
			RedeWorld.downedThorn = flags6[1];
			RedeWorld.downedThornPZ = flags6[2];
			RedeWorld.downedJanitor = flags6[3];
			RedeWorld.downedVolt = flags6[4];
			RedeWorld.voltBegin = flags6[5];
			RedeWorld.pzUS = flags6[6];
			RedeWorld.maceUS = flags6[7];
			BitsByte flags7 = reader.ReadByte();
			RedeWorld.downedMossyGoliath = flags7[0];
			RedeWorld.downedTheWarden = flags7[1];
			RedeWorld.wardenSaved = flags7[2];
			RedeWorld.spawnXenoBiome = flags7[3];
			RedeWorld.downedMansionWraith = flags7[4];
			RedeWorld.redemptionPoints = reader.ReadInt32();
			RedeWorld.girusCloakTimer = reader.ReadInt32();
			RedeWorld.slayerRep = reader.ReadInt32();
			RedeWorld.daerelDownedTimer = reader.ReadInt32();
			RedeWorld.zephosDownedTimer = reader.ReadInt32();
			RedeWorld.tbotDownedTimer = reader.ReadInt32();
			RedeWorld.oblitDeath = reader.ReadInt32();
		}

		private const int saveVersion = 0;

		public static bool spawnOre;

		public static bool spawnDragonOre;

		public static bool spawnSapphironOre;

		public static bool spawnScarlionOre;

		public static bool spawnXenoBiome;

		public static bool messageKingSlayer;

		public static bool starliteGenned;

		public static bool labSafe;

		public static bool infectionBegin;

		public static int xenoBiome;

		public static int evilXenoBiome;

		public static int evilXenoBiome2;

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

		public static int zephosDownedTimer;

		public static int daerelDownedTimer;

		public static int tbotDownedTimer;

		public static bool spawnKeeper;

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

		public static bool downedStage3Scientist;

		public static bool keeperSaved;

		public static bool downedIBehemoth;

		public static bool downedMACE;

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

		public static bool downedMossyGoliath;

		public static bool downedTheWarden;

		public static bool wardenSaved;

		public static bool downedMansionWraith;

		public static int oblitDeath;
	}
}
