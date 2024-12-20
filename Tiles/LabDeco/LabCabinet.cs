using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class LabCabinet : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 226;
			this.minPick = 300;
			this.mineResist = 8f;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(180, 200, 200), null);
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = base.mod.ItemType("RadiationPill");
		}

		public override bool NewRightClick(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 2);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 2);
			if (Main.tile[left, top].frameX == 0)
			{
				Player player = Main.LocalPlayer;
				player.QuickSpawnItem(base.mod.ItemType("RadiationPill"), 1);
				if (Main.rand.Next(40) == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("EmptyMutagen"), 1);
				}
				if (Main.rand.Next(4) == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("FirstAidKit"), 1);
				}
				if (Main.rand.Next(20) == 0)
				{
					player.QuickSpawnItem(885, 1);
				}
				if (Main.rand.Next(20) == 0)
				{
					player.QuickSpawnItem(892, 1);
				}
				if (Main.rand.Next(66666) == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("Panacea"), 1);
				}
			}
			for (int x = left; x < left + 2; x++)
			{
				for (int y = top; y < top + 2; y++)
				{
					if (Main.tile[x, y].frameX < 36)
					{
						Tile tile = Main.tile[x, y];
						tile.frameX += 36;
					}
				}
			}
			return true;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
