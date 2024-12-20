using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Materials.HM;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Bars
{
	public class StarliteBarTile : ModTile
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
			this.drop = ModContent.ItemType<StarliteBar>();
			this.dustType = 8;
			base.AddMapEntry(new Color(40, 200, 100), null);
			this.minPick = 0;
		}
	}
}
