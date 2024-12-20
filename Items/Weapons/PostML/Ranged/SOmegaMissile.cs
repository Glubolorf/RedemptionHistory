﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class SOmegaMissile : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/OmegaOblit/OmegaMissile";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Missile");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			int DustID2 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 0, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			Projectile projAim = Main.projectile[(int)base.projectile.ai[0]];
			if (!projAim.active)
			{
				Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SOOMissileBlast>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				base.projectile.Kill();
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			else
			{
				base.projectile.localAI[0] += 1f;
			}
			if (base.projectile.localAI[0] > 20f)
			{
				Vector2 move = Vector2.Zero;
				float distance = 5000f;
				bool target = false;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].identity == projAim.identity)
					{
						Vector2 newMove = Main.projectile[i].Center - base.projectile.Center;
						float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
						if (distanceTo < distance)
						{
							move = newMove;
							distance = distanceTo;
							target = true;
						}
					}
				}
				if (target)
				{
					this.AdjustMagnitude(ref move);
					base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
					this.AdjustMagnitude(ref base.projectile.velocity);
				}
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && proj.identity == projAim.identity)
				{
					if (base.projectile.ai[1] == 1f)
					{
						proj.localAI[0] += 1f;
						Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SOOMissileBlast>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						base.projectile.Kill();
					}
					else
					{
						Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SOOMissileBlast>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						base.projectile.Kill();
						proj.Kill();
					}
				}
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 20f)
			{
				vector *= 20f / magnitude;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SOOMissileBlast>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			base.projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			Projectile projAim = Main.projectile[(int)base.projectile.ai[0]];
			if (base.projectile.ai[1] == 0f)
			{
				for (int p = 0; p < 1000; p++)
				{
					this.clearCheck = Main.projectile[p];
					if (this.clearCheck.identity == projAim.identity && this.clearCheck.type != base.projectile.type && this.clearCheck.active)
					{
						this.clearCheck.Kill();
					}
				}
			}
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
			for (int j = 0; j < 10; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex2].velocity *= 1.6f;
			}
			for (int k = 0; k < 5; k++)
			{
				int dustIndex3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex3].velocity *= 1.4f;
			}
		}

		public int hitCounter;

		private Projectile clearCheck;
	}
}
