using System;
using Redemption.Items.Placeable.Furniture.Shade;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeCrateTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileTable[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile((int)base.Type);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 64, 32, ModContent.ItemType<ShadeCrate>(), 1, false, 0, false, false);
		}
	}
}
