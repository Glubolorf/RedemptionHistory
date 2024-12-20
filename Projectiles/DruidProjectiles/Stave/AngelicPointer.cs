using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Stave
{
	public class AngelicPointer : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Angelic Javalin");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			int Dust = Dust.NewDust(new Vector2(base.projectile.Center.X - 4f, base.projectile.Center.Y), 1, 1, 15, 0f, 0f, 0, default(Color), 1f);
			Main.dust[Dust].noGravity = true;
			Dust dust = Main.dust[Dust];
			dust.velocity.X = 0f;
			dust.velocity.Y = 0f;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
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
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 14f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-6 + Main.rand.Next(0, 12)), base.mod.ProjectileType("AngelicArrow"), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 14f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-6 + Main.rand.Next(0, 12)), base.mod.ProjectileType("AngelicArrow"), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 14f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-6 + Main.rand.Next(0, 12)), base.mod.ProjectileType("AngelicArrow"), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 14f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-6 + Main.rand.Next(0, 12)), base.mod.ProjectileType("AngelicArrow"), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Vector2 usePos = base.projectile.position;
			Vector2 rotVector = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			usePos += rotVector * 16f;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(usePos, base.projectile.width, base.projectile.height, 81, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
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
	}
}
