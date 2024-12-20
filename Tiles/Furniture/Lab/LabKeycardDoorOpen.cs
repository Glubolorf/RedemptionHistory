using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabKeycardDoorOpen : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = false;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			TileID.Sets.NotReallySolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 3);
			TileObjectData.addAlternate(0);
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			TileID.Sets.HousingWalls[(int)base.Type] = true;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Keycard Door");
			base.AddMapEntry(new Color(189, 191, 200), name);
			this.minPick = 500;
			this.mineResist = 20f;
			this.dustType = 226;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				11
			};
			this.animationFrameHeight = 72;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 15)
			{
				frameCounter = 0;
				frame++;
				if (frame > 1)
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
