using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Bars
{
	public class LightSteelBarTile : ModTile
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
			this.drop = ModContent.ItemType<LightSteel>();
			this.dustType = 8;
			base.AddMapEntry(new Color(200, 200, 200), null);
			this.minPick = 0;
		}
	}
}
