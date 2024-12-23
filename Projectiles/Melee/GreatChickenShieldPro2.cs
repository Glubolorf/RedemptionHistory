﻿using System;
using Redemption.Buffs;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class GreatChickenShieldPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Greater Chickman Escutcheon");
			Main.projFrames[base.projectile.type] = 4;
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
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 10)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 20f)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Slam3").WithVolume(0.9f).WithPitchVariance(0.1f), base.projectile.position);
				if (base.projectile.spriteDirection == -1)
				{
					Projectile.NewProjectile(base.projectile.Center.X - 12f, base.projectile.Center.Y + 2f, 0f, 0f, ModContent.ProjectileType<DamagePro5>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
				}
				else
				{
					Projectile.NewProjectile(base.projectile.Center.X + 78f, base.projectile.Center.Y + 2f, 0f, 0f, ModContent.ProjectileType<DamagePro5>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
				}
			}
			if (base.projectile.localAI[0] >= 40f)
			{
				base.projectile.Kill();
			}
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.position.X = player.Center.X - 72f;
			base.projectile.position.Y = player.Center.Y - 28f;
		}
	}
}
