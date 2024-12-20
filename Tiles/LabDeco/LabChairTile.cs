using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.LabDeco;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class LabChairTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.Direction = 1;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = 2;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Lab Chair");
			base.AddMapEntry(new Color(210, 220, 210), name);
			this.dustType = 226;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				15
			};
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<LabChair>(), 1, false, 0, false, false);
		}
	}
}
