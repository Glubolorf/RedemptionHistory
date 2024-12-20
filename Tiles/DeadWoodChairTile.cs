using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class DeadWoodChairTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18
			};
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Petrified Wood Chair");
			base.AddMapEntry(new Color(200, 200, 200), name);
			this.dustType = 214;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				18
			};
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<DeadWoodChair>(), 1, false, 0, false, false);
		}
	}
}
