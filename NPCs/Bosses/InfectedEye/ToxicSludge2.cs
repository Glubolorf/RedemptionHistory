using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.InfectedEye
{
	public class ToxicSludge2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Toxic Sludge");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
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
				if (num >= 3)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.8f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.2f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(-base.projectile.velocity.X + (float)Main.rand.Next(-2, 2), -base.projectile.velocity.Y + (float)Main.rand.Next(-2, 2)), ModContent.ProjectileType<ToxicSludge3>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(-base.projectile.velocity.X + (float)Main.rand.Next(-2, 2), -base.projectile.velocity.Y + (float)Main.rand.Next(-2, 2)), ModContent.ProjectileType<ToxicSludge3>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			base.projectile.Kill();
			return false;
		}
	}
}
