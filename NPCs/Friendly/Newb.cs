using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Dusts;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Weapons.PreHM.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Friendly
{
	[AutoloadHead]
	public class Newb : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fool");
			Main.npcFrameCount[base.npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 80;
			NPCID.Sets.AttackType[base.npc.type] = 3;
			NPCID.Sets.AttackTime[base.npc.type] = 18;
			NPCID.Sets.AttackAverageChance[base.npc.type] = 30;
			NPCID.Sets.HatOffsetY[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 18;
			base.npc.height = 40;
			base.npc.aiStyle = 7;
			base.npc.damage = 10;
			base.npc.defense = 9999;
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
				for (int i = 0; i < 15; i++)
				{
					Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SightDust>(), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 4f);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return RedeWorld.foundNewb;
		}

		public override string TownNPCName()
		{
			return "Newb";
		}

		public override string GetChat()
		{
			if (!RedeWorld.downedNebuleus)
			{
				Player player = Main.player[Main.myPlayer];
				WeightedRandom<string> chat = new WeightedRandom<string>();
				if (BasePlayer.HasHelmet(player, ModContent.ItemType<KingSlayerMask>(), true))
				{
					chat.Add("Heheh! Hewwo mister slayer! Wait... who's that?", 1.0);
				}
				if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
				{
					chat.Add("IT'S A CHICKEN! Come on mister chicken, time for your walk!", 1.0);
				}
				if (BasePlayer.HasHelmet(player, ModContent.ItemType<ArmorHKHead>(), true) && BasePlayer.HasChestplate(player, ModContent.ItemType<ArmorHK>(), true) && BasePlayer.HasChestplate(player, ModContent.ItemType<ArmorHKLeggings>(), true))
				{
					chat.Add("Do I know you?", 1.0);
				}
				chat.Add("Who you? You Terrarian?", 1.0);
				chat.Add("Me find shiny stones!", 1.0);
				chat.Add("You look stupid! Haha!", 1.0);
				chat.Add("My dirt is 10% off!", 1.0);
				chat.Add("Heheheh!", 1.0);
				chat.Add("Hewwo! I am Newb!", 1.0);
				return chat;
			}
			switch (Main.rand.Next(4))
			{
			case 0:
				return "Soon... I can feel myself being restored...";
			case 1:
				return "My death, my sleep within the earth, it has undone myself. But as my time awake grows longer, my lost self returns.";
			case 2:
				return "It's all coming back to me... I saw what you did... I can comprehend more than just the dirt below my feet now... I have something to say to you, but I am still not ready.";
			default:
				return "... I saw what you did.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Lang.inter[28].Value;
			if (RedeWorld.downedNebuleus)
			{
				button2 = "...";
				return;
			}
			button2 = "Fool's Blessing";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
				return;
			}
			if (RedeWorld.downedNebuleus)
			{
				Main.npcChatText = Newb.SeriousChat();
				return;
			}
			Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
			Main.LocalPlayer.AddBuff(ModContent.BuffType<NoobsBlessingBuff>(), 36000, true);
		}

		public static string SeriousChat()
		{
			return "No blessings from me.";
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(2, false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(176, false);
			nextSlot++;
			if (NPC.downedBoss1)
			{
				shop.item[nextSlot].SetDefaults(181, false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(180, false);
				nextSlot++;
			}
			if (NPC.downedBoss2)
			{
				shop.item[nextSlot].SetDefaults(177, false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(179, false);
				nextSlot++;
			}
			if (NPC.downedBoss3)
			{
				shop.item[nextSlot].SetDefaults(178, false);
				nextSlot++;
			}
			if (Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(182, false);
				nextSlot++;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D seriousAni = base.mod.GetTexture("NPCs/Friendly/NewbSerious");
			if (!RedeWorld.downedNebuleus)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y - 4f);
				spriteBatch.Draw(texture, drawCenter - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 4f);
				Main.spriteBatch.Draw(seriousAni, drawCenter2 - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 18;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = (itemHeight = 34);
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			item = Main.itemTexture[ModContent.ItemType<AncientWoodSword>()];
		}
	}
}
