using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class DragonfireYoyoPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragonfire");
		}

		public override void SetDefaults()
		{
			base.projectile.extraUpdates = 0;
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = 99;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			ProjectileID.Sets.YoyosLifeTimeMultiplier[base.projectile.type] = 10f;
			ProjectileID.Sets.YoyosMaximumRange[base.projectile.type] = 200f;
			ProjectileID.Sets.YoyosTopSpeed[base.projectile.type] = 13f;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(24, 600, false);
		}
	}
}
