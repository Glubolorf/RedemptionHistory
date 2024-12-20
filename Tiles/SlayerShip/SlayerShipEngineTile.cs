using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Datalogs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.SlayerShip
{
	public class SlayerShipEngineTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = 226;
			this.minPick = 200;
			this.mineResist = 10f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(110, 110, 130), null);
			this.animationFrameHeight = 108;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<SlayerShipEngine>(), 1, false, 0, false, false);
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 2)
			{
				frameCounter = 0;
				frame++;
				if (frame > 3)
				{
					frame = 0;
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
