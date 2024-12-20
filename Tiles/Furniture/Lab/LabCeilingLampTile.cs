using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Lab;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabCeilingLampTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(9, 1, 1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.StyleWrapLimit = 37;
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Ceiling Lamp");
			base.AddMapEntry(new Color(150, 150, 200), name);
			this.dustType = 226;
			this.adjTiles = new int[]
			{
				34
			};
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void HitWire(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 3);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 2);
			for (int x = left; x < left + 3; x++)
			{
				for (int y = top; y < top + 3; y++)
				{
					if (Main.tile[x, y].frameX >= 54)
					{
						Tile tile = Main.tile[x, y];
						tile.frameX -= 54;
					}
					else
					{
						Tile tile2 = Main.tile[x, y];
						tile2.frameX += 54;
					}
				}
			}
			if (Wiring.running)
			{
				Wiring.SkipWire(left, top);
				Wiring.SkipWire(left, top + 1);
				Wiring.SkipWire(left + 1, top);
				Wiring.SkipWire(left + 1, top + 1);
			}
			NetMessage.SendTileSquare(-1, left, top + 1, 2, 0);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameX < 36)
			{
				r = 0.6f;
				g = 0.6f;
				b = 0.8f;
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<LabCeilingLamp>(), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}
	}
}
