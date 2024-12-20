﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CursedSpearPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Warmonger");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = -1;
			base.projectile.timeLeft = 600;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.hide = true;
			base.projectile.ownerHitCheck = true;
			base.projectile.melee = true;
			base.projectile.alpha = 254;
		}

		public override void AI()
		{
			CursedSpearPro1.AIArcStabSpear(base.projectile, ref base.projectile.ai, false);
			if (Main.rand.Next(3) != 0)
			{
				int num = Dust.NewDust(base.projectile.Center, 0, 0, 75, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(39, 300, false);
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			BaseDrawing.DrawProjectileSpear(sb, Main.projectileTexture[base.projectile.type], 0, base.projectile, null, 0f, 0f);
			return false;
		}

		public static void AIArcStabSpear(Projectile p, ref float[] ai, bool overrideKill = false)
		{
			Player player = Main.player[p.owner];
			Item item = player.inventory[player.selectedItem];
			if (Main.myPlayer == p.owner && item != null && item.autoReuse && player.itemAnimation == 1)
			{
				p.Kill();
				return;
			}
			Main.player[p.owner].heldProj = p.whoAmI;
			Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 vector;
			vector..ctor(0f, player.gfxOffY);
			CursedSpearPro1.AIArcStabSpear(p, ref ai, player.Center + vector, BaseUtility.RotationTo(p.Center, p.Center + p.velocity), player.direction, player.itemAnimation, player.itemAnimationMax, overrideKill, player.frozen);
		}

		public static void AIArcStabSpear(Projectile p, ref float[] ai, Vector2 center, float itemRot, int ownerDirection, int itemAnimation, int itemAnimationMax, bool overrideKill = false, bool frozen = false)
		{
			if (p.timeLeft < 598)
			{
				p.alpha -= 70;
			}
			if (p.alpha < 0)
			{
				p.alpha = 0;
			}
			p.direction = ownerDirection;
			Vector2 center2 = p.Center;
			p.position.X = center.X - (float)p.width * 0.5f;
			p.position.Y = center.Y - (float)p.height * 0.5f;
			p.position += BaseUtility.RotateVector(default(Vector2), BaseUtility.MultiLerpVector(1f - (float)itemAnimation / (float)itemAnimationMax, CursedSpearPro1.spearPos), itemRot);
			if (!overrideKill && Main.player[p.owner].itemAnimation == 0)
			{
				p.Kill();
			}
			p.rotation = BaseUtility.RotationTo(center, center2) + 2.355f;
			if (p.direction == -1)
			{
				p.rotation -= 0f;
				return;
			}
			if (p.direction == 1)
			{
				p.rotation -= 1.57f;
			}
		}

		public static Color lightColor = new Color(0, 100, 0);

		public static Vector2[] spearPos = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(30f, -5f),
			new Vector2(70f, -30f),
			new Vector2(70f, 0f),
			new Vector2(70f, 30f),
			new Vector2(30f, 5f),
			new Vector2(10f, 0f),
			new Vector2(130f, 0f),
			new Vector2(130f, 0f),
			new Vector2(10f, 0f)
		};
	}
}
