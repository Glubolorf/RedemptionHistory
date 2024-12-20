using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class PZ2HideTheAAA : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Patient Zero");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 98;
			base.projectile.height = 80;
			base.projectile.timeLeft = 4;
			base.projectile.ignoreWater = true;
		}
	}
}
