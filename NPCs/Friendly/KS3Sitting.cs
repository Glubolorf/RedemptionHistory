using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Single;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Lore;
using Redemption.Items.Materials.HM;
using Redemption.Items.Quest.KingSlayer;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.Tiles.Furniture.Ship;
using Redemption.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Friendly
{
	public class KS3Sitting : ModNPC
	{
		public override void SetDefaults()
		{
			base.npc.friendly = true;
			base.npc.townNPC = true;
			base.npc.dontTakeDamage = true;
			base.npc.noGravity = true;
			base.npc.width = 56;
			base.npc.height = 82;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 999;
			base.npc.aiStyle = -1;
			base.npc.knockBackResist = 0f;
			base.npc.npcSlots = 0f;
			base.npc.immortal = true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
			Main.npcFrameCount[base.npc.type] = 7;
			NPCID.Sets.TownCritter[base.npc.type] = true;
		}

		public override bool UsesPartyHat()
		{
			return false;
		}

		public override void AI()
		{
			base.npc.ai[0] += 1f;
			if (base.npc.ai[0] >= 300f && base.npc.ai[0] <= 370f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 10.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 88;
					if (base.npc.frame.Y > 528)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (base.npc.ai[0] >= 370f)
				{
					base.npc.frame.Y = 0;
					base.npc.ai[0] = 0f;
				}
				if (base.npc.ai[0] == 330f)
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						if (RedeWorld.slayerRep >= 3)
						{
							int p = Projectile.NewProjectile(base.npc.Center.X - 140f, base.npc.Center.Y - 50f, 0f, 0f, ModContent.ProjectileType<HologramShip2>(), 0, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						else
						{
							int p2 = Projectile.NewProjectile(base.npc.Center.X - 140f, base.npc.Center.Y - 50f, 0f, 0f, ModContent.ProjectileType<HologramShip>(), 0, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
						break;
					case 1:
					{
						int p3 = Projectile.NewProjectile(base.npc.Center.X - 140f, base.npc.Center.Y - 50f, 0f, 0f, ModContent.ProjectileType<HologramLab>(), 0, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
						break;
					}
					case 2:
					{
						int p4 = Projectile.NewProjectile(base.npc.Center.X - 140f, base.npc.Center.Y - 50f, 0f, 0f, ModContent.ProjectileType<HologramPlanet>(), 0, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
						break;
					}
					case 3:
					{
						int p5 = Projectile.NewProjectile(base.npc.Center.X - 140f, base.npc.Center.Y - 50f, 0f, 0f, ModContent.ProjectileType<HologramEpidotra>(), 0, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p5].netUpdate = true;
						break;
					}
					}
				}
			}
			base.npc.wet = false;
			base.npc.lavaWet = false;
			base.npc.honeyWet = false;
			base.npc.velocity.X = (base.npc.velocity.Y = 0f);
			base.npc.dontTakeDamage = true;
			base.npc.immune[255] = 30;
			if (RedeWorld.downedVlitch3 || RedeWorld.downedNebuleus)
			{
				base.npc.active = false;
			}
			if (NPC.CountNPCS(ModContent.NPCType<KS3Sitting>()) >= 2 && Main.rand.Next(2) == 0)
			{
				base.npc.active = false;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<KS3_Body>()))
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 92, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				base.npc.active = false;
			}
			if (Main.netMode != 1)
			{
				base.npc.homeless = false;
				base.npc.homeTileX = -1;
				base.npc.homeTileY = -1;
				base.npc.netUpdate = true;
			}
		}

		public override void ResetEffects()
		{
			KS3Sitting.SwitchInfo = false;
			KS3Sitting.CrashedShip = false;
			KS3Sitting.WhatAreYouDoing = false;
			KS3Sitting.GiveUranium = false;
			KS3Sitting.Fight = false;
			KS3Sitting.AreYouHuman = false;
			KS3Sitting.HallOfHeroes = false;
			KS3Sitting.Lab = false;
			KS3Sitting.Planet = false;
			KS3Sitting.Wasteland = false;
			KS3Sitting.DataLogs = false;
			KS3Sitting.Give1 = false;
			KS3Sitting.Give2 = false;
			KS3Sitting.Give3 = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Dialogue";
			string WhatT = "Why are you here?";
			string ShipT = "Crashed Spaceship?";
			string UraniumT = "(Give Uranium)";
			string FightT = "You aren't fighting me?";
			string HumanT = "Are you a human?";
			string HallT = "Hall of Heroes?";
			string LabT = "Abandoned Lab?";
			string PlanetT = "Epidotra?";
			string WastelandT = "Other world?";
			string DataT = "Data Logs?";
			string QuestT = "Quest";
			button = SwitchInfoT;
			if (KS3Sitting.ChatNumber == 0)
			{
				button2 = WhatT;
				KS3Sitting.WhatAreYouDoing = true;
				return;
			}
			if (KS3Sitting.ChatNumber == 1)
			{
				button2 = ShipT;
				KS3Sitting.CrashedShip = true;
				return;
			}
			if (KS3Sitting.ChatNumber == 2 && RedeWorld.slayerRep == 0)
			{
				button2 = UraniumT;
				KS3Sitting.GiveUranium = true;
				return;
			}
			if (KS3Sitting.ChatNumber == 2 && RedeWorld.slayerRep >= 1)
			{
				button2 = FightT;
				KS3Sitting.Fight = true;
				return;
			}
			if (KS3Sitting.ChatNumber == 3 && RedeWorld.slayerRep >= 1)
			{
				button2 = HumanT;
				KS3Sitting.AreYouHuman = true;
				return;
			}
			if (KS3Sitting.ChatNumber == 4 && RedeWorld.slayerRep >= 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall)
			{
				button2 = HallT;
				KS3Sitting.HallOfHeroes = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 5 && RedeWorld.slayerRep >= 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 4 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = LabT;
				KS3Sitting.Lab = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 6 && RedeWorld.slayerRep >= 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 5 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = PlanetT;
				KS3Sitting.Planet = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 7 && RedeWorld.slayerRep >= 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 6 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = WastelandT;
				KS3Sitting.Wasteland = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep >= 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 7 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = DataT;
				KS3Sitting.DataLogs = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep == 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = QuestT;
				KS3Sitting.Give1 = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep == 2 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = QuestT;
				KS3Sitting.Give2 = true;
				return;
			}
			if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep == 3 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep >= 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				button2 = QuestT;
				KS3Sitting.Give3 = true;
				return;
			}
			KS3Sitting.ChatNumber = 0;
			button2 = WhatT;
			KS3Sitting.WhatAreYouDoing = true;
		}

		public void ResetBools()
		{
			KS3Sitting.CrashedShip = false;
			KS3Sitting.WhatAreYouDoing = false;
			KS3Sitting.GiveUranium = false;
			KS3Sitting.Fight = false;
			KS3Sitting.AreYouHuman = false;
			KS3Sitting.HallOfHeroes = false;
			KS3Sitting.Lab = false;
			KS3Sitting.Planet = false;
			KS3Sitting.Wasteland = false;
			KS3Sitting.DataLogs = false;
			KS3Sitting.Give1 = false;
			KS3Sitting.Give2 = false;
			KS3Sitting.Give3 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				this.ResetBools();
				KS3Sitting.ChatNumber++;
				if (KS3Sitting.ChatNumber > 9)
				{
					KS3Sitting.ChatNumber = 0;
					return;
				}
			}
			else if (KS3Sitting.ChatNumber == 2 && RedeWorld.slayerRep == 0)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Player player = Main.LocalPlayer;
				int Urani = player.FindItem(ModContent.ItemType<Uranium>());
				if (Urani >= 0)
				{
					player.inventory[Urani].stack--;
					if (player.inventory[Urani].stack <= 0)
					{
						player.inventory[Urani] = new Item();
					}
					Main.npcChatText = this.UraniChat();
					player.QuickSpawnItem(72, 20);
					RedeWorld.slayerRep++;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					CombatText.NewText(base.npc.getRect(), Color.LightCyan, "New Dialogue Available", true, false);
					Main.PlaySound(24, -1, -1, 1, 1f, 0f);
					return;
				}
				Main.npcChatText = this.NoUraniChat();
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				return;
			}
			else if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep == 1 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep == 1 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Player player2 = Main.LocalPlayer;
				int WiringKit = player2.FindItem(ModContent.ItemType<SlayerWiringKit>());
				if (WiringKit >= 0)
				{
					player2.inventory[WiringKit].stack--;
					if (player2.inventory[WiringKit].stack <= 0)
					{
						player2.inventory[WiringKit] = new Item();
					}
					Main.npcChatText = this.Give1Chat();
					player2.QuickSpawnItem(73, 4);
					RedeWorld.slayerRep++;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					CombatText.NewText(base.npc.getRect(), Color.LightCyan, "New Dialogue Available", true, false);
					Main.PlaySound(24, -1, -1, 1, 1f, 0f);
					Mod inst = Redemption.Inst;
					Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
					colorToTile[new Color(150, 150, 150)] = -2;
					colorToTile[Color.Black] = -1;
					TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/SlayerShipFix1"), colorToTile, null, null, null, null, null, null);
					Point origin = new Point((int)((float)Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.3f));
					if (Main.dungeonX < Main.maxTilesX / 2)
					{
						origin = new Point((int)((float)Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.3f));
					}
					texGenerator.Generate(origin.X, origin.Y, true, true);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					return;
				}
				Main.npcChatText = this.QuestChat();
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				return;
			}
			else if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep == 2 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep == 2 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Player player3 = Main.LocalPlayer;
				int HullPlating = player3.FindItem(ModContent.ItemType<SlayerHullPlating>());
				if (HullPlating >= 0)
				{
					player3.inventory[HullPlating].stack--;
					if (player3.inventory[HullPlating].stack <= 0)
					{
						player3.inventory[HullPlating] = new Item();
					}
					Main.npcChatText = this.Give2Chat();
					player3.QuickSpawnItem(73, 8);
					RedeWorld.slayerRep++;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					CombatText.NewText(base.npc.getRect(), Color.LightCyan, "New Dialogue Available", true, false);
					Main.PlaySound(24, -1, -1, 1, 1f, 0f);
					Mod inst2 = Redemption.Inst;
					Dictionary<Color, int> colorToTile2 = new Dictionary<Color, int>();
					colorToTile2[new Color(0, 255, 255)] = ModContent.TileType<SlayerShipPanelTile>();
					colorToTile2[new Color(255, 0, 255)] = ModContent.TileType<ShipGlassTile>();
					colorToTile2[new Color(150, 150, 150)] = -2;
					colorToTile2[Color.Black] = -1;
					TexGen texGenerator2 = BaseWorldGenTex.GetTexGenerator(inst2.GetTexture("WorldGeneration/SlayerShipFix2"), colorToTile2, null, null, null, null, null, null);
					Point origin2 = new Point((int)((float)Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.3f));
					if (Main.dungeonX < Main.maxTilesX / 2)
					{
						origin2 = new Point((int)((float)Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.3f));
					}
					texGenerator2.Generate(origin2.X, origin2.Y, true, true);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					return;
				}
				Main.npcChatText = this.QuestChat();
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				return;
			}
			else if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep == 3 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep == 3 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Player player4 = Main.LocalPlayer;
				int ShipEngine = player4.FindItem(ModContent.ItemType<SlayerShipEngine>());
				if (ShipEngine >= 0)
				{
					player4.inventory[ShipEngine].stack--;
					if (player4.inventory[ShipEngine].stack <= 0)
					{
						player4.inventory[ShipEngine] = new Item();
					}
					Main.npcChatText = this.Give3Chat();
					player4.QuickSpawnItem(73, 12);
					player4.QuickSpawnItem(ModContent.ItemType<MemoryChip>(), 1);
					RedeWorld.slayerRep++;
					RedeWorld.redemptionPoints += 2;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					CombatText.NewText(base.npc.getRect(), Color.LightCyan, "New Dialogue Available", true, false);
					CombatText.NewText(player4.getRect(), Color.Gold, "+2", true, false);
					Main.PlaySound(24, -1, -1, 1, 1f, 0f);
					Mod mod = Redemption.Inst;
					Dictionary<Color, int> colorToTile3 = new Dictionary<Color, int>();
					colorToTile3[new Color(0, 255, 255)] = ModContent.TileType<SlayerShipPanelTile>();
					colorToTile3[new Color(255, 0, 255)] = ModContent.TileType<ShipGlassTile>();
					colorToTile3[new Color(150, 150, 150)] = -2;
					colorToTile3[Color.Black] = -1;
					Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
					colorToWall[new Color(0, 255, 0)] = ModContent.WallType<SlayerShipPanelWallTile>();
					colorToWall[Color.Black] = -1;
					TexGen texGenerator3 = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/SlayerShipFix3"), colorToTile3, mod.GetTexture("WorldGeneration/SlayerShipWallsFix"), colorToWall, null, null, null, null);
					Point origin3 = new Point((int)((float)Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.3f));
					if (Main.dungeonX < Main.maxTilesX / 2)
					{
						origin3 = new Point((int)((float)Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.3f));
					}
					texGenerator3.Generate(origin3.X, origin3.Y, true, true);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					return;
				}
				Main.npcChatText = this.QuestChat();
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				return;
			}
			else
			{
				if ((KS3Sitting.ChatNumber == 9 && RedeWorld.slayerRep >= 4 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep >= 4 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
				{
					Main.npcChatText = this.QuestChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				if ((KS3Sitting.ChatNumber == 8 && RedeWorld.slayerRep >= 4 && Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall) || (KS3Sitting.ChatNumber == 7 && RedeWorld.slayerRep >= 4 && !Main.LocalPlayer.GetModPlayer<RedePlayer>().foundHall))
				{
					shop = true;
					return;
				}
				Main.npcChatText = KS3Sitting.ChitChat();
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog1>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog2>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog3>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog6>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog335>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog772>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog919>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog180499>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog182500>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog182501>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog182573>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog184753>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog184989>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog466105>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog500198>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog545675>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog999735>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog1000000>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog1012875>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog3650000>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog5385430>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog36500001>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog164550614>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog364635000>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog365000000>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Datalog389035250>(), false);
			nextSlot++;
		}

		public string NoUraniChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (RedeWorld.slayerRep == 1)
			{
				chat.Add("I don't need any more uranium. The amount you gave me will do the job.", 1.0);
			}
			else
			{
				chat.Add("You don't have any uranium, idiot.", 1.0);
			}
			return chat;
		}

		public string QuestChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (RedeWorld.slayerRep == 1)
			{
				chat.Add("You want to do something? Craft me a wiring kit, should be simple enough. I would do it, but I can't be bothered to get off this chair. Use that Cyber Fabricator the room under me.", 1.0);
			}
			else if (RedeWorld.slayerRep == 2)
			{
				chat.Add("You seem rather eager to help... Well, just get me some hull plating would you? Thanks.", 1.0);
			}
			else if (RedeWorld.slayerRep == 3)
			{
				chat.Add("This is probably a bit too complicated for you, but craft me a AFTL engine. AFTL stands for 'Almost Faster Than Light' since, well, I don't know if going faster than light is even possible. I'm planning on leaving soon, so be quick.", 1.0);
			}
			else if (RedeWorld.slayerRep == 4)
			{
				chat.Add("You've done a lot for me, thank you, I guess. There isn't anything else that you can do now. But I appreciate the help you've given me.", 1.0);
			}
			return chat;
		}

		public string UraniChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You really went out of your way to give me some uranium. Thanks I guess.", 1.0);
			weightedRandom.Add("I could've found some myself you know.", 1.0);
			weightedRandom.Add("Thanks... ?", 1.0);
			return weightedRandom;
		}

		public string Give1Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You actually bothered to do it... Good job.", 1.0);
			return weightedRandom;
		}

		public string Give2Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("How can you even carry that? Uh, thanks, I suppose.", 1.0);
			return weightedRandom;
		}

		public string Give3Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Was helping me with all that really necessary for you? You don't gain anything from it. But thank you regardless. I'll be leaving soon, but I want you to have this. I have yet to figure out a use for it, but take it.", 1.0);
			return weightedRandom;
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (KS3Sitting.WhatAreYouDoing)
			{
				if (RedeWorld.slayerRep >= 1 && RedeWorld.slayerRep < 4)
				{
					chat.Add("Fixing this crashed ship, and just reflecting on our fight. Honestly I'm just doing it because I got nothing else to do.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("The ship is fixed... I just can't be bothered to get up.", 1.0);
				}
				else
				{
					chat.Add("I can ask you the same question. If you've come here to just chit-chat after our fight I'm not interested.", 1.0);
				}
			}
			else if (KS3Sitting.CrashedShip)
			{
				if (RedeWorld.slayerRep == 1)
				{
					chat.Add("Yes. An android thought it'd be a good idea to 'borrow' it, and ended up yeeting it 20 feet under. The uranium you gave me should help.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 2 && RedeWorld.slayerRep < 4)
				{
					chat.Add("Yes. An android thought it'd be a good idea to 'borrow' it, and ended up yeeting it 20 feet under. The things you gave me should help.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("Why are you still here? Ship's fixed now.", 1.0);
				}
				else
				{
					chat.Add("What's the matter, you never seen a spaceship before? Some android thought it'd be a good idea to 'borrow' it, and ended up yeeting it 20 feet under. Unfortunately I've ran out of Uranium, but I can't be bothered to find it right now.", 1.0);
				}
			}
			else if (KS3Sitting.Fight)
			{
				if (RedeWorld.slayerRep == 2)
				{
					chat.Add("Can't be asked. Now that I think about it, fighting because you killed a weak little undead before me is kinda dumb.", 1.0);
				}
				else if (RedeWorld.slayerRep == 3)
				{
					chat.Add("If you really want to fight, go ahead, use that cyber tech thing. I don't care.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("After all you've done, I don't feel like fighting you.", 1.0);
				}
				else
				{
					chat.Add("Why would I want to fight now? I lost.", 1.0);
					chat.Add("Because unlike you, I actually have a life.", 1.0);
				}
			}
			else if (KS3Sitting.AreYouHuman)
			{
				if (RedeWorld.slayerRep == 2)
				{
					chat.Add("I was long ago, I became a robot when I realised it would be the only way to survive my world's end.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 3)
				{
					chat.Add("I wish I still was, mate. I can't eat or sleep in this robot body, and honestly I'm struggling to think of a reason to continue this torture.", 1.0);
				}
				else
				{
					chat.Add("I'm not some dude in a spacesuit, I'm a complete robot with a human mind. You may think that's cool, but it's really not. I seriously regret becoming one.", 1.0);
				}
			}
			else if (KS3Sitting.HallOfHeroes)
			{
				if (RedeWorld.slayerRep == 2)
				{
					chat.Add("I did say I'm part of the Heroes, but I'm considering leaving. It's never interesting anymore.", 1.0);
				}
				else if (RedeWorld.slayerRep == 3)
				{
					chat.Add("The members of the Heroes are all dumbasses. The leader just travels around the world not doing anything, and I don't even know what the other 2 members are up to.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("There are 4 members of the Heroes. The first is that damn demigod, honestly he's a chill guy, I just hate how much stronger he is compared to me. The 2nd member is some moron who's supposedly invincible, not once have I seen him get hurt. 3rd is... Well she's probably the most normal out of all of us, but I don't know what she's up to now.", 1.0);
					chat.Add("... There's something strange about the Demigod's statue... It doesn't look like him. Did someone change it?", 1.0);
				}
				else
				{
					chat.Add("You saw my statue there? Yeah, I'm part of the Heroes. But it's pretty boring, I'm always assigned to kill the weaklings, like the Keeper.", 1.0);
				}
			}
			else if (KS3Sitting.Lab)
			{
				if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("I'm planning to check it out soon, it's located on the other world but I can easily fly there with the SoS.", 1.0);
				}
				else
				{
					chat.Add("Haven't been there myself, but I'm looking into it. Could have plenty of supplies to fix this ship. It had a security system, but it malfunctioned, so I might take a look and maybe steal some supplies.", 1.0);
				}
			}
			else if (KS3Sitting.Planet)
			{
				if (RedeWorld.slayerRep >= 2 && RedeWorld.slayerRep < 4)
				{
					chat.Add("The world is called Epidotra, it's where the Heroes are from.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("The world we are in is called Epidotra, this appears to be just a tiny island on it, so I was lucky for the ship to crash here than in the ocean. The mainland has 6 domains, Anglon, Ithon, Gathuram, Nirin, Erellon, and Thamor. There's another domain which is it's own island called Swaylan, but that's disconnected from the rest of the world.", 1.0);
				}
				else
				{
					chat.Add("What? You want a history lesson or something?", 1.0);
				}
			}
			else if (KS3Sitting.Wasteland)
			{
				if (RedeWorld.slayerRep >= 2 && RedeWorld.slayerRep < 4)
				{
					chat.Add("You see the planet on the right in that one hologram? That's what I've named Liden, a radioactive and desolate wasteland devoid of any life.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("I'll be leaving soon to check it out. Scans show barely any life, just a frozen radioactive wasteland.", 1.0);
				}
				else
				{
					chat.Add("The other world just suddenly appeared out of nowhere, I'm interested in it though. I've scanned the surface and it seems to be rather frozen, with so far no signs of human life. Reminds me of a planet I checked out while in space... In fact, reminds me of many planets.", 1.0);
				}
			}
			else
			{
				if (!KS3Sitting.DataLogs)
				{
					return "...";
				}
				if (RedeWorld.slayerRep == 2)
				{
					chat.Add("I really just wrote those to keep my sanity. I don't want to tell you how boring travelling through space for a million years is.", 1.0);
				}
				else if (RedeWorld.slayerRep == 3)
				{
					chat.Add("I wrote those every day I was in space, waiting for my world to be restored. Even though I'm back now, I don't feel satisfied.", 1.0);
				}
				else
				{
					chat.Add("Can you not read through my data logs please.", 1.0);
				}
			}
			return chat;
		}

		public override string GetChat()
		{
			Player player = Main.player[Main.myPlayer];
			ModLoader.GetMod("Grealm");
			ModLoader.GetMod("AAMod");
			ModLoader.GetMod("Calamity");
			ModLoader.GetMod("ThoriumMod");
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneSlayer)
			{
				chat.Add("Wait a second... This isn't my ship. Did you move my chair?", 1.0);
			}
			else
			{
				if (RedeWorld.slayerRep >= 2)
				{
					chat.Add("Hey, I'm busy... Procrastinating.", 1.0);
					chat.Add("What is it?", 1.0);
					chat.Add("Come to help again or what?", 1.0);
					chat.Add("Leave me alone.", 1.0);
				}
				else if (RedeWorld.slayerRep >= 4)
				{
					chat.Add("Hey, what's up?", 1.0);
					chat.Add("Come by to talk or what?", 1.0);
					chat.Add("You've been a big help, thanks.", 1.0);
				}
				else
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
					{
						chat.Add("Oh great, the robot is here... What do you want?", 1.0);
					}
					else
					{
						chat.Add("Oh great, the Terrarian is here... What do you want?", 1.0);
					}
					chat.Add("Hey, I'm busy, piss off.", 1.0);
					chat.Add("Did you really feel the need to break into my ship?", 1.0);
					chat.Add("Fight's over. I'm busy. Get lost.", 1.0);
					chat.Add("Could you just leave me alone.", 1.0);
				}
				if (NPC.downedMoonlord)
				{
					chat.Add("I'll be leaving here soon, make it quick.", 1.0);
				}
				if (BasePlayer.HasHelmet(player, ModContent.ItemType<KingSlayerMask>(), true))
				{
					chat.Add("What have you got on your head? Are you trying to cosplay as me or something?", 1.0);
				}
				if (BasePlayer.HasHelmet(player, ModContent.ItemType<AndroidHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<PrototypeSilverHead>(), true))
				{
					chat.Add("I'm not an idiot ya know, I know one of my own minions when I see one.", 1.0);
				}
				if (player.HasBuff(26) && RedeWorld.slayerRep < 2)
				{
					chat.Add("Look at you all well fed... Good for you.", 1.0);
				}
				if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
				{
					chat.Add("How did a chicken break into my ship?", 1.0);
				}
			}
			return chat;
		}

		public static bool SwitchInfo;

		public static bool CrashedShip;

		public static bool WhatAreYouDoing;

		public static bool GiveUranium;

		public static bool Fight;

		public static bool AreYouHuman;

		public static bool HallOfHeroes;

		public static bool Lab;

		public static bool Planet;

		public static bool Wasteland;

		public static bool DataLogs;

		public static bool BaseChat;

		public static bool Give1;

		public static bool Give2;

		public static bool Give3;

		public static int ChatNumber;
	}
}
