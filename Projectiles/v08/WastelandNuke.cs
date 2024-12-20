using System;
using Microsoft.Xna.Framework;
using Redemption.Tiles;
using Redemption.Tiles.Wasteland;
using Redemption.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	internal class WastelandNuke : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 15;
			base.projectile.height = 15;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 300;
			this.drawOffsetX = 5;
			this.drawOriginOffsetY = 5;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.timeLeft = 3;
			return false;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
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
				base.projectile.knockBack = 15f;
				this.Convert((int)base.projectile.Center.X / 16, (int)base.projectile.Center.Y / 16, 80);
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PlutoniumBoom>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom2>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
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
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && (double)(Math.Abs(k - i) + Math.Abs(l - j)) < Math.Sqrt((double)(size * size + size * size)))
					{
						int type = (int)Main.tile[k, l].type;
						int wall = (int)Main.tile[k, l].wall;
						if (wall == 1 || wall == 83 || wall == 3 || wall == 28)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<DeadRockWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == 187 || wall == 222 || wall == 221 || wall == 220)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<RadioactiveSandstoneWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == 216 || wall == 219 || wall == 218 || wall == 217)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<HardenedRadioactiveSandWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == 71)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<RadioactiveIceWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == 63 || wall == 69 || wall == 81 || wall == 70 || wall == 65 || wall == 64)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<DeadGrassWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == 78)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<LivingDeadWoodWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == 60)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<LivingDeadLeavesWallTile>();
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						if (TileID.Sets.Conversion.Stone[type] || type == 179 || type == 180 || type == 181 || type == 182 || type == 183)
						{
							if (type != 25)
							{
								Main.tile[k, l].type = (ushort)ModContent.TileType<DeadRockTile>();
								WorldGen.SquareTileFrame(k, l, true);
								NetMessage.SendTileSquare(-1, k, l, 1, 0);
							}
							else
							{
								Main.tile[k, l].type = (ushort)ModContent.TileType<IrradiatedEbonstoneTile>();
								WorldGen.SquareTileFrame(k, l, true);
								NetMessage.SendTileSquare(-1, k, l, 1, 0);
							}
							if (type != 203)
							{
								Main.tile[k, l].type = (ushort)ModContent.TileType<DeadRockTile>();
								WorldGen.SquareTileFrame(k, l, true);
								NetMessage.SendTileSquare(-1, k, l, 1, 0);
							}
							else
							{
								Main.tile[k, l].type = (ushort)ModContent.TileType<IrradiatedCrimstoneTile>();
								WorldGen.SquareTileFrame(k, l, true);
								NetMessage.SendTileSquare(-1, k, l, 1, 0);
							}
						}
						else if (type == 2 || type == 109)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<DeadGrassTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 23)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<DeadGrassTileCorruption>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 199)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<DeadGrassTileCrimson>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 53 || type == 234 || type == 112 || type == 116)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<RadioactiveSandTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 161 || type == 200 || type == 163 || type == 164)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<RadioactiveIceTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 396 || type == 403 || type == 401 || type == 400)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<RadioactiveSandstoneTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 397 || type == 402 || type == 399 || type == 398)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<HardenedRadioactiveSandTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 67 || type == 66 || type == 63 || type == 65 || type == 64 || type == 68)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<StarliteGemOreTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 191)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<LivingDeadWoodTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 192)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<LivingDeadLeavesTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 40; i++)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), 686, 70, 3f, 255, 0f, 0f);
			}
			for (int j = 0; j < 50; j++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int k = 0; k < 80; k++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex2].noGravity = true;
				Main.dust[dustIndex2].velocity *= 5f;
				dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
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
				goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
		}
	}
}
