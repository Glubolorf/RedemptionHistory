using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class AncientWoodPlatformTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolidTop[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileID.Sets.Platforms[(int)base.Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			base.AddMapEntry(new Color(200, 200, 200), null);
			this.dustType = 78;
			this.drop = ModContent.ItemType<AncientWoodPlatform>();
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				19
			};
		}

		public override void PostSetDefaults()
		{
			Main.tileNoSunLight[(int)base.Type] = false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
