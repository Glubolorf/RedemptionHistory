using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class HolyFirePro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Incandesent Flames");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.magic = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 180;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(base.mod.BuffType("HolyFireDebuff"), 400, false);
		}

		public override void AI()
		{
			int num = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 64, 0f, 0f, 100, default(Color), 3f);
			Main.dust[num].noGravity = true;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item74, base.projectile.position);
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 8f, 0f, 0f, base.mod.ProjectileType("HolyFirePro3"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
		}
	}
}
