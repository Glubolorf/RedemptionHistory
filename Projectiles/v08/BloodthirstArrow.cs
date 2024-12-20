using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class BloodthirstArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloodthirst Arrow");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = 1;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 600;
			base.projectile.alpha = 0;
			base.projectile.extraUpdates = 1;
			this.aiType = 1;
		}

		public override void AI()
		{
			Player projOwner = Main.player[base.projectile.owner];
			if (base.projectile.localAI[0] == 0f)
			{
				if (projOwner.statLife > 10)
				{
					projOwner.statLife -= 10;
				}
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[base.projectile.owner];
			player.statLife += 20;
			player.HealEffect(20, true);
			for (int index = 0; index < 10; index++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 2f);
				dust.velocity = -player.DirectionTo(dust.position) * 10f;
				dust.noGravity = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
