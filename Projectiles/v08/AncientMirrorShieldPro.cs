using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
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
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 6)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (!player.HasBuff(ModContent.BuffType<AncientMirrorBuff>()))
			{
				base.projectile.Kill();
			}
			if (player.dead)
			{
				modPlayer.ancientMirror = false;
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.friendly && proj.hostile && !proj.minion && proj.velocity.X != 0f && proj.velocity.Y != 0f && proj.width <= 58 && proj.height <= 58)
				{
					if (proj.penetrate == 1)
					{
						proj.penetrate = 2;
					}
					proj.damage *= 4;
					proj.friendly = true;
					proj.hostile = false;
					proj.velocity.X = -proj.velocity.X * 1.5f;
					proj.velocity.Y = -proj.velocity.Y * 1.5f;
				}
				if (base.projectile != proj && !proj.friendly && proj.hostile && !proj.minion && proj.velocity.X != 0f && proj.velocity.Y != 0f && proj.width > 58 && proj.height > 58)
				{
					Main.PlaySound(SoundID.Item27, base.projectile.position);
					for (int i = 0; i < 20; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 269, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex].velocity *= 1.9f;
					}
					for (int j = 0; j < 12; j++)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), ModContent.ProjectileType<GlassShardPro1>(), 200, 1f, Main.myPlayer, 0f, 0f);
					}
					base.projectile.Kill();
				}
			}
			base.projectile.localAI[0] += 1f;
			int Num3 = 1;
			if (base.projectile.position.X + (float)(base.projectile.width / 2) < Main.player[base.projectile.owner].position.X + (float)Main.player[base.projectile.owner].width)
			{
				Num3 = -1;
			}
			Vector2 Vector = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num2 = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) + (float)(Num3 * 180) - Vector.X;
			float Num4 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - Vector.Y;
			Math.Sqrt((double)(num2 * num2 + Num4 * Num4));
			float Num5 = base.projectile.position.X + (float)(base.projectile.width / 2) - Main.player[base.projectile.owner].position.X - (float)(Main.player[base.projectile.owner].width / 2);
			float Num6 = (float)Math.Atan2((double)(base.projectile.position.Y + (float)base.projectile.height - 59f - Main.player[base.projectile.owner].position.Y - (float)(Main.player[base.projectile.owner].height / 2)), (double)Num5) + 1.57f;
			if (Num6 < 0f)
			{
				Num6 += 6.283f;
			}
			else if ((double)Num6 > 6.283)
			{
				Num6 -= 6.283f;
			}
			float Num7 = 0.15f;
			if (base.projectile.rotation < Num6)
			{
				if ((double)(Num6 - base.projectile.rotation) > 3.1415)
				{
					base.projectile.rotation -= Num7;
				}
				else
				{
					base.projectile.rotation += Num7;
				}
			}
			else if (base.projectile.rotation > Num6)
			{
				if ((double)(base.projectile.rotation - Num6) > 3.1415)
				{
					base.projectile.rotation += Num7;
				}
				else
				{
					base.projectile.rotation -= Num7;
				}
			}
			if (base.projectile.rotation > Num6 - Num7 && base.projectile.rotation < Num6 + Num7)
			{
				base.projectile.rotation = Num6;
			}
			if (base.projectile.rotation < 0f)
			{
				base.projectile.rotation += 6.283f;
			}
			else if ((double)base.projectile.rotation > 6.283)
			{
				base.projectile.rotation -= 6.283f;
			}
			if (base.projectile.rotation > Num6 - Num7 && base.projectile.rotation < Num6 + Num7)
			{
				base.projectile.rotation = Num6;
			}
			Vector2 direction = Main.player[base.projectile.owner].Center - base.projectile.Center;
			direction.Normalize();
			direction *= 9f;
			double rad = (double)base.projectile.ai[1] / 2.0 * 0.017453292519943295;
			double dist = 100.0;
			base.projectile.position.X = player.Center.X - (float)((int)(Math.Cos(rad) * dist)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = player.Center.Y - (float)((int)(Math.Sin(rad) * dist)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 2f;
			return false;
		}
	}
}
