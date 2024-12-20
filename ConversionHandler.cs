using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Tiles.Ores;
using Redemption.Tiles.Tiles;
using Redemption.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption
{
	internal class ConversionHandler
	{
		public static void ConvertWasteland(Vector2 Center, int radius = 287, bool crater = true)
		{
			if (crater)
			{
				Mod mod = Redemption.Inst;
				Tile tile = Framing.GetTileSafely((int)Center.X / 16, BaseWorldGen.GetFirstTileFloor((int)Center.X / 16, (int)Center.Y / 16, true));
				int tileType = (int)tile.type;
				if (tile.active() && (TileID.Sets.Conversion.Ice[tileType] || tileType == 147))
				{
					Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
					Color key = new Color(0, 255, 255);
					dictionary[key] = ModContent.TileType<PlutoniumTile>();
					Color black = new Color(150, 150, 150);
					dictionary[black] = -2;
					Color black2 = Color.Black;
					dictionary[black2] = -1;
					Dictionary<Color, int> colorToTile = dictionary;
					Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
					black2 = new Color(150, 150, 150);
					dictionary2[black2] = -2;
					black = Color.Black;
					dictionary2[black] = -1;
					Dictionary<Color, int> colorToWall = dictionary2;
					BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/CraterSnow"), colorToTile, mod.GetTexture("WorldGeneration/CraterWalls"), colorToWall, null, null, null, null).Generate((int)(Center.X / 16f) - 50, (int)(Center.Y / 16f) - 46, true, true);
				}
				else if (tile.active() && (TileID.Sets.Conversion.Sand[tileType] || TileID.Sets.Conversion.Sandstone[tileType] || TileID.Sets.Conversion.HardenedSand[tileType]))
				{
					Dictionary<Color, int> dictionary3 = new Dictionary<Color, int>();
					Color black = new Color(0, 255, 255);
					dictionary3[black] = ModContent.TileType<PlutoniumTile>();
					Color black2 = new Color(0, 0, 255);
					dictionary3[black2] = 396;
					Color key = new Color(255, 0, 0);
					dictionary3[key] = 54;
					Color black3 = new Color(150, 150, 150);
					dictionary3[black3] = -2;
					Color black4 = Color.Black;
					dictionary3[black4] = -1;
					Dictionary<Color, int> colorToTile2 = dictionary3;
					Dictionary<Color, int> dictionary4 = new Dictionary<Color, int>();
					black4 = new Color(150, 150, 150);
					dictionary4[black4] = -2;
					black3 = Color.Black;
					dictionary4[black3] = -1;
					Dictionary<Color, int> colorToWall2 = dictionary4;
					BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/Crater"), colorToTile2, mod.GetTexture("WorldGeneration/CraterWalls"), colorToWall2, null, null, null, null).Generate((int)(Center.X / 16f) - 50, (int)(Center.Y / 16f) - 46, true, true);
				}
				else
				{
					Dictionary<Color, int> dictionary5 = new Dictionary<Color, int>();
					Color black3 = new Color(0, 255, 255);
					dictionary5[black3] = ModContent.TileType<PlutoniumTile>();
					Color black4 = new Color(0, 0, 255);
					dictionary5[black4] = 0;
					Color key = new Color(255, 0, 0);
					dictionary5[key] = 57;
					Color black2 = new Color(150, 150, 150);
					dictionary5[black2] = -2;
					Color black = Color.Black;
					dictionary5[black] = -1;
					Dictionary<Color, int> colorToTile3 = dictionary5;
					Dictionary<Color, int> dictionary6 = new Dictionary<Color, int>();
					black = new Color(150, 150, 150);
					dictionary6[black] = -2;
					black2 = Color.Black;
					dictionary6[black2] = -1;
					Dictionary<Color, int> colorToWall3 = dictionary6;
					BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/Crater"), colorToTile3, mod.GetTexture("WorldGeneration/CraterWalls"), colorToWall3, null, null, null, null).Generate((int)(Center.X / 16f) - 50, (int)(Center.Y / 16f) - 46, true, true);
				}
			}
			BaseWorldGen.ReplaceTiles(Center, radius, new int[]
			{
				1,
				182,
				180,
				179,
				381,
				183,
				181,
				181,
				181,
				181,
				25,
				203,
				117,
				2,
				23,
				199,
				109,
				161,
				163,
				200,
				164,
				53,
				112,
				234,
				116,
				397,
				398,
				399,
				402,
				396,
				400,
				401,
				403,
				191,
				67,
				66,
				63,
				65,
				64,
				68
			}, new int[]
			{
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<IrradiatedEbonstoneTile>(),
				ModContent.TileType<IrradiatedCrimstoneTile>(),
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<DeadGrassTile>(),
				ModContent.TileType<DeadGrassTileCorruption>(),
				ModContent.TileType<DeadGrassTileCrimson>(),
				ModContent.TileType<DeadGrassTile>(),
				ModContent.TileType<RadioactiveIceTile>(),
				ModContent.TileType<RadioactiveIceTile>(),
				ModContent.TileType<RadioactiveIceTile>(),
				ModContent.TileType<RadioactiveIceTile>(),
				ModContent.TileType<RadioactiveSandTile>(),
				ModContent.TileType<RadioactiveSandTile>(),
				ModContent.TileType<RadioactiveSandTile>(),
				ModContent.TileType<RadioactiveSandTile>(),
				ModContent.TileType<HardenedRadioactiveSandTile>(),
				ModContent.TileType<HardenedRadioactiveSandTile>(),
				ModContent.TileType<HardenedRadioactiveSandTile>(),
				ModContent.TileType<HardenedRadioactiveSandTile>(),
				ModContent.TileType<RadioactiveSandstoneTile>(),
				ModContent.TileType<RadioactiveSandstoneTile>(),
				ModContent.TileType<RadioactiveSandstoneTile>(),
				ModContent.TileType<RadioactiveSandstoneTile>(),
				ModContent.TileType<LivingDeadWoodTile>(),
				ModContent.TileType<StarliteGemOreTile>(),
				ModContent.TileType<StarliteGemOreTile>(),
				ModContent.TileType<StarliteGemOreTile>(),
				ModContent.TileType<StarliteGemOreTile>(),
				ModContent.TileType<StarliteGemOreTile>(),
				ModContent.TileType<StarliteGemOreTile>()
			}, true, true);
			BaseWorldGen.ReplaceWalls(Center, 287, new int[]
			{
				63,
				69,
				81,
				70,
				1,
				3,
				83,
				28,
				216,
				217,
				218,
				219,
				187,
				220,
				221,
				222,
				71,
				60,
				78
			}, new int[]
			{
				ModContent.WallType<DeadGrassWallTile>(),
				ModContent.WallType<DeadGrassWallTile>(),
				ModContent.WallType<DeadGrassWallTile>(),
				ModContent.WallType<DeadGrassWallTile>(),
				ModContent.WallType<DeadRockWallTile>(),
				ModContent.WallType<DeadRockWallTile>(),
				ModContent.WallType<DeadRockWallTile>(),
				ModContent.WallType<DeadRockWallTile>(),
				ModContent.WallType<HardenedRadioactiveSandWallTile>(),
				ModContent.WallType<HardenedRadioactiveSandWallTile>(),
				ModContent.WallType<HardenedRadioactiveSandWallTile>(),
				ModContent.WallType<HardenedRadioactiveSandWallTile>(),
				ModContent.WallType<RadioactiveSandstoneWallTile>(),
				ModContent.WallType<RadioactiveSandstoneWallTile>(),
				ModContent.WallType<RadioactiveSandstoneWallTile>(),
				ModContent.WallType<RadioactiveSandstoneWallTile>(),
				ModContent.WallType<RadioactiveIceWallTile>(),
				ModContent.WallType<LivingDeadLeavesWallTile>(),
				ModContent.WallType<LivingDeadWoodWallTile>()
			}, true, true);
			int radiusLeft = (int)(Center.X / 16f - (float)radius);
			int radiusRight = (int)(Center.X / 16f + (float)radius);
			int radiusUp = (int)(Center.Y / 16f - (float)radius);
			int radiusDown = (int)(Center.Y / 16f + (float)radius);
			if (radiusLeft < 0)
			{
				radiusLeft = 0;
			}
			if (radiusRight > Main.maxTilesX)
			{
				radiusRight = Main.maxTilesX;
			}
			if (radiusUp < 0)
			{
				radiusUp = 0;
			}
			if (radiusDown > Main.maxTilesY)
			{
				radiusDown = Main.maxTilesY;
			}
			float distRad = (float)radius * 16f;
			for (int x = radiusLeft; x <= radiusRight; x++)
			{
				for (int y = radiusUp; y <= radiusDown; y++)
				{
					double dist = (double)Vector2.Distance(new Vector2((float)x * 16f + 8f, (float)y * 16f + 8f), Center);
					if (WorldGen.InWorld(x, y, 0) && dist < (double)distRad && Main.tile[x, y] != null && Main.tile[x, y].active() && Main.tile[x, y].type == 192)
					{
						WorldGen.KillTile(x, y, false, false, true);
					}
				}
			}
			if (Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, (int)(Center.X / 16f), (int)(Center.Y / 16f), radius * 2 + 2, 0);
			}
		}
	}
}
