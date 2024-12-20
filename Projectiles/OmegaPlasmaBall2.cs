using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class OmegaPlasmaBall2 : ModProjectile
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
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 100;
			base.projectile.alpha = 100;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			int num = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num].noGravity = true;
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 60f)
			{
				Main.PlaySound(SoundID.Item33, (int)base.projectile.position.X, (int)base.projectile.position.Y);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(0f, -8f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(0f, 8f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(-8f, 0f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(6f, 6f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(6f, -6f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(-6f, 6f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 25f, base.projectile.position.Y + 24f), new Vector2(-6f, -6f), base.mod.ProjectileType("OmegaBlast2"), 150, 3f, base.projectile.owner, 0f, 1f);
				base.projectile.localAI[0] = 0f;
			}
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 vector = Vector2.Zero;
			float num2 = 50f;
			bool flag = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5)
				{
					Vector2 vector2 = Main.npc[i].Center - base.projectile.Center;
					float num3 = (float)Math.Sqrt((double)(vector2.X * vector2.X + vector2.Y * vector2.Y));
					if (num3 < num2)
					{
						vector = vector2;
						num2 = num3;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.AdjustMagnitude(ref vector);
				base.projectile.velocity = (10f * base.projectile.velocity + vector) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 6f)
			{
				vector *= 8f / num;
			}
		}
	}
}
