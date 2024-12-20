using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class ChickenCoopTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = 78;
			this.minPick = 0;
			this.mineResist = 1.2f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(120, 85, 60), null);
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.rand.Next(8) == 0)
			{
				Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<ChickenEgg>(), 1, false, 0, false, false);
			}
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 4;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<ChickenCoop>(), 1, false, 0, false, false);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
