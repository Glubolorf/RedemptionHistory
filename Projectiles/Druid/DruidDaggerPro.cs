﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Weapons.PreHM.Druid;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class DruidDaggerPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid Dagger");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 48;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 26f), base.projectile.velocity, ModContent.ProjectileType<PollenCloud1>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
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
			int item = (Main.rand.Next(18) == 0) ? Item.NewItem((int)base.projectile.position.X, (int)base.projectile.position.Y, base.projectile.width, base.projectile.height, ModContent.ItemType<DruidDagger>(), 1, false, 0, false, false) : 0;
			if (Main.netMode == 1 && item >= 0)
			{
				NetMessage.SendData(21, -1, -1, null, item, 1f, 0f, 0f, 0, 0, 0);
			}
		}
	}
}