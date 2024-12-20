using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.WorldStave
{
	public class MechanicalNanofieldPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mechanical Nanofield");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 498;
			base.projectile.height = 498;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 20;
			base.projectile.timeLeft = 900;
		}

		public override void AI()
		{
			Projectile worldTree = Main.projectile[(int)base.projectile.ai[0]];
			base.projectile.Center = worldTree.Center;
			base.projectile.velocity = Vector2.Zero;
			base.projectile.rotation += 0.04f;
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(ModContent.BuffType<MechEnhancementBuff>(), 1800, false);
				}
			}
		}

		private Player clearCheck;
	}
}
