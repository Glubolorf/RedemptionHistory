﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class AttackHovercopterPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Attack Hovercopter");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 184;
			base.projectile.height = 50;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 31, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
			if (base.projectile.ai[0] == 0f)
			{
				base.projectile.frame = 0;
			}
			else
			{
				base.projectile.frame = 1;
			}
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			base.projectile.rotation += 0.01f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore1"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore2"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore3"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore4"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore5"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore7"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore8"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore9"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/Hostile/AttackHovercopterGore10"), 1f);
			for (int i = 0; i < 10; i++)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), 686, 40, 3f, 255, 0f, 0f);
			}
			for (int j = 0; j < 25; j++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
			for (int k = 0; k < 65; k++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 5f);
				Main.dust[dustIndex2].velocity *= 4.8f;
			}
			for (int l = 0; l < 45; l++)
			{
				int dustIndex3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 5f);
				Main.dust[dustIndex3].velocity *= 2.3f;
			}
		}
	}
}
