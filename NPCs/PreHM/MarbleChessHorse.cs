﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Friendly;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PreHM
{
	public class MarbleChessHorse : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Marble Chess Horse");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 52;
			base.npc.friendly = false;
			base.npc.damage = 30;
			base.npc.defense = 25;
			base.npc.lifeMax = 120;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.value = 200f;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 1;
			this.aiType = 16;
			this.animationType = 302;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 236, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 24, (int)base.npc.position.Y + 18, ModContent.NPCType<LostSoul2>(), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			if (this.weakened)
			{
				this.weakenCounter++;
				if (this.weakenCounter > 10)
				{
					this.weakenFrame++;
					this.weakenCounter = 0;
				}
				if (this.weakenFrame >= 2)
				{
					this.weakenFrame = 0;
				}
			}
			if (Main.expertMode && base.npc.life <= 90)
			{
				this.weakened = true;
			}
			if (!Main.expertMode && base.npc.life <= 45)
			{
				this.weakened = true;
			}
			if (this.weakened)
			{
				base.npc.defense = 0;
				if (Main.rand.Next(25) == 0)
				{
					int p = Projectile.NewProjectile(base.npc.position.X + 24f, base.npc.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), ModContent.ProjectileType<FireSparkle>(), 5, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 367 && RedeWorld.downedKeeper) ? 0.2f : 0f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D weakenedAni = base.mod.GetTexture("NPCs/PreHM/MarbleChessHorseBroken");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.weakened)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.weakened)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = weakenedAni.Height / 2;
				int y6 = num214 * this.weakenFrame;
				Main.spriteBatch.Draw(weakenedAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, weakenedAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)weakenedAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private int weakenFrame;

		private bool weakened;

		private int weakenCounter;
	}
}
