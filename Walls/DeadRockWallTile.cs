using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DeadRockWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			this.drop = base.mod.ItemType("DeadRockWall");
			base.AddMapEntry(new Color(50, 50, 50), null);
		}
	}
}
