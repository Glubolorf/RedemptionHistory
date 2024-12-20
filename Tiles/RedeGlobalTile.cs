using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class RedeGlobalTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			if (type == 2 && !Main.dayTime && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(300) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("NightshadeTile"), true, false, -1, 0);
			}
			if (type == base.mod.TileType("DeadGrassTileCorruption") && type == base.mod.TileType("IrradiatedEbonstoneTile") && type == base.mod.TileType("DeadGrassTileCrimson") && type == base.mod.TileType("IrradiatedCrimstoneTile") && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(400) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("RadRootTile"), true, false, -1, 0);
			}
		}
	}
}
