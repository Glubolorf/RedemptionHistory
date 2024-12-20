using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	internal class MiniNukePro : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 15;
			base.projectile.height = 15;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 300;
			this.drawOffsetX = 5;
			this.drawOriginOffsetY = 5;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.expertMode && target.type >= 13 && target.type <= 15)
			{
				damage /= 5;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.ai[1] != 0f)
			{
				return true;
			}
			base.projectile.soundDelay = 10;
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0.3f)
			{
				base.projectile.velocity.X = oldVelocity.X * -0.2f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.3f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * -0.2f;
			}
			return false;
		}

		public override void AI()
		{
			if (base.projectile.owner == Main.myPlayer && base.projectile.timeLeft <= 3)
			{
				base.projectile.tileCollide = false;
				base.projectile.alpha = 255;
				base.projectile.position.X = base.projectile.position.X + (float)(base.projectile.width / 2);
				base.projectile.position.Y = base.projectile.position.Y + (float)(base.projectile.height / 2);
				base.projectile.width = 500;
				base.projectile.height = 500;
				base.projectile.position.X = base.projectile.position.X - (float)(base.projectile.width / 2);
				base.projectile.position.Y = base.projectile.position.Y - (float)(base.projectile.height / 2);
				base.projectile.damage = 550;
				base.projectile.knockBack = 10f;
			}
			else if (Main.rand.Next(2) == 0)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[num].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = base.projectile.Center + Utils.RotatedBy(new Vector2(0f, -(float)base.projectile.height / 2f), (double)base.projectile.rotation, default(Vector2)) * 1.1f;
				num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = base.projectile.Center + Utils.RotatedBy(new Vector2(0f, -(float)base.projectile.height / 2f - 6f), (double)base.projectile.rotation, default(Vector2)) * 1.1f;
			}
			base.projectile.ai[0] += 1f;
			if (base.projectile.ai[0] > 5f)
			{
				base.projectile.ai[0] = 10f;
				if (base.projectile.velocity.Y == 0f && base.projectile.velocity.X != 0f)
				{
					base.projectile.velocity.X = base.projectile.velocity.X * 0.97f;
					base.projectile.velocity.X = base.projectile.velocity.X * 0.99f;
					if ((double)base.projectile.velocity.X > -0.01 && (double)base.projectile.velocity.X < 0.01)
					{
						base.projectile.velocity.X = 0f;
						base.projectile.netUpdate = true;
					}
				}
				base.projectile.velocity.Y = base.projectile.velocity.Y + 0.2f;
			}
			base.projectile.rotation += base.projectile.velocity.X * 0.1f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item15, base.projectile.position);
			for (int i = 0; i < 50; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.4f;
			}
			for (int j = 0; j < 80; j++)
			{
				int num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 5f;
				num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num2].velocity *= 3f;
			}
			for (int k = 0; k < 2; k++)
			{
				int num3 = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num3].scale = 1.5f;
				Main.gore[num3].velocity.X = Main.gore[num3].velocity.X + 1.5f;
				Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y + 1.5f;
				num3 = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num3].scale = 1.5f;
				Main.gore[num3].velocity.X = Main.gore[num3].velocity.X - 1.5f;
				Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y + 1.5f;
				num3 = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num3].scale = 1.5f;
				Main.gore[num3].velocity.X = Main.gore[num3].velocity.X + 1.5f;
				Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y - 1.5f;
				num3 = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num3].scale = 1.5f;
				Main.gore[num3].velocity.X = Main.gore[num3].velocity.X - 1.5f;
				Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y - 1.5f;
			}
			base.projectile.position.X = base.projectile.position.X + (float)(base.projectile.width / 2);
			base.projectile.position.Y = base.projectile.position.Y + (float)(base.projectile.height / 2);
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.position.X = base.projectile.position.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = base.projectile.position.Y - (float)(base.projectile.height / 2);
			int num4 = 18;
			int num5 = (int)(base.projectile.position.X / 16f - (float)num4);
			int num6 = (int)(base.projectile.position.X / 16f + (float)num4);
			int num7 = (int)(base.projectile.position.Y / 16f - (float)num4);
			int num8 = (int)(base.projectile.position.Y / 16f + (float)num4);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			bool flag = false;
			for (int l = num5; l <= num6; l++)
			{
				for (int m = num7; m <= num8; m++)
				{
					float num9 = Math.Abs((float)l - base.projectile.position.X / 16f);
					float num10 = Math.Abs((float)m - base.projectile.position.Y / 16f);
					double num11 = Math.Sqrt((double)(num9 * num9 + num10 * num10));
					if (num11 < (double)num4 && Main.tile[l, m] != null && Main.tile[l, m].wall == 0)
					{
						flag = true;
						break;
					}
				}
			}
			AchievementsHelper.CurrentlyMining = true;
			for (int n = num5; n <= num6; n++)
			{
				for (int num12 = num7; num12 <= num8; num12++)
				{
					float num13 = Math.Abs((float)n - base.projectile.position.X / 16f);
					float num14 = Math.Abs((float)num12 - base.projectile.position.Y / 16f);
					double num15 = Math.Sqrt((double)(num13 * num13 + num14 * num14));
					if (num15 < (double)num4)
					{
						bool flag2 = true;
						if (Main.tile[n, num12] != null && Main.tile[n, num12].active())
						{
							flag2 = true;
							if (Main.tileDungeon[(int)Main.tile[n, num12].type] || Main.tile[n, num12].type == 88 || Main.tile[n, num12].type == 21 || Main.tile[n, num12].type == 26 || Main.tile[n, num12].type == 107 || Main.tile[n, num12].type == 108 || Main.tile[n, num12].type == 111 || Main.tile[n, num12].type == 226 || Main.tile[n, num12].type == 237 || Main.tile[n, num12].type == 221 || Main.tile[n, num12].type == 222 || Main.tile[n, num12].type == 223 || Main.tile[n, num12].type == 211 || Main.tile[n, num12].type == 404)
							{
								flag2 = false;
							}
							if (!Main.hardMode && Main.tile[n, num12].type == 58)
							{
								flag2 = false;
							}
							if (!TileLoader.CanExplode(n, num12))
							{
								flag2 = false;
							}
							if (flag2)
							{
								WorldGen.KillTile(n, num12, false, false, false);
								if (!Main.tile[n, num12].active() && Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 0, (float)n, (float)num12, 0f, 0, 0, 0);
								}
							}
						}
						if (flag2)
						{
							for (int num16 = n - 1; num16 <= n + 1; num16++)
							{
								for (int num17 = num12 - 1; num17 <= num12 + 1; num17++)
								{
									if (Main.tile[num16, num17] != null && Main.tile[num16, num17].wall > 0 && flag && WallLoader.CanExplode(num16, num17, (int)Main.tile[num16, num17].wall))
									{
										WorldGen.KillWall(num16, num17, false);
										if (Main.tile[num16, num17].wall == 0 && Main.netMode != 0)
										{
											NetMessage.SendData(17, -1, -1, null, 2, (float)num16, (float)num17, 0f, 0, 0, 0);
										}
									}
								}
							}
						}
					}
				}
			}
			AchievementsHelper.CurrentlyMining = false;
		}
	}
}
