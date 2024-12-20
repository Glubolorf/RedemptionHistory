using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientBrassBoomerangProj : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Weapons/v08/AncientBrassBoomerang";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Boomerang");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = 3;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 600;
		}

		public override void AI()
		{
			this.speen++;
			this.aiSwitch++;
			if (this.aiSwitch <= 35)
			{
				base.projectile.aiStyle = 1;
				base.projectile.rotation += (float)this.speen * 0.5f;
			}
			if (this.aiSwitch >= 35 && this.aiSwitch <= 50)
			{
				base.projectile.velocity *= 0.5f;
				base.projectile.rotation += (float)this.speen * 0.5f;
			}
			if (this.aiSwitch >= 50)
			{
				base.projectile.aiStyle = 3;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (this.aiSwitch <= 50)
			{
				Collision.HitTiles(base.projectile.position + base.projectile.velocity, base.projectile.velocity, base.projectile.width, base.projectile.height);
				if (base.projectile.velocity.X != oldVelocity.X)
				{
					base.projectile.velocity.X = -oldVelocity.X;
				}
				if (base.projectile.velocity.Y != oldVelocity.Y)
				{
					base.projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}

		private int aiSwitch;

		private int speen;
	}
}
