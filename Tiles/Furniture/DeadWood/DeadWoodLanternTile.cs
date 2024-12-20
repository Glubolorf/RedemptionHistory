using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.DeadWood;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.DeadWood
{
	public class DeadWoodLanternTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = 0;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Petrified Wood Lantern");
			base.AddMapEntry(new Color(200, 200, 200), name);
			this.adjTiles = new int[]
			{
				42
			};
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void HitWire(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 1);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 2);
			for (int x = left; x < left + 1; x++)
			{
				for (int y = top; y < top + 2; y++)
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
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<DeadWoodLantern>(), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}
	}
}
