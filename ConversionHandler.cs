using System;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption
{
	internal class ConversionHandler
	{
		public static void ConvertDown(int centerX, int y, int width, ConversionType convertType)
		{
			int worldSize = ConversionHandler.GetWorldSize();
			int num = ((worldSize == 3) ? 200 : ((worldSize == 2) ? 160 : 130)) / 2;
			if (convertType == ConversionType.WASTELAND)
			{
				ConversionHandler.startXenoX = centerX;
				ConversionHandler.startXenoY = y;
				ConversionHandler.genXenoWidth = width;
				ThreadPool.QueueUserWorkItem(new WaitCallback(ConversionHandler.ConvertDownXenoCallback), null);
			}
		}

		public static int GetWorldSize()
		{
			int maxTilesX = Main.maxTilesX;
			if (maxTilesX == 4200)
			{
				return 1;
			}
			if (maxTilesX == 6400)
			{
				return 2;
			}
			if (maxTilesX != 8400)
			{
				return 1;
			}
			return 3;
		}

		public static void ConvertDownXenoCallback(object threadContext)
		{
			try
			{
				ConversionHandler.Do_ConvertDownXeno(threadContext);
			}
			catch (Exception)
			{
			}
		}

		public static void Do_ConvertDownXeno(object threadContext)
		{
			ConversionHandler.Dodo_ConvertDown(ConversionHandler.startXenoX, ConversionHandler.startXenoY, ConversionHandler.genXenoWidth, ConversionType.WASTELAND);
		}

		public static void Dodo_ConvertDown(int startX, int startY, int genWidth, ConversionType conversionType)
		{
			Mod mod = Redemption.inst;
			int tileGrass = 0;
			int tileCorruptGrass = 0;
			int tileCrimsonGrass = 0;
			int wallGrass = 0;
			int tileStone = 0;
			int tileCorruptStone = 0;
			int tileCrimsonStone = 0;
			int wallStone = 0;
			int tileSand = 0;
			int tileSandHard = 0;
			int wallSandHard = 0;
			int tileSandstone = 0;
			int wallSandstone = 0;
			int tileIce = 0;
			int wallIce = 0;
			int tileThorn = 0;
			int tileWood = 0;
			int tileLeaves = 0;
			int wallLeaves = 0;
			int livingwoodWall = 0;
			int tileGem = 0;
			if (conversionType == ConversionType.WASTELAND)
			{
				tileGrass = mod.TileType("DeadGrassTile");
				tileCorruptGrass = mod.TileType("DeadGrassTileCorruption");
				tileCrimsonGrass = mod.TileType("DeadGrassTileCrimson");
				wallGrass = mod.WallType("DeadGrassWallTile");
				tileStone = mod.TileType("DeadRockTile");
				tileCorruptStone = mod.TileType("IrradiatedEbonstoneTile");
				tileCrimsonStone = mod.TileType("IrradiatedCrimstoneTile");
				wallStone = mod.WallType("DeadRockWallTile");
				tileSand = mod.TileType("RadioactiveSandTile");
				tileSandHard = mod.TileType("HardenedRadioactiveSandTile");
				wallSandHard = mod.WallType("HardenedRadioactiveSandWallTile");
				tileSandstone = mod.TileType("RadioactiveSandstoneTile");
				wallSandstone = mod.WallType("RadioactiveSandstoneWallTile");
				tileIce = mod.TileType("RadioactiveIceTile");
				wallIce = mod.TileType("RadioactiveIceWallTile");
				tileWood = mod.TileType("LivingDeadWoodTile");
				tileLeaves = mod.TileType("LivingDeadLeavesTile");
				wallLeaves = mod.TileType("LivingDeadLeavesWallTile");
				livingwoodWall = mod.TileType("LivingDeadWoodWallTile");
				tileGem = mod.TileType("StarliteGemOreTile");
			}
			int y = startY;
			for (int x = 0; x < genWidth; x++)
			{
				while (y < Main.maxTilesY - 50)
				{
					ConversionHandler.Convert(startX + x, y, genWidth, tileGrass, tileCorruptGrass, tileCrimsonGrass, wallGrass, tileStone, tileCorruptStone, tileCrimsonStone, wallStone, tileSand, tileSandHard, wallSandHard, tileSandstone, wallSandstone, tileIce, wallIce, tileThorn, tileWood, tileLeaves, wallLeaves, livingwoodWall, tileGem);
					y += genWidth * 2;
				}
			}
		}

		public static void Convert(int i, int j, int size, int tileGrass, int tileCorruptGrass, int tileCrimsonGrass, int wallGrass, int tileStone, int tileCorruptStone, int tileCrimsonStone, int wallStone, int tileSand, int tileSandHard, int wallSandHard, int tileSandstone, int wallSandstone, int tileIce, int wallIce, int tileThorn, int tileWood, int tileLeaves, int wallLeaves, int wallWood, int tileGem = 0)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1))
					{
						int type = (int)Main.tile[k, l].type;
						int wall = (int)Main.tile[k, l].wall;
						if (wallGrass != 0 && WallID.Sets.Conversion.Grass[wall] && wall != wallGrass)
						{
							Main.tile[k, l].wall = (ushort)wallGrass;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wallStone != 0 && WallID.Sets.Conversion.Stone[wall] && wall != wallStone)
						{
							Main.tile[k, l].wall = (ushort)wallStone;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wallSandHard != 0 && WallID.Sets.Conversion.HardenedSand[wall] && wall != wallSandHard)
						{
							Main.tile[k, l].wall = (ushort)wallSandHard;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wallSandstone != 0 && WallID.Sets.Conversion.Sandstone[wall] && wall != wallSandstone)
						{
							Main.tile[k, l].wall = (ushort)wallSandstone;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wallIce != 0 && wall == 71 && wall != wallIce)
						{
							Main.tile[k, l].wall = (ushort)wallIce;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wallLeaves != 0 && wall == 60 && wall != wallLeaves)
						{
							Main.tile[k, l].wall = (ushort)wallLeaves;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (wallWood != 0 && wall == 78 && wall != wallLeaves)
						{
							Main.tile[k, l].wall = (ushort)wallWood;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						if (tileStone != 0 && (Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != tileStone && type != 25 && type != 203)
						{
							Main.tile[k, l].type = (ushort)tileStone;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileCorruptStone != 0 && type == 25 && type != tileCorruptStone)
						{
							Main.tile[k, l].type = (ushort)tileCorruptStone;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileCrimsonStone != 0 && type == 203 && type != tileCrimsonStone)
						{
							Main.tile[k, l].type = (ushort)tileCrimsonStone;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileGrass != 0 && TileID.Sets.Conversion.Grass[type] && type != tileGrass && type != 23 && type != 199)
						{
							Main.tile[k, l].type = (ushort)tileGrass;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileCorruptGrass != 0 && type == 23 && type != tileCorruptGrass)
						{
							Main.tile[k, l].type = (ushort)tileCorruptGrass;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileCrimsonGrass != 0 && type == 199 && type != tileCrimsonGrass)
						{
							Main.tile[k, l].type = (ushort)tileCrimsonGrass;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileIce != 0 && TileID.Sets.Conversion.Ice[type] && type != tileIce)
						{
							Main.tile[k, l].type = (ushort)tileIce;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileSand != 0 && TileID.Sets.Conversion.Sand[type] && type != tileSand)
						{
							Main.tile[k, l].type = (ushort)tileSand;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileSandHard != 0 && TileID.Sets.Conversion.HardenedSand[type] && type != tileSandHard)
						{
							Main.tile[k, l].type = (ushort)tileSandHard;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileSandstone != 0 && TileID.Sets.Conversion.Sandstone[type] && type != tileSandstone)
						{
							Main.tile[k, l].type = (ushort)tileSandstone;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileThorn != 0 && TileID.Sets.Conversion.Thorn[type] && type != tileThorn)
						{
							Main.tile[k, l].type = (ushort)tileThorn;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileWood != 0 && (type == 383 || type == 191) && type != tileWood)
						{
							Main.tile[k, l].type = (ushort)tileWood;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileLeaves != 0 && (type == 384 || type == 192) && type != tileLeaves)
						{
							Main.tile[k, l].type = (ushort)tileLeaves;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (tileGem != 0 && (type == 67 || type == 66 || type == 63 || type == 65 || type == 64 || type == 68) && type != tileGem)
						{
							Main.tile[k, l].type = (ushort)tileGem;
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
					}
				}
			}
		}

		public static int startXenoX = -1;

		public static int startXenoY = -1;

		public static int genXenoWidth = -1;
	}
}
