﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class ShadeboundPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadebound");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(333);
			this.aiType = 333;
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 220;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 3)
			{
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 2)
			{
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 1)
			{
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
			}
			else
			{
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + Utils.NextFloat(Main.rand, (float)base.projectile.width), base.projectile.position.Y + Utils.NextFloat(Main.rand, (float)base.projectile.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<ShadesoulPro1>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 1f);
			}
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 260, false);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 213, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.8f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(lightColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
