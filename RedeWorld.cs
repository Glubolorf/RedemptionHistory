using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Redemption.Items.Lore;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Weapons.PreHM.Druid;
using Redemption.Items.Weapons.PreHM.Druid.Staves;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.NPCs.Friendly;
using Redemption.NPCs.Lab;
using Redemption.NPCs.Minibosses;
using Redemption.NPCs.Minibosses.DarkSlime;
using Redemption.NPCs.Soulless;
using Redemption.Projectiles.Hostile;
using Redemption.StructureHelper;
using Redemption.Tiles.Containers;
using Redemption.Tiles.Furniture.Ship;
using Redemption.Tiles.Ores;
using Redemption.Tiles.Plants;
using Redemption.Tiles.Tiles;
using Redemption.WorldGeneration.Soulless;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.Generation;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Redemption
{
	public class RedeWorld : ModWorld
	{
		public override void PreUpdate()
		{
			if (Subworld.IsActive<SoullessSub>())
			{
				Wiring.UpdateMech();
				Liquid.skipCount++;
				if (Liquid.skipCount > 1)
				{
					Liquid.UpdateLiquid();
					Liquid.skipCount = 0;
				}
				for (int num = 0; num < 20; num++)
				{
					int i = Main.rand.Next(10, 1790);
					int j = Main.rand.Next(10, 1790);
					ModTile tile = TileLoader.GetTile((int)Main.tile[i, j].type);
					if (tile != null)
					{
						tile.RandomUpdate(i, j);
					}
				}
			}
		}

		public static bool CheckFlat(int startX, int startY, int width, float threshold, int goingDownWeight = 0, int goingUpWeight = 0)
		{
			if (!WorldGen.SolidTile(startX + width, startY))
			{
				return false;
			}
			float totalVariance = 0f;
			for (int i = 0; i < width; i++)
			{
				if (startX + i >= Main.maxTilesX)
				{
					return false;
				}
				for (int j = startY - 1; j > startY - 100; j--)
				{
					if (WorldGen.SolidTile(startX + i, j))
					{
						return false;
					}
				}
				int offset = 0;
				bool goingUp = WorldGen.SolidTile(startX + i, startY);
				offset += (goingUp ? goingUpWeight : goingDownWeight);
				while ((goingUp && WorldGen.SolidTile(startX + i, startY - offset)) || (!goingUp && !WorldGen.SolidTile(startX + i, startY + offset)))
				{
					offset++;
				}
				if (goingUp)
				{
					offset--;
				}
				totalVariance += (float)offset;
			}
			return totalVariance / (float)width <= threshold;
		}

		private static void SpawnThornSummon()
		{
			bool placed = false;
			int attempts = 0;
			int placed2 = 0;
			int placeX2 = 0;
			while (!placed)
			{
				if (attempts++ >= 100000)
				{
					break;
				}
				int placeX3 = Main.spawnTileX + WorldGen.genRand.Next(-600, 600);
				int placeY = (int)Main.worldSurface - 200;
				if (placeX3 <= Main.spawnTileX - 100 || placeX3 >= Main.spawnTileX + 100)
				{
					while (!WorldGen.SolidTile(placeX3, placeY) && (double)placeY <= Main.worldSurface)
					{
						placeY++;
					}
					if ((double)placeY <= Main.worldSurface && Main.tile[placeX3, placeY].type == 2 && RedeWorld.CheckFlat(placeX3, placeY, 2, 0f, 0, 0))
					{
						WorldGen.PlaceObject(placeX3, placeY - 1, ModContent.TileType<HeartOfThornsTile>(), true, 0, 0, -1, -1);
						NetMessage.SendObjectPlacment(-1, placeX3, placeY - 1, (int)((ushort)ModContent.TileType<HeartOfThornsTile>()), 0, 0, -1, -1);
						if ((int)Main.tile[placeX3, placeY - 1].type == ModContent.TileType<HeartOfThornsTile>())
						{
							placeX2 = placeX3;
							attempts = 0;
							placed = true;
						}
					}
				}
			}
			while (placed && placed2 < 30 && attempts++ < 100000)
			{
				int placeX4 = placeX2 + WorldGen.genRand.Next(-20, 20);
				int placeY2 = (int)Main.worldSurface - 200;
				while (!WorldGen.SolidTile(placeX4, placeY2) && (double)placeY2 <= Main.worldSurface)
				{
					placeY2++;
				}
				if ((double)placeY2 <= Main.worldSurface && Main.tile[placeX4, placeY2].type == 2)
				{
					int num = WorldGen.genRand.Next(2);
					if (num != 0)
					{
						if (num == 1)
						{
							WorldGen.PlaceObject(placeX4, placeY2 - 1, ModContent.TileType<ThornsTile2>(), true, WorldGen.genRand.Next(2), 0, -1, -1);
							NetMessage.SendObjectPlacment(-1, placeX4, placeY2 - 1, (int)((ushort)ModContent.TileType<ThornsTile>()), WorldGen.genRand.Next(2), 0, -1, -1);
						}
					}
					else
					{
						WorldGen.PlaceObject(placeX4, placeY2 - 1, ModContent.TileType<ThornsTile>(), true, WorldGen.genRand.Next(2), 0, -1, -1);
						NetMessage.SendObjectPlacment(-1, placeX4, placeY2 - 1, (int)((ushort)ModContent.TileType<ThornsTile>()), WorldGen.genRand.Next(2), 0, -1, -1);
					}
					placed2++;
				}
			}
		}

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
			RedeWorld.soullessBiome = tileCounts[ModContent.TileType<ShadestoneTile>()] + tileCounts[ModContent.TileType<ShadestoneBrickTile>()];
		}

		public override void ResetNearbyTileEffects()
		{
			RedeWorld.xenoBiome = 0;
			RedeWorld.evilXenoBiome = 0;
			RedeWorld.evilXenoBiome2 = 0;
			RedeWorld.labBiome = 0;
			RedeWorld.slayerBiome = 0;
			RedeWorld.soullessBiome = 0;
		}

		public override void PostUpdate()
		{
			Player player = Main.player[Main.myPlayer];
			if (Main.netMode != 1)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active && !player2.dead && player2.GetModPlayer<RedePlayer>().ZoneSoulless && Subworld.IsActive<SoullessSub>())
					{
						Vector2 MansionPos = new Vector2(673f, 1190f) * 16f;
						if (Vector2.Distance(player2.Center, MansionPos) < 192f && !NPC.AnyNPCs(ModContent.NPCType<MansionWraith>()) && !RedeWorld.downedMansionWraith)
						{
							if (Main.netMode != 1)
							{
								NPC.NewNPC(10704, 19056, ModContent.NPCType<MansionWraith>(), 0, 0f, 0f, 0f, 0f, 255);
								break;
							}
							break;
						}
					}
				}
			}
			if (!Main.dayTime && NPC.downedBoss1 && !Main.hardMode && !Main.fastForwardTime)
			{
				if (Main.time == 1.0 && !WorldGen.spawnEye && !RedeWorld.downedKeeper && Main.netMode != 1)
				{
					bool flag3 = false;
					for (int j = 0; j < 255; j++)
					{
						if (Main.player[j].active && Main.player[j].statLifeMax >= 200 && Main.player[j].statDefense > 10)
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
					for (int k = 0; k < 255; k++)
					{
						if (Main.player[k].active && !Main.player[k].dead && (double)Main.player[k].position.Y < Main.worldSurface * 16.0)
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
							Redemption.SpawnBoss(Main.player[k], "Keeper", false, new Vector2(Main.player[k].position.X + (float)Main.rand.Next(400, 500), Main.player[k].position.Y - 0f), "The Keeper", false);
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
					if (Main.netMode != 0)
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
					for (int l = 0; l < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); l++)
					{
						int num10 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.8f));
						WorldGen.OreRunner(num10, j2, (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(5, 12), (ushort)ModContent.TileType<SapphironOreTile>());
					}
				}
				if (WorldGen.crimson)
				{
					RedeWorld.spawnScarlionOre = true;
					if (Main.netMode != 0)
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
					for (int m = 0; m < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); m++)
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
				if (Main.netMode != 0)
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
				for (int n = 0; n < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); n++)
				{
					int num12 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int j4 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.6f), (int)((float)Main.maxTilesY * 0.8f));
					WorldGen.OreRunner(num12, j4, (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(4, 15), (ushort)ModContent.TileType<DragonLeadOreTile>());
				}
			}
			if (NPC.downedMoonlord && !RedeWorld.messageKingSlayer)
			{
				RedeWorld.messageKingSlayer = true;
				if (Main.netMode != 0)
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
			if (RedeWorld.downedSoI && !RedeWorld.infectionBegin)
			{
				RedeWorld.infectionBegin = true;
				if (Main.netMode != 0)
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
					if (Main.netMode != 0)
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
			if (RedeWorld.downedPatientZero && !RedeWorld.patientZeroMessages)
			{
				RedeWorld.patientZeroMessages = true;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key8 = "Mods.Redemption.PatientZeroMessage1";
				string key9 = "Mods.Redemption.PatientZeroMessage2";
				string key10 = "Mods.Redemption.PatientZeroMessage3";
				Color messageColor8 = Color.Gold;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key8, new object[0]), messageColor8, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key8), messageColor8, false);
				}
				Color messageColor9 = Color.DarkRed;
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
				int x = Main.maxTilesX;
				int y = Main.maxTilesY;
				for (int k2 = 0; k2 < (int)((double)(x * y) * 0.00015); k2++)
				{
					int tilesX = WorldGen.genRand.Next(0, x);
					int tilesY = WorldGen.genRand.Next(y - 200, y);
					if (Main.tile[tilesX, tilesY].type == 57)
					{
						WorldGen.TileRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (int)((ushort)ModContent.TileType<ShinkiteTile>()), false, 0f, 0f, false, true);
					}
				}
			}
			if (RedeWorld.downedThorn && !RedeWorld.thornMessage)
			{
				RedeWorld.thornMessage = true;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key11 = "Mods.Redemption.ThornMessage";
				Color messageColor11 = Color.LawnGreen;
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey(key11, new object[0]), messageColor11, -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key11), messageColor11, false);
				}
			}
			if (NPC.AnyNPCs(ModContent.NPCType<TheSoulless>()) || NPC.AnyNPCs(ModContent.NPCType<TheSoulless2>()))
			{
				if (!Filters.Scene["MoonLordShake"].IsActive())
				{
					Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
				}
				Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
			}
			if (Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneSoulless)
			{
				if (!Filters.Scene["MoonLordShake"].IsActive())
				{
					Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
				}
				Filters.Scene["MoonLordShake"].GetShader().UseIntensity(0.3f);
			}
			else if (Subworld.IsActive<SoullessSub>())
			{
				Filters.Scene["MoonLordShake"].Deactivate(new object[0]);
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
			if ((RedeWorld.darkSlimeLure && !player.HasBuff(ModContent.BuffType<EvilJellyBuff>())) || NPC.AnyNPCs(ModContent.NPCType<EvilJellyBoss>()))
			{
				RedeWorld.darkSlimeLure = false;
				player.ClearBuff(ModContent.BuffType<EvilJellyBuff>());
			}
			if (RedeWorld.blobbleSwarm)
			{
				RedeWorld.blobbleSwarmTimer++;
				if (RedeWorld.blobbleSwarmTimer > 180)
				{
					RedeWorld.blobbleSwarm = false;
					RedeWorld.blobbleSwarmTimer = 0;
					RedeWorld.blobbleSwarmCooldown = 86400;
				}
			}
			if (RedeWorld.blobbleSwarmCooldown > 0)
			{
				RedeWorld.blobbleSwarmCooldown--;
			}
			this.UpdateNukeCountdown();
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int ShiniesIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
			int ShiniesIndex2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Final Cleanup"));
			int GuideIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Sunflowers"));
			if (GuideIndex == -1)
			{
				return;
			}
			tasks.Insert(GuideIndex, new PassLegacy("Heart of Thorns", delegate(GenerationProgress progress)
			{
				progress.Message = "Cursing the forest";
				RedeWorld.SpawnThornSummon();
			}));
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
			tasks.Insert(ShiniesIndex + 5, new PassLegacy("Ancient Decal", delegate(GenerationProgress progress)
			{
				for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); i++)
				{
					bool placed = false;
					int attempts = 0;
					while (!placed && attempts++ < 10000)
					{
						int tilesX = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
						int tilesY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.4f), (int)((double)Main.maxTilesY * 0.8));
						if (WorldGen.InWorld(tilesX + 2, tilesY + 4, 0))
						{
							Tile tile = Framing.GetTileSafely(tilesX + 2, tilesY + 4);
							if (tile.type == 1 || tile.type == 59)
							{
								Point16 origin;
								origin..ctor(tilesX, tilesY);
								Generator.GenerateMultistructureRandom("WorldGeneration/AncientSRocksM", origin, base.mod, false, false);
								placed = true;
							}
						}
					}
				}
				for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); j++)
				{
					bool placed2 = false;
					int attempts2 = 0;
					while (!placed2 && attempts2++ < 10000)
					{
						int tilesX2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
						int tilesY2 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.4f), (int)((double)Main.maxTilesY * 0.8));
						if (WorldGen.InWorld(tilesX2 + 2, tilesY2 + 7, 0))
						{
							Tile tile2 = Framing.GetTileSafely(tilesX2 + 2, tilesY2 + 7);
							if (tile2.type == 1 || tile2.type == 59)
							{
								Point16 origin2;
								origin2..ctor(tilesX2, tilesY2);
								Generator.GenerateMultistructureRandom("WorldGeneration/AncientSPillarsM", origin2, base.mod, false, false);
								placed2 = true;
							}
						}
					}
				}
				for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 4E-06); k++)
				{
					bool placed3 = false;
					int attempts3 = 0;
					while (!placed3 && attempts3++ < 10000)
					{
						int tilesX3 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
						int tilesY3 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.4f), (int)((double)Main.maxTilesY * 0.8));
						if (WorldGen.InWorld(tilesX3 + 6, tilesY3 + 10, 0))
						{
							Tile tile3 = Framing.GetTileSafely(tilesX3 + 6, tilesY3 + 10);
							if (tile3.type == 1 || tile3.type == 59)
							{
								Point16 dims = Point16.Zero;
								Generator.GetDimensions("WorldGeneration/AncientSArch1", base.mod, ref dims, false);
								for (int x = 0; x < (int)dims.X; x++)
								{
									for (int y = 0; y < (int)(dims.Y - 3); y++)
									{
										Framing.GetTileSafely(tilesX3 + x, tilesY3 + y).active();
									}
								}
								Point16 origin3;
								origin3..ctor(tilesX3, tilesY3);
								Generator.GenerateStructure("WorldGeneration/AncientSArch1", origin3, base.mod, false, false);
								placed3 = true;
							}
						}
					}
				}
				for (int l = 0; l < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 4E-06); l++)
				{
					bool placed4 = false;
					int attempts4 = 0;
					while (!placed4 && attempts4++ < 10000)
					{
						int tilesX4 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
						int tilesY4 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.4f), (int)((double)Main.maxTilesY * 0.8));
						if (WorldGen.InWorld(tilesX4 + 5, tilesY4 + 7, 0))
						{
							Tile tile4 = Framing.GetTileSafely(tilesX4 + 5, tilesY4 + 7);
							if (tile4.type == 1 || tile4.type == 59)
							{
								Point16 dims2 = Point16.Zero;
								Generator.GetDimensions("WorldGeneration/AncientSCoinPile1", base.mod, ref dims2, false);
								for (int x2 = 0; x2 < (int)dims2.X; x2++)
								{
									for (int y2 = 0; y2 < (int)(dims2.Y - 6); y2++)
									{
										Framing.GetTileSafely(tilesX4 + x2, tilesY4 + y2).active();
									}
								}
								Point16 origin4;
								origin4..ctor(tilesX4, tilesY4);
								Generator.GenerateStructure("WorldGeneration/AncientSCoinPile1", origin4, base.mod, false, false);
								placed4 = true;
							}
						}
					}
				}
				for (int m = 0; m < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-06); m++)
				{
					int tilesX5 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
					int tilesY5 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((double)Main.maxTilesY * 0.8));
					if (WorldGen.InWorld(tilesX5, tilesY5, 0) && (Framing.GetTileSafely(tilesX5, tilesY5).type == 1 || Framing.GetTileSafely(tilesX5, tilesY5).type == 59))
					{
						Point16 origin5;
						origin5..ctor(tilesX5, tilesY5);
						Generator.GenerateMultistructureRandom("WorldGeneration/AncientSHutM", origin5, base.mod, false, false);
					}
				}
				for (int n = 0; n < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-06); n++)
				{
					int tilesX6 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
					int tilesY6 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((double)Main.maxTilesY * 0.8));
					if (WorldGen.InWorld(tilesX6, tilesY6, 0) && (Framing.GetTileSafely(tilesX6, tilesY6).type == 1 || Framing.GetTileSafely(tilesX6, tilesY6).type == 59))
					{
						Point16 origin6;
						origin6..ctor(tilesX6, tilesY6);
						Generator.GenerateStructure("WorldGeneration/AncientSBridge", origin6, base.mod, false, false);
					}
				}
			}));
			tasks.Insert(ShiniesIndex + 10, new PassLegacy("Tied Lair", delegate(GenerationProgress progress)
			{
				bool placed = false;
				int attempts = 0;
				while (!placed && attempts++ < 100000)
				{
					int tilesX = WorldGen.genRand.Next(12, Main.maxTilesX - 12);
					int tilesY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((double)Main.maxTilesY * 0.8));
					if (WorldGen.InWorld(tilesX, tilesY, 0) && Framing.GetTileSafely(tilesX, tilesY).type == 1)
					{
						Point16 dims = Point16.Zero;
						Generator.GetDimensions("WorldGeneration/TiedLair", base.mod, ref dims, false);
						for (int x = 0; x < (int)dims.X; x++)
						{
							for (int y = 0; y < (int)dims.Y; y++)
							{
								TileLists.WhitelistTiles.Contains((int)Main.tile[tilesX + x, tilesY + y].type);
							}
						}
						Point16 origin;
						origin..ctor(tilesX, tilesY);
						Generator.GenerateStructure("WorldGeneration/TiedLair", origin, base.mod, false, false);
						placed = true;
					}
				}
			}));
			tasks.Insert(ShiniesIndex2 + 4, new PassLegacy("???", delegate(GenerationProgress progress)
			{
				this.HeroHall();
			}));
		}

		public void AncientHouse()
		{
			Mod mod = Redemption.Inst;
			Point16 origin;
			origin..ctor((int)((float)Main.maxTilesX * 0.07f), (int)((float)Main.maxTilesY * 0.45f));
			if (Main.dungeonX < Main.maxTilesX / 2)
			{
				origin..ctor((int)((float)Main.maxTilesX * 0.93f), (int)((float)Main.maxTilesY * 0.45f));
			}
			Generator.GenerateStructure("WorldGeneration/AncientHut", origin, mod, false, false);
			this.AncientWoodChest((int)(origin.X + 4), (int)(origin.Y + 14));
		}

		public void AncientWoodChest(int x, int y)
		{
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<AncientWoodChestTile>(), false, 0);
			int[] ChestLoot2 = new int[]
			{
				ModContent.ItemType<AncientWoodStave>(),
				ModContent.ItemType<AncientWoodSword>(),
				ModContent.ItemType<AncientWoodBow>()
			};
			int[] ChestLoot3 = new int[]
			{
				ModContent.ItemType<AncientWood>(),
				ModContent.ItemType<AncientStone>(),
				ModContent.ItemType<AncientDirt>()
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				chest.item[0].SetDefaults(ModContent.ItemType<Falcon>(), false);
				chest.item[0].stack = 1;
				chest.item[1].SetDefaults(Utils.Next<int>(WorldGen.genRand, ChestLoot2), false);
				chest.item[1].stack = 1;
				chest.item[2].SetDefaults(Utils.Next<int>(WorldGen.genRand, ChestLoot3), false);
				chest.item[2].stack = WorldGen.genRand.Next(20, 60);
			}
		}

		public void HeroHall()
		{
			Mod mod = Redemption.Inst;
			Point16 origin;
			origin..ctor((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesY * 0.45f));
			Generator.GenerateStructure("WorldGeneration/HallOfHeroes", origin, mod, false, false);
		}

		public override void PostDrawTiles()
		{
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i < 6000; i++)
			{
				Dust dust = Main.dust[i];
				if (dust.type == ModContent.DustType<RainbowStarDust>())
				{
					Texture2D texture = base.mod.GetTexture("ExtraTextures/WhiteFlare");
					Color color = dust.color * (1f - (float)dust.alpha / 255f);
					color.A = 0;
					float sizeMod = dust.scale;
					spriteBatch.Draw(texture, dust.position - Main.screenPosition, null, color, 0f, new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2)), sizeMod, SpriteEffects.None, 0f);
				}
			}
			Main.spriteBatch.End();
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
		}

		public void UpdateNukeCountdown()
		{
			if (!RedeWorld.nukeCountdownActive)
			{
				RedeWorld.nukeTimerInternal = 1800;
				return;
			}
			if (RedeWorld.nukeGroundZero == Vector2.Zero)
			{
				RedeWorld.nukeCountdownActive = false;
				return;
			}
			RedeWorld.nukeTimerShown = RedeWorld.nukeTimerInternal / 60;
			if (RedeWorld.nukeTimerInternal % 60 == 0 && RedeWorld.nukeTimerInternal > 0)
			{
				if (RedeConfigClient.Instance.NoLoreElements)
				{
					Main.NewText(RedeWorld.nukeTimerShown.ToString(), Color.Red, false);
				}
				else if (!Main.dedServ)
				{
					Redemption.Inst.DialogueUIElement.DisplayDialogue(RedeWorld.nukeTimerShown.ToString(), 40, 8, 1f, null, (30f - (float)RedeWorld.nukeTimerShown) / 30f * 2f, new Color?(Color.Red), new Color?(Color.Black), null, null, 0, 0);
				}
			}
			RedeWorld.nukeTimerInternal--;
			if (RedeWorld.nukeTimerInternal <= 0)
			{
				MoonlordDeathDrama.RequestLight(1f, RedeWorld.nukeGroundZero);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && !player.dead)
					{
						if (Vector2.Distance(player.Center, RedeWorld.nukeGroundZero) < 4592f)
						{
							MoonlordDeathDrama.RequestLight(1f, player.Center);
						}
						else if (Vector2.Distance(player.Center, RedeWorld.nukeGroundZero) < 9184f)
						{
							MoonlordDeathDrama.RequestLight(0.5f, player.Center);
						}
						else
						{
							MoonlordDeathDrama.RequestLight(0.35f, player.Center);
						}
					}
				}
			}
			if (RedeWorld.nukeTimerInternal <= -60)
			{
				RedeHelper.ProjectileExploson(RedeWorld.nukeGroundZero, 0f, 90, ModContent.ProjectileType<NukeShockwave>(), 1, 80f, RedeWorld.nukeGroundZero.X, RedeWorld.nukeGroundZero.Y);
				this.HandleNukeExplosion();
				WorldGen.KillTile((int)(RedeWorld.nukeGroundZero.X / 16f), (int)(RedeWorld.nukeGroundZero.Y / 16f), false, false, true);
				ConversionHandler.ConvertWasteland(RedeWorld.nukeGroundZero, 287, true);
				RedeWorld.nukeCountdownActive = false;
				RedeWorld.nukeGroundZero = Vector2.Zero;
				RedeWorld.nukeDropped = true;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public void HandleNukeExplosion()
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead)
				{
					if (Vector2.Distance(player.Center, RedeWorld.nukeGroundZero) < 4592f)
					{
						WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
						weightedRandom.Add(player.name + " saw a second sunrise.", 5.0);
						weightedRandom.Add(player.name + " was wiped off the face of " + Main.worldName + ".", 5.0);
						weightedRandom.Add(player.name + " experienced doomsday.", 5.0);
						weightedRandom.Add(player.name + " became a shadow on the ground.", 5.0);
						weightedRandom.Add(player.name + " couldn't find the fridge in time.", 1.0);
						string nukeDeathReason = weightedRandom;
						Main.PlaySound(Redemption.Inst.GetLegacySoundSlot(50, "Sounds/Custom/NukeExplosion"), player.Center);
						player.KillMe(PlayerDeathReason.ByCustomReason(nukeDeathReason), 999999.0, 1, false);
					}
					if (Vector2.Distance(player.Center, RedeWorld.nukeGroundZero) < 9184f && Collision.CanHit(player.position, player.width, player.height, RedeWorld.nukeGroundZero, 1, 1))
					{
						player.AddBuff(80, 900, true);
					}
				}
			}
			for (int j = 0; j < 200; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.active)
				{
					Player player2 = Main.LocalPlayer;
					if (Vector2.Distance(npc.Center, RedeWorld.nukeGroundZero) < 4592f)
					{
						player2.ApplyDamageToNPC(npc, 50000, 0f, 0, false);
					}
				}
			}
		}

		public override void Initialize()
		{
			if (!Redemption.cachedata)
			{
				RedeWorld.downedKeeper = false;
				RedeWorld.redemptionPoints = 0;
				RedeWorld.downedTheWarden = false;
				RedeWorld.downedMansionWraith = false;
			}
			RedeWorld.slayerRep = 0;
			RedeWorld.nebRep = 0;
			RedeWorld.downedJanitor = false;
			RedeWorld.downedVolt = false;
			RedeWorld.voltBegin = false;
			RedeWorld.downedMossyGoliath = false;
			RedeWorld.downedSoI = false;
			RedeWorld.nukeDropped = false;
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
			for (int i = 0; i < RedeWorld.labAccess.Length; i++)
			{
				RedeWorld.labAccess[i] = false;
			}
			RedeWorld.labSafe = false;
			RedeWorld.downedIBehemoth = false;
			RedeWorld.downedMACE = false;
			RedeWorld.downedPatientZero = false;
			RedeWorld.patientZeroMessages = false;
			RedeWorld.thornMessage = false;
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
			RedeWorld.slayerDeath = 0;
			RedeWorld.foundNewb = false;
			RedeWorld.spawnXenoBiome = false;
			RedeWorld.starliteGenned = false;
			RedeWorld.girusTalk1 = false;
			RedeWorld.girusTalk2 = false;
			RedeWorld.girusTalk3 = false;
			RedeWorld.infectionBegin = false;
			RedeWorld.girusCloaked = false;
			RedeWorld.girusCloakTimer = 0;
			RedeWorld.pzUS = false;
			RedeWorld.maceUS = false;
			RedeWorld.zephosDownedTimer = 0;
			RedeWorld.daerelDownedTimer = 0;
			RedeWorld.tbotDownedTimer = 0;
			RedeWorld.spawnKeeper = false;
			RedeWorld.oblitDeath = 0;
			RedeWorld.nebDeath = 0;
			RedeWorld.messageKingSlayer = false;
			RedeWorld.nukeCountdownActive = false;
			RedeWorld.nukeTimerInternal = 1800;
		}

		public override TagCompound Save()
		{
			List<string> downed = new List<string>();
			if (RedeWorld.downedKingChicken)
			{
				downed.Add("KingChicken");
			}
			if (RedeWorld.downedKeeper)
			{
				downed.Add("Keeper");
			}
			if (RedeWorld.downedSoI)
			{
				downed.Add("XenomiteCrystalPhase2");
			}
			if (RedeWorld.nukeDropped)
			{
				downed.Add("nukeDropped");
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
			for (int i = 0; i < RedeWorld.labAccess.Length; i++)
			{
				if (RedeWorld.labAccess[i])
				{
					downed.Add("labA" + i);
				}
			}
			if (RedeWorld.patientZeroMessages)
			{
				downed.Add("pzMessage");
			}
			if (RedeWorld.thornMessage)
			{
				downed.Add("thornMessage");
			}
			if (RedeWorld.keeperSaved)
			{
				downed.Add("keeperS");
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
				downed.Add("NebP1");
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
			tagCompound.Add("nebRep", RedeWorld.nebRep);
			tagCompound.Add("tbotDowned", RedeWorld.tbotDownedTimer);
			tagCompound.Add("zephosDowned", RedeWorld.zephosDownedTimer);
			tagCompound.Add("daerelDowned", RedeWorld.daerelDownedTimer);
			tagCompound.Add("oblitDeath", RedeWorld.oblitDeath);
			tagCompound.Add("nebDeath", RedeWorld.nebDeath);
			tagCompound.Add("slayerDeath", RedeWorld.slayerDeath);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			if (!Redemption.cachedata)
			{
				IList<string> downed = tag.GetList<string>("downed");
				RedeWorld.downedKingChicken = downed.Contains("KingChicken");
				RedeWorld.downedKeeper = downed.Contains("Keeper");
				RedeWorld.downedSoI = downed.Contains("XenomiteCrystalPhase2");
				RedeWorld.nukeDropped = downed.Contains("nukeDropped");
				RedeWorld.downedStrangePortal = downed.Contains("StrangePortal");
				RedeWorld.downedVlitch1 = downed.Contains("VlitchCleaver");
				RedeWorld.downedVlitch2 = downed.Contains("VlitchWormHead");
				RedeWorld.downedDarkSlime = downed.Contains("DarkSlime");
				RedeWorld.downedSlayer = downed.Contains("KSEntrance");
				RedeWorld.spawnSapphironOre = downed.Contains("sapphiron");
				RedeWorld.spawnScarlionOre = downed.Contains("scarlion");
				RedeWorld.spawnDragonOre = downed.Contains("dragore");
				RedeWorld.foundNewb = downed.Contains("newbFound");
				RedeWorld.downedVlitch3 = downed.Contains("OO");
				RedeWorld.downedSkullDigger = downed.Contains("SkullDigger");
				RedeWorld.downedSunkenCaptain = downed.Contains("SunkenCaptain");
				RedeWorld.spawnXenoBiome = downed.Contains("wasteland");
				RedeWorld.starliteGenned = downed.Contains("starliteGen");
				RedeWorld.girusTalk1 = downed.Contains("girTalk1");
				RedeWorld.girusTalk2 = downed.Contains("girTalk2");
				RedeWorld.girusTalk3 = downed.Contains("girTalk3");
				RedeWorld.labSafe = downed.Contains("labSafe1");
				RedeWorld.infectionBegin = downed.Contains("infection1");
				RedeWorld.downedBlisterface = downed.Contains("Blisterface");
				RedeWorld.downedStage3Scientist = downed.Contains("Stage3Scientist");
				for (int i = 0; i < RedeWorld.labAccess.Length; i++)
				{
					RedeWorld.labAccess[i] = downed.Contains("labA" + i);
				}
				RedeWorld.patientZeroMessages = downed.Contains("pzMessage");
				RedeWorld.thornMessage = downed.Contains("thornMessage");
				RedeWorld.keeperSaved = downed.Contains("keeperS");
				RedeWorld.downedIBehemoth = downed.Contains("IrradiatedBehemoth");
				RedeWorld.downedMACE = downed.Contains("MACEProjectHead");
				RedeWorld.downedPatientZero = downed.Contains("PatientZero");
				RedeWorld.downedNebuleus = downed.Contains("NebP1");
				RedeWorld.downedEaglecrestGolem = downed.Contains("EaglecrestGolem");
				RedeWorld.downedChickenInv = downed.Contains("downedChickenInv");
				RedeWorld.downedChickenInvPZ = downed.Contains("downedChickenInvPZ");
				RedeWorld.downedEaglecrestGolemPZ = downed.Contains("EaglecrestGolemPZ");
				RedeWorld.downedThorn = downed.Contains("Thorn");
				RedeWorld.downedThornPZ = downed.Contains("ThornPZ");
				RedeWorld.redemptionPoints = tag.GetInt("redePoints");
				RedeWorld.slayerRep = tag.GetInt("slayRep");
				RedeWorld.nebRep = tag.GetInt("nebRep");
				RedeWorld.downedJanitor = downed.Contains("JanitorBot");
				RedeWorld.downedVolt = downed.Contains("TbotMiniboss");
				RedeWorld.voltBegin = downed.Contains("voltBeginFight");
				RedeWorld.pzUS = downed.Contains("PUS");
				RedeWorld.maceUS = downed.Contains("MPUS");
				RedeWorld.daerelDownedTimer = tag.GetInt("daerelDowned");
				RedeWorld.tbotDownedTimer = tag.GetInt("tbotDowned");
				RedeWorld.zephosDownedTimer = tag.GetInt("zephosDowned");
				RedeWorld.downedMossyGoliath = downed.Contains("MossyGoliath");
				RedeWorld.downedTheWarden = downed.Contains("TheWarden");
				RedeWorld.downedMansionWraith = downed.Contains("MansionWraith");
				RedeWorld.messageKingSlayer = downed.Contains("messageKingSlayer");
				RedeWorld.oblitDeath = tag.GetInt("oblitDeath");
				RedeWorld.nebDeath = tag.GetInt("nebDeath");
				RedeWorld.slayerDeath = tag.GetInt("slayerDeath");
			}
			Redemption.cachedata = false;
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = default(BitsByte);
			flags[0] = RedeWorld.downedKingChicken;
			flags[1] = RedeWorld.downedKeeper;
			flags[2] = RedeWorld.downedSoI;
			flags[3] = RedeWorld.nukeDropped;
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
			flags3[2] = RedeWorld.girusTalk1;
			flags3[3] = RedeWorld.girusTalk2;
			flags3[4] = RedeWorld.girusTalk3;
			flags3[5] = RedeWorld.keeperSaved;
			flags3[6] = RedeWorld.downedPatientZero;
			flags3[7] = RedeWorld.labSafe;
			writer.Write(flags3);
			BitsByte flags4 = default(BitsByte);
			for (int i = 0; i < RedeWorld.labAccess.Length; i++)
			{
				flags4[i] = RedeWorld.labAccess[i];
			}
			flags4[7] = RedeWorld.infectionBegin;
			writer.Write(flags4);
			BitsByte flags5 = default(BitsByte);
			flags5[0] = RedeWorld.patientZeroMessages;
			flags5[1] = RedeWorld.downedNebuleus;
			flags5[2] = RedeWorld.downedEaglecrestGolem;
			flags5[3] = RedeWorld.downedChickenInv;
			flags5[4] = RedeWorld.downedChickenInvPZ;
			flags5[5] = RedeWorld.downedEaglecrestGolemPZ;
			flags5[6] = RedeWorld.downedThorn;
			flags5[7] = RedeWorld.downedThornPZ;
			writer.Write(flags5);
			BitsByte flags6 = default(BitsByte);
			flags6[0] = RedeWorld.downedJanitor;
			flags6[1] = RedeWorld.downedVolt;
			flags6[2] = RedeWorld.voltBegin;
			flags6[3] = RedeWorld.pzUS;
			flags6[4] = RedeWorld.maceUS;
			flags6[5] = RedeWorld.downedMossyGoliath;
			flags6[6] = RedeWorld.downedTheWarden;
			writer.Write(flags6);
			BitsByte flags7 = default(BitsByte);
			flags7[0] = RedeWorld.spawnXenoBiome;
			flags7[1] = RedeWorld.downedMansionWraith;
			flags7[5] = RedeWorld.thornMessage;
			writer.Write(flags7);
			writer.Write(RedeWorld.redemptionPoints);
			writer.Write(RedeWorld.girusCloakTimer);
			writer.Write(RedeWorld.slayerRep);
			writer.Write(RedeWorld.nebRep);
			writer.Write(RedeWorld.zephosDownedTimer);
			writer.Write(RedeWorld.daerelDownedTimer);
			writer.Write(RedeWorld.tbotDownedTimer);
			writer.Write(RedeWorld.oblitDeath);
			writer.Write(RedeWorld.nebDeath);
			writer.Write(RedeWorld.slayerDeath);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			RedeWorld.downedKingChicken = flags[0];
			RedeWorld.downedKeeper = flags[1];
			RedeWorld.downedSoI = flags[2];
			RedeWorld.nukeDropped = flags[3];
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
			RedeWorld.girusTalk1 = flags3[2];
			RedeWorld.girusTalk2 = flags3[3];
			RedeWorld.girusTalk3 = flags3[4];
			RedeWorld.keeperSaved = flags3[5];
			RedeWorld.downedPatientZero = flags3[6];
			RedeWorld.labSafe = flags3[7];
			BitsByte flags4 = reader.ReadByte();
			for (int i = 0; i < RedeWorld.labAccess.Length; i++)
			{
				RedeWorld.labAccess[i] = flags4[i];
			}
			RedeWorld.infectionBegin = flags4[7];
			BitsByte flags5 = reader.ReadByte();
			RedeWorld.patientZeroMessages = flags5[0];
			RedeWorld.downedNebuleus = flags5[1];
			RedeWorld.downedEaglecrestGolem = flags5[2];
			RedeWorld.downedChickenInv = flags5[3];
			RedeWorld.downedChickenInvPZ = flags5[4];
			RedeWorld.downedEaglecrestGolemPZ = flags5[5];
			RedeWorld.downedThorn = flags5[6];
			RedeWorld.downedThornPZ = flags5[7];
			BitsByte flags6 = reader.ReadByte();
			RedeWorld.downedJanitor = flags6[0];
			RedeWorld.downedVolt = flags6[1];
			RedeWorld.voltBegin = flags6[2];
			RedeWorld.pzUS = flags6[3];
			RedeWorld.maceUS = flags6[4];
			RedeWorld.downedMossyGoliath = flags6[5];
			RedeWorld.downedTheWarden = flags6[6];
			BitsByte flags7 = reader.ReadByte();
			RedeWorld.spawnXenoBiome = flags7[0];
			RedeWorld.downedMansionWraith = flags7[1];
			RedeWorld.thornMessage = flags7[5];
			RedeWorld.redemptionPoints = reader.ReadInt32();
			RedeWorld.girusCloakTimer = reader.ReadInt32();
			RedeWorld.slayerRep = reader.ReadInt32();
			RedeWorld.nebRep = reader.ReadInt32();
			RedeWorld.daerelDownedTimer = reader.ReadInt32();
			RedeWorld.zephosDownedTimer = reader.ReadInt32();
			RedeWorld.tbotDownedTimer = reader.ReadInt32();
			RedeWorld.oblitDeath = reader.ReadInt32();
			RedeWorld.nebDeath = reader.ReadInt32();
			RedeWorld.slayerDeath = reader.ReadInt32();
		}

		public static bool spawnOre = false;

		public static bool spawnDragonOre = false;

		public static bool spawnSapphironOre = false;

		public static bool spawnScarlionOre = false;

		public static bool spawnXenoBiome = false;

		public static bool messageKingSlayer = false;

		public static bool starliteGenned = false;

		public static bool labSafe = false;

		public static bool infectionBegin = false;

		public static int xenoBiome = 0;

		public static int evilXenoBiome = 0;

		public static int evilXenoBiome2 = 0;

		public static int labBiome = 0;

		public static int slayerBiome = 0;

		private int labSafeMessageTimer;

		public static bool[] labAccess = new bool[7];

		public static bool patientZeroMessages = false;

		public static bool thornMessage = false;

		public static bool KSRajahInteraction = false;

		public static int redemptionPoints = 0;

		public static int girusCloakTimer = 0;

		public static bool girusCloaked = false;

		public static int slayerRep = 0;

		public static int nebRep = 0;

		public static bool darkSlimeLure = false;

		public static int zephosDownedTimer = 0;

		public static int daerelDownedTimer = 0;

		public static int tbotDownedTimer = 0;

		public static int soullessBiome = 0;

		public static bool spawnKeeper;

		public static bool blobbleSwarm;

		public static int blobbleSwarmTimer;

		public static int blobbleSwarmCooldown;

		public static int nukeTimerInternal = 1800;

		public static int nukeTimerShown = 30;

		public static int nukeFireballRadius = 287;

		public static bool nukeCountdownActive = false;

		public static Vector2 nukeGroundZero = Vector2.Zero;

		public static bool downedKingChicken = false;

		public static bool downedKeeper = false;

		public static bool downedSoI = false;

		public static bool nukeDropped = false;

		public static bool downedStrangePortal = false;

		public static bool downedVlitch1 = false;

		public static bool downedVlitch2 = false;

		public static bool downedDarkSlime = false;

		public static bool downedSlayer = false;

		public static int slayerDeath = 0;

		public static bool foundNewb = false;

		public static bool downedVlitch3 = false;

		public static bool downedSkullDigger = false;

		public static bool downedSunkenCaptain = false;

		public static bool girusTalk1 = false;

		public static bool girusTalk2 = false;

		public static bool girusTalk3 = false;

		public static bool downedBlisterface = false;

		public static bool downedStage3Scientist = false;

		public static bool keeperSaved = false;

		public static bool downedIBehemoth = false;

		public static bool downedMACE = false;

		public static bool downedPatientZero = false;

		public static bool downedNebuleus = false;

		public static bool downedEaglecrestGolem = false;

		public static bool downedChickenInvPZ = false;

		public static bool downedChickenInv = false;

		public static bool downedEaglecrestGolemPZ = false;

		public static bool downedThorn = false;

		public static bool downedThornPZ = false;

		public static bool downedJanitor = false;

		public static bool downedVolt = false;

		public static bool voltBegin = false;

		public static bool pzUS;

		public static bool maceUS;

		public static bool downedMossyGoliath = false;

		public static bool downedTheWarden = false;

		public static bool downedMansionWraith = false;

		public static int oblitDeath = 0;

		public static int nebDeath = 0;
	}
}
