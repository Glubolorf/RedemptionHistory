﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class CorpseWalkerPriest : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corpse-Walker Priest");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 52;
			base.npc.friendly = false;
			base.npc.damage = 17;
			base.npc.defense = 2;
			base.npc.lifeMax = 45;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = 28f;
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("CorpseWalkerPriestBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorpseWGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorpseWGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorpseWGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Main.rand.Next(2) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y + 30, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			if (this.specialAttack)
			{
				this.attackCounter++;
				if (this.attackCounter > 3)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 14)
				{
					this.attackFrame = 0;
				}
			}
			if (Main.rand.Next(150) == 0 && !this.specialAttack)
			{
				this.specialAttack = true;
			}
			if (!this.specialAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.specialAttack)
			{
				this.attackTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(-6f, 1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(6f, 1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
					}
				}
				if (this.attackTimer == 24)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(-6f, 0f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(6f, 0f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
					}
				}
				if (this.attackTimer == 27)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(-6f, -1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(6f, -1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
					}
				}
				if (this.attackTimer >= 42)
				{
					this.specialAttack = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/CorpseWalkerPriestAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 14;
				int num2 = num * this.attackFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.DesertCave.Chance * 0.01f;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;
	}
}
