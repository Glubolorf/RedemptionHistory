using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DeadWoodWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.dustType = 78;
			this.drop = ModContent.ItemType<DeadWoodWall>();
			base.AddMapEntry(new Color(150, 150, 150), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
