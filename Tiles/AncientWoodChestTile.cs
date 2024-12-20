using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class AncientWoodChestTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileContainer[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 500;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Ancient Wood Chest");
			base.AddMapEntry(new Color(200, 200, 200), modTranslation, new Func<string, int, int, string>(this.MapChestName));
			this.dustType = 78;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				21
			};
			this.chest = "Ancient Wood Chest";
			this.chestDrop = base.mod.ItemType("AncientWoodChest");
		}

		public string MapChestName(string name, int i, int j)
		{
			int num = i;
			int num2 = j;
			Tile tile = Main.tile[i, j];
			if (tile.frameX % 36 != 0)
			{
				num--;
			}
			if (tile.frameY != 0)
			{
				num2--;
			}
			int num3 = Chest.FindChest(num, num2);
			if (Main.chest[num3].name == "")
			{
				return name;
			}
			return name + ": " + Main.chest[num3].name;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, this.chestDrop, 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}

		public override void RightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			Main.mouseRightRelease = false;
			int num = i;
			int num2 = j;
			if (tile.frameX % 36 != 0)
			{
				num--;
			}
			if (tile.frameY != 0)
			{
				num2--;
			}
			if (localPlayer.sign >= 0)
			{
				Main.PlaySound(11, -1, -1, 1, 1f, 0f);
				localPlayer.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}
			if (Main.editChest)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Main.editChest = false;
				Main.npcChatText = "";
			}
			if (localPlayer.editedChestName)
			{
				NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[localPlayer.chest].name), localPlayer.chest, 1f, 0f, 0f, 0, 0, 0);
				localPlayer.editedChestName = false;
			}
			if (Main.netMode != 1)
			{
				int num3 = Chest.FindChest(num, num2);
				if (num3 >= 0)
				{
					Main.stackSplit = 600;
					if (num3 == localPlayer.chest)
					{
						localPlayer.chest = -1;
						Main.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
					else
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						localPlayer.chestX = num;
						localPlayer.chestY = num2;
						Main.PlaySound((localPlayer.chest < 0) ? 10 : 12, -1, -1, 1, 1f, 0f);
					}
					Recipe.FindRecipes();
				}
				return;
			}
			if (num == localPlayer.chestX && num2 == localPlayer.chestY && localPlayer.chest >= 0)
			{
				localPlayer.chest = -1;
				Recipe.FindRecipes();
				Main.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			NetMessage.SendData(31, -1, -1, null, num, (float)num2, 0f, 0f, 0, 0, 0);
			Main.stackSplit = 600;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int num = i;
			int num2 = j;
			if (tile.frameX % 36 != 0)
			{
				num--;
			}
			if (tile.frameY != 0)
			{
				num2--;
			}
			int num3 = Chest.FindChest(num, num2);
			localPlayer.showItemIcon2 = -1;
			if (num3 < 0)
			{
				localPlayer.showItemIconText = Lang.chestType[0].Value;
			}
			else
			{
				localPlayer.showItemIconText = ((Main.chest[num3].name.Length > 0) ? Main.chest[num3].name : "Ancient Wood Chest");
				if (localPlayer.showItemIconText == "Ancient Wood Chest")
				{
					localPlayer.showItemIcon2 = base.mod.ItemType("AncientWoodChest");
					localPlayer.showItemIconText = "";
				}
			}
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			this.MouseOver(i, j);
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer.showItemIconText == "")
			{
				localPlayer.showItemIcon = false;
				localPlayer.showItemIcon2 = 0;
			}
		}
	}
}
