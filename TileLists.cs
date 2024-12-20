using System;
using System.Collections.Generic;
using Redemption.Tiles.Containers;
using Redemption.Tiles.Ores;
using Terraria.ModLoader;

namespace Redemption
{
	public static class TileLists
	{
		public static List<int> CorruptTiles = new List<int>
		{
			23,
			25,
			112,
			163,
			398,
			400
		};

		public static List<int> CrimsonTiles = new List<int>
		{
			199,
			203,
			234,
			200,
			399,
			401,
			205
		};

		public static List<int> EvilTiles = new List<int>
		{
			23,
			25,
			112,
			163,
			398,
			400,
			199,
			203,
			234,
			200,
			399,
			401,
			205
		};

		public static List<int> HallowTiles = new List<int>
		{
			109,
			117,
			116,
			164,
			402,
			403,
			115
		};

		public static List<int> ForestTiles = new List<int>
		{
			2
		};

		public static List<int> GloomTiles = new List<int>
		{
			59,
			70,
			194
		};

		public static List<int> GlowingMushTiles = new List<int>
		{
			59,
			70,
			190
		};

		public static List<int> CloudTiles = new List<int>
		{
			189,
			196,
			460
		};

		public static List<int> HellTiles = new List<int>
		{
			57,
			198,
			58,
			76,
			75
		};

		public static List<int> SnowTiles = new List<int>
		{
			161,
			206,
			164,
			200,
			163,
			162,
			147,
			148
		};

		public static List<int> DesertTiles = new List<int>
		{
			53,
			396,
			397,
			403,
			402,
			401,
			399,
			400,
			398
		};

		public static List<int> JungleTiles = new List<int>
		{
			59,
			120,
			60
		};

		public static List<int> DirtTiles = new List<int>
		{
			0,
			59,
			40
		};

		public static List<int> OreTiles = new List<int>
		{
			408,
			7,
			166,
			6,
			167,
			9,
			168,
			8,
			169,
			22,
			204,
			37,
			58,
			107,
			221,
			108,
			222,
			111,
			223,
			211,
			ModContent.TileType<DragonLeadOreTile>(),
			ModContent.TileType<KaniteOreTile>(),
			ModContent.TileType<SapphironOreTile>(),
			ModContent.TileType<ScarlionOreTile>(),
			ModContent.TileType<StarliteOreTile>(),
			ModContent.TileType<XenomiteOreBlock>()
		};

		public static List<int> HotTiles = new List<int>
		{
			53,
			396,
			397,
			403,
			402,
			401,
			399,
			400,
			398,
			57,
			198,
			58,
			76,
			75
		};

		public static List<int> NatureTiles = new List<int>
		{
			2,
			59,
			120,
			60
		};

		public static List<int> WhitelistTiles = new List<int>
		{
			41,
			43,
			44,
			226,
			444
		};

		public static List<int> ModdedChests = new List<int>
		{
			ModContent.TileType<AncientWoodChestTile>(),
			ModContent.TileType<DeadWoodChestTile>(),
			ModContent.TileType<LabChestTile>(),
			ModContent.TileType<LabChestTileLocked>(),
			ModContent.TileType<LabChestTileLocked2>(),
			ModContent.TileType<ShadeChestTile>(),
			ModContent.TileType<HolochestTile>()
		};
	}
}
