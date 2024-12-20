using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class ClearTileSafely : GenAction
	{
		public ClearTileSafely(bool frameNeighbors = false)
		{
			this._frameNeighbors = frameNeighbors;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return false;
			}
			if (GenBase._tiles[x, y] == null)
			{
				GenBase._tiles[x, y] = new Tile();
			}
			GenBase._tiles[x, y].ClearTile();
			if (this._frameNeighbors)
			{
				WorldGen.TileFrame(x + 1, y, false, false);
				WorldGen.TileFrame(x - 1, y, false, false);
				WorldGen.TileFrame(x, y + 1, false, false);
				WorldGen.TileFrame(x, y - 1, false, false);
			}
			return base.UnitApply(origin, x, y, args);
		}

		private bool _frameNeighbors;
	}
}
