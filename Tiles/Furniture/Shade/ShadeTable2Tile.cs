using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Furniture.Shade;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeTable2Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 6;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(2, 1);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shadestone Table");
			base.AddMapEntry(new Color(65, 65, 65), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				14
			};
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<ShadeTable2>(), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
