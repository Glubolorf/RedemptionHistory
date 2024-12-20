using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KS3_FlashGrenadeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Stun Grenade");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 24;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.timeLeft = 90;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			base.projectile.Kill();
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
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
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.3f;
		}

		public override void Kill(int timeLeft)
		{
			if (!Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithPitchVariance(0.1f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
			}
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 92, 0f, 0f, 100, default(Color), 4f);
				Main.dust[dustIndex].velocity *= 12f;
			}
			Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<FlashGrenadeBlast>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<FlashGrenadeBlast2>(), 0, 0f, Main.myPlayer, 0f, 0f);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.velocity *= 0.98f;
			return false;
		}
	}
}
