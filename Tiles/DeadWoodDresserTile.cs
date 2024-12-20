using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class DeadWoodDresserTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = true;
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
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Petrified Wood Dresser");
			base.AddMapEntry(new Color(200, 200, 200), modTranslation);
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				88
			};
			this.dresser = "Petrified Dresser";
			this.dresserDrop = base.mod.ItemType("DeadWoodDresser");
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public override void RightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 0)
			{
				Main.CancelClothesWindow(true);
				Main.mouseRightRelease = false;
				int num = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
				num %= 3;
				num = Player.tileTargetX - num;
				int num2 = Player.tileTargetY - (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
				if (localPlayer.sign > -1)
				{
					Main.PlaySound(11, -1, -1, 1, 1f, 0f);
					localPlayer.sign = -1;
					Main.editSign = false;
					Main.npcChatText = string.Empty;
				}
				if (Main.editChest)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.editChest = false;
					Main.npcChatText = string.Empty;
				}
				if (localPlayer.editedChestName)
				{
					NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[localPlayer.chest].name), localPlayer.chest, 1f, 0f, 0f, 0, 0, 0);
					localPlayer.editedChestName = false;
				}
				if (Main.netMode == 1)
				{
					if (num == localPlayer.chestX && num2 == localPlayer.chestY && localPlayer.chest != -1)
					{
						localPlayer.chest = -1;
						Recipe.FindRecipes();
						Main.PlaySound(11, -1, -1, 1, 1f, 0f);
						return;
					}
					NetMessage.SendData(31, -1, -1, null, num, (float)num2, 0f, 0f, 0, 0, 0);
					Main.stackSplit = 600;
					return;
				}
				else
				{
					localPlayer.flyingPigChest = -1;
					int num3 = Chest.FindChest(num, num2);
					if (num3 != -1)
					{
						Main.stackSplit = 600;
						if (num3 == localPlayer.chest)
						{
							localPlayer.chest = -1;
							Recipe.FindRecipes();
							Main.PlaySound(11, -1, -1, 1, 1f, 0f);
						}
						else if (num3 != localPlayer.chest && localPlayer.chest == -1)
						{
							localPlayer.chest = num3;
							Main.playerInventory = true;
							Main.recBigList = false;
							Main.PlaySound(10, -1, -1, 1, 1f, 0f);
							localPlayer.chestX = num;
							localPlayer.chestY = num2;
						}
						else
						{
							localPlayer.chest = num3;
							Main.playerInventory = true;
							Main.recBigList = false;
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							localPlayer.chestX = num;
							localPlayer.chestY = num2;
						}
						Recipe.FindRecipes();
						return;
					}
				}
			}
			else
			{
				Main.playerInventory = false;
				localPlayer.chest = -1;
				Recipe.FindRecipes();
				Main.dresserX = Player.tileTargetX;
				Main.dresserY = Player.tileTargetY;
				Main.OpenClothesWindow();
			}
		}

		public override void MouseOverFar(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			int num = Player.tileTargetX;
			int num2 = Player.tileTargetY;
			num -= (int)(tile.frameX % 54 / 18);
			if (tile.frameY % 36 != 0)
			{
				num2--;
			}
			int num3 = Chest.FindChest(num, num2);
			localPlayer.showItemIcon2 = -1;
			if (num3 < 0)
			{
				localPlayer.showItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				if (Main.chest[num3].name != "")
				{
					localPlayer.showItemIconText = Main.chest[num3].name;
				}
				else
				{
					localPlayer.showItemIconText = this.chest;
				}
				if (localPlayer.showItemIconText == this.chest)
				{
					localPlayer.showItemIcon2 = base.mod.ItemType("DeadWoodDresser");
					localPlayer.showItemIconText = "";
				}
			}
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			if (localPlayer.showItemIconText == "")
			{
				localPlayer.showItemIcon = false;
				localPlayer.showItemIcon2 = 0;
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			int num = Player.tileTargetX;
			int num2 = Player.tileTargetY;
			num -= (int)(tile.frameX % 54 / 18);
			if (tile.frameY % 36 != 0)
			{
				num2--;
			}
			int num3 = Chest.FindChest(num, num2);
			localPlayer.showItemIcon2 = -1;
			if (num3 < 0)
			{
				localPlayer.showItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				if (Main.chest[num3].name != "")
				{
					localPlayer.showItemIconText = Main.chest[num3].name;
				}
				else
				{
					localPlayer.showItemIconText = this.chest;
				}
				if (localPlayer.showItemIconText == this.chest)
				{
					localPlayer.showItemIcon2 = base.mod.ItemType("DeadWoodDresser");
					localPlayer.showItemIconText = "";
				}
			}
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY > 0)
			{
				localPlayer.showItemIcon2 = 269;
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
