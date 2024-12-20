using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	internal class ToxicBreath : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Toxic Breath");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = 1;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 100;
			base.projectile.aiStyle = -1;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return false;
		}

		public override void AI()
		{
			if (base.projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (base.projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (base.projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (base.projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				base.projectile.ai[0] += 1f;
				int num297 = 70;
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 1; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, num297, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if (Main.rand.Next(3) == 0)
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 3f;
							Main.dust[num299].noLight = true;
							Dust dust = Main.dust[num299];
							dust.velocity.X = dust.velocity.X * 2f;
							Dust dust2 = Main.dust[num299];
							dust2.velocity.Y = dust2.velocity.Y * 2f;
						}
						Main.dust[num299].scale *= 1f;
						Main.dust[num299].noLight = true;
						Dust dust3 = Main.dust[num299];
						dust3.velocity.X = dust3.velocity.X * 1.2f;
						Dust dust4 = Main.dust[num299];
						dust4.velocity.Y = dust4.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
						Main.dust[num299].velocity += base.projectile.velocity;
						if (!Main.dust[num299].noGravity)
						{
							Main.dust[num299].velocity *= 0.5f;
						}
					}
				}
			}
			else
			{
				base.projectile.ai[0] += 1f;
				base.projectile.width += 6;
				base.projectile.height += 6;
			}
			base.projectile.rotation += 0.3f * (float)base.projectile.direction;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(70, 400, true);
		}
	}
}
