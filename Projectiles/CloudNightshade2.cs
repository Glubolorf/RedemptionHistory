using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class CloudNightshade2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Poisonous Vapor Cloud");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.07f;
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 10f, base.projectile.position.Y + 18f), new Vector2(0f, 0f), base.mod.ProjectileType("CloudNightshadeRain"), 11, 3f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 14f), new Vector2(0f, 0f), base.mod.ProjectileType("CloudNightshadeRain"), 11, 3f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 20f, base.projectile.position.Y + 20f), new Vector2(0f, 0f), base.mod.ProjectileType("CloudNightshadeRain"), 11, 3f, base.projectile.owner, 0f, 1f);
			}
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
			for (int i = 0; i < 40; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.4f;
			}
		}
	}
}
