using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Plants
{
	public class ThornsTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileCut[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				24
			};
			TileObjectData.newTile.CoordinateWidth = 26;
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			base.CreateMapEntryName(null).SetDefault("Thorns");
			base.AddMapEntry(new Color(159, 208, 159), null);
			this.disableSmartCursor = true;
			this.dustType = 3;
			this.soundType = 6;
		}
	}
}
