using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class UltraVioletPlatingWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.drop = ModContent.ItemType<UltraVioletPlatingWall>();
			base.AddMapEntry(new Color(100, 0, 100), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
