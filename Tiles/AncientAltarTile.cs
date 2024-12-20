using System;
using Microsoft.Xna.Framework;
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
			localPlayer.showItemIcon2 = base.mod.ItemType("CursedGem");
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override void RightClick(int i, int j)
		{
			int num = i - (int)(Main.tile[i, j].frameX / 18 % 2);
			int num2 = j - (int)(Main.tile[i, j].frameY / 18 % 3);
			if (Main.tile[num, num2].frameX == 0)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.QuickSpawnItem(base.mod.ItemType("CursedGem"), 1);
			}
			for (int k = num; k < num + 2; k++)
			{
				for (int l = num2; l < num2 + 3; l++)
				{
					if (Main.tile[k, l].frameX < 36)
					{
						Tile tile = Main.tile[k, l];
						tile.frameX += 36;
					}
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
