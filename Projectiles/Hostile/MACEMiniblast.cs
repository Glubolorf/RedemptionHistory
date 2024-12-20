using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	public class MACEMiniblast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flaming Blast");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 160;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 2;
		}

		public override void AI()
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 57, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			base.projectile.Kill();
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14.WithVolume(0.2f), base.projectile.position);
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}
	}
}
