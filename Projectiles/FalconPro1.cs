using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class FalconPro1 : ModProjectile
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
			base.DisplayName.SetDefault("Mini Earthquake Starter");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 20;
			base.projectile.alpha = 255;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.75f;
			base.projectile.velocity.X = 0f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Main.myPlayer == base.projectile.owner)
			{
				Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, ModContent.ProjectileType<FalconPro2>(), base.projectile.damage, 7f, base.projectile.owner, 0f, 0f);
			}
			return true;
		}
	}
}
