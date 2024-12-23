﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Containers;
using Redemption.Items.Usable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Containers
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
			TileID.Sets.HasOutlines[(int)base.Type] = true;
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
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Lab Chest");
			base.AddMapEntry(new Color(0, 0, 255), name);
			this.dustType = 206;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				21
			};
			this.chest = "Lab Chest";
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Main.tile[i, j];
			if (tile.frameX % 36 != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}
			int chest = Chest.FindChest(left, top);
			if (Main.chest[chest].name == "")
			{
				return name;
			}
			return name + ": " + Main.chest[chest].name;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}
			return Chest.CanDestroyChest(left, top);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<LabChest>(), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}

		public override bool NewRightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			if (Main.LocalPlayer.HasItem(ModContent.ItemType<Keycard2>()))
			{
				Tile tile = Main.tile[i, j];
				Main.mouseRightRelease = false;
				int left = i;
				int top = j;
				if (tile.frameX % 36 != 0)
				{
					left--;
				}
				if (tile.frameY != 0)
				{
					top--;
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
					if (left == player.chestX && top == player.chestY && player.chest >= 0)
					{
						player.chest = -1;
						Recipe.FindRecipes();
						Main.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
					else
					{
						NetMessage.SendData(31, -1, -1, null, left, (float)top, 0f, 0f, 0, 0, 0);
						Main.stackSplit = 600;
					}
				}
				else
				{
					int chest = Chest.FindChest(left, top);
					if (chest >= 0)
					{
						Main.stackSplit = 600;
						if (chest == player.chest)
						{
							player.chest = -1;
							Main.PlaySound(11, -1, -1, 1, 1f, 0f);
						}
						else
						{
							player.chest = chest;
							Main.playerInventory = true;
							Main.recBigList = false;
							player.chestX = left;
							player.chestY = top;
							Main.PlaySound((player.chest < 0) ? 10 : 12, -1, -1, 1, 1f, 0f);
						}
						Recipe.FindRecipes();
					}
				}
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}
			int chest = Chest.FindChest(left, top);
			player.showItemIcon2 = -1;
			if (chest < 0)
			{
				player.showItemIconText = Lang.chestType[0].Value;
			}
			else
			{
				player.showItemIconText = ((Main.chest[chest].name.Length > 0) ? Main.chest[chest].name : "Lab Chest");
				if (player.showItemIconText == "Lab Chest")
				{
					player.showItemIcon2 = ModContent.ItemType<Keycard2>();
					player.showItemIconText = "";
				}
			}
			player.noThrow = 2;
			player.showItemIcon = true;
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
