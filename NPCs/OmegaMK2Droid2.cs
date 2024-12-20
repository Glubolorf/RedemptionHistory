using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.NPCs.Bosses.OmegaOblit;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class OmegaMK2Droid2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Mk-2 Droid");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			this.aiType = 0;
			base.npc.lifeMax = 7000;
			base.npc.damage = 280;
			base.npc.defense = 40;
			base.npc.knockBackResist = 0f;
			base.npc.width = 50;
			base.npc.height = 62;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[ModContent.BuffType<UltraFlameDebuff>()] = true;
			base.npc.buffImmune[ModContent.BuffType<EnjoymentDebuff>()] = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
			this.animationType = -3;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				for (int j = 0; j < 25; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.8f;
				}
				for (int k = 0; k < 15; k++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void FindFrame(int frameHeight)
		{
			base.npc.spriteDirection = base.npc.direction;
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X + 12f, base.npc.position.Y + 30f), 4, 4, 235, 0f, 0f, 0, default(Color), 1f);
			}
			this.Move(new Vector2(-280f, 100f));
			this.timer++;
			if (Main.expertMode)
			{
				if (base.npc.life > 7000 && this.timer >= 50)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 30f), new Vector2(10f, -4f), ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					this.timer = 0;
				}
				if (base.npc.life <= 7000 && this.timer >= 30)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 30f), new Vector2(10f, -4f), ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
					Main.projectile[p2].netUpdate = true;
					this.timer = 0;
				}
			}
			else
			{
				if (base.npc.life > 3500 && this.timer >= 50)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 30f), new Vector2(10f, -4f), ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
					Main.projectile[p3].netUpdate = true;
					this.timer = 0;
				}
				if (base.npc.life <= 3500 && this.timer >= 30)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 30f), new Vector2(10f, -4f), ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
					Main.projectile[p4].netUpdate = true;
					this.timer = 0;
				}
			}
			this.aiCounter++;
			if (this.aiCounter == 1)
			{
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 20f;
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 10f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
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

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;

		public int timer;

		private int aiCounter;
	}
}
