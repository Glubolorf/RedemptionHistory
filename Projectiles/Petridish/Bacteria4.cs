using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Petridish
{
	public class Bacteria4 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bacteria");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = 4;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 10)
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
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation -= 0.02f;
			if (base.projectile.localAI[0] > 80f)
			{
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 4, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 8f, (float)(-2 + Main.rand.Next(0, 5)), (float)(-2 + Main.rand.Next(0, 5)), ModContent.ProjectileType<Bacteria6>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 8f, (float)(-2 + Main.rand.Next(0, 5)), (float)(-2 + Main.rand.Next(0, 5)), ModContent.ProjectileType<Bacteria7>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
			target.AddBuff(ModContent.BuffType<BInfectionDebuff>(), 1000, false);
		}
	}
}
