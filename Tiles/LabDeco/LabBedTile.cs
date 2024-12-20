﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class LabBedTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Direction = 1;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = 2;
			TileObjectData.addAlternate(1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Hospital Bed");
			base.AddMapEntry(new Color(120, 120, 200), modTranslation);
			this.dustType = 226;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				79
			};
			this.bed = true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 64, 32, base.mod.ItemType("LabBed"), 1, false, 0, false, false);
		}

		public override void RightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int num = i - (int)(tile.frameX / 18);
			int num2 = j + 2;
			num += ((tile.frameX >= 72) ? 5 : 2);
			if (tile.frameY % 38 != 0)
			{
				num2--;
			}
			localPlayer.FindSpawn();
			if (localPlayer.SpawnX == num && localPlayer.SpawnY == num2)
			{
				localPlayer.RemoveSpawn();
				Main.NewText("Spawn point removed!", byte.MaxValue, 240, 20, false);
				return;
			}
			if (Player.CheckSpawn(num, num2))
			{
				localPlayer.ChangeSpawn(num, num2);
				Main.NewText("Spawn point set!", byte.MaxValue, 240, 20, false);
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = base.mod.ItemType("LabBed");
		}
	}
}