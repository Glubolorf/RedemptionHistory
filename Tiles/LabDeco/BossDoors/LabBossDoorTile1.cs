using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco.BossDoors
{
	public class LabBossDoorTile1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileID.Sets.NotReallySolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, TileObjectData.newTile.Width, 0);
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
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Reinforced Door");
			base.AddMapEntry(new Color(80, 100, 80), name);
			this.dustType = 226;
			this.disableSmartCursor = true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 1);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 3);
			if (NPC.AnyNPCs(base.mod.NPCType("JanitorBot")))
			{
				if (Main.tile[left, top].frameX == 0)
				{
					Main.tileSolid[(int)base.Type] = true;
				}
				for (int x = left; x < left + 1; x++)
				{
					for (int y = top; y < top + 3; y++)
					{
						if (Main.tile[x, y].frameX < 18)
						{
							Tile tile = Main.tile[x, y];
							tile.frameX += 18;
						}
					}
				}
			}
			else
			{
				if (Main.tile[left, top].frameX >= 18)
				{
					Main.tileSolid[(int)base.Type] = false;
				}
				for (int x2 = left; x2 < left + 1; x2++)
				{
					for (int y2 = top; y2 < top + 3; y2++)
					{
						if (Main.tile[x2, y2].frameX >= 18)
						{
							Tile tile2 = Main.tile[x2, y2];
							tile2.frameX -= 18;
						}
					}
				}
			}
			return true;
		}
	}
}
