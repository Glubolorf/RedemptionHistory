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
	public class Squire : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Squire";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Squire";
			return base.mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Squire");
			Main.npcFrameCount[base.npc.type] = 26;
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
			base.npc.width = 40;
			base.npc.height = 54;
			base.npc.aiStyle = 7;
			base.npc.damage = 10;
			base.npc.defense = 15;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.4f;
			this.animationType = 22;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SquireGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SquireGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SquireGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SquireGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SquireGore5"), 1f);
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
			return true;
		}

		public override string TownNPCName()
		{
			WorldGen.genRand.Next(1);
			return "Ragnos";
		}

		public override void FindFrame(int frameHeight)
		{
		}

		public override string GetChat()
		{
			if (NPC.downedBoss1 && !NPC.downedBoss2 && !Main.hardMode && Main.rand.Next(15) == 0)
			{
				return "I can't believe you killed the Eye of Cthulhu, I guess I could've helped a little more...";
			}
			if (NPC.downedBoss2 && NPC.downedBoss1 && Main.rand.Next(15) == 0)
			{
				return "You are doing great, you killing bosses and stuff.";
			}
			if (NPC.downedBoss3 && !Main.hardMode && Main.rand.Next(15) == 0)
			{
				return "You defeating Skeletron was good, now I can explore the dungeon!";
			}
			if (Main.hardMode && !NPC.downedMechBossAny && Main.rand.Next(15) == 0)
			{
				return "The Spirits of Light and Dark has been released... Things are gonna get a lot harder.";
			}
			if (Main.hardMode && NPC.downedMechBossAny && !NPC.downedPlantBoss && Main.rand.Next(15) == 0)
			{
				return "Wow, you really are something... Fighting the mechanical guys.";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.downedPlantBoss && Main.rand.Next(15) == 0)
			{
				return "You've defeated all the mechanical bosses, but I feel uneasy about something...";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && !NPC.downedAncientCultist && Main.rand.Next(15) == 0)
			{
				return "More and more bosses are being defeated... But I still feel uneasy...";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && !NPC.downedAncientCultist && Main.rand.Next(15) == 0)
			{
				return "There are strange people at the dungeon... I... Don't want you to fight them, I feel the end is coming.";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist && !NPC.downedTowers && Main.rand.Next(1) == 0)
			{
				return "I feel the end is coming...";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist && NPC.downedTowers && !NPC.downedMoonlord && Main.rand.Next(1) == 0)
			{
				return "Impending Doom approaches...";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist && NPC.downedTowers && NPC.downedMoonlord && Main.rand.Next(15) == 0)
			{
				return "You defeated the Moonlord, wow, good job, I totally could've done that...";
			}
			int num = NPC.FindFirstNPC(22);
			if (num >= 0 && Main.rand.Next(15) == 0)
			{
				return "If you want to know what materials can be crafted into, just ask " + Main.npc[num].GivenName + ".";
			}
			int num2 = NPC.FindFirstNPC(17);
			if (num2 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num2].GivenName + " is a pretty nice guy, but I feel like he's ripping me off...";
			}
			int num3 = NPC.FindFirstNPC(18);
			if (num3 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num3].GivenName + " seems helpful... but let's be honest, you only need her to cheese a boss fight.";
			}
			int num4 = NPC.FindFirstNPC(20);
			if (num4 >= 0 && Main.rand.Next(15) == 0)
			{
				return "This land is a nice place to be in, so I guess that's why " + Main.npc[num4].GivenName + " seems pretty determined to stop the evil from spreading.";
			}
			int num5 = NPC.FindFirstNPC(19);
			if (num5 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num5].GivenName + "'s weaponry is very strange, they are like bows, but shoot tiny balls of metal...";
			}
			int num6 = NPC.FindFirstNPC(353);
			if (num6 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num6].GivenName + " was stuck in a spider's nest! I hate spiders! A colossal Dragon is fine, but SPIDERS!";
			}
			int num7 = NPC.FindFirstNPC(369);
			if (num7 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num7].GivenName + " is pretty bossy, we did 'Rock, Paper, Scissors', and when I won, he smacked me with a salmon...";
			}
			int num8 = NPC.FindFirstNPC(107);
			if (num8 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num8].GivenName + " sells boots... that make you fly! Mind... Blown...";
			}
			int num9 = NPC.FindFirstNPC(54);
			if (num9 >= 0 && Main.rand.Next(15) == 0)
			{
				return "I think " + Main.npc[num9].GivenName + " is a creepy guy, he keeps on saying that he remembers throwing a girl into a dungeon...";
			}
			int num10 = NPC.FindFirstNPC(208);
			if (num10 >= 0 && Main.rand.Next(15) == 0)
			{
				return "Is it just me, or does " + Main.npc[num10].GivenName + " remind you of technicoloured ponies from another universe...";
			}
			int num11 = NPC.FindFirstNPC(108);
			if (num11 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num11].GivenName + " is a wizard... He is now my favourite NPC.";
			}
			int num12 = NPC.FindFirstNPC(209);
			if (num12 >= 0 && Main.rand.Next(15) == 0)
			{
				return Main.npc[num12].GivenName + "'s equipment is witch craft...";
			}
			switch (Main.rand.Next(5))
			{
			case 0:
				return "Hey, I have some pretty cool things, you can have them if you got the money.";
			case 1:
				return "Have you seen a robot-lookin' person anywhere? I want revenge. He's got a red visor and wears black armour... No? Okay.";
			case 2:
				return "So, how are you?";
			case 3:
				return "If you are a melee guy, like me, then I will happily sharpen your weapon for free.";
			default:
				return "I've been travelling this land for a while, but staying in a house is nice.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Sharpen";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
				return;
			}
			Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
			Main.LocalPlayer.AddBuff(159, 36000, true);
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("LivingWood"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("LivingLeaf"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("LeatherPouch"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("WoodenBuckler"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("AncientNovicesStaff"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("NoblesSwordIron"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("NoblesSwordLead"), false);
			nextSlot++;
			if (NPC.downedSlimeKing)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("NoblesSwordSilver"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("NoblesSwordTungsten"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("NoblesSwordGold"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("NoblesSwordPlatinum"), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("IronfurAmulet"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("Archcloth"), false);
			nextSlot++;
			if (NPC.downedBoss1)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ForestGolemPainting"), false);
				nextSlot++;
			}
			if (NPC.downedBoss3)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("GathicCryoCrystal"), false);
				nextSlot++;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 28;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			scale = 1f;
			item = Main.itemTexture[base.mod.ItemType<NoblesSwordPlatinum>()];
			itemSize = 36;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 36;
			itemHeight = 34;
		}
	}
}
