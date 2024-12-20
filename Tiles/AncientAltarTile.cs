using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class AncientAltarTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 3f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(120, 190, 40), null);
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<CursedGem>();
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override bool NewRightClick(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 2);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 3);
			if (Main.tile[left, top].frameX == 0)
			{
				Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<CursedGem>(), 1);
			}
			for (int x = left; x < left + 2; x++)
			{
				for (int y = top; y < top + 3; y++)
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
