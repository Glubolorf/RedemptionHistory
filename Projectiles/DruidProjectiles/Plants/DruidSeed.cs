using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public abstract class DruidSeed : ModProjectile
	{
		public virtual void SetSafeDefaults()
		{
		}

		public override void AI()
		{
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.7f;
			if (base.projectile.velocity.Y >= 10f)
			{
				base.projectile.velocity.Y = 9f;
			}
		}

		public override void SetDefaults()
		{
			this.SetSafeDefaults();
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			if (this.plantID != -1)
			{
				Projectile projectile = Main.projectile[Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, this.plantID, base.projectile.damage, 0f, base.projectile.owner, 0f, 1f)];
				projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs;
				projectile.timeLeft = (int)((float)projectile.timeLeft * base.projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier);
			}
			return true;
		}

		protected int plantID = -1;
	}
}
