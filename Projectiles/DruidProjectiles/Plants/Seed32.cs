using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Seed32 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloodroot Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.75f;
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			if (Main.myPlayer == base.projectile.owner)
			{
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int projID = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<BloodrootRoot>(), base.projectile.damage, 3f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)i / (float)pieCut * 6.28f);
				}
			}
			for (int j = 0; j < 5; j++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 5, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.8f;
			}
		}
	}
}
