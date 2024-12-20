using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MysteriousArtifactPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Artifact");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(272);
			this.aiType = 272;
			base.projectile.width = 24;
			base.projectile.height = 34;
			base.projectile.melee = true;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 21, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[num].noGravity = true;
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, base.mod.ProjectileType("MysteriousArtifactProC"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
		}
	}
}
