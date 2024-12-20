using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class SongDust : ModProjectile
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
			base.DisplayName.SetDefault("Song of the Abyss");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.tileCollide = false;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.alpha = 255;
			base.projectile.penetrate = -1;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Dust dust = Dust.NewDustDirect(base.projectile.position, 2, 2, 261, 0f, 0f, 100, default(Color), 1f);
			dust.velocity *= 0f;
			dust.noGravity = true;
			double rad = (double)base.projectile.ai[1] * 0.017453292519943295;
			Projectile host = Main.projectile[(int)base.projectile.ai[0]];
			base.projectile.position.X = host.Center.X - (float)((int)(Math.Cos(rad) * this.dist)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = host.Center.Y - (float)((int)(Math.Sin(rad) * this.dist)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 4f;
			if (!host.active || host.type != ModContent.ProjectileType<ShadeTreble>())
			{
				base.projectile.Kill();
			}
			float num = 8f;
			Vector2 vector = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float hostX = host.position.X + (float)(host.width / 2);
			float hostY = host.position.Y + (float)(host.height / 2);
			hostX = (float)((int)(hostX / 8f) * 8);
			hostY = (float)((int)(hostY / 8f) * 8);
			vector.X = (float)((int)(vector.X / 8f) * 8);
			vector.Y = (float)((int)(vector.Y / 8f) * 8);
			hostX -= vector.X;
			hostY -= vector.Y;
			float rootXY = (float)Math.Sqrt((double)(hostX * hostX + hostY * hostY));
			if (rootXY == 0f)
			{
				hostX = base.projectile.velocity.X;
				hostY = base.projectile.velocity.Y;
				return;
			}
			rootXY = num / rootXY;
			hostX *= rootXY;
			hostY *= rootXY;
		}

		private readonly double dist = 30.0;
	}
}
