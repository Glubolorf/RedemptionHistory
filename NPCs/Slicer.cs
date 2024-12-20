using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	[AutoloadHead]
	public class Slicer : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Slicer";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Slicer";
			return base.mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Slicer");
			Main.npcFrameCount[base.npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 100;
			NPCID.Sets.AttackType[base.npc.type] = 3;
			NPCID.Sets.AttackTime[base.npc.type] = 30;
			NPCID.Sets.AttackAverageChance[base.npc.type] = 30;
			NPCID.Sets.HatOffsetY[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 26;
			base.npc.height = 48;
			base.npc.aiStyle = 7;
			base.npc.damage = 140;
			base.npc.defense = 25;
			base.npc.lifeMax = 500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.3f;
			this.animationType = 22;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SlicerGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SlicerGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SlicerGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return RedeWorld.downedDarkSlime;
		}

		public override string TownNPCName()
		{
			WorldGen.genRand.Next(1);
			return "Zephos";
		}

		public override void FindFrame(int frameHeight)
		{
		}

		public override string GetChat()
		{
			if (NPC.downedMoonlord && Main.hardMode && Main.rand.Next(12) == 0)
			{
				return "I wonder how the demi-dude is doing... Wait, you don't know who that is.";
			}
			int num = NPC.FindFirstNPC(20);
			if (num >= 0 && Main.rand.Next(8) == 0)
			{
				return "Doesn't " + Main.npc[num].GivenName + " know how to put clothes on? Whatever, I like it!";
			}
			switch (Main.rand.Next(6))
			{
			case 0:
				return "How's it goin' bro!";
			case 1:
				return "Woah! That slime was my kill! I was just... attacking its insides...";
			case 2:
				return "Don't know how I managed to get trapped in that slime.";
			case 3:
				return "My favourite colour is orange! Donno why I'm tellin' ya though...";
			case 4:
				return "I don't know what the deal with cats are. Dogs are definitely better!";
			default:
				return "'Ey bro!";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Sharpen II";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
				return;
			}
			Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
			Main.LocalPlayer.AddBuff(base.mod.BuffType("Sharpen2Buff"), 36000, true);
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("SwordSlicer"), false);
			nextSlot++;
			if (NPC.downedMoonlord)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("Godslayer"), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("Zweihander"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(426, false);
			nextSlot++;
			if (NPC.downedFrost)
			{
				shop.item[nextSlot].SetDefaults(676, false);
				nextSlot++;
			}
			if (NPC.downedChristmasTree)
			{
				shop.item[nextSlot].SetDefaults(1928, false);
				nextSlot++;
			}
			if (NPC.downedPlantBoss)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("LightSteel"), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("MagicMetalPolish"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(901, false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(886, false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(936, false);
			nextSlot++;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 120;
			knockback = 5f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 10;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			scale = 1f;
			item = Main.itemTexture[base.mod.ItemType<MythrilsBane>()];
			itemSize = 58;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 58;
			itemHeight = 58;
		}
	}
}
