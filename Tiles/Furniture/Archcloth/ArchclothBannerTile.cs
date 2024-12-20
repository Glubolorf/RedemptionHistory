using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Archcloth;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Archcloth
{
	public class ArchclothBannerTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.addTile((int)base.Type);
			this.disableSmartCursor = true;
			base.AddMapEntry(new Color(123, 44, 122), null);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 48, ModContent.ItemType<ArchclothBanner>(), 1, false, 0, false, false);
		}
	}
}
