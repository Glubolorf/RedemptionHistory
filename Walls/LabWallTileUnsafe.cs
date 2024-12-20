using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class LabWallTileUnsafe : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			this.drop = ModContent.ItemType<LabPlatingWall>();
			base.AddMapEntry(new Color(100, 100, 100), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void KillWall(int i, int j, ref bool fail)
		{
			fail = true;
		}
	}
}
