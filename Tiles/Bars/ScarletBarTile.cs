using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass.DruidS;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Bars
{
	public class ScarletBarTile : ModTile
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
			this.drop = ModContent.ItemType<ScarletBar>();
			this.dustType = 8;
			base.AddMapEntry(new Color(255, 30, 30), null);
			this.minPick = 0;
		}
	}
}
