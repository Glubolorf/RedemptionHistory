using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Tiles;
using Redemption.Tiles.Wasteland;
using Redemption.Walls;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XenoSolutionPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xeno Spray");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.friendly = true;
			base.projectile.alpha = 255;
			base.projectile.penetrate = -1;
			base.projectile.extraUpdates = 2;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			int dustType = ModContent.DustType<XenoSolutionDust>();
			if (base.projectile.owner == Main.myPlayer)
			{
				this.Convert((int)(base.projectile.position.X + (float)(base.projectile.width / 2)) / 16, (int)(base.projectile.position.Y + (float)(base.projectile.height / 2)) / 16, 2);
			}
			if (base.projectile.timeLeft > 133)
			{
				base.projectile.timeLeft = 133;
			}
			if (base.projectile.ai[0] > 7f)
			{
				float dustScale = 1f;
				if (base.projectile.ai[0] == 8f)
				{
					dustScale = 0.2f;
				}
				else if (base.projectile.ai[0] == 9f)
				{
					dustScale = 0.4f;
				}
				else if (base.projectile.ai[0] == 10f)
				{
					dustScale = 0.6f;
				}
				else if (base.projectile.ai[0] == 11f)
				{
					dustScale = 0.8f;
				}
				base.projectile.ai[0] += 1f;
				for (int i = 0; i < 1; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, dustType, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					Dust dust = Main.dust[dustIndex];
					dust.noGravity = true;
					dust.scale *= 1.75f;
					dust.velocity.X = dust.velocity.X * 2f;
					dust.velocity.Y = dust.velocity.Y * 2f;
					dust.scale *= dustScale;
				}
			}
			else
			{
				base.projectile.ai[0] += 1f;
			}
			base.projectile.rotation += 0.3f * (float)base.projectile.direction;
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
						if (type == 1 || type == 179 || type == 180 || type == 181 || type == 182 || type == 183)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<DeadRockTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 25)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<IrradiatedEbonstoneTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == 203)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<IrradiatedCrimstoneTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
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
	}
}
