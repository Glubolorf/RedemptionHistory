using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	internal class PommisauvaBomb : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 180;
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
				base.projectile.width = 100;
				base.projectile.height = 100;
				base.projectile.position.X = base.projectile.position.X - (float)(base.projectile.width / 2);
				base.projectile.position.Y = base.projectile.position.Y - (float)(base.projectile.height / 2);
			}
			else if (Main.rand.Next(2) == 0)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].position = base.projectile.Center + Utils.RotatedBy(new Vector2(0f, -(float)base.projectile.height / 2f), (double)base.projectile.rotation, default(Vector2)) * 1.1f;
				dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].position = base.projectile.Center + Utils.RotatedBy(new Vector2(0f, -(float)base.projectile.height / 2f - 6f), (double)base.projectile.rotation, default(Vector2)) * 1.1f;
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
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<NoitaBombBlast>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int j = 0; j < 80; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<NoitaBombDust>(), 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex2].noGravity = true;
				Main.dust[dustIndex2].velocity *= 5f;
				dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<NoitaBombDust>(), 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex2].velocity *= 3f;
			}
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
			}
			base.projectile.position.X = base.projectile.position.X + (float)(base.projectile.width / 2);
			base.projectile.position.Y = base.projectile.position.Y + (float)(base.projectile.height / 2);
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.position.X = base.projectile.position.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = base.projectile.position.Y - (float)(base.projectile.height / 2);
			int explosionRadius = 5;
			int minTileX = (int)(base.projectile.position.X / 16f - (float)explosionRadius);
			int maxTileX = (int)(base.projectile.position.X / 16f + (float)explosionRadius);
			int minTileY = (int)(base.projectile.position.Y / 16f - (float)explosionRadius);
			int maxTileY = (int)(base.projectile.position.Y / 16f + (float)explosionRadius);
			if (minTileX < 0)
			{
				minTileX = 0;
			}
			if (maxTileX > Main.maxTilesX)
			{
				maxTileX = Main.maxTilesX;
			}
			if (minTileY < 0)
			{
				minTileY = 0;
			}
			if (maxTileY > Main.maxTilesY)
			{
				maxTileY = Main.maxTilesY;
			}
			bool canKillWalls = false;
			for (int x = minTileX; x <= maxTileX; x++)
			{
				for (int y = minTileY; y <= maxTileY; y++)
				{
					float num = Math.Abs((float)x - base.projectile.position.X / 16f);
					float diffY = Math.Abs((float)y - base.projectile.position.Y / 16f);
					if (Math.Sqrt((double)(num * num + diffY * diffY)) < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].wall == 0)
					{
						canKillWalls = true;
						break;
					}
				}
			}
			AchievementsHelper.CurrentlyMining = true;
			for (int k = minTileX; k <= maxTileX; k++)
			{
				for (int l = minTileY; l <= maxTileY; l++)
				{
					float num2 = Math.Abs((float)k - base.projectile.position.X / 16f);
					float diffY2 = Math.Abs((float)l - base.projectile.position.Y / 16f);
					if (Math.Sqrt((double)(num2 * num2 + diffY2 * diffY2)) < (double)explosionRadius)
					{
						bool canKillTile = true;
						if (Main.tile[k, l] != null && Main.tile[k, l].active())
						{
							canKillTile = true;
							if (Main.tileDungeon[(int)Main.tile[k, l].type] || Main.tile[k, l].type == 88 || Main.tile[k, l].type == 21 || Main.tile[k, l].type == 26 || Main.tile[k, l].type == 107 || Main.tile[k, l].type == 108 || Main.tile[k, l].type == 111 || Main.tile[k, l].type == 226 || Main.tile[k, l].type == 237 || Main.tile[k, l].type == 221 || Main.tile[k, l].type == 222 || Main.tile[k, l].type == 223 || Main.tile[k, l].type == 211 || Main.tile[k, l].type == 404)
							{
								canKillTile = false;
							}
							if (!Main.hardMode && Main.tile[k, l].type == 58)
							{
								canKillTile = false;
							}
							if (!TileLoader.CanExplode(k, l))
							{
								canKillTile = false;
							}
							if (canKillTile)
							{
								WorldGen.KillTile(k, l, false, false, false);
								if (!Main.tile[k, l].active() && Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 0, (float)k, (float)l, 0f, 0, 0, 0);
								}
							}
						}
						if (canKillTile)
						{
							for (int x2 = k - 1; x2 <= k + 1; x2++)
							{
								for (int y2 = l - 1; y2 <= l + 1; y2++)
								{
									if (Main.tile[x2, y2] != null && Main.tile[x2, y2].wall > 0 && canKillWalls && WallLoader.CanExplode(x2, y2, (int)Main.tile[x2, y2].wall))
									{
										WorldGen.KillWall(x2, y2, false);
										if (Main.tile[x2, y2].wall == 0 && Main.netMode != 0)
										{
											NetMessage.SendData(17, -1, -1, null, 2, (float)x2, (float)y2, 0f, 0, 0, 0);
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
