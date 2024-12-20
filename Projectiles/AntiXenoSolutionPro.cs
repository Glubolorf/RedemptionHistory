using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class AntiXenoSolutionPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anti-Xeno Spray");
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
			int dustType = base.mod.DustType("AntiXenoSolutionDust");
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
						if (wall == base.mod.WallType("DeadRockWallTile"))
						{
							Main.tile[k, l].wall = 1;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == base.mod.WallType("RadioactiveSandstoneWallTile"))
						{
							Main.tile[k, l].wall = 187;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == base.mod.WallType("HardenedRadioactiveSandWallTile"))
						{
							Main.tile[k, l].wall = 216;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == base.mod.WallType("RadioactiveIceWallTile"))
						{
							Main.tile[k, l].wall = 71;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wall == base.mod.WallType("DeadGrassWallTile"))
						{
							Main.tile[k, l].wall = 63;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						if (type == base.mod.TileType("DeadRockTile"))
						{
							Main.tile[k, l].type = 1;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("IrradiatedEbonstoneTile"))
						{
							Main.tile[k, l].type = 25;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("IrradiatedCrimstoneTile"))
						{
							Main.tile[k, l].type = 203;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("DeadGrassTile"))
						{
							Main.tile[k, l].type = 2;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("DeadGrassTileCorruption"))
						{
							Main.tile[k, l].type = 23;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("DeadGrassTileCrimson"))
						{
							Main.tile[k, l].type = 199;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("RadioactiveIceTile"))
						{
							Main.tile[k, l].type = 161;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("RadioactiveSandstoneTile"))
						{
							Main.tile[k, l].type = 396;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("RadioactiveSandTile"))
						{
							Main.tile[k, l].type = 53;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("HardenedRadioactiveSandTile"))
						{
							Main.tile[k, l].type = 397;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("LivingDeadLeavesTile"))
						{
							Main.tile[k, l].type = 192;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("LivingDeadWoodTile"))
						{
							Main.tile[k, l].type = 191;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
					}
				}
			}
		}
	}
}
