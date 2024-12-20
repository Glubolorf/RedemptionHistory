using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable.Potions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Friendly
{
	public class Daerel2Unconscious : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Daerel");
			Main.npcFrameCount[base.npc.type] = 4;
			NPCID.Sets.TownCritter[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 56;
			base.npc.height = 48;
			base.npc.aiStyle = -1;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.dontTakeDamage = true;
			base.npc.npcSlots = 0f;
		}

		public override bool UsesPartyHat()
		{
			return false;
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 10.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 40;
				if (base.npc.frame.Y > 120)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.wet = false;
			base.npc.lavaWet = false;
			base.npc.honeyWet = false;
			base.npc.dontTakeDamage = true;
			base.npc.immune[255] = 30;
			base.npc.velocity.X = 0f;
			if (Main.netMode != 1)
			{
				base.npc.homeless = false;
				base.npc.homeTileX = -1;
				base.npc.homeTileY = -1;
				base.npc.netUpdate = true;
			}
			if (++RedeWorld.daerelDownedTimer >= Main.rand.Next(43200, 57600))
			{
				RedeWorld.daerelDownedTimer = 0;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				base.npc.Transform(ModContent.NPCType<Daerel2>());
				Main.NewText("Daerel the Wayfarer has woken up!", new Color(45, 114, 233), false);
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Use Revival Potion";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.player[Main.myPlayer];
			if (firstButton)
			{
				int potion = player.FindItem(ModContent.ItemType<RevivalPotion>());
				if (potion >= 0)
				{
					player.inventory[potion].stack--;
					if (player.inventory[potion].stack <= 0)
					{
						player.inventory[potion] = new Item();
					}
					Main.PlaySound(SoundID.Item3, (int)base.npc.position.X, (int)base.npc.position.Y);
					RedeWorld.daerelDownedTimer += 151200;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					Main.npcChatText = Daerel2Unconscious.PotionChat();
					return;
				}
				Main.npcChatCornerItem = ModContent.ItemType<RevivalPotion>();
				Main.npcChatText = Daerel2Unconscious.NoPotionChat();
			}
		}

		public static string NoPotionChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You aren't holding a Revival Potion.", 1.0);
			return weightedRandom;
		}

		public static string PotionChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Did I just get knocked out? Thanks for waking me.", 1.0);
			weightedRandom.Add("I'm up. Wide awake. What did you give me? Some sort of potion?", 1.0);
			weightedRandom.Add("Was I asleep or unconscious? Most likely unconscious since, well, I wouldn't just go to sleep on the floor.", 1.0);
			return weightedRandom;
		}

		public override string GetChat()
		{
			if (RedeWorld.daerelDownedTimer < 3600)
			{
				return "Daerel has been unconsious for less than a minute.";
			}
			return "Daerel has been unconsious for " + RedeWorld.daerelDownedTimer / 3600 + " minute(s).";
		}
	}
}
