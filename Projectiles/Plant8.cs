﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Plant8 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shiverthorn");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 42;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 1f;
			if (base.projectile.localAI[0] == 30f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 8f), base.projectile.velocity, base.mod.ProjectileType("ShiverthornIcicle"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 60f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 8f), base.projectile.velocity, base.mod.ProjectileType("ShiverthornIcicle"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 90f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 8f), base.projectile.velocity, base.mod.ProjectileType("ShiverthornIcicle"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 120f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 8f), base.projectile.velocity, base.mod.ProjectileType("ShiverthornIcicle"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] > 150f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 8f), base.projectile.velocity, base.mod.ProjectileType("ShiverthornIcicle"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}
	}
}