using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeHangedCellTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Hanged Cage");
			base.AddMapEntry(new Color(45, 45, 45), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.minPick = 310;
			this.mineResist = 11f;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
