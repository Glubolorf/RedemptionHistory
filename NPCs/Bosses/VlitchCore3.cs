﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class VlitchCore3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Core");
		}

		public override void SetDefaults()
		{
			base.npc.width = 122;
			base.npc.height = 138;
			base.npc.friendly = false;
			base.npc.damage = 80;
			base.npc.defense = 0;
			base.npc.lifeMax = 18000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 5;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.boss = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 82;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/VlitchCoreGore1"), 1f);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (Main.rand.Next(6) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VlitchFlame>(), 0f, 0f, 0, default(Color), 1f);
			}
			if (base.npc.life >= 6000)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 30f)
				{
					int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, -8f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
				}
				if (base.npc.ai[1] == 60f)
				{
					int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, -6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p2].netUpdate = true;
				}
				if (base.npc.ai[1] == 90f)
				{
					int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(8f, 0f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p3].netUpdate = true;
				}
				if (base.npc.ai[1] == 120f)
				{
					int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, 6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p4].netUpdate = true;
				}
				if (base.npc.ai[1] == 150f)
				{
					int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, 8f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p5].netUpdate = true;
				}
				if (base.npc.ai[1] == 180f)
				{
					int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, 6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p6].netUpdate = true;
				}
				if (base.npc.ai[1] == 210f)
				{
					int p7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-8f, 0f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[1] >= 240f)
				{
					int p8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, -6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
					base.npc.ai[1] = 0f;
					return;
				}
			}
			else
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 10f)
				{
					int p9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, -8f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
				}
				if (base.npc.ai[2] == 20f)
				{
					int p10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, -6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p10].netUpdate = true;
				}
				if (base.npc.ai[2] == 30f)
				{
					int p11 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(8f, 0f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p11].netUpdate = true;
				}
				if (base.npc.ai[2] == 40f)
				{
					int p12 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, 6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p12].netUpdate = true;
				}
				if (base.npc.ai[2] == 50f)
				{
					int p13 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, 8f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p13].netUpdate = true;
				}
				if (base.npc.ai[2] == 60f)
				{
					int p14 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, 6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p14].netUpdate = true;
				}
				if (base.npc.ai[2] == 70f)
				{
					int p15 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-8f, 0f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p15].netUpdate = true;
				}
				if (base.npc.ai[2] >= 80f)
				{
					int p16 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, -6f), ModContent.ProjectileType<VlitchLaserPro>(), 60, 3f, 255, 0f, 0f);
					Main.projectile[p16].netUpdate = true;
					base.npc.ai[2] = 0f;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VlitchCore_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private Player player;
	}
}
