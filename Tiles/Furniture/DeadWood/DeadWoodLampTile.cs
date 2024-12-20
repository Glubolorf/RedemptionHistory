using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.DeadWood;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.DeadWood
{
	public class DeadWoodLampTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileWaterDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
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
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.WaterPlacement = 1;
			TileObjectData.newTile.LavaPlacement = 1;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Petrified Wood Lamp");
			base.AddMapEntry(new Color(200, 200, 200), name);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void HitWire(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 1);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 3);
			for (int x = left; x < left + 1; x++)
			{
				for (int y = top; y < top + 3; y++)
				{
					if (Main.tile[x, y].frameX >= 18)
					{
						Tile tile = Main.tile[x, y];
						tile.frameX -= 18;
					}
					else
					{
						Tile tile2 = Main.tile[x, y];
						tile2.frameX += 18;
					}
				}
			}
			if (Wiring.running)
			{
				Wiring.SkipWire(left, top);
				Wiring.SkipWire(left, top + 1);
				Wiring.SkipWire(left, top + 2);
			}
			NetMessage.SendTileSquare(-1, left, top + 1, 2, 0);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameX < 18)
			{
				r = 0.7f;
				g = 0.7f;
				b = 0.7f;
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<DeadWoodLamp>(), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}
	}
}
