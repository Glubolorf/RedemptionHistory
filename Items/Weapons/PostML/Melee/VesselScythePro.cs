using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class VesselScythePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadescythe");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 70;
			base.projectile.melee = true;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 40;
			base.projectile.penetrate = 12;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			base.projectile.velocity *= 0.95f;
			base.projectile.rotation += 0.4f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				Color.GhostWhite,
				Color.Black,
				Color.GhostWhite
			}));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1f;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
			target.immune[base.projectile.owner] = 6;
		}
	}
}
