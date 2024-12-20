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
		}
	}
}
