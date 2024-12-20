using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DayPulse : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Day Pulse");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 114;
			base.projectile.height = 114;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 150;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 10)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 2;
			base.projectile.velocity *= 0.01f;
			base.projectile.rotation += 0.03f;
			if (base.projectile.localAI[0] >= 50f)
			{
				base.projectile.Kill();
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).frostburnSeedbag)
			{
				target.AddBuff(44, 160, false);
			}
		}
	}
}
