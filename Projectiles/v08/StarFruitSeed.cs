﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class StarFruitSeed : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starfruit Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.7f;
			if (base.projectile.velocity.Y >= 10f)
			{
				base.projectile.velocity.Y = 9f;
			}
			if (base.projectile.localAI[0] >= 40f)
			{
				Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, base.mod.ProjectileType("StarfruitPlant"), base.projectile.damage, 0f, base.projectile.owner, 1f, 0f);
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, base.mod.ProjectileType("StarfruitPlant"), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
			return true;
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
		}
	}
}