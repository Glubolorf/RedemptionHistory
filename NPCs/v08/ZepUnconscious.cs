using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.v08
{
	public class ZepUnconscious : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Zephos");
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
				npc.frame.Y = npc.frame.Y + 44;
				if (base.npc.frame.Y > 132)
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
			if (++RedeWorld.zephosDownedTimer >= Main.rand.Next(43200, 151200))
			{
				RedeWorld.zephosDownedTimer = 0;
				base.npc.Transform(base.mod.NPCType("Zephos1"));
				Main.NewText("Zephos the Wayfarer has woken up!", new Color(45, 114, 233), false);
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
				int potion = player.FindItem(base.mod.ItemType("RevivalPotion"));
				if (potion >= 0)
				{
					player.inventory[potion].stack--;
					if (player.inventory[potion].stack <= 0)
					{
						player.inventory[potion] = new Item();
					}
					Main.PlaySound(SoundID.Item3, (int)base.npc.position.X, (int)base.npc.position.Y);
					RedeWorld.zephosDownedTimer += 151200;
					Main.npcChatText = ZepUnconscious.PotionChat();
					return;
				}
				Main.npcChatCornerItem = base.mod.ItemType("RevivalPotion");
				Main.npcChatText = ZepUnconscious.NoPotionChat();
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
			weightedRandom.Add("*Yawn* Why'd ya wake me up? I was havin' a dream about... doesn't matter.", 1.0);
			weightedRandom.Add("Alright, I'm up. Did I fall asleep or somethin'?", 1.0);
			weightedRandom.Add("Yuck, what did you make me drink? Tastes bitter... like strawberries...", 1.0);
			return weightedRandom;
		}

		public override string GetChat()
		{
			if (RedeWorld.zephosDownedTimer < 3600)
			{
				return "Zephos has been unconsious for less than a minute.";
			}
			return "Zephos has been unconsious for " + RedeWorld.zephosDownedTimer / 3600 + " minute(s).";
		}
	}
}
