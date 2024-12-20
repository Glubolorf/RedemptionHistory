using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DeadRockWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			this.drop = ModContent.ItemType<DeadRockWall>();
			base.AddMapEntry(new Color(50, 50, 50), null);
		}
	}
}
