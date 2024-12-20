using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class SpiritGolemHead : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 36000;
			base.projectile.light = 0f;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Golem Head");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 89, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
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
				if (num >= 8)
				{
					base.projectile.frame = 0;
				}
			}
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritGolemCross)
			{
				base.projectile.Kill();
			}
			else
			{
				base.projectile.timeLeft = 5;
			}
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 89, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			if (base.projectile.localAI[1] == 0f)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 89, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				base.projectile.localAI[1] = 1f;
			}
			float[] localAI = base.projectile.localAI;
			int num2 = 0;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 % 40f == 0f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(-7f, 0f), base.mod.ProjectileType("SpiritGolemBolt"), 50, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(7f, 0f), base.mod.ProjectileType("SpiritGolemBolt"), 50, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
			}
			int Num3 = 1;
			if (base.projectile.position.X + (float)(base.projectile.width / 2) < Main.player[base.projectile.owner].position.X + (float)Main.player[base.projectile.owner].width)
			{
				Num3 = -1;
			}
			Vector2 Vector = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num4 = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) + (float)(Num3 * 180) - Vector.X;
			float Num4 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - Vector.Y;
			Math.Sqrt((double)(num4 * num4 + Num4 * Num4));
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
			Player player = Main.player[base.projectile.owner];
			double rad = (double)base.projectile.ai[1] / 2.0 * 0.017453292519943295;
			double dist = 80.0;
			base.projectile.position.X = player.Center.X - (float)((int)(Math.Cos(rad) * dist)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = player.Center.Y - (float)((int)(Math.Sin(rad) * dist)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 2f;
			return false;
		}
	}
}
