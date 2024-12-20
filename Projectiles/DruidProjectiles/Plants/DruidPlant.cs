using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public abstract class DruidPlant : ModProjectile
	{
		public virtual void SetSafeDefaults()
		{
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
			if (this.runOnce)
			{
				this.IsOnNativeTerrain = this.CheckNativeTerrain();
				this.runOnce = false;
			}
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}

		public virtual bool CheckNativeTerrain()
		{
			Point rootLocation = Utils.ToTileCoordinates(base.projectile.Bottom);
			bool yes = false;
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					foreach (int type in base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs)
					{
						if ((int)Main.tile[rootLocation.X + x, rootLocation.Y + y].type == type && Main.tile[rootLocation.X + x, rootLocation.Y + y].active())
						{
							yes = true;
							break;
						}
					}
				}
			}
			return yes;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 1f;
			this.PlantAI();
		}

		protected virtual void PlantAI()
		{
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (this.IsOnNativeTerrain)
			{
				damage = (int)((float)damage * this.nativeDamageMultiplier);
			}
			this.PlantModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
		}

		protected virtual void PlantModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
		}

		protected float nativeDamageMultiplier = 1.2f;

		private bool runOnce = true;

		public bool IsOnNativeTerrain;
	}
}
