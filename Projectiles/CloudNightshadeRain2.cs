using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class CloudNightshadeRain2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Poisonous Rain");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 2;
			base.projectile.height = 8;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.5f;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int crit2 = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref crit2);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref crit2);
			if (crit2 >= 100 || Main.rand.Next(1, 101) <= crit2)
			{
				crit = true;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).frostburnSeedbag)
			{
				target.AddBuff(44, 160, false);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num].velocity *= 1.4f;
			}
		}
	}
}
