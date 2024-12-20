using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class AncientStoneWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.drop = ModContent.ItemType<AncientStoneWall>();
			base.AddMapEntry(new Color(150, 150, 150), null);
		}
	}
}
