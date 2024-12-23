﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class CursedMoonPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Moon");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 50;
			base.projectile.height = 50;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 160;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation += 0.04f;
			Vector2 position = base.projectile.Center + Vector2.Normalize(base.projectile.velocity) * 10f;
			Dust dust = Main.dust[Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 75, 0f, 0f, 0, default(Color), 2f)];
			dust.position = position;
			dust.velocity = Utils.RotatedBy(base.projectile.velocity, 1.5707963705062866, default(Vector2)) * 0.33f + base.projectile.velocity / 4f;
			dust.position += Utils.RotatedBy(base.projectile.velocity, 1.5707963705062866, default(Vector2));
			dust.fadeIn = 0.5f;
			dust.noGravity = true;
			Dust dust2 = Main.dust[Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 75, 0f, 0f, 0, default(Color), 2f)];
			dust2.position = position;
			dust2.velocity = Utils.RotatedBy(base.projectile.velocity, -1.5707963705062866, default(Vector2)) * 0.33f + base.projectile.velocity / 4f;
			dust2.position += Utils.RotatedBy(base.projectile.velocity, -1.5707963705062866, default(Vector2));
			dust2.fadeIn = 0.5f;
			dust2.noGravity = true;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(39, 300, false);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 75, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}
	}
}
