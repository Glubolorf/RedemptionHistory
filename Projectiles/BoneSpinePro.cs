using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class BoneSpinePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Leviathan");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(494);
			this.aiType = 494;
		}
	}
}
