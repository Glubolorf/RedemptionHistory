using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Trophies;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Trophies
{
	public class XenomiteCrystalTrophyTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.disableSmartCursor = true;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Xenomite Crystal Trophy");
			base.AddMapEntry(new Color(120, 85, 60), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<XenomiteCrystalTrophy>(), 1, false, 0, false, false);
		}
	}
}
