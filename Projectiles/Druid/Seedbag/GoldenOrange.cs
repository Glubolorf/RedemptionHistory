using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class GoldenOrange : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Orange");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(261);
			this.aiType = 261;
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.magic = false;
			base.projectile.penetrate = 6;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					Main.PlaySound(SoundID.Item2.WithVolume(0.3f), base.projectile.position);
					this.clearCheck.AddBuff(26, 300, false);
					for (int i = 0; i < 10; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 160, 0f, 0f, 100, default(Color), 1.8f);
						Main.dust[dustIndex].velocity *= 1.3f;
					}
					base.projectile.Kill();
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 160, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		private Player clearCheck;
	}
}
