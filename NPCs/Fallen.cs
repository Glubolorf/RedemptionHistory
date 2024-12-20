using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Redemption.Items.Placeable;
using Redemption.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	[AutoloadHead]
	public class Fallen : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Fallen";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Fallen";
			return base.mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fallen");
			Main.npcFrameCount[base.npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 10;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 100;
			NPCID.Sets.AttackType[base.npc.type] = 3;
			NPCID.Sets.AttackTime[base.npc.type] = 40;
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
			base.npc.damage = 15;
			base.npc.defense = 5;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.5f;
			this.animationType = 22;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore8"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
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
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y + 20, base.mod.NPCType("AAAA"), 0, 0f, 0f, 0f, 0f, 255);
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return RedeWorld.downedTheKeeper;
		}

		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(3))
			{
			case 0:
				return "Happins";
			case 1:
				return "Tenvon";
			default:
				return "Okvot";
			}
		}

		public override void FindFrame(int frameHeight)
		{
		}

		public override string GetChat()
		{
			switch (Main.rand.Next(8))
			{
			case 0:
				return "Hey there.";
			case 1:
				return "Yes?";
			case 2:
				return "I don't really like the sunlight, because, I'm undead.";
			case 3:
				return "You seem pretty happy to get rid of your Ancient Gold Coins.";
			case 4:
				return "What are Lost Souls used for? No idea.";
			case 5:
				return "Y'know, planting saplings on Ancient Dirt will make Ancient Trees grow! It's like magic!";
			case 6:
				return "I always feel restless... Maybe it's just an Undead thing.";
			default:
				return "Greetings.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<OldGathicWaraxe>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(30);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<BronzeGreatsword>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(25);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<BronzeStaff>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(30);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<AncientDirt>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(2);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<AncientWood>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(3);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<AncientStone>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(4);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<MysteriousTabletCorrupt>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(20);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<MysteriousTabletCrimson>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(20);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType<SmallLostSoul>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(4);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			if (Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType<LostSoul>(), false);
				shop.item[nextSlot].shopCustomPrice = new int?(8);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 46;
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
			item = Main.itemTexture[base.mod.ItemType<OldGathicWaraxe>()];
			itemSize = 60;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 60;
			itemHeight = 60;
		}
	}
}
