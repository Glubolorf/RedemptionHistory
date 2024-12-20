using System;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Placeable;
using Redemption.Items.Placeable.LabDeco;
using Redemption.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.LabNPCs.New
{
	public class JanitorBotNPC : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/LabNPCs/New/JanitorBotCleaning";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Janitor");
			Main.npcFrameCount[base.npc.type] = 5;
			NPCID.Sets.TownCritter[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.npc.width = 34;
			base.npc.height = 44;
			base.npc.friendly = true;
			base.npc.townNPC = true;
			base.npc.dontTakeDamage = true;
			base.npc.noGravity = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 999;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.npcSlots = 0f;
		}

		public override bool UsesPartyHat()
		{
			return false;
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 20.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 46;
				if (base.npc.frame.Y > 138)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (NPC.CountNPCS(ModContent.NPCType<JanitorBotNPC>()) >= 2 && Main.rand.Next(2) == 0)
			{
				base.npc.active = false;
			}
			base.npc.wet = false;
			base.npc.lavaWet = false;
			base.npc.honeyWet = false;
			base.npc.dontTakeDamage = true;
			base.npc.immune[255] = 30;
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
			JanitorBotNPC.SwitchInfo = false;
			JanitorBotNPC.WhatsUp = false;
			JanitorBotNPC.TBots = false;
			JanitorBotNPC.Volt = false;
			JanitorBotNPC.MACE = false;
			JanitorBotNPC.EndLab = false;
			JanitorBotNPC.AboutGirus = false;
			JanitorBotNPC.Shop = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Dialogue";
			string WhatUpT = "What's up?";
			string TBotT = "Other T-Bots?";
			string VoltT = "Protector Volt?";
			string MACET = "MACE Project?";
			string EndT = "What's at the bottom of the lab?";
			string GirusT = "Girus?";
			string ShopT = "Shop";
			button = SwitchInfoT;
			if (JanitorBotNPC.ChatNumber == 0)
			{
				button2 = WhatUpT;
				JanitorBotNPC.WhatsUp = true;
				return;
			}
			if (JanitorBotNPC.ChatNumber == 1)
			{
				button2 = TBotT;
				JanitorBotNPC.TBots = true;
				return;
			}
			if (JanitorBotNPC.ChatNumber == 2 && RedeWorld.downedVolt)
			{
				button2 = VoltT;
				JanitorBotNPC.Volt = true;
				return;
			}
			if (JanitorBotNPC.ChatNumber == 2 && !RedeWorld.downedVolt)
			{
				button2 = EndT;
				JanitorBotNPC.EndLab = true;
				return;
			}
			if (JanitorBotNPC.ChatNumber == 3)
			{
				button2 = GirusT;
				JanitorBotNPC.AboutGirus = true;
				return;
			}
			if (JanitorBotNPC.ChatNumber == 4)
			{
				button2 = ShopT;
				JanitorBotNPC.Shop = true;
				return;
			}
			if (JanitorBotNPC.ChatNumber == 5 && RedeWorld.downedMACE)
			{
				button2 = MACET;
				JanitorBotNPC.MACE = true;
				return;
			}
			JanitorBotNPC.ChatNumber = 0;
			button2 = WhatUpT;
			JanitorBotNPC.WhatsUp = true;
		}

		public void ResetBools()
		{
			JanitorBotNPC.WhatsUp = false;
			JanitorBotNPC.TBots = false;
			JanitorBotNPC.Volt = false;
			JanitorBotNPC.EndLab = false;
			JanitorBotNPC.AboutGirus = false;
			JanitorBotNPC.MACE = false;
			JanitorBotNPC.Shop = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				this.ResetBools();
				JanitorBotNPC.ChatNumber++;
				if (JanitorBotNPC.ChatNumber > 6)
				{
					JanitorBotNPC.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (JanitorBotNPC.ChatNumber == 4)
				{
					shop = true;
					return;
				}
				Main.npcChatText = JanitorBotNPC.ChitChat();
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			Player player = Main.player[Main.myPlayer];
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabPlating>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabPlatingWall>(), false);
			nextSlot++;
			if (RedeWorld.downedVolt)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<XenoTank>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabForge>(), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Vent3>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Vent2>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Vent1>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SignElectric>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SignDeath>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SignBoi>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SignRadioactive>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabWorkbench>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabTestTubes>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabTable>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabFan>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabDoor3>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabDoor1>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabChest>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabChair>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabBookshelf>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabBed>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Intercom>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Computer>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabReceptionCouch>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabCeilingMonitor>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabReceptionDesk>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabCeilingLamp>(), false);
			nextSlot++;
			if (player.IsTBotHead())
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<OperatorHead>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<VoltHead>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<JanitorOutfit>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<JanitorPants>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<AndroidArmour>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<AndroidPants>(), false);
				nextSlot++;
			}
			if (BasePlayer.HasChestplate(player, ModContent.ItemType<JanitorOutfit>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<JanitorPants>(), true))
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<NoveltyMop>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedVolt)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<BotHanger>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<BotHangerOccupied>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedBlisterface)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<NanoAxe>(), false);
				nextSlot++;
			}
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (JanitorBotNPC.WhatsUp)
			{
				chat.Add("You've probably seen the personnel who are basically walking corpses, ye? Our security usually deals with them, but it's not enough. They crawl out of overrun rooms like goddamn cockroaches! And I am the one who has to clean all the mess left by them and security.", 1.0);
			}
			else if (JanitorBotNPC.TBots)
			{
				chat.Add("The other bots have learned to fear me. Good for me, I can actually focus on my job. Except now there is a certain someone bugging me when I'm trying to do my job. Hmm, I wonder who it could be... Now bugger off!", 1.0);
			}
			else if (JanitorBotNPC.Volt)
			{
				chat.Add("The big bot at Sector Omicron is a cool guy, if you ask me. Probably the only bot I don't feel annoyed by. Now if you excuse me, you brought in some of that slime with ya and I have to clean that up!", 1.0);
			}
			else if (JanitorBotNPC.EndLab)
			{
				chat.Add("Don't you dare go into Sector Zero, that place is absolutely filled with the impossible-to-remove black slime! And I don't want ye to bring that stuff here for me to try to pry off the steel plating of the lab! Takes ages.", 1.0);
			}
			else if (JanitorBotNPC.AboutGirus)
			{
				chat.Add("Respect Girus and she will learn to respect you. Quite sure I'll get a change of a job after 2 centuries of cleaning the lab after the... Incident. Don't ask me 'bout it, I was activated AFTER it happened.", 1.0);
			}
			else
			{
				if (!JanitorBotNPC.MACE)
				{
					return "...";
				}
				chat.Add("There's this one copper-haired bot that I can recall, who works as the crane operator in the Mech Storage room. I don't know that much about her, but her design is a little peculiar. Not that she's more feminine, but the hair... Why tho'?", 1.0);
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
			if (BasePlayer.HasChestplate(player, ModContent.ItemType<JanitorOutfit>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<JanitorPants>(), true))
			{
				chat.Add("So you've been assigned to janitor duty, eh? Here's a task, go to the reactors and remove that dense lava stuff from under the reactor. My Nano-chisel won't even leave a mark on it!", 1.0);
				chat.Add("About time she got me an assistant to help clean this mess of a place. Remember to check under the tables for dust.", 1.0);
			}
			else
			{
				chat.Add("Bugger off, you're bringing a whole bunch of dust in here!", 1.0);
				chat.Add("Oh it's you again. You better not interrupt my cleaning.", 1.0);
				chat.Add("Make it quick, I've got to resume mopping the floor.", 1.0);
			}
			return chat;
		}

		public static bool SwitchInfo;

		public static bool WhatsUp;

		public static bool TBots;

		public static bool Volt;

		public static bool MACE;

		public static bool EndLab;

		public static bool AboutGirus;

		public static bool Shop;

		public static int ChatNumber;
	}
}
