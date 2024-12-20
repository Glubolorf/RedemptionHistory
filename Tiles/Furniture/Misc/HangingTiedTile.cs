using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Armor.Vanity;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class HangingTiedTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 26;
			this.disableSmartCursor = true;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Hanging Tied");
			base.AddMapEntry(new Color(81, 81, 81), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<OldTophat>(), 1, false, 0, false, false);
		}
	}
}
