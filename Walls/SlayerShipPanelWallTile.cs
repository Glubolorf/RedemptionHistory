using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class SlayerShipPanelWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			base.AddMapEntry(new Color(80, 80, 80), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
