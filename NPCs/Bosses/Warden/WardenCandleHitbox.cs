using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenCandleHitbox : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 174;
			base.projectile.height = 174;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 10;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.Center = player.Center;
		}
	}
}
