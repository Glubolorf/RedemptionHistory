using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Furniture.Shade;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeDoorOpen : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileLavaDeath[(int)base.Type] = true;
			Main.tileNoSunLight[(int)base.Type] = true;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(1, 1, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, 1, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.Direction = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(1, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(1, 1, 1);
			TileObjectData.newAlternate.Direction = 1;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 1);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(1, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(1, 1, 1);
			TileObjectData.newAlternate.Direction = 1;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 2);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(1, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(1, 1, 1);
			TileObjectData.newAlternate.Direction = 1;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			TileID.Sets.HousingWalls[(int)base.Type] = true;
			TileID.Sets.HasOutlines[(int)base.Type] = true;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shade Door");
			base.AddMapEntry(new Color(100, 100, 100), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				11
			};
			this.closeDoorID = ModContent.TileType<ShadeDoorClosed>();
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<ShadeDoor>(), 1, false, 0, false, false);
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<ShadeDoor>();
		}
	}
}
