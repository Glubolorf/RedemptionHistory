using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class FalconPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Earthquake BOOM!");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 92;
			base.projectile.height = 12;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.melee = true;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 8;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			if (base.projectile.localAI[0] == 0f)
			{
				Main.PlaySound(SoundID.Item89, base.projectile.position);
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 0, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				base.projectile.localAI[0] = 1f;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int targetHitBox = target.width + target.height;
			if (target.velocity.X != 0f && target.velocity.Y != 0f && targetHitBox < 200 && !target.noGravity)
			{
				target.velocity.Y = -10f * target.knockBackResist;
			}
		}
	}
}
