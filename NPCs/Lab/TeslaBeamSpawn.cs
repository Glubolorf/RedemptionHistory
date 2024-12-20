using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class TeslaBeamSpawn : ModProjectile
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
			base.DisplayName.SetDefault("Tesla Beam");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				int p = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (base.projectile.ai[0] == 0f) ? 10f : -10f, 0f, ModContent.ProjectileType<TeslaBeam1>(), base.projectile.damage, 1f, 255, 0f, 0f);
				Main.projectile[p].netUpdate = true;
				base.projectile.localAI[0] = 1f;
			}
		}
	}
}
