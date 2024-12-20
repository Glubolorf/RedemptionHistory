using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class BoneHeadPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Leviathan");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(493);
			this.aiType = 493;
		}
	}
}
