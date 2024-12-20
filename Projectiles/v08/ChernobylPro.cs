using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class ChernobylPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chernobyl");
		}

		public override void SetDefaults()
		{
			base.projectile.extraUpdates = 0;
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.aiStyle = 99;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			ProjectileID.Sets.YoyosLifeTimeMultiplier[base.projectile.type] = 16f;
			ProjectileID.Sets.YoyosMaximumRange[base.projectile.type] = 310f;
			ProjectileID.Sets.YoyosTopSpeed[base.projectile.type] = 17f;
		}

		public override void AI()
		{
			if (Main.rand.Next(7) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 74, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			if (Main.myPlayer == base.projectile.owner && Main.rand.Next(20) == 0)
			{
				int Proj = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("DribblingOoze"), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
				Main.projectile[Proj].hostile = false;
				Main.projectile[Proj].friendly = true;
			}
		}
	}
}
