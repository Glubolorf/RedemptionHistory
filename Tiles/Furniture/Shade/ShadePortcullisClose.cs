using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadePortcullisClose : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			TileID.Sets.NotReallySolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
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
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shade Portcullis");
			base.AddMapEntry(new Color(130, 150, 130), name);
			this.minPick = 500;
			this.mineResist = 12f;
			this.dustType = 8;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				10
			};
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void HitWire(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 1);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 3);
			WorldGen.KillTile(i, j, false, false, true);
			WorldGen.PlaceObject(i, j, ModContent.TileType<ShadePortcullisOpen>(), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, i, j, ModContent.TileType<ShadePortcullisOpen>(), 0, 0, -1, -1);
			if (Wiring.running)
			{
				Wiring.SkipWire(left, top);
				Wiring.SkipWire(left, top + 1);
				Wiring.SkipWire(left, top + 2);
			}
			NetMessage.SendTileSquare(-1, left, top + 1, 2, 0);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
