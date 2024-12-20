using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class TiedRapierPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tied's Rapier");
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
			TiedRapierPro.AIArcStabSpear(base.projectile, ref base.projectile.ai, false);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			BaseDrawing.DrawProjectileSpear(sb, Main.projectileTexture[base.projectile.type], 0, base.projectile, null, 0f, 0f);
			return false;
		}

		public static void AIArcStabSpear(Projectile p, ref float[] ai, bool overrideKill = false)
		{
			Player plr = Main.player[p.owner];
			Item item = plr.inventory[plr.selectedItem];
			if (Main.myPlayer == p.owner && item != null && item.autoReuse && plr.itemAnimation == 1)
			{
				p.Kill();
				return;
			}
			Main.player[p.owner].heldProj = p.whoAmI;
			Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 gfxOffset = new Vector2(0f, plr.gfxOffY);
			TiedRapierPro.AIArcStabSpear(p, ref ai, plr.Center + gfxOffset, BaseUtility.RotationTo(p.Center, p.Center + p.velocity), plr.direction, plr.itemAnimation, plr.itemAnimationMax, overrideKill, plr.frozen);
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
			Vector2 oldCenter = p.Center;
			p.position.X = center.X - (float)p.width * 0.5f;
			p.position.Y = center.Y - (float)p.height * 0.5f;
			p.position += BaseUtility.RotateVector(default(Vector2), BaseUtility.MultiLerpVector(1f - (float)itemAnimation / (float)itemAnimationMax, TiedRapierPro.spearPos), itemRot);
			if (!overrideKill && Main.player[p.owner].itemAnimation == 0)
			{
				p.Kill();
			}
			p.rotation = BaseUtility.RotationTo(center, oldCenter) + 2.355f;
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
