using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class LabWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.drop = base.mod.ItemType("LabPlatingWall");
			base.AddMapEntry(new Color(100, 100, 100), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
