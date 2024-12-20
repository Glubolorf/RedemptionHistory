using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class GreatChickenShieldPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Greater Chickman Escutcheon");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 72;
			base.projectile.height = 50;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 900;
			base.projectile.netImportant = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (!player.HasBuff(ModContent.BuffType<ChickenShieldBuff>()))
			{
				base.projectile.Kill();
			}
			if (player.direction == 1)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<GreatChickenShieldPro2>()] != 0)
			{
				base.projectile.alpha = 255;
				base.projectile.netUpdate = true;
			}
			else
			{
				base.projectile.alpha = 0;
				base.projectile.netUpdate = true;
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.position.X = player.Center.X - 72f;
			base.projectile.position.Y = player.Center.Y - 28f;
		}
	}
}
