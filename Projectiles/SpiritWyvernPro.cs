using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SpiritWyvernPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Wyvern");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 18;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 60;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (Main.player[base.projectile.owner].Center.X > base.projectile.Center.X)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.Y = projectile4.velocity.Y + 0f;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 2;
			if (base.projectile.localAI[0] == 25f)
			{
				Main.PlaySound(SoundID.NPCDeath8.WithVolume(0.2f), base.projectile.position);
				if (base.projectile.spriteDirection == 1)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 36f), new Vector2(-6f, 0f), base.mod.ProjectileType("SpiritFlame"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				}
				else
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 60f, base.projectile.position.Y + 36f), new Vector2(6f, 0f), base.mod.ProjectileType("SpiritFlame"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				}
			}
			if (base.projectile.localAI[0] == 50f)
			{
				Main.PlaySound(SoundID.NPCDeath8.WithVolume(0.2f), base.projectile.position);
				if (base.projectile.spriteDirection == 1)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 36f), new Vector2(-6f, 0f), base.mod.ProjectileType("SpiritFlame"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				}
				else
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 60f, base.projectile.position.Y + 36f), new Vector2(6f, 0f), base.mod.ProjectileType("SpiritFlame"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				}
			}
			if (base.projectile.localAI[0] == 75f)
			{
				Main.PlaySound(SoundID.NPCDeath8.WithVolume(0.2f), base.projectile.position);
				if (base.projectile.spriteDirection == 1)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 36f), new Vector2(-6f, 0f), base.mod.ProjectileType("SpiritFlame"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				}
				else
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 60f, base.projectile.position.Y + 36f), new Vector2(6f, 0f), base.mod.ProjectileType("SpiritFlame"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				}
			}
			if (base.projectile.localAI[0] > 80f)
			{
				Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.4f), base.projectile.position);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				base.projectile.Kill();
			}
		}
	}
}
