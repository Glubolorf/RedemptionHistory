using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class SetMapBrightness : GenAction
	{
		public SetMapBrightness(byte brightness)
		{
			this._brightness = brightness;
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
			Main.Map.UpdateLighting(x, y, Math.Max(Main.Map[x, y].Light, this._brightness));
			return base.UnitApply(origin, x, y, args);
		}

		public byte _brightness;
	}
}
