﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class KingChicken : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Mighty King Chicken");
			Main.npcFrameCount[base.npc.type] = 7;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 24;
			base.npc.defense = 0;
			base.npc.lifeMax = 5;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 46;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossKingChicken");
			this.bossBag = base.mod.ItemType("KingChickenBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/KingChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/KingChickenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
			}
		}

		public override void AI()
		{
			if (this.peckPeck)
			{
				this.peckCounter++;
				if (this.peckCounter > 5)
				{
					this.peckFrame++;
					this.peckCounter = 0;
				}
				if (this.peckFrame >= 6)
				{
					this.peckFrame = 0;
				}
			}
			if (Main.rand.Next(500) == 0 && !this.peckPeck)
			{
				this.peckPeck = true;
			}
			if (this.peckPeck)
			{
				base.npc.velocity.X = 0f;
				this.peckTimer++;
				if (this.peckTimer >= 30)
				{
					this.peckPeck = false;
					this.peckCounter = 0;
					this.peckFrame = 0;
					this.peckTimer = 0;
				}
			}
			if (Main.rand.Next(500) == 0 && !this.cluckCluck)
			{
				this.cluckCluck = true;
			}
			if (this.cluckCluck)
			{
				this.cluckTimer++;
				if (this.cluckTimer == 1)
				{
					int num = Main.rand.Next(3);
					if (num == 0 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck1").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
					}
					if (num == 1 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck2").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
					}
					if (num == 2 && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck3").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
					}
				}
				if (this.cluckTimer >= 2)
				{
					this.cluckCluck = false;
					this.cluckTimer = 0;
				}
			}
			this.timer++;
			if (this.timer == 40)
			{
				string text = "*cluck cluck* You fool... You dare think you can kill me? *cluck cluck*";
				Color rarityOrange = Colors.RarityOrange;
				byte r = rarityOrange.R;
				Color rarityOrange2 = Colors.RarityOrange;
				byte g = rarityOrange2.G;
				Color rarityOrange3 = Colors.RarityOrange;
				Main.NewText(text, r, g, rarityOrange3.B, false);
			}
			if (this.timer == 200)
			{
				string text2 = "*cluck cluck* I am the Mighty King Chicken, and I have come to annihilate you! *cluck cluck*";
				Color rarityOrange4 = Colors.RarityOrange;
				byte r2 = rarityOrange4.R;
				Color rarityOrange5 = Colors.RarityOrange;
				byte g2 = rarityOrange5.G;
				Color rarityOrange6 = Colors.RarityOrange;
				Main.NewText(text2, r2, g2, rarityOrange6.B, false);
			}
			if (this.timer == 600)
			{
				string text3 = "*cluck cluck* So face me, mortal! And accept your fate! *cluck cluck*";
				Color rarityOrange7 = Colors.RarityOrange;
				byte r3 = rarityOrange7.R;
				Color rarityOrange8 = Colors.RarityOrange;
				byte g3 = rarityOrange8.G;
				Color rarityOrange9 = Colors.RarityOrange;
				Main.NewText(text3, r3, g3, rarityOrange9.B, false);
			}
			if (this.timer == 900)
			{
				string text4 = "*cluck cluck* Stubborn fool... Are you letting me make the first move? *cluck cluck*";
				Color rarityOrange10 = Colors.RarityOrange;
				byte r4 = rarityOrange10.R;
				Color rarityOrange11 = Colors.RarityOrange;
				byte g4 = rarityOrange11.G;
				Color rarityOrange12 = Colors.RarityOrange;
				Main.NewText(text4, r4, g4, rarityOrange12.B, false);
			}
			if (this.timer == 1400)
			{
				string text5 = "*cluck cluck* So be it! A GOD DOES NOT FEAR DEATH! *cluck cluck*";
				Color rarityOrange13 = Colors.RarityOrange;
				byte r5 = rarityOrange13.R;
				Color rarityOrange14 = Colors.RarityOrange;
				byte g5 = rarityOrange14.G;
				Color rarityOrange15 = Colors.RarityOrange;
				Main.NewText(text5, r5, g5, rarityOrange15.B, false);
				base.npc.damage = 1;
			}
			if (this.timer == 1500)
			{
				string text6 = "*pecks gracefully*";
				Color rarityOrange16 = Colors.RarityOrange;
				byte r6 = rarityOrange16.R;
				Color rarityOrange17 = Colors.RarityOrange;
				byte g6 = rarityOrange17.G;
				Color rarityOrange18 = Colors.RarityOrange;
				Main.NewText(text6, r6, g6, rarityOrange18.B, false);
				this.peckPeck = true;
			}
			if (this.timer == 2800)
			{
				string text7 = "*cluck cluck* You still stand before me? *cluck cluck*";
				Color rarityOrange19 = Colors.RarityOrange;
				byte r7 = rarityOrange19.R;
				Color rarityOrange20 = Colors.RarityOrange;
				byte g7 = rarityOrange20.G;
				Color rarityOrange21 = Colors.RarityOrange;
				Main.NewText(text7, r7, g7, rarityOrange21.B, false);
			}
			if (this.timer == 4000)
			{
				string text8 = "*clucks impatiently*";
				Color rarityOrange22 = Colors.RarityOrange;
				byte r8 = rarityOrange22.R;
				Color rarityOrange23 = Colors.RarityOrange;
				byte g8 = rarityOrange23.G;
				Color rarityOrange24 = Colors.RarityOrange;
				Main.NewText(text8, r8, g8, rarityOrange24.B, false);
			}
			if (this.timer == 8000)
			{
				string text9 = "*cluck cluck* You bore me, mortal! I shall be gone, but I will return!   ...   Maybe.";
				Color rarityOrange25 = Colors.RarityOrange;
				byte r9 = rarityOrange25.R;
				Color rarityOrange26 = Colors.RarityOrange;
				byte g9 = rarityOrange26.G;
				Color rarityOrange27 = Colors.RarityOrange;
				Main.NewText(text9, r9, g9, rarityOrange27.B, false);
			}
			if (this.timer == 8100)
			{
				string text10 = "*clucks a goodbye*";
				Color rarityOrange28 = Colors.RarityOrange;
				byte r10 = rarityOrange28.R;
				Color rarityOrange29 = Colors.RarityOrange;
				byte g10 = rarityOrange29.G;
				Color rarityOrange30 = Colors.RarityOrange;
				Main.NewText(text10, r10, g10, rarityOrange30.B, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 264, 1, false, 0, false, false);
				base.npc.active = false;
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			string text = "It's a freakin' chicken, what did you expect...";
			Color rarityGreen = Colors.RarityGreen;
			byte r = rarityGreen.R;
			Color rarityGreen2 = Colors.RarityGreen;
			byte g = rarityGreen2.G;
			Color rarityGreen3 = Colors.RarityGreen;
			Main.NewText(text, r, g, rarityGreen3.B, false);
			potionType = base.mod.ItemType("ChickenEgg");
			RedeWorld.downedKingChicken = true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingChickenTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingChickenMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CrownOfTheKing"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("EggStaff"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Grain"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 264, 1, false, 0, false, false);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/KingChickenPeck");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.peckPeck)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.peckPeck)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 6;
				int num2 = num * this.peckFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.timer >= 1450;
		}

		public int timer;

		private bool peckPeck;

		private int peckFrame;

		private int peckCounter;

		private int peckTimer;

		private int cluckTimer;

		private bool cluckCluck;
	}
}
