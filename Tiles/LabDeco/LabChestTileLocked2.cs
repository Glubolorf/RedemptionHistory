using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class LabChestTileLocked2 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileContainer[(int)base.Type] = true;
			Main.tileShine2[(int)base.Type] = true;
			Main.tileShine[(int)base.Type] = 1200;
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
			modTranslation.SetDefault("Lab Chest");
			base.AddMapEntry(new Color(0, 0, 255), modTranslation);
			this.dustType = 206;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				21
			};
			this.chest = "Lab Chest";
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

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
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
			return Chest.CanDestroyChest(num, num2);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, base.mod.ItemType("LabChest"), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}

		public override void RightClick(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			foreach (Item item in player.inventory)
			{
				if (item.type == base.mod.ItemType("Keycard2"))
				{
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
					if (player.sign >= 0)
					{
						Main.PlaySound(11, -1, -1, 1, 1f, 0f);
						player.sign = -1;
						Main.editSign = false;
						Main.npcChatText = "";
					}
					if (Main.editChest)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Main.editChest = false;
						Main.npcChatText = "";
					}
					if (player.editedChestName)
					{
						NetMessage.SendData(33, -1, -1, null, player.chest, 1f, 0f, 0f, 0, 0, 0);
						player.editedChestName = false;
					}
					if (Main.netMode == 1)
					{
						if (num == player.chestX && num2 == player.chestY && player.chest >= 0)
						{
							player.chest = -1;
							Recipe.FindRecipes();
							Main.PlaySound(11, -1, -1, 1, 1f, 0f);
						}
						else
						{
							NetMessage.SendData(31, -1, -1, null, num, (float)num2, 0f, 0f, 0, 0, 0);
							Main.stackSplit = 600;
						}
					}
					else
					{
						int num3 = Chest.FindChest(num, num2);
						if (num3 >= 0)
						{
							Main.stackSplit = 600;
							if (num3 == player.chest)
							{
								player.chest = -1;
								Main.PlaySound(11, -1, -1, 1, 1f, 0f);
							}
							else
							{
								player.chest = num3;
								Main.playerInventory = true;
								Main.recBigList = false;
								player.chestX = num;
								player.chestY = num2;
								Main.PlaySound((player.chest < 0) ? 10 : 12, -1, -1, 1, 1f, 0f);
							}
							Recipe.FindRecipes();
						}
					}
				}
			}
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
				localPlayer.showItemIconText = ((Main.chest[num3].name.Length > 0) ? Main.chest[num3].name : "Lab Chest");
				if (localPlayer.showItemIconText == "Lab Chest")
				{
					localPlayer.showItemIcon2 = base.mod.ItemType("Keycard2");
					localPlayer.showItemIconText = "";
				}
			}
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			this.MouseOver(i, j);
			Player player = Main.player[Main.myPlayer];
			if (player.showItemIconText == "")
			{
				player.showItemIcon = false;
				player.showItemIcon2 = 0;
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
