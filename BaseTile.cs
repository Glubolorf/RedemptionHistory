using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace Redemption
{
	public class BaseTile
	{
		public static void AddMapEntry(ModTile tile, Color color)
		{
			tile.AddMapEntry(color, null);
		}

		public static void AddMapEntry(ModTile tile, Color color, string name)
		{
			ModTranslation modTranslation = tile.CreateMapEntryName(null);
			modTranslation.SetDefault(name);
			tile.AddMapEntry(color, modTranslation);
		}

		public static Chest GetClosestVanillaChest(Vector2 origin, float distance, int chestStyle = -1, int special = -1)
		{
			return BaseTile.GetClosestVanillaChest(origin, distance, (chestStyle == -1) ? null : new int[]
			{
				chestStyle
			}, special);
		}

		public static Chest GetClosestVanillaChest(Vector2 origin, float distance, int[] chestStyles = null, int special = -1)
		{
			Chest[] vanillaChestsNear = BaseTile.GetVanillaChestsNear(origin, distance, chestStyles, special);
			if (vanillaChestsNear.Length == 0)
			{
				return null;
			}
			Chest result = null;
			foreach (Chest chest in vanillaChestsNear)
			{
				float num = Vector2.Distance(origin, new Vector2((float)(chest.x * 16), (float)(chest.y * 16)));
				if (distance == -1f || num < distance)
				{
					distance = num;
					result = chest;
				}
			}
			return result;
		}

		public static Chest[] GetVanillaChestsNear(Vector2 origin, float distance, int chestStyle = -1, int special = -1)
		{
			return BaseTile.GetVanillaChestsNear(origin, distance, (chestStyle == -1) ? null : new int[]
			{
				chestStyle
			}, special);
		}

		public static Chest[] GetVanillaChestsNear(Vector2 origin, float distance, int[] chestStyles = null, int special = -1)
		{
			List<Chest> list = new List<Chest>();
			for (int i = 0; i < Main.chest.Length; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					int x = chest.x;
					int num = chest.y;
					if (distance == -1f || Vector2.Distance(origin, new Vector2((float)x * 16f + 8f, (float)num * 16f + 8f)) <= distance)
					{
						Tile tile = Main.tile[x, num];
						if (tile != null && tile.active() && tile.type == 21 && (chestStyles == null || BaseTile.IsVanillaChestOfStyle(chest, chestStyles)))
						{
							if (special == -1)
							{
								list.Add(chest);
							}
							else
							{
								if (tile.frameY == 0)
								{
									num++;
								}
								if (num + 1 <= Main.maxTilesY && special == 0 && BaseUtility.InArray(BaseConstants.TILEIDS_DUNGEONSTRICT, (int)Main.tile[x, num + 1].type))
								{
									list.Add(chest);
								}
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		public static Chest[] GetVanillaChests(int minY, int maxY, int chestStyle = -1, int special = -1)
		{
			return BaseTile.GetVanillaChests(minY, maxY, (chestStyle == -1) ? null : new int[]
			{
				chestStyle
			}, special);
		}

		public static Chest[] GetVanillaChests(int minY, int maxY, int[] chestStyles = null, int special = -1)
		{
			List<Chest> list = new List<Chest>();
			for (int i = 0; i < Main.chest.Length; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					int x = chest.x;
					int num = chest.y;
					if (num >= minY && num <= maxY)
					{
						Tile tile = Main.tile[x, num];
						if (tile != null && tile.active() && tile.type == 21 && (chestStyles == null || BaseTile.IsVanillaChestOfStyle(chest, chestStyles)))
						{
							if (special == -1)
							{
								list.Add(chest);
							}
							else
							{
								if (tile.frameY == 0)
								{
									num++;
								}
								if (num + 1 <= Main.maxTilesY && special == 0 && BaseUtility.InArray(BaseConstants.TILEIDS_DUNGEONSTRICT, (int)Main.tile[x, num + 1].type))
								{
									list.Add(chest);
								}
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		public static bool IsVanillaChestOfStyle(Chest chest, int chestStyle)
		{
			return BaseTile.IsVanillaChestOfStyle(chest.x, chest.y, chestStyle);
		}

		public static bool IsVanillaChestOfStyle(int x, int y, int chestStyle)
		{
			return BaseTile.IsVanillaChestOfStyle(x, y, new int[]
			{
				chestStyle
			});
		}

		public static bool IsVanillaChestOfStyle(Chest chest, int[] chestStyles)
		{
			return BaseTile.IsVanillaChestOfStyle(chest.x, chest.y, chestStyles);
		}

		public static bool IsVanillaChestOfStyle(int x, int y, int[] chestStyles)
		{
			x = Math.Max(0, Math.Min(Main.maxTilesX, x));
			y = Math.Max(0, Math.Min(Main.maxTilesY, y));
			Tile tile = Main.tile[x, y];
			if (tile != null && tile.active() && tile.type == 21)
			{
				foreach (int num in chestStyles)
				{
					if (tile.frameX == (short)(36 * num) || tile.frameX == (short)(36 * num) + 18)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool IsVanillaChestLocked(Chest chest)
		{
			return BaseTile.IsVanillaChestLocked(chest.x, chest.y);
		}

		public static bool IsVanillaChestLocked(int x, int y)
		{
			for (int i = x; i <= x + 1; i++)
			{
				for (int j = y; j <= y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if ((Main.tile[i, j].frameX >= 72 && Main.tile[i, j].frameX <= 106) || (Main.tile[i, j].frameX >= 144 && Main.tile[i, j].frameX <= 178))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static void SetTileFrame(int x, int y, int tileWidth, int tileHeight, int frame, int tileFrameWidth = 16)
		{
			ushort type = Main.tile[x, y].type;
			int num = (tileFrameWidth + 2) * tileWidth;
			for (int i = 0; i < tileWidth; i++)
			{
				for (int j = 0; j < tileHeight; j++)
				{
					int num2 = x + i;
					int num3 = y + j;
					Main.tile[num2, num3].frameX = (short)(frame * num + (tileFrameWidth + 2) * i);
				}
			}
		}

		public static Vector2 GetClosestTile(int x, int y, int type, int distance = 25, Func<Tile, bool> addTile = null)
		{
			Vector2 vector;
			vector..ctor((float)x, (float)y);
			int num = Math.Max(10, x - distance);
			int num2 = Math.Max(10, y - distance);
			int num3 = Math.Min(Main.maxTilesX - 10, x + distance);
			int num4 = Math.Min(Main.maxTilesY - 10, y + distance);
			Vector2 result = default(Vector2);
			float num5 = -1f;
			for (int i = num; i < num3; i++)
			{
				for (int j = num2; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && (int)tile.type == type && (addTile == null || addTile(tile)) && (num5 == -1f || Vector2.Distance(vector, new Vector2((float)i, (float)j)) < num5))
					{
						num5 = Vector2.Distance(vector, new Vector2((float)i, (float)j));
						if (type == 21 || TileObjectData.GetTileData((int)tile.type, 0, 0).Width > 1 || TileObjectData.GetTileData((int)tile.type, 0, 0).Height > 1)
						{
							int num6 = i;
							int num7 = j;
							if (type == 21)
							{
								num6 -= (int)(tile.frameX / 18 % 2);
								num7 -= (int)(tile.frameY / 18 % 2);
							}
							else
							{
								Vector2 vector2 = BaseTile.FindTopLeft(num6, num7);
								num6 = (int)vector2.X;
								num7 = (int)vector2.Y;
							}
							result..ctor((float)num6, (float)num7);
						}
						else
						{
							result..ctor((float)i, (float)j);
						}
					}
				}
			}
			return result;
		}

		public static Point FindTopLeftPoint(int x, int y)
		{
			Vector2 vector = BaseTile.FindTopLeft(x, y);
			return new Point((int)vector.X, (int)vector.Y);
		}

		public static Vector2 FindTopLeft(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null)
			{
				return new Vector2((float)x, (float)y);
			}
			TileObjectData tileData = TileObjectData.GetTileData((int)tile.type, 0, 0);
			x -= (int)(tile.frameX / 18) % tileData.Width;
			y -= (int)(tile.frameY / 18) % tileData.Height;
			return new Vector2((float)x, (float)y);
		}

		public static Vector2[] GetTiles(int x, int y, int type, int distance = 25, Func<Tile, bool> addTile = null)
		{
			int num = Math.Max(10, x - distance);
			int num2 = Math.Max(10, y - distance);
			int num3 = Math.Min(Main.maxTilesX - 10, x + distance);
			int num4 = Math.Min(Main.maxTilesY - 10, y + distance);
			List<Vector2> list = new List<Vector2>();
			for (int i = num; i < num3; i++)
			{
				for (int j = num2; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && (int)tile.type == type && (addTile == null || addTile(tile)))
					{
						if (type == 21 || TileObjectData.GetTileData(tile).Width > 1 || TileObjectData.GetTileData(tile).Height > 1)
						{
							int num5 = i;
							int num6 = j;
							if (type == 21)
							{
								num5 -= (int)(tile.frameX / 18 % 2);
								num6 -= (int)(tile.frameY / 18 % 2);
							}
							else
							{
								Point point = BaseTile.FindTopLeftPoint(num5, num6);
								num5 = point.X;
								num6 = point.Y;
							}
							Vector2 item;
							item..ctor((float)num5, (float)num6);
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
						else
						{
							list.Add(new Vector2((float)i, (float)j));
						}
					}
				}
			}
			return list.ToArray();
		}

		public static int LiquidCount(int x, int y, int distance = 25, int liquidType = 0)
		{
			int num = 0;
			int num2 = Math.Max(10, x - distance);
			int num3 = Math.Max(10, y - distance);
			int num4 = Math.Min(Main.maxTilesX - 10, x + distance);
			int num5 = Math.Min(Main.maxTilesY - 10, y + distance);
			for (int i = num2; i < num4; i++)
			{
				for (int j = num3; j < num5; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.liquid > 0 && ((liquidType == 0) ? tile.water() : ((liquidType == 1) ? tile.lava() : tile.honey())))
					{
						num += (int)tile.liquid;
					}
				}
			}
			return num;
		}

		public static bool IsPlatform(int type)
		{
			return Main.tileSolid[type] && Main.tileSolidTop[type];
		}

		public static bool AlchemyFlower(int type)
		{
			return type == 82 || type == 83 || type == 84;
		}

		public static int GetTileDust(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.active())
			{
				return -1;
			}
			return BaseTile.GetTileDust((int)tile.type, (int)tile.frameX, (int)tile.frameY);
		}

		public static int GetTileDust(int type, int frameX = 0, int frameY = 0)
		{
			int result = 0;
			if (type == 216)
			{
				result = -1;
			}
			if (type == 335)
			{
				result = -1;
			}
			if (type == 338)
			{
				result = -1;
			}
			if (type == 0)
			{
				result = 0;
			}
			if (type == 192)
			{
				result = 3;
			}
			if (type == 208)
			{
				result = 126;
			}
			if (type == 16)
			{
				result = 1;
				if (frameX >= 36)
				{
					result = 82;
				}
			}
			if (type == 1 || type == 17 || type == 38 || type == 39 || type == 41 || type == 43 || type == 44 || type == 48 || Main.tileStone[type] || type == 85 || type == 90 || type == 92 || type == 96 || type == 97 || type == 99 || type == 117 || type == 130 || type == 131 || type == 132 || type == 135 || type == 135 || type == 142 || type == 143 || type == 144 || type == 210 || type == 207 || type == 235 || type == 247 || type == 272 || type == 273 || type == 283)
			{
				result = 1;
			}
			if (type == 311)
			{
				result = 207;
			}
			if (type == 312)
			{
				result = 208;
			}
			if (type == 313)
			{
				result = 209;
			}
			if (type == 104)
			{
				result = -1;
			}
			if (type == 95 || type == 98 || type == 100 || type == 174 || type == 173)
			{
				result = 6;
			}
			if (type == 30 || type == 86 || type == 94 || type == 106 || type == 114 || type == 124 || type == 128 || type == 269)
			{
				result = 7;
			}
			if (type == 334)
			{
				result = 7;
			}
			if (type <= 89)
			{
				switch (type)
				{
				case 10:
				case 11:
					return -1;
				default:
					switch (type)
					{
					case 87:
					case 89:
						return -1;
					}
					break;
				}
			}
			else
			{
				if (type == 93 || type == 139)
				{
					return -1;
				}
				switch (type)
				{
				case 319:
				case 320:
					return -1;
				}
			}
			if (type == 240)
			{
				int num = frameX / 54;
				if (frameY >= 54)
				{
					num += 36;
				}
				result = 7;
				if (num == 16 || num == 17)
				{
					result = 26;
				}
				if (num >= 46 && num <= 49)
				{
					result = -1;
				}
			}
			if (type == 241)
			{
				result = 1;
			}
			if (type == 242)
			{
				result = -1;
			}
			if (type == 246)
			{
				result = -1;
			}
			if (type == 36)
			{
				result = -1;
			}
			if (type == 170)
			{
				result = 196;
			}
			if (type == 315)
			{
				result = 225;
			}
			if (type == 171)
			{
				if (Main.rand.Next(2) == 0)
				{
					result = 196;
				}
				else
				{
					result = -1;
				}
			}
			if (type == 326)
			{
				result = 13;
			}
			if (type == 327)
			{
				result = 13;
			}
			if (type == 336)
			{
				result = 6;
			}
			if (type == 328)
			{
				result = 13;
			}
			if (type == 329)
			{
				result = 13;
			}
			if (type == 330)
			{
				result = 9;
			}
			if (type == 331)
			{
				result = 11;
			}
			if (type == 332)
			{
				result = 19;
			}
			if (type == 333)
			{
				result = 11;
			}
			if (type == 101)
			{
				result = -1;
			}
			if (type == 19)
			{
				int num2 = frameY / 18;
				if (num2 == 0 || num2 == 9 || num2 == 10 || num2 == 11 || num2 == 12)
				{
					result = 7;
				}
				else if (num2 == 1)
				{
					result = 77;
				}
				else if (num2 == 2)
				{
					result = 78;
				}
				else if (num2 == 3)
				{
					result = 79;
				}
				else if (num2 == 4)
				{
					result = 26;
				}
				else if (num2 == 5)
				{
					result = 126;
				}
				else if (num2 == 13)
				{
					result = 109;
				}
				else if (num2 == 14)
				{
					result = 13;
				}
				else if (num2 >= 15 || num2 <= 16)
				{
					result = -1;
				}
				else if (num2 == 17)
				{
					result = 215;
				}
				else if (num2 == 18)
				{
					result = 214;
				}
				else
				{
					result = 1;
				}
			}
			if (type == 79)
			{
				int num3 = frameY / 36;
				if (num3 == 0)
				{
					result = 7;
				}
				else if (num3 == 1)
				{
					result = 77;
				}
				else if (num3 == 2)
				{
					result = 78;
				}
				else if (num3 == 3)
				{
					result = 79;
				}
				else if (num3 == 4)
				{
					result = 126;
				}
				else if (num3 == 8)
				{
					result = 109;
				}
				else if (num3 >= 9)
				{
					result = -1;
				}
				else
				{
					result = 1;
				}
			}
			if (type == 18)
			{
				int num4 = frameX / 36;
				if (num4 == 0)
				{
					result = 7;
				}
				else if (num4 == 1)
				{
					result = 77;
				}
				else if (num4 == 2)
				{
					result = 78;
				}
				else if (num4 == 3)
				{
					result = 79;
				}
				else if (num4 == 4)
				{
					result = 26;
				}
				else if (num4 == 5)
				{
					result = 40;
				}
				else if (num4 == 6)
				{
					result = 5;
				}
				else if (num4 == 7)
				{
					result = 26;
				}
				else if (num4 == 8)
				{
					result = 4;
				}
				else if (num4 == 9)
				{
					result = 126;
				}
				else if (num4 == 10)
				{
					result = 148;
				}
				else if (num4 == 11 || num4 == 12 || num4 == 13)
				{
					result = 1;
				}
				else if (num4 == 14)
				{
					result = 109;
				}
				else if (num4 == 15)
				{
					result = 126;
				}
				else
				{
					result = -1;
				}
			}
			if (type == 14 || type == 87 || type == 88)
			{
				result = -1;
			}
			if (type >= 255 && type <= 261)
			{
				int num5 = type - 255;
				result = 86 + num5;
				if (num5 == 6)
				{
					result = 138;
				}
			}
			if (type >= 262 && type <= 268)
			{
				int num6 = type - 262;
				result = 86 + num6;
				if (num6 == 6)
				{
					result = 138;
				}
			}
			if (type == 178)
			{
				int num7 = frameX / 18;
				result = 86 + num7;
				if (num7 == 6)
				{
					result = 138;
				}
			}
			if (type == 186)
			{
				if (frameX <= 360)
				{
					result = 26;
				}
				else if (frameX <= 846)
				{
					result = 1;
				}
				else if (frameX <= 954)
				{
					result = 9;
				}
				else if (frameX <= 1062)
				{
					result = 11;
				}
				else if (frameX <= 1170)
				{
					result = 10;
				}
				else if (frameX <= 1332)
				{
					result = 0;
				}
				else if (frameX <= 1386)
				{
					result = 10;
				}
				else
				{
					result = 80;
				}
			}
			if (type == 187)
			{
				if (frameX <= 144)
				{
					result = 1;
				}
				else if (frameX <= 306)
				{
					result = 38;
				}
				else if (frameX <= 468)
				{
					result = 36;
				}
				else if (frameX <= 738)
				{
					result = 30;
				}
				else if (frameX <= 970)
				{
					result = 1;
				}
				else if (frameX <= 1132)
				{
					result = 148;
				}
				else if (frameX <= 1132)
				{
					result = 155;
				}
				else if (frameX <= 1348)
				{
					result = 1;
				}
				else if (frameX <= 1564)
				{
					result = 0;
				}
			}
			if (type == 105)
			{
				result = 1;
				if (frameX >= 1548 && frameX <= 1654)
				{
					result = 148;
				}
			}
			if (type == 337)
			{
				result = 1;
			}
			if (type == 239)
			{
				int num8 = frameX / 18;
				if (num8 == 0)
				{
					result = 9;
				}
				if (num8 == 1)
				{
					result = 81;
				}
				if (num8 == 2)
				{
					result = 8;
				}
				if (num8 == 3)
				{
					result = 82;
				}
				if (num8 == 4)
				{
					result = 11;
				}
				if (num8 == 5)
				{
					result = 83;
				}
				if (num8 == 6)
				{
					result = 10;
				}
				if (num8 == 7)
				{
					result = 84;
				}
				if (num8 == 8)
				{
					result = 14;
				}
				if (num8 == 9)
				{
					result = 23;
				}
				if (num8 == 10)
				{
					result = 25;
				}
				if (num8 == 11)
				{
					result = 48;
				}
				if (num8 == 12)
				{
					result = 144;
				}
				if (num8 == 13)
				{
					result = 49;
				}
				if (num8 == 14)
				{
					result = 145;
				}
				if (num8 == 15)
				{
					result = 50;
				}
				if (num8 == 16)
				{
					result = 146;
				}
				if (num8 == 17)
				{
					result = 128;
				}
				if (num8 == 18)
				{
					result = 84;
				}
				if (num8 == 19)
				{
					result = 117;
				}
				if (num8 == 20)
				{
					result = 26;
				}
			}
			if (type == 185)
			{
				if (frameY == 18)
				{
					int num9 = frameX / 36;
					if (num9 < 6)
					{
						result = 1;
					}
					else if (num9 < 16)
					{
						result = 26;
					}
					else if (num9 == 16)
					{
						result = 9;
					}
					else if (num9 == 17)
					{
						result = 11;
					}
					else if (num9 == 18)
					{
						result = 10;
					}
					else if (num9 == 19)
					{
						result = 86;
					}
					else if (num9 == 20)
					{
						result = 87;
					}
					else if (num9 == 21)
					{
						result = 88;
					}
					else if (num9 == 22)
					{
						result = 89;
					}
					else if (num9 == 23)
					{
						result = 90;
					}
					else if (num9 == 24)
					{
						result = 91;
					}
					else if (num9 < 31)
					{
						result = 80;
					}
					else if (num9 < 33)
					{
						result = 7;
					}
					else if (num9 < 34)
					{
						result = 8;
					}
					else if (num9 < 39)
					{
						result = 30;
					}
					else if (num9 < 42)
					{
						result = 1;
					}
				}
				else
				{
					int num10 = frameX / 18;
					if (num10 < 6)
					{
						result = 1;
					}
					else if (num10 < 12)
					{
						result = 0;
					}
					else if (num10 < 27)
					{
						result = 26;
					}
					else if (num10 < 32)
					{
						result = 1;
					}
					else if (num10 < 35)
					{
						result = 0;
					}
					else if (num10 < 46)
					{
						result = 80;
					}
					else if (num10 < 52)
					{
						result = 30;
					}
				}
			}
			if (type == 184)
			{
				int num11 = frameX / 22;
				result = 93 + num11;
			}
			if (type == 237)
			{
				result = 148;
			}
			if (type == 157)
			{
				result = 77;
			}
			if (type == 158 || type == 232)
			{
				result = 78;
			}
			if (type == 159)
			{
				result = 78;
			}
			if (type == 15)
			{
				result = -1;
			}
			if (type == 191)
			{
				result = 7;
			}
			if (type == 5)
			{
				result = 7;
			}
			if (type == 323)
			{
				result = 215;
			}
			if (type == 137)
			{
				result = 1;
				int num12 = frameY / 18;
				if (num12 > 0)
				{
					result = 148;
				}
			}
			if (type == 212)
			{
				result = -1;
			}
			if (type == 213)
			{
				result = 129;
			}
			if (type == 214)
			{
				result = 1;
			}
			if (type == 215)
			{
				result = 6;
			}
			if (type == 325)
			{
				result = 81;
			}
			if (type == 251)
			{
				result = 189;
			}
			if (type == 252)
			{
				result = 190;
			}
			if (type == 253)
			{
				result = 191;
			}
			if (type == 254)
			{
				if (frameX < 72)
				{
					result = 3;
				}
				else if (frameX < 108)
				{
					result = 3;
					if (Main.rand.Next(3) == 0)
					{
						result = 189;
					}
				}
				else if (frameX < 144)
				{
					result = 3;
					if (Main.rand.Next(2) == 0)
					{
						result = 189;
					}
				}
				else
				{
					result = 3;
					if (Main.rand.Next(4) != 0)
					{
						result = 189;
					}
				}
			}
			if (type == 21)
			{
				if (frameX >= 1008)
				{
					result = -1;
				}
				else if (frameX >= 612)
				{
					result = 11;
				}
				else if (frameX >= 576)
				{
					result = 148;
				}
				else if (frameX >= 540)
				{
					result = 26;
				}
				else if (frameX >= 504)
				{
					result = 126;
				}
				else if (frameX >= 468)
				{
					result = 116;
				}
				else if (frameX >= 432)
				{
					result = 7;
				}
				else if (frameX >= 396)
				{
					result = 11;
				}
				else if (frameX >= 360)
				{
					result = 10;
				}
				else if (frameX >= 324)
				{
					result = 79;
				}
				else if (frameX >= 288)
				{
					result = 78;
				}
				else if (frameX >= 252)
				{
					result = 77;
				}
				else if (frameX >= 216)
				{
					result = 1;
				}
				else if (frameX >= 180)
				{
					result = 7;
				}
				else if (frameX >= 108)
				{
					result = 37;
				}
				else if (frameX >= 36)
				{
					result = 10;
				}
				else
				{
					result = 7;
				}
			}
			if (type == 2)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 0;
				}
				else
				{
					result = 2;
				}
			}
			if (Main.tileMoss[type])
			{
				result = type - 179 + 93;
			}
			if (type == 127)
			{
				result = 67;
			}
			if (type == 91)
			{
				result = -1;
			}
			if (type == 198)
			{
				result = 109;
			}
			if (type == 26)
			{
				if (frameX >= 54)
				{
					result = 5;
				}
				else
				{
					result = 8;
				}
			}
			if (type == 34)
			{
				result = -1;
			}
			if (type == 6)
			{
				result = 8;
			}
			if (type == 7 || type == 47 || type == 284)
			{
				result = 9;
			}
			if (type == 8 || type == 45 || type == 102)
			{
				result = 10;
			}
			if (type == 9 || type == 42 || type == 46 || type == 126 || type == 136)
			{
				result = 11;
			}
			if (type == 166 || type == 175)
			{
				result = 81;
			}
			if (type == 167)
			{
				result = 82;
			}
			if (type == 168 || type == 176)
			{
				result = 83;
			}
			if (type == 169 || type == 177)
			{
				result = 84;
			}
			if (type == 199)
			{
				result = 117;
			}
			if (type == 205)
			{
				result = 125;
			}
			if (type == 201)
			{
				result = 125;
			}
			if (type == 211)
			{
				result = 128;
			}
			if (type == 227)
			{
				int num13 = frameX / 34;
				if (num13 == 0 || num13 == 1)
				{
					result = 26;
				}
				else if (num13 == 3)
				{
					result = 3;
				}
				else if (num13 == 2 || num13 == 4 || num13 == 5 || num13 == 6)
				{
					result = 40;
				}
				else if (num13 == 7)
				{
					result = 117;
				}
			}
			if (type == 204)
			{
				result = 117;
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 1;
				}
			}
			if (type == 203)
			{
				result = 117;
			}
			if (type == 243)
			{
				if (Main.rand.Next(2) == 0)
				{
					result = 7;
				}
				else
				{
					result = 13;
				}
			}
			if (type == 244)
			{
				if (Main.rand.Next(2) == 0)
				{
					result = 1;
				}
				else
				{
					result = 13;
				}
			}
			else if ((type >= 275 && type <= 282) || (type == 285 || type == 286 || (type >= 288 && type <= 297)) || (type >= 316 && type <= 318) || type == 298 || type == 299 || type == 309 || type == 310 || type == 339)
			{
				result = 13;
				if (Main.rand.Next(3) != 0)
				{
					result = -1;
				}
			}
			if (type == 13)
			{
				if (frameX >= 90)
				{
					result = -1;
				}
				else
				{
					result = 13;
				}
			}
			if (type == 189)
			{
				result = 16;
			}
			if (type == 12)
			{
				result = 12;
			}
			if (type == 3 || type == 73)
			{
				result = 3;
			}
			if (type == 54)
			{
				result = 13;
			}
			if (type == 22 || type == 140)
			{
				result = 14;
			}
			if (type == 78)
			{
				result = 22;
			}
			if (type == 28)
			{
				result = 22;
				if (frameY >= 72 && frameY <= 90)
				{
					result = 1;
				}
				if (frameY >= 144 && frameY <= 234)
				{
					result = 48;
				}
				if (frameY >= 252 && frameY <= 358)
				{
					result = 85;
				}
				if (frameY >= 360 && frameY <= 466)
				{
					result = 26;
				}
				if (frameY >= 468 && frameY <= 574)
				{
					result = 36;
				}
				if (frameY >= 576 && frameY <= 790)
				{
					result = 18;
				}
				if (frameY >= 792 && frameY <= 898)
				{
					result = 5;
				}
				if (frameY >= 900 && frameY <= 1006)
				{
					result = 0;
				}
				if (frameY >= 1008 && frameY <= 1114)
				{
					result = 148;
				}
			}
			if (type == 163)
			{
				result = 118;
			}
			if (type == 164)
			{
				result = 119;
			}
			if (type == 200)
			{
				result = 120;
			}
			if (type == 221 || type == 248)
			{
				result = 144;
			}
			if (type == 222 || type == 249)
			{
				result = 145;
			}
			if (type == 223 || type == 250)
			{
				result = 146;
			}
			if (type == 224)
			{
				result = 149;
			}
			if (type == 225)
			{
				result = 147;
			}
			if (type == 229)
			{
				result = 153;
			}
			if (type == 231)
			{
				result = 153;
				if (Main.rand.Next(3) == 0)
				{
					result = 26;
				}
			}
			if (type == 226)
			{
				result = 148;
			}
			if (type == 103)
			{
				result = -1;
			}
			if (type == 29)
			{
				result = 23;
			}
			if (type == 40)
			{
				result = 28;
			}
			if (type == 49)
			{
				result = 29;
			}
			if (type == 50)
			{
				result = 22;
			}
			if (type == 51)
			{
				result = 30;
			}
			if (type == 52)
			{
				result = 3;
			}
			if (type == 53 || type == 81 || type == 151 || type == 202 || type == 274)
			{
				result = 32;
			}
			if (type == 56 || type == 152)
			{
				result = 37;
			}
			if (type == 75)
			{
				result = 109;
			}
			if (type == 57 || type == 119 || type == 141 || type == 234)
			{
				result = 36;
			}
			if (type == 59 || type == 120)
			{
				result = 38;
			}
			if (type == 61 || type == 62 || type == 74 || type == 80 || type == 188 || type == 233 || type == 236)
			{
				result = 40;
			}
			if (type == 238)
			{
				if (WorldGen.genRand.Next(3) == 0)
				{
					result = 167;
				}
				else
				{
					result = 166;
				}
			}
			if (type == 69)
			{
				result = 7;
			}
			if (type == 71 || type == 72 || type == 190)
			{
				result = 26;
			}
			if (type == 70)
			{
				result = 17;
			}
			if (type == 112)
			{
				result = 14;
			}
			if (type == 123)
			{
				result = 53;
			}
			if (type == 161)
			{
				result = 80;
			}
			if (type == 206)
			{
				result = 80;
			}
			if (type == 162)
			{
				result = 80;
			}
			if (type == 165)
			{
				if (frameX < 54)
				{
					result = 80;
				}
				else if (frameX >= 324)
				{
					result = 117;
				}
				else if (frameX >= 270)
				{
					result = 14;
				}
				else if (frameX >= 216)
				{
					result = 1;
				}
				else if (frameX >= 162)
				{
					result = 147;
				}
				else if (frameX >= 108)
				{
					result = 30;
				}
				else
				{
					result = 1;
				}
			}
			if (type == 193)
			{
				result = 4;
			}
			if (type == 194)
			{
				result = 26;
			}
			if (type == 195)
			{
				result = 5;
			}
			if (type == 196)
			{
				result = 108;
			}
			if (type == 197)
			{
				result = 4;
			}
			if (type == 153)
			{
				result = 26;
			}
			if (type == 154)
			{
				result = 32;
			}
			if (type == 155)
			{
				result = 2;
			}
			if (type == 156)
			{
				result = 1;
			}
			if (type == 116 || type == 118 || type == 147 || type == 148)
			{
				result = 51;
			}
			if (type == 109)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 0;
				}
				else
				{
					result = 47;
				}
			}
			if (type == 110 || type == 113 || type == 115)
			{
				result = 47;
			}
			if (type == 107 || type == 121)
			{
				result = 48;
			}
			if (type == 108 || type == 122 || type == 146)
			{
				result = 49;
			}
			if (type == 111 || type == 145 || type == 150)
			{
				result = 50;
			}
			if (type == 133)
			{
				result = 50;
				if (frameX >= 54)
				{
					result = 146;
				}
			}
			if (type == 134)
			{
				result = 49;
				if (frameX >= 36)
				{
					result = 145;
				}
			}
			if (type == 149)
			{
				result = 49;
			}
			if (BaseTile.AlchemyFlower(type))
			{
				int num14 = frameX / 18;
				if (num14 == 0)
				{
					result = 3;
				}
				if (num14 == 1)
				{
					result = 3;
				}
				if (num14 == 2)
				{
					result = 7;
				}
				if (num14 == 3)
				{
					result = 17;
				}
				if (num14 == 4)
				{
					result = 3;
				}
				if (num14 == 5)
				{
					result = 6;
				}
				if (num14 == 6)
				{
					result = 224;
				}
			}
			if (type == 58 || type == 76 || type == 77)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 6;
				}
				else
				{
					result = 25;
				}
			}
			if (type == 37)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 6;
				}
				else
				{
					result = 23;
				}
			}
			if (type == 32)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 14;
				}
				else
				{
					result = 24;
				}
			}
			if (type == 23 || type == 24)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 14;
				}
				else
				{
					result = 17;
				}
			}
			if (type == 25 || type == 31)
			{
				if (type == 31 && frameX >= 36)
				{
					result = 5;
				}
				else if (WorldGen.genRand.Next(2) == 0)
				{
					result = 14;
				}
				else
				{
					result = 1;
				}
			}
			if (type == 20)
			{
				int num15 = frameX / 54;
				if (num15 == 1)
				{
					result = 122;
				}
				else if (num15 == 2)
				{
					result = 78;
				}
				else if (num15 == 3)
				{
					result = 77;
				}
				else if (num15 == 4)
				{
					result = 121;
				}
				else if (num15 == 5)
				{
					result = 79;
				}
				else
				{
					result = 7;
				}
			}
			if (type == 27)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					result = 3;
				}
				else
				{
					result = 19;
				}
			}
			if (type == 129)
			{
				if (frameX == 0 || frameX == 54 || frameX == 108)
				{
					result = 68;
				}
				else if (frameX == 18 || frameX == 72 || frameX == 126)
				{
					result = 69;
				}
				else
				{
					result = 70;
				}
			}
			if (type == 4)
			{
				int num16 = frameY / 22;
				if (num16 == 0)
				{
					result = 6;
				}
				else if (num16 == 8)
				{
					result = 75;
				}
				else if (num16 == 9)
				{
					result = 135;
				}
				else if (num16 == 10)
				{
					result = 158;
				}
				else if (num16 == 11)
				{
					result = 169;
				}
				else if (num16 == 12)
				{
					result = 156;
				}
				else
				{
					result = 58 + num16;
				}
			}
			if (type == 35)
			{
				result = 189;
				if (frameX < 36 && WorldGen.genRand.Next(2) == 0)
				{
					result = 6;
				}
			}
			if ((type == 34 || type == 42) && Main.rand.Next(2) == 0)
			{
				result = 6;
			}
			if (type == 270)
			{
				result = -1;
			}
			if (type == 271)
			{
				result = -1;
			}
			if (type == 79 || type == 90 || type == 101)
			{
				result = -1;
			}
			if (type == 33 || type == 34 || type == 42 || type == 93 || type == 100)
			{
				result = -1;
			}
			if (type == 321)
			{
				result = 214;
			}
			if (type == 322)
			{
				result = 215;
			}
			if (type >= 0 && TileLoader.GetTile(type) != null)
			{
				result = TileLoader.GetTile(type).dustType;
			}
			return result;
		}

		public static int GetTileMinPick(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.active())
			{
				return -1;
			}
			return BaseTile.GetTileMinPick((int)tile.type);
		}

		public static int GetTileMinPick(int type)
		{
			if (type == 211)
			{
				return 200;
			}
			if (type == 25 || type == 203)
			{
				return 65;
			}
			if (type == 117)
			{
				return 65;
			}
			if (type == 37)
			{
				return 50;
			}
			if (type == 404)
			{
				return 65;
			}
			if (type == 22 || type == 204)
			{
				return 55;
			}
			if (type == 56)
			{
				return 65;
			}
			if (type == 58)
			{
				return 65;
			}
			if (type == 226 || type == 237)
			{
				return 210;
			}
			if (Main.tileDungeon[type])
			{
				return 65;
			}
			if (type == 107)
			{
				return 100;
			}
			if (type == 108)
			{
				return 110;
			}
			if (type == 111)
			{
				return 150;
			}
			if (type == 221)
			{
				return 100;
			}
			if (type == 222)
			{
				return 110;
			}
			if (type == 223)
			{
				return 150;
			}
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				return tile.minPick;
			}
			return 0;
		}

		public static int GetTileResist(int x, int y, int pickPower)
		{
			Tile tile = Main.tile[x, y];
			int type = (int)tile.type;
			int num = 0;
			if (Main.tileNoFail[type])
			{
				num = 100;
			}
			if (Main.tileDungeon[type] || type == 25 || type == 58 || type == 117 || type == 203)
			{
				num += pickPower / 2;
			}
			else if (type == 48 || type == 232)
			{
				num += pickPower / 4;
			}
			else if (type == 226)
			{
				num += pickPower / 4;
			}
			else if (type == 107 || type == 221)
			{
				num += pickPower / 2;
			}
			else if (type == 108 || type == 222)
			{
				num += pickPower / 3;
			}
			else if (type == 111 || type == 223)
			{
				num += pickPower / 4;
			}
			else if (type == 211)
			{
				num += pickPower / 5;
			}
			else
			{
				TileLoader.MineDamage(pickPower, ref num);
			}
			int tileMinPick = BaseTile.GetTileMinPick(type);
			if (tileMinPick > pickPower)
			{
				num = 0;
			}
			if (type == 147 || type == 0 || type == 40 || type == 53 || type == 57 || type == 59 || type == 123 || type == 224 || type == 397)
			{
				num += pickPower;
			}
			if (type == 165 || Main.tileRope[type] || type == 199 || Main.tileMoss[type])
			{
				num = 100;
			}
			if (type == 128 || type == 269)
			{
				if (tile.frameX == 18 || tile.frameX == 54)
				{
					x--;
					tile = Main.tile[x, y];
					type = (int)tile.type;
				}
				if (tile.frameX >= 100)
				{
					num = 0;
				}
			}
			if (type == 334)
			{
				if (tile.frameY == 0)
				{
					y++;
					tile = Main.tile[x, y];
					type = (int)tile.type;
				}
				if (tile.frameY == 36)
				{
					y--;
					tile = Main.tile[x, y];
					type = (int)tile.type;
				}
				int i = (int)tile.frameX;
				bool flag = i >= 5000;
				bool flag2 = false;
				if (!flag)
				{
					int num2 = i / 18;
					num2 %= 3;
					x -= num2;
					tile = Main.tile[x, y];
					type = (int)tile.type;
					if (tile.frameX >= 5000)
					{
						flag = true;
					}
				}
				if (flag)
				{
					i = (int)tile.frameX;
					int num3 = 0;
					while (i >= 5000)
					{
						i -= 5000;
						num3++;
					}
					if (num3 != 0)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					num = 0;
				}
			}
			if (!WorldGen.CanKillTile(x, y))
			{
				num = 0;
			}
			return num;
		}

		public static void SpawnExplosion(Vector2 position, int explosionIntensity = 3, int damage = 50, bool doeffects = true, bool dotiles = true, bool dodamage = true, bool sync = true)
		{
			if (dodamage)
			{
				int[] npcs = BaseAI.GetNPCs(position, -1, (float)(explosionIntensity * 16) + 10f, null);
				int[] players = BaseAI.GetPlayers(position, (float)(explosionIntensity * 16) + 10f, null);
				for (int i = 0; i < npcs.Length; i++)
				{
					NPC npc = Main.npc[npcs[i]];
					if (!npc.dontTakeDamage)
					{
						BaseAI.DamageNPC(npc, Math.Max(1, damage + Main.rand.Next(-7, 8)), Math.Min(10f, (float)explosionIntensity / 3f), null, true, false);
					}
				}
				for (int j = 0; j < players.Length; j++)
				{
					BaseAI.DamagePlayer(Main.player[players[j]], Math.Max(1, damage + Main.rand.Next(-7, 8)), Math.Min(10f, (float)explosionIntensity / 3f), null, true, false);
				}
				if (!doeffects && !dotiles)
				{
					return;
				}
			}
			int num = (int)(position.X / 16f - (float)explosionIntensity);
			int num2 = (int)(position.X / 16f + (float)explosionIntensity);
			int num3 = (int)(position.Y / 16f - (float)explosionIntensity);
			int num4 = (int)(position.Y / 16f + (float)explosionIntensity);
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			if (doeffects)
			{
				BaseAI.SpawnSmoke(position - new Vector2((float)(explosionIntensity * 16), (float)(explosionIntensity * 16)), (float)(explosionIntensity * 32), (float)(explosionIntensity * 32), (explosionIntensity > 4) ? explosionIntensity : 2, 0.75f);
				UnifiedRandom rand = Main.rand;
				for (int k = num; k <= num2; k++)
				{
					for (int l = num3; l <= num4; l++)
					{
						float num5 = Math.Abs((float)k - position.X / 16f);
						float num6 = Math.Abs((float)l - position.Y / 16f);
						double num7 = Math.Sqrt((double)(num5 * num5 + num6 * num6));
						if (num7 < (double)explosionIntensity)
						{
							Vector2 vector;
							vector..ctor((float)(k * 16), (float)(l * 16));
							Dust.NewDust(vector, 1 + rand.Next(16), 1 + rand.Next(16), 31, 0f, 0f, 100, Color.White, 2f);
							int num8 = Dust.NewDust(vector, 1 + rand.Next(16), 1 + rand.Next(16), 6, 0f, 0f, 100, Color.White, 2f);
							Main.dust[num8].noGravity = true;
							Main.dust[num8].velocity *= 5f;
							num8 = Dust.NewDust(vector, 1 + rand.Next(16), 1 + rand.Next(16), 6, 0f, 0f, 100, Color.White, 1f);
							Main.dust[num8].velocity *= 3f;
						}
					}
				}
				if (!dotiles)
				{
					return;
				}
			}
			if (dotiles)
			{
				bool flag = false;
				for (int m = num; m <= num2; m++)
				{
					for (int n = num3; n <= num4; n++)
					{
						float num9 = Math.Abs((float)m - position.X / 16f);
						float num10 = Math.Abs((float)n - position.Y / 16f);
						double num11 = Math.Sqrt((double)(num9 * num9 + num10 * num10));
						if (num11 < (double)explosionIntensity && Main.tile[m, n] != null && Main.tile[m, n].wall == 0)
						{
							flag = true;
							break;
						}
					}
				}
				for (int num12 = num; num12 <= num2; num12++)
				{
					for (int num13 = num3; num13 <= num4; num13++)
					{
						float num14 = Math.Abs((float)num12 - position.X / 16f);
						float num15 = Math.Abs((float)num13 - position.Y / 16f);
						double num16 = Math.Sqrt((double)(num14 * num14 + num15 * num15));
						if (num16 < (double)explosionIntensity)
						{
							bool flag2 = true;
							if (Main.tile[num12, num13] != null && Main.tile[num12, num13].active())
							{
								if (BaseUtility.InArray(BaseConstants.TILEIDS_DUNGEONSTRICT, (int)Main.tile[num12, num13].type) || Main.tile[num12, num13].type == 21 || Main.tile[num12, num13].type == 26 || Main.tile[num12, num13].type == 107 || Main.tile[num12, num13].type == 108 || Main.tile[num12, num13].type == 111)
								{
									flag2 = false;
								}
								if (!Main.hardMode && Main.tile[num12, num13].type == 58)
								{
									flag2 = false;
								}
								if (flag2)
								{
									WorldGen.KillTile(num12, num13, false, false, false);
									if (sync && !Main.tile[num12, num13].active() && Main.netMode != 0)
									{
										NetMessage.SendData(17, -1, -1, NetworkText.FromLiteral(""), 0, (float)num12, (float)num13, 0f, 0, 0, 0);
									}
								}
							}
							if (flag2)
							{
								for (int num17 = num12 - 1; num17 <= num12 + 1; num17++)
								{
									for (int num18 = num13 - 1; num18 <= num13 + 1; num18++)
									{
										if (Main.tile[num17, num18] != null && Main.tile[num17, num18].wall > 0 && flag)
										{
											WorldGen.KillWall(num17, num18, false);
											if (sync && Main.tile[num17, num18].wall == 0 && Main.netMode != 0)
											{
												NetMessage.SendData(17, -1, -1, NetworkText.FromLiteral(""), 2, (float)num17, (float)num18, 0f, 0, 0, 0);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		public static void PlayTileHitSound(int tileX, int tileY)
		{
			Tile tile = Main.tile[tileX, tileY];
			if (tile != null)
			{
				BaseTile.PlayTileHitSound((float)(tileX * 16), (float)(tileY * 16), (int)tile.type);
			}
		}

		public static void PlayTileHitSound(float x, float y, int tileType)
		{
			if (tileType >= 0 && TileLoader.GetTile(tileType) != null)
			{
				ModTile tile = TileLoader.GetTile(tileType);
				Main.PlaySound(tile.soundStyle, (int)x, (int)y, tile.soundType, 1f, 0f);
				return;
			}
			if (tileType == 127)
			{
				Main.PlaySound(2, (int)x, (int)y, 27, 1f, 0f);
				return;
			}
			if (BaseTile.AlchemyFlower(tileType) || tileType == 3 || tileType == 110 || tileType == 24 || tileType == 32 || tileType == 51 || tileType == 52 || tileType == 61 || tileType == 62 || tileType == 69 || tileType == 71 || tileType == 73 || tileType == 74 || tileType == 113 || tileType == 115)
			{
				Main.PlaySound(6, (int)x, (int)y, 1, 1f, 0f);
				return;
			}
			if (tileType == 1 || tileType == 6 || tileType == 7 || tileType == 8 || tileType == 9 || tileType == 22 || tileType == 140 || tileType == 25 || tileType == 37 || tileType == 38 || tileType == 39 || tileType == 41 || tileType == 43 || tileType == 44 || tileType == 45 || tileType == 46 || tileType == 47 || tileType == 48 || tileType == 56 || tileType == 58 || tileType == 63 || tileType == 64 || tileType == 65 || tileType == 66 || tileType == 67 || tileType == 68 || tileType == 75 || tileType == 76 || tileType == 107 || tileType == 108 || tileType == 111 || tileType == 117 || tileType == 118 || tileType == 119 || tileType == 120 || tileType == 121 || tileType == 122)
			{
				Main.PlaySound(21, (int)x, (int)y, 1, 1f, 0f);
				return;
			}
			if (tileType != 138)
			{
				Main.PlaySound(0, (int)x, (int)y, 1, 1f, 0f);
			}
		}

		public static bool IsType(int x, int y, int width, int height, int type)
		{
			for (int i = x; i < x + width; i++)
			{
				for (int j = y; j < y + height; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null || !tile.active() || (int)tile.type != type)
					{
						return false;
					}
				}
			}
			return true;
		}

		public static int GetTileAndWallCount(Vector2 tileCenter, int[] tileTypes, int[] wallTypes, int distance = 35)
		{
			int num = 0;
			for (int i = -distance; i < distance + 1; i++)
			{
				for (int j = -distance; j < distance + 1; j++)
				{
					int num2 = (int)tileCenter.X + i;
					int num3 = (int)tileCenter.Y + j;
					if (num2 >= 0 && num3 >= 0 && num2 <= Main.maxTilesX && num3 <= Main.maxTilesY)
					{
						Tile tile = Main.tile[num2, num3];
						if (tile != null)
						{
							bool flag = false;
							if (tile.active())
							{
								foreach (int num4 in tileTypes)
								{
									if ((int)tile.type == num4)
									{
										num++;
										flag = true;
										break;
									}
								}
							}
							if (!flag)
							{
								foreach (int num5 in wallTypes)
								{
									if ((int)tile.wall == num5)
									{
										num++;
										break;
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		public static int GetWallCount(Vector2 tileCenter, int[] wallTypes, int distance = 35)
		{
			int num = 0;
			for (int i = -distance; i < distance + 1; i++)
			{
				for (int j = -distance; j < distance + 1; j++)
				{
					int num2 = (int)tileCenter.X + i;
					int num3 = (int)tileCenter.Y + j;
					if (num2 >= 0 && num3 >= 0 && num2 <= Main.maxTilesX && num3 <= Main.maxTilesY)
					{
						Tile tile = Main.tile[num2, num3];
						if (tile != null)
						{
							foreach (int num4 in wallTypes)
							{
								if ((int)tile.wall == num4)
								{
									num++;
									break;
								}
							}
						}
					}
				}
			}
			return num;
		}

		public static int GetTileCount(Vector2 tileCenter, int[] tileTypes, int distance = 35)
		{
			int num = 0;
			for (int i = -distance; i < distance + 1; i++)
			{
				for (int j = -distance; j < distance + 1; j++)
				{
					int num2 = (int)tileCenter.X + i;
					int num3 = (int)tileCenter.Y + j;
					if (num2 >= 0 && num3 >= 0 && num2 <= Main.maxTilesX && num3 <= Main.maxTilesY)
					{
						Tile tile = Main.tile[num2, num3];
						if (tile != null && tile.active())
						{
							foreach (int num4 in tileTypes)
							{
								if ((int)tile.type == num4)
								{
									num++;
									break;
								}
							}
						}
					}
				}
			}
			return num;
		}
	}
}
