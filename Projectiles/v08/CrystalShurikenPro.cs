using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CrystalShurikenPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Shuriken");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.penetrate = 1;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 70, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 20f)
			{
				base.projectile.Kill();
				Main.PlaySound(27, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 70, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 69, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 70, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 69, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 7f, base.projectile.position.Y + 28f), new Vector2(base.projectile.velocity.X + 0f, base.projectile.velocity.Y + 5f), base.mod.ProjectileType("CrystalShurikenShardPro"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 7f, base.projectile.position.Y + 28f), new Vector2(base.projectile.velocity.X + 0f, base.projectile.velocity.Y - 5f), base.mod.ProjectileType("CrystalShurikenShardPro"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
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
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 4, 1f, base.projectile.owner, 0f, 1f);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return new bool?(projHitbox.Intersects(targetHitbox));
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(27, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Vector2 vector = base.projectile.position;
			Vector2 vector2 = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			vector += vector2 * 16f;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(vector, base.projectile.width, base.projectile.height, 70, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += vector2 * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				vector -= vector2 * 8f;
			}
		}
	}
}
