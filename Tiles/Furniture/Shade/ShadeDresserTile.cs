using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Furniture.Shade;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeDresserTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileContainer[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileID.Sets.HasOutlines[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
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
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shade Dresser");
			base.AddMapEntry(new Color(90, 90, 90), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				88
			};
			this.dresser = "Shade Dresser";
			this.dresserDrop = ModContent.ItemType<ShadeDresser>();
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public override bool NewRightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 0)
			{
				Main.CancelClothesWindow(true);
				Main.mouseRightRelease = false;
				int left = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
				left %= 3;
				left = Player.tileTargetX - left;
				int top = Player.tileTargetY - (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
				if (player.sign > -1)
				{
					Main.PlaySound(11, -1, -1, 1, 1f, 0f);
					player.sign = -1;
					Main.editSign = false;
					Main.npcChatText = string.Empty;
				}
				if (Main.editChest)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.editChest = false;
					Main.npcChatText = string.Empty;
				}
				if (player.editedChestName)
				{
					NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
					player.editedChestName = false;
				}
				if (Main.netMode == 1)
				{
					if (left == player.chestX && top == player.chestY && player.chest != -1)
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
					player.flyingPigChest = -1;
					int num213 = Chest.FindChest(left, top);
					if (num213 != -1)
					{
						Main.stackSplit = 600;
						if (num213 == player.chest)
						{
							player.chest = -1;
							Recipe.FindRecipes();
							Main.PlaySound(11, -1, -1, 1, 1f, 0f);
						}
						else if (num213 != player.chest && player.chest == -1)
						{
							player.chest = num213;
							Main.playerInventory = true;
							Main.recBigList = false;
							Main.PlaySound(10, -1, -1, 1, 1f, 0f);
							player.chestX = left;
							player.chestY = top;
						}
						else
						{
							player.chest = num213;
							Main.playerInventory = true;
							Main.recBigList = false;
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							player.chestX = left;
							player.chestY = top;
						}
						Recipe.FindRecipes();
					}
				}
			}
			else
			{
				Main.playerInventory = false;
				player.chest = -1;
				Recipe.FindRecipes();
				Main.dresserX = Player.tileTargetX;
				Main.dresserY = Player.tileTargetY;
				Main.OpenClothesWindow();
			}
			return true;
		}

		public override void MouseOverFar(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			int tileTargetX = Player.tileTargetX;
			int top = Player.tileTargetY;
			int num = tileTargetX - (int)(tile.frameX % 54 / 18);
			if (tile.frameY % 36 != 0)
			{
				top--;
			}
			int chestIndex = Chest.FindChest(num, top);
			player.showItemIcon2 = -1;
			if (chestIndex < 0)
			{
				player.showItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				if (Main.chest[chestIndex].name != "")
				{
					player.showItemIconText = Main.chest[chestIndex].name;
				}
				else
				{
					player.showItemIconText = this.chest;
				}
				if (player.showItemIconText == this.chest)
				{
					player.showItemIcon2 = ModContent.ItemType<ShadeDresser>();
					player.showItemIconText = "";
				}
			}
			player.noThrow = 2;
			player.showItemIcon = true;
			if (player.showItemIconText == "")
			{
				player.showItemIcon = false;
				player.showItemIcon2 = 0;
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			int tileTargetX = Player.tileTargetX;
			int top = Player.tileTargetY;
			int num139 = tileTargetX - (int)(tile.frameX % 54 / 18);
			if (tile.frameY % 36 != 0)
			{
				top--;
			}
			int num138 = Chest.FindChest(num139, top);
			player.showItemIcon2 = -1;
			if (num138 < 0)
			{
				player.showItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				if (Main.chest[num138].name != "")
				{
					player.showItemIconText = Main.chest[num138].name;
				}
				else
				{
					player.showItemIconText = this.chest;
				}
				if (player.showItemIconText == this.chest)
				{
					player.showItemIcon2 = ModContent.ItemType<ShadeDresser>();
					player.showItemIconText = "";
				}
			}
			player.noThrow = 2;
			player.showItemIcon = true;
			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY > 0)
			{
				player.showItemIcon2 = 269;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, this.dresserDrop, 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}
	}
}
