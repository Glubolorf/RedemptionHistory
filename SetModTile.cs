using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class SetModTile : GenAction
	{
		public SetModTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
		{
			this._type = type;
			this._doFraming = setSelfFrames;
			this._doNeighborFraming = setNeighborFrames;
		}

		public SetModTile ExtraParams(Func<int, int, Tile, bool> canReplace)
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
				GenBase._tiles[x, y].ResetToType(this._type);
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
			}
			return base.UnitApply(origin, x, y, args);
		}

		public ushort _type;

		public bool _doFraming;

		public bool _doNeighborFraming;

		public Func<int, int, Tile, bool> _canReplace;
	}
}
