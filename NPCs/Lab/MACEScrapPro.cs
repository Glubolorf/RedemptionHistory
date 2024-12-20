using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class MACEScrapPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scrap");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(450);
			this.aiType = 450;
			base.projectile.hostile = true;
			base.projectile.width = 12;
			base.projectile.height = 12;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 160;
		}
	}
}
