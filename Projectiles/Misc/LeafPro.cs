using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class LeafPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Leaf");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(206);
			base.projectile.width = 24;
			base.projectile.height = 14;
			base.projectile.magic = false;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}
	}
}
