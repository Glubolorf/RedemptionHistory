using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.AncientWood;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.AncientWood
{
	public class AncientWoodBedTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Ancient Wood Bed");
			base.AddMapEntry(new Color(200, 200, 200), name);
			this.dustType = 78;
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
			Item.NewItem(i * 16, j * 16, 64, 32, ModContent.ItemType<AncientWoodBed>(), 1, false, 0, false, false);
		}

		public override bool NewRightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int spawnX = i - (int)(tile.frameX / 18);
			int spawnY = j + 2;
			spawnX += ((tile.frameX >= 72) ? 5 : 2);
			if (tile.frameY % 38 != 0)
			{
				spawnY--;
			}
			player.FindSpawn();
			if (player.SpawnX == spawnX && player.SpawnY == spawnY)
			{
				player.RemoveSpawn();
				Main.NewText("Spawn point removed!", byte.MaxValue, 240, 20, false);
			}
			else if (Player.CheckSpawn(spawnX, spawnY))
			{
				player.ChangeSpawn(spawnX, spawnY);
				Main.NewText("Spawn point set!", byte.MaxValue, 240, 20, false);
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<AncientWoodBed>();
		}
	}
}
