using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class PlaceModWall : GenAction
	{
		public PlaceModWall(int type, bool neighbors = true)
		{
			this._type = (ushort)type;
			this._neighbors = neighbors;
		}

		public PlaceModWall ExtraParams(Func<int, int, Tile, bool> canReplace)
		{
			this._canReplace = canReplace;
			return this;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
			{
				return false;
			}
			if (GenBase._tiles[x, y] == null)
			{
				GenBase._tiles[x, y] = new Tile();
			}
			if (this._canReplace == null || (this._canReplace != null && this._canReplace(x, y, GenBase._tiles[x, y])))
			{
				GenBase._tiles[x, y].wall = this._type;
				WorldGen.SquareWallFrame(x, y, true);
				if (this._neighbors)
				{
					WorldGen.SquareWallFrame(x + 1, y, true);
					WorldGen.SquareWallFrame(x - 1, y, true);
					WorldGen.SquareWallFrame(x, y - 1, true);
					WorldGen.SquareWallFrame(x, y + 1, true);
				}
			}
			return base.UnitApply(origin, x, y, args);
		}

		public ushort _type;

		public bool _neighbors;

		public Func<int, int, Tile, bool> _canReplace;
	}
}
