using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.DeadWood;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DeadWoodFenceTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.dustType = 214;
			this.drop = ModContent.ItemType<DeadWoodFence>();
			base.AddMapEntry(new Color(150, 150, 150), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
