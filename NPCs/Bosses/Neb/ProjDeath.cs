using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class ProjDeath : ModProjectile
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
			base.DisplayName.SetDefault("death");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 2000;
			base.projectile.height = 2000;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 6;
		}

		public override void AI()
		{
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.hostile)
				{
					proj.Kill();
				}
			}
		}
	}
}
