using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class AncientStoneBrickWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.drop = ModContent.ItemType<AncientStoneBrickWall>();
			base.AddMapEntry(new Color(150, 150, 150), null);
		}
	}
}
