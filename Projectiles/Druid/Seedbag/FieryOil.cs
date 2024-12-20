using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class FieryOil : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fiery Oil");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 130;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 6, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.75f;
			if (base.projectile.velocity.Y >= 13f)
			{
				base.projectile.velocity.Y = 12f;
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(24, 160, false);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0.2f)
			{
				base.projectile.velocity.X = oldVelocity.X * -0.1f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.2f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * -0.1f;
			}
			return false;
		}
	}
}
