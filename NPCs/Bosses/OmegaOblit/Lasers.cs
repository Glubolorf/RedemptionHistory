using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	public class Lasers : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("jo mama");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 2;
			base.projectile.height = 2;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 48;
		}

		public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 100.0);
				this.vector.Y = (float)(Math.Cos(angle) * 100.0);
				Dust dust2 = Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 2, 2, 235, 0f, 0f, 100, default(Color), 2f)];
				dust2.noGravity = true;
				dust2.velocity = -base.projectile.DirectionTo(dust2.position) * 10f;
			}
		}

		private Vector2 vector;
	}
}
