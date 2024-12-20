using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable.Summons;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Plants
{
	public class HeartOfThornsTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileObsidianKill[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Heart of Thorns");
			base.AddMapEntry(new Color(144, 244, 144), name);
			this.disableSmartCursor = true;
			this.dustType = 3;
			this.soundType = 6;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 4;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<HeartOfTheThorns>(), 1, false, 0, false, false);
		}
	}
}
