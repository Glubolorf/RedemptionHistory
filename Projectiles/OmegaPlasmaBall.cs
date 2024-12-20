using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class OmegaPlasmaBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Plasma Ball");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 48;
			base.projectile.height = 48;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			int DustID2 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 0, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 60f)
			{
				Main.PlaySound(SoundID.Item33, (int)base.projectile.position.X, (int)base.projectile.position.Y);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(0f, -8f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(0f, 8f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(-8f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(6f, 6f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(6f, -6f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(-6f, 6f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(-6f, -6f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
				base.projectile.localAI[0] = 0f;
			}
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 50f;
			bool target = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.player[i].active)
				{
					Vector2 newMove = Main.player[i].Center - base.projectile.Center;
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

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 6f)
			{
				vector *= 8f / magnitude;
			}
		}
	}
}
