using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class BrothersPaintingTileBig : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 5;
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("'Brothers?'");
			base.AddMapEntry(new Color(56, 36, 59), name);
			this.disableSmartCursor = true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<BrothersPaintingAlt>(), 1, false, 0, false, false);
		}
	}
}
