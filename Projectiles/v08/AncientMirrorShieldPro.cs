using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientMirrorShieldPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Mirror Shield");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 58;
			base.projectile.height = 58;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.extraUpdates = 1;
			base.projectile.timeLeft = 360000;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 9;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			base.projectile.netImportant = true;
		}

		public override bool PreAI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 6)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (!player.HasBuff(base.mod.BuffType("AncientMirrorBuff")))
			{
				base.projectile.Kill();
			}
			if (player.dead)
			{
				modPlayer.ancientMirror = false;
			}
			IEnumerable<Projectile> enumerable = Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox));
			foreach (Projectile projectile in enumerable)
			{
				if (base.projectile != projectile && !projectile.friendly && projectile.hostile && !projectile.minion && projectile.velocity.X != 0f && projectile.velocity.Y != 0f && projectile.width <= 58 && projectile.height <= 58)
				{
					if (projectile.penetrate == 1)
					{
						projectile.penetrate = 2;
					}
					projectile.damage *= 4;
					projectile.friendly = true;
					projectile.hostile = false;
					projectile.velocity.X = -projectile.velocity.X * 1.5f;
					projectile.velocity.Y = -projectile.velocity.Y * 1.5f;
				}
				if (base.projectile != projectile && !projectile.friendly && projectile.hostile && !projectile.minion && projectile.velocity.X != 0f && projectile.velocity.Y != 0f && projectile.width > 58 && projectile.height > 58)
				{
					Main.PlaySound(SoundID.Item27, base.projectile.position);
					for (int i = 0; i < 20; i++)
					{
						int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 269, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num].velocity *= 1.9f;
					}
					for (int j = 0; j < 12; j++)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("GlassShardPro1"), 200, 1f, Main.myPlayer, 0f, 0f);
					}
					base.projectile.Kill();
				}
			}
			base.projectile.localAI[0] += 1f;
			int num2 = 1;
			if (base.projectile.position.X + (float)(base.projectile.width / 2) < Main.player[base.projectile.owner].position.X + (float)Main.player[base.projectile.owner].width)
			{
				num2 = -1;
			}
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num3 = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) + (float)(num2 * 180) - vector.X;
			float num4 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - vector.Y;
			Math.Sqrt((double)(num3 * num3 + num4 * num4));
			float num5 = base.projectile.position.X + (float)(base.projectile.width / 2) - Main.player[base.projectile.owner].position.X - (float)(Main.player[base.projectile.owner].width / 2);
			float num6 = base.projectile.position.Y + (float)base.projectile.height - 59f - Main.player[base.projectile.owner].position.Y - (float)(Main.player[base.projectile.owner].height / 2);
			float num7 = (float)Math.Atan2((double)num6, (double)num5) + 1.57f;
			if (num7 < 0f)
			{
				num7 += 6.283f;
			}
			else if ((double)num7 > 6.283)
			{
				num7 -= 6.283f;
			}
			float num8 = 0.15f;
			if (base.projectile.rotation < num7)
			{
				if ((double)(num7 - base.projectile.rotation) > 3.1415)
				{
					base.projectile.rotation -= num8;
				}
				else
				{
					base.projectile.rotation += num8;
				}
			}
			else if (base.projectile.rotation > num7)
			{
				if ((double)(base.projectile.rotation - num7) > 3.1415)
				{
					base.projectile.rotation += num8;
				}
				else
				{
					base.projectile.rotation -= num8;
				}
			}
			if (base.projectile.rotation > num7 - num8 && base.projectile.rotation < num7 + num8)
			{
				base.projectile.rotation = num7;
			}
			if (base.projectile.rotation < 0f)
			{
				base.projectile.rotation += 6.283f;
			}
			else if ((double)base.projectile.rotation > 6.283)
			{
				base.projectile.rotation -= 6.283f;
			}
			if (base.projectile.rotation > num7 - num8 && base.projectile.rotation < num7 + num8)
			{
				base.projectile.rotation = num7;
			}
			Vector2 vector2 = Main.player[base.projectile.owner].Center - base.projectile.Center;
			vector2.Normalize();
			vector2 *= 9f;
			double num9 = (double)base.projectile.ai[1] / 2.0;
			double num10 = num9 * 0.017453292519943295;
			double num11 = 100.0;
			base.projectile.position.X = player.Center.X - (float)((int)(Math.Cos(num10) * num11)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = player.Center.Y - (float)((int)(Math.Sin(num10) * num11)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 2f;
			return false;
		}
	}
}
