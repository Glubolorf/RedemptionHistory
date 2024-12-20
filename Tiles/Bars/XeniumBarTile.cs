using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Bars
{
	public class XeniumBarTile : ModTile
	{
		public override void SetDefaults()
		{
			this.soundType = 21;
			Main.tileShine[(int)base.Type] = 1100;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile((int)base.Type);
			this.drop = ModContent.ItemType<XeniumBar>();
			this.dustType = 8;
			base.AddMapEntry(new Color(10, 90, 10), null);
			this.minPick = 0;
		}
	}
}
