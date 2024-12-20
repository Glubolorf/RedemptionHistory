using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption
{
	public class BaseWorldGen
	{
		public static void GenOre(int tileType, int amountInWorld = -1, float oreStrength = 5f, int oreSteps = 5, int heightLimit = -1, bool mapDebug = false)
		{
			if (WorldGen.noTileActions)
			{
				return;
			}
			if (heightLimit == -1)
			{
				heightLimit = (int)Main.worldSurface;
			}
			if (amountInWorld == -1)
			{
				float num = (float)(Main.maxTilesX / 4200);
				num *= 50f;
				amountInWorld = (int)num;
			}
			for (int i = 0; i < amountInWorld; i++)
			{
				int num2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				int num3 = WorldGen.genRand.Next(heightLimit, Main.maxTilesY - 150);
				WorldGen.OreRunner(num2, num3, (double)oreStrength, oreSteps, (ushort)tileType);
			}
		}

		public static int GetFirstTileFloor(int x, int startY, bool solid = true)
		{
			for (int i = startY; i < Main.maxTilesY; i++)
			{
				Tile tile = Main.tile[x, i];
				if (tile != null && tile.nactive() && (!solid || Main.tileSolid[(int)tile.type]))
				{
					return i;
				}
			}
			return Main.maxTilesY;
		}

		public static int GetFirstTileCeiling(int x, int startY, bool solid = true)
		{
			for (int i = startY; i > 0; i--)
			{
				Tile tile = Main.tile[x, i];
				if (tile != null && tile.nactive() && (!solid || Main.tileSolid[(int)tile.type]))
				{
					return i;
				}
			}
			return 0;
		}

		public static int GetFirstTileSide(int startX, int y, bool left, bool solid = true)
		{
			if (left)
			{
				for (int i = startX; i > 0; i--)
				{
					Tile tile = Main.tile[i, y];
					if (tile != null && tile.nactive() && (!solid || Main.tileSolid[(int)tile.type]))
					{
						return i;
					}
				}
				return 0;
			}
			for (int j = startX; j < Main.maxTilesX; j++)
			{
				Tile tile2 = Main.tile[j, y];
				if (tile2 != null && tile2.nactive() && (!solid || Main.tileSolid[(int)tile2.type]))
				{
					return j;
				}
			}
			return Main.maxTilesX;
		}

		public static int GetBelowFloatingIslandY()
		{
			int worldSize = BaseWorldGen.GetWorldSize();
			return ((worldSize == 1) ? 1200 : ((worldSize == 2) ? 1600 : ((worldSize == 3) ? 2000 : 1200))) + 1;
		}

		public static int GetWorldSize()
		{
			if (Main.maxTilesX == 4200)
			{
				return 1;
			}
			if (Main.maxTilesX == 6300)
			{
				return 2;
			}
			if (Main.maxTilesX == 8400)
			{
				return 3;
			}
			return 1;
		}

		public static void ReplaceTiles(Vector2 position, int radius, int[] tiles, int[] replacements, bool silent = false, bool sync = true)
		{
			int num = (int)(position.X / 16f - (float)radius);
			int num2 = (int)(position.X / 16f + (float)radius);
			int num3 = (int)(position.Y / 16f - (float)radius);
			int num4 = (int)(position.Y / 16f + (float)radius);
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
			float num5 = (float)radius * 16f;
			for (int i = num; i <= num2; i++)
			{
				for (int j = num3; j <= num4; j++)
				{
					double num6 = (double)Vector2.Distance(new Vector2((float)i * 16f + 8f, (float)j * 16f + 8f), position);
					if (num6 < (double)num5 && Main.tile[i, j] != null && Main.tile[i, j].active())
					{
						int type = (int)Main.tile[i, j].type;
						int num7 = 0;
						if (BaseUtility.InArray(tiles, type, ref num7))
						{
							BaseWorldGen.GenerateTile(i, j, replacements[num7], -1, 0, true, false, -2, silent, false);
						}
					}
				}
			}
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, (int)(position.X / 16f), (int)(position.Y / 16f), radius * 2 + 2, 0);
			}
		}

		public static bool KillChestAndItems(int X, int Y)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
				{
					Main.chest[i] = null;
					return true;
				}
			}
			return false;
		}

		public static void GenerateLiquid(int x, int y, int liquidType, bool updateFlow = true, int liquidHeight = 255, bool sync = true)
		{
			liquidHeight = (int)MathHelper.Clamp((float)liquidHeight, 0f, 255f);
			Main.tile[x, y].liquid = (byte)liquidHeight;
			if (liquidType == 0)
			{
				Main.tile[x, y].lava(false);
				Main.tile[x, y].honey(false);
			}
			else if (liquidType == 1)
			{
				Main.tile[x, y].lava(true);
				Main.tile[x, y].honey(false);
			}
			else if (liquidType == 2)
			{
				Main.tile[x, y].lava(false);
				Main.tile[x, y].honey(true);
			}
			if (updateFlow)
			{
				Liquid.AddWater(x, y);
			}
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, x, y, 1, 0);
			}
		}

		public static void GenerateLiquid(int x, int y, int width, int height, int liquidType, bool updateFlow = true, int liquidHeight = 255, bool sync = true)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					BaseWorldGen.GenerateLiquid(i + x, j + y, liquidType, updateFlow, liquidHeight, false);
				}
			}
			int num = (width > height) ? width : height;
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, x + (int)((float)width * 0.5f) - 1, y + (int)((float)height * 0.5f) - 1, num + 4, 0);
			}
		}

		public static void GenerateTile(int x, int y, int tile, int wall, int tileStyle = 0, bool active = true, bool removeLiquid = true, int slope = -2, bool silent = false, bool sync = true)
		{
			try
			{
				if (Main.tile[x, y] == null)
				{
					Main.tile[x, y] = new Tile();
				}
				TileObjectData tileObjectData = (tile <= -1) ? null : TileObjectData.GetTileData(tile, tileStyle, 0);
				int num = (tileObjectData == null) ? 1 : tileObjectData.Width;
				int num2 = (tileObjectData == null) ? 1 : tileObjectData.Height;
				int num3 = (tile == -1 || tileObjectData == null) ? 1 : tileObjectData.Width;
				int num4 = (tile == -1 || tileObjectData == null) ? 1 : tileObjectData.Height;
				byte b = Main.tile[x, y].slope();
				bool flag = Main.tile[x, y].halfBrick();
				if (tile != -1)
				{
					WorldGen.destroyObject = true;
					if (num > 1 || num2 > 1)
					{
						Vector2 vector = BaseTile.FindTopLeft(x, y);
						for (int i = 0; i < num; i++)
						{
							for (int j = 0; j < num2; j++)
							{
								int num5 = (int)vector.X + i;
								int num6 = (int)vector.Y + j;
								if (i == 0 && j == 0 && Main.tile[num5, num6].type == 21)
								{
									BaseWorldGen.KillChestAndItems(num5, num6);
								}
								Main.tile[x, y].type = 0;
								Main.tile[x, y].active(false);
								if (!silent)
								{
									WorldGen.KillTile(x, y, false, false, true);
								}
								if (removeLiquid)
								{
									BaseWorldGen.GenerateLiquid(num5, num6, 0, true, 0, false);
								}
							}
						}
						for (int k = 0; k < num; k++)
						{
							for (int l = 0; l < num2; l++)
							{
								int num7 = (int)vector.X + k;
								int num8 = (int)vector.Y + l;
								WorldGen.SquareTileFrame(num7, num8, true);
								WorldGen.SquareWallFrame(num7, num8, true);
							}
						}
					}
					else if (!silent)
					{
						WorldGen.KillTile(x, y, false, false, true);
					}
					WorldGen.destroyObject = false;
					if (active)
					{
						if (num3 <= 1 && num4 <= 1 && !Main.tileFrameImportant[tile])
						{
							Main.tile[x, y].type = (ushort)tile;
							Main.tile[x, y].active(true);
							if (slope == -2 && flag)
							{
								Main.tile[x, y].halfBrick(true);
							}
							else if (slope == -1)
							{
								Main.tile[x, y].halfBrick(true);
							}
							else
							{
								Main.tile[x, y].slope((slope == -2) ? b : ((byte)slope));
							}
							WorldGen.SquareTileFrame(x, y, true);
						}
						else
						{
							WorldGen.destroyObject = true;
							if (!silent)
							{
								for (int m = 0; m < num3; m++)
								{
									for (int n = 0; n < num4; n++)
									{
										WorldGen.KillTile(x + m, y + n, false, false, true);
									}
								}
							}
							WorldGen.destroyObject = false;
							int num9 = (tile == 10) ? y : (y + num2);
							WorldGen.PlaceTile(x, num9, tile, true, true, -1, tileStyle);
							for (int num10 = 0; num10 < num3; num10++)
							{
								for (int num11 = 0; num11 < num4; num11++)
								{
									WorldGen.SquareTileFrame(x + num10, y + num11, true);
								}
							}
						}
					}
					else
					{
						Main.tile[x, y].active(false);
					}
				}
				if (wall != -1)
				{
					if (wall == -2)
					{
						wall = 0;
					}
					Main.tile[x, y].wall = 0;
					WorldGen.PlaceWall(x, y, wall, true);
				}
				if (sync && Main.netMode != 0)
				{
					int num12 = num3 + Math.Max(0, num - 1);
					int num13 = num4 + Math.Max(0, num2 - 1);
					int num14 = (num12 > num13) ? num12 : num13;
					NetMessage.SendTileSquare(-1, x + (int)((float)num14 * 0.5f), y + (int)((float)num14 * 0.5f), num14 + 1, 0);
				}
			}
			catch (Exception ex)
			{
				ErrorLogger.Log("TILEGEN ERROR: " + ex.Message);
				ErrorLogger.Log(ex.StackTrace);
				ErrorLogger.Log("--------");
			}
		}

		public static void GenerateLine(GenConditions gen, int x, int y, int endX, int endY, int thickness, bool sync = true)
		{
			if (gen == null)
			{
				throw new Exception("GenConditions cannot be null!");
			}
			if (endX < x)
			{
				int num = x;
				x = endX;
				endX = num;
			}
			bool flag = endY < y;
			if (flag)
			{
				x += Math.Abs(endX - x);
			}
			if (x == endX && y == endY)
			{
				int tile = gen.GetTile(0);
				int wall = gen.GetWall(0);
				if ((tile > -1 && gen.CanPlace != null && !gen.CanPlace(x, y, tile, wall)) || (wall > -1 && gen.CanPlaceWall != null && !gen.CanPlaceWall(x, y, tile, wall)))
				{
					return;
				}
				BaseWorldGen.GenerateTile(x, y, tile, wall, 0, tile != -1, true, 0, false, sync);
				if (gen.slope)
				{
					BaseWorldGen.SmoothTiles(x, y, x, y);
					return;
				}
			}
			else if (x == endX || y == endY)
			{
				if (endY < y)
				{
					int num2 = y;
					y = endY;
					endY = num2;
				}
				bool flag2 = x == endX;
				int num3 = -1;
				int num4 = -1;
				for (int i = 0; i < (flag2 ? (endY - y) : (endX - x)); i++)
				{
					for (int j = 0; j < thickness; j++)
					{
						num3 = ((gen.tiles == null) ? -1 : (gen.orderTiles ? (num3 + 1) : WorldGen.genRand.Next(gen.tiles.Length)));
						num4 = ((gen.walls == null) ? -1 : (gen.orderWalls ? (num4 + 1) : WorldGen.genRand.Next(gen.walls.Length)));
						if (num3 != -1 && num3 >= gen.tiles.Length)
						{
							num3 = 0;
						}
						if (num4 != -1 && num4 >= gen.walls.Length)
						{
							num4 = 0;
						}
						int num5 = flag2 ? j : i;
						int num6 = flag2 ? i : j;
						int num7 = x + num5;
						int num8 = y + num6;
						bool flag3 = num3 == -1 || gen.CanPlace == null || gen.CanPlace(num7, num8, gen.GetTile(num3), gen.GetWall(num4));
						bool flag4 = num4 == -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(num7, num8, gen.GetTile(num3), gen.GetWall(num4));
						if (flag3 && flag4)
						{
							BaseWorldGen.GenerateTile(num7, num8, gen.GetTile(num3), gen.GetWall(num4), 0, gen.GetTile(num3) != -1, true, 0, false, false);
						}
					}
				}
				bool slope = gen.slope;
				if (sync && Main.netMode != 0)
				{
					int num9 = (endY - y > endX - x) ? (endY - y) : (endX - x);
					if (thickness > num9)
					{
						num9 = thickness;
					}
					NetMessage.SendData(20, -1, -1, NetworkText.FromLiteral(""), num9, (float)x, (float)y, 0f, 0, 0, 0);
					return;
				}
			}
			else
			{
				Vector2 vector;
				vector..ctor((float)x, (float)y);
				Vector2 vector2;
				vector2..ctor((float)endX, (float)endY);
				Vector2 vector3 = new Vector2((float)endX, (float)endY) - new Vector2((float)x, (float)y);
				vector3.Normalize();
				float num10 = Vector2.Distance(vector, vector2);
				float num11 = 0f;
				float num12 = BaseUtility.RotationTo(vector, vector2);
				if (num12 < 0f)
				{
					num12 = 6.2831855f - Math.Abs(num12);
				}
				float num13 = MathHelper.Lerp(0f, 1f, num12 / 6.2831855f);
				bool flag5 = num13 < 0.125f || (num13 > 0.375f && num13 < 0.625f) || num13 > 0.825f;
				int num14 = -1;
				int num15 = -1;
				int num16 = x;
				int num17 = y;
				while (num11 < num10)
				{
					Vector2 vector4 = vector + vector3 * num11;
					Point point;
					point..ctor((int)vector4.X, (int)vector4.Y);
					for (int k = 0; k < thickness; k++)
					{
						num14 = ((gen.tiles == null) ? -1 : (gen.orderTiles ? (num14 + 1) : WorldGen.genRand.Next(gen.tiles.Length)));
						num15 = ((gen.walls == null) ? -1 : (gen.orderWalls ? (num15 + 1) : WorldGen.genRand.Next(gen.walls.Length)));
						if (num14 != -1 && num14 >= gen.tiles.Length)
						{
							num14 = 0;
						}
						if (num15 != -1 && num15 >= gen.walls.Length)
						{
							num15 = 0;
						}
						int num18 = flag5 ? 0 : k;
						int num19 = flag5 ? k : 0;
						int num20 = point.X + num18;
						int num21 = flag ? (point.Y - num19) : (point.Y + num19);
						bool flag6 = num14 == -1 || gen.CanPlace == null || gen.CanPlace(num20, num21, gen.GetTile(num14), gen.GetWall(num15));
						bool flag7 = num15 == -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(num20, num21, gen.GetTile(num14), gen.GetWall(num15));
						if (flag6 && flag7)
						{
							BaseWorldGen.GenerateTile(num20, num21, gen.GetTile(num14), gen.GetWall(num15), 0, gen.GetTile(num14) != -1, true, 0, false, false);
						}
					}
					if (sync && Main.netMode != 0 && ((!flag5 && Math.Abs(num17 - point.Y) >= 5) || (flag5 && Math.Abs(num17 - point.Y) >= 5) || num11 + 1f > num10))
					{
						int num22 = Math.Max(5, thickness);
						NetMessage.SendData(10, -1, -1, NetworkText.FromLiteral(""), num16, (float)num17, (float)num22, (float)num22, 0, 0, 0);
						num16 = point.X;
						num17 = point.Y;
					}
					num11 += 1f;
				}
			}
		}

		public static void GenerateHall(GenConditions gen, int x, int y, int endX, int endY, int thickness, int height, bool sync = true)
		{
			if (gen == null)
			{
				throw new Exception("GenConditions cannot be null!");
			}
			if (endX < x)
			{
				int num = x;
				x = endX;
				endX = num;
			}
			bool flag = endX < x;
			bool flag2 = endY < y;
			int num2 = flag ? -1 : 1;
			int num3 = flag2 ? -1 : 1;
			Vector2 startPos;
			startPos..ctor((float)x, (float)y);
			Vector2 endPos;
			endPos..ctor((float)endX, (float)endY);
			float num4 = MathHelper.Lerp(0f, 1f, BaseUtility.RotationTo(startPos, endPos) / 6.2831855f);
			bool flag3 = num4 < 0.125f || (num4 > 0.375f && num4 < 0.625f) || num4 > 0.825f;
			Vector2 vector;
			vector..ctor((float)endX, (float)endY);
			Vector2 vector2;
			vector2..ctor((float)(flag3 ? x : (x + 2 * num2)), (float)(flag3 ? (y + 2 * num3) : y));
			Vector2 vector3;
			vector3..ctor((float)(flag3 ? endX : (endX + 2 * num2)), (float)(flag3 ? (endY + 2 * num3) : endY));
			Vector2 vector4;
			vector4..ctor((float)(flag3 ? x : (x + (thickness * 2 + height) * num2)), (float)(flag3 ? (y + (thickness * 2 + height) * num3) : y));
			Vector2 vector5;
			vector5..ctor((float)(flag3 ? endX : (endX + (thickness * 2 + height) * num2)), (float)(flag3 ? (endY + (thickness * 2 + height) * num3) : endY));
			int[] tiles = gen.tiles;
			int[] walls = gen.walls;
			gen.tiles = null;
			BaseWorldGen.GenerateLine(gen, (int)vector2.X, (int)vector2.Y, (int)vector3.X, (int)vector3.Y, thickness * 3 + height - 2, false);
			gen.tiles = tiles;
			gen.walls = null;
			BaseWorldGen.GenerateLine(gen, x, y, (int)vector.X, (int)vector.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, (int)vector4.X, (int)vector4.Y, (int)vector5.X, (int)vector5.Y, thickness, false);
			gen.walls = walls;
		}

		public static void GenerateTrapezoid(GenConditions gen, int x, int y, int endX, int endY, int thickness, int height, bool sync = true)
		{
			if (gen == null)
			{
				throw new Exception("GenConditions cannot be null!");
			}
			if (endX < x)
			{
				int num = x;
				x = endX;
				endX = num;
			}
			Vector2 startPos;
			startPos..ctor((float)x, (float)y);
			Vector2 endPos;
			endPos..ctor((float)endX, (float)endY);
			float num2 = MathHelper.Lerp(0f, 1f, BaseUtility.RotationTo(startPos, endPos) / 6.2831855f);
			bool flag = num2 < 0.125f || (num2 > 0.375f && num2 < 0.625f) || num2 > 0.825f;
			Vector2 vector;
			vector..ctor((float)endX, (float)endY);
			Vector2 vector2;
			vector2..ctor((float)(x + thickness), (float)(y + thickness));
			Vector2 vector3;
			vector3..ctor((float)(flag ? endX : (endX + thickness)), (float)(flag ? (endY + thickness) : endY));
			Vector2 vector4;
			vector4..ctor((float)(flag ? x : (x + thickness * 2 + height)), (float)(flag ? (y + thickness * 2 + height) : y));
			Vector2 vector5;
			vector5..ctor((float)(flag ? endX : (endX + thickness * 2 + height)), (float)(flag ? (endY + thickness * 2 + height) : endY));
			int[] tiles = gen.tiles;
			int[] walls = gen.walls;
			gen.tiles = null;
			BaseWorldGen.GenerateLine(gen, (int)vector2.X, (int)vector2.Y, (int)vector3.X, (int)vector3.Y, thickness + height, false);
			gen.tiles = tiles;
			gen.walls = null;
			BaseWorldGen.GenerateLine(gen, x, y, (int)vector.X, (int)vector.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, (int)vector4.X, (int)vector4.Y, (int)vector5.X, (int)vector5.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, x, y, (int)vector4.X, (int)vector4.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, (int)vector.X, (int)vector.Y, flag ? ((int)vector5.X) : ((int)vector5.X + thickness), flag ? ((int)vector5.Y + thickness) : ((int)vector5.Y), thickness, false);
			gen.walls = walls;
		}

		public static void GenerateRoomOld(int x, int y, int width, int height, int tile, int wall)
		{
			BaseWorldGen.GenerateRoomOld(x, y, width, height, tile, tile, tile, wall, false, 1, 1, 1, true);
		}

		public static void GenerateRoomOld(int x, int y, int width, int height, int tileSides, int tileFloor, int tileCeiling, int wall, bool wallEnds = false, int sideThickness = 1, int floorThickness = 1, int ceilingThickness = 1, bool sync = true)
		{
			if (tileSides != -1 && sideThickness > 1)
			{
				width += sideThickness;
				x -= sideThickness / 2;
			}
			if (tileFloor != -1 && floorThickness > 1)
			{
				height += floorThickness;
			}
			if (tileCeiling != -1 && ceilingThickness > 1)
			{
				height += ceilingThickness;
				y -= ceilingThickness / 2;
			}
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					int x2 = i + x;
					int y2 = j + y;
					if ((wallEnds || tileCeiling != -1) && j < ceilingThickness)
					{
						BaseWorldGen.GenerateTile(x2, y2, tileCeiling, (wallEnds && j == 0) ? wall : -1, 0, tileCeiling != -1 || !wallEnds, true, 0, false, false);
					}
					else if ((wallEnds || tileFloor != -1) && j >= height - floorThickness)
					{
						BaseWorldGen.GenerateTile(x2, y2, tileFloor, (wallEnds && j >= height - 1) ? wall : -1, 0, tileFloor != -1 || !wallEnds, true, 0, false, false);
					}
					else if ((wallEnds || tileSides != -1) && (i < sideThickness || i >= width - sideThickness))
					{
						BaseWorldGen.GenerateTile(x2, y2, tileSides, (wallEnds && i > 0 && i < width - 1) ? wall : -1, 0, tileSides != -1 || !wallEnds, true, 0, false, false);
					}
					else if (i >= sideThickness && i < width - sideThickness && j >= ceilingThickness && j < height - floorThickness)
					{
						BaseWorldGen.GenerateTile(x2, y2, -1, wall, 0, false, true, 0, false, false);
					}
				}
			}
			int num = (width > height) ? width : height;
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, x + (int)((float)width * 0.5f) - 1, y + (int)((float)height * 0.5f) - 1, num + 4, 0);
			}
		}

		public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, bool randomAmounts = false, bool randomPrefix = false, bool sync = true)
		{
			int[] array = new int[20];
			for (int i = 0; i < array.Length; i++)
			{
				if (randomAmounts)
				{
					array[i] = WorldGen.genRand.Next(1, 6);
				}
				else
				{
					array[i] = 1;
				}
			}
			BaseWorldGen.GenerateChest(x, y, type, chestStyle, stackIDs, array, randomPrefix, sync);
		}

		public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, int[] stackAmounts, bool randomPrefix = false, bool sync = true)
		{
			int[] array = new int[20];
			for (int i = 0; i < array.Length; i++)
			{
				if (randomPrefix)
				{
					array[i] = -1;
				}
				else
				{
					array[i] = -10;
				}
			}
			BaseWorldGen.GenerateChest(x, y, type, chestStyle, stackIDs, stackAmounts, array, sync);
		}

		public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, int[] stackAmounts, int[] stackPrefixes, bool sync = true)
		{
			int num = WorldGen.PlaceChest(x - 1, y, (ushort)type, false, chestStyle);
			if (num >= 0)
			{
				int num2 = 0;
				while (num2 < Main.chest[num].item.Length && stackIDs != null && stackIDs.Length > num2)
				{
					Main.chest[num].item[num2].SetDefaults(stackIDs[num2], false);
					Main.chest[num].item[num2].stack = stackAmounts[num2];
					if (stackPrefixes[num2] != -10)
					{
						Main.chest[num].item[num2].Prefix(stackPrefixes[num2]);
					}
					num2++;
				}
			}
			WorldGen.SquareTileFrame(x + 1, y, true);
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, x, y, 2, 0);
			}
		}

		public static void SmoothTiles(int topX, int topY, int bottomX, int bottomY)
		{
			Main.tileSolid[137] = false;
			for (int i = topX; i < bottomX; i++)
			{
				for (int j = topY; j < bottomY; j++)
				{
					if (Main.tile[i, j].type != 48 && Main.tile[i, j].type != 137 && Main.tile[i, j].type != 232 && Main.tile[i, j].type != 191 && Main.tile[i, j].type != 151 && Main.tile[i, j].type != 274)
					{
						if (!Main.tile[i, j - 1].active())
						{
							if (WorldGen.SolidTile(i, j))
							{
								if (!Main.tile[i - 1, j].halfBrick() && !Main.tile[i + 1, j].halfBrick() && Main.tile[i - 1, j].slope() == 0 && Main.tile[i + 1, j].slope() == 0)
								{
									if (WorldGen.SolidTile(i, j + 1))
									{
										if (!WorldGen.SolidTile(i - 1, j) && !Main.tile[i - 1, j + 1].halfBrick() && WorldGen.SolidTile(i - 1, j + 1) && WorldGen.SolidTile(i + 1, j) && !Main.tile[i + 1, j - 1].active())
										{
											if (WorldGen.genRand.Next(2) == 0)
											{
												WorldGen.SlopeTile(i, j, 2);
											}
											else
											{
												WorldGen.PoundTile(i, j);
											}
										}
										else if (!WorldGen.SolidTile(i + 1, j) && !Main.tile[i + 1, j + 1].halfBrick() && WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j) && !Main.tile[i - 1, j - 1].active())
										{
											if (WorldGen.genRand.Next(2) == 0)
											{
												WorldGen.SlopeTile(i, j, 1);
											}
											else
											{
												WorldGen.PoundTile(i, j);
											}
										}
										else if (WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j + 1) && !Main.tile[i + 1, j].active() && !Main.tile[i - 1, j].active())
										{
											WorldGen.PoundTile(i, j);
										}
										if (WorldGen.SolidTile(i, j))
										{
											if (WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i + 1, j + 2) && !Main.tile[i + 1, j].active() && !Main.tile[i + 1, j + 1].active() && !Main.tile[i - 1, j - 1].active())
											{
												WorldGen.KillTile(i, j, false, false, false);
											}
											else if (WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i - 1, j + 2) && !Main.tile[i - 1, j].active() && !Main.tile[i - 1, j + 1].active() && !Main.tile[i + 1, j - 1].active())
											{
												WorldGen.KillTile(i, j, false, false, false);
											}
											else if (!Main.tile[i - 1, j + 1].active() && !Main.tile[i - 1, j].active() && WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i, j + 2))
											{
												if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.KillTile(i, j, false, false, false);
												}
												else if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.PoundTile(i, j);
												}
												else
												{
													WorldGen.SlopeTile(i, j, 2);
												}
											}
											else if (!Main.tile[i + 1, j + 1].active() && !Main.tile[i + 1, j].active() && WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i, j + 2))
											{
												if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.KillTile(i, j, false, false, false);
												}
												else if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.PoundTile(i, j);
												}
												else
												{
													WorldGen.SlopeTile(i, j, 1);
												}
											}
										}
									}
									if (WorldGen.SolidTile(i, j) && !Main.tile[i - 1, j].active() && !Main.tile[i + 1, j].active())
									{
										WorldGen.KillTile(i, j, false, false, false);
									}
								}
							}
							else if (!Main.tile[i, j].active() && Main.tile[i, j + 1].type != 151 && Main.tile[i, j + 1].type != 274)
							{
								if (Main.tile[i + 1, j].type != 190 && Main.tile[i + 1, j].type != 48 && Main.tile[i + 1, j].type != 232 && WorldGen.SolidTile(i - 1, j + 1) && WorldGen.SolidTile(i + 1, j) && !Main.tile[i - 1, j].active() && !Main.tile[i + 1, j - 1].active())
								{
									WorldGen.PlaceTile(i, j, (int)Main.tile[i, j + 1].type, false, false, -1, 0);
									if (WorldGen.genRand.Next(2) == 0)
									{
										WorldGen.SlopeTile(i, j, 2);
									}
									else
									{
										WorldGen.PoundTile(i, j);
									}
								}
								if (Main.tile[i - 1, j].type != 190 && Main.tile[i - 1, j].type != 48 && Main.tile[i - 1, j].type != 232 && WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j) && !Main.tile[i + 1, j].active() && !Main.tile[i - 1, j - 1].active())
								{
									WorldGen.PlaceTile(i, j, (int)Main.tile[i, j + 1].type, false, false, -1, 0);
									if (WorldGen.genRand.Next(2) == 0)
									{
										WorldGen.SlopeTile(i, j, 1);
									}
									else
									{
										WorldGen.PoundTile(i, j);
									}
								}
							}
						}
						else if (!Main.tile[i, j + 1].active() && WorldGen.genRand.Next(2) == 0 && WorldGen.SolidTile(i, j) && !Main.tile[i - 1, j].halfBrick() && !Main.tile[i + 1, j].halfBrick() && Main.tile[i - 1, j].slope() == 0 && Main.tile[i + 1, j].slope() == 0 && WorldGen.SolidTile(i, j - 1))
						{
							if (WorldGen.SolidTile(i - 1, j) && !WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i - 1, j - 1))
							{
								WorldGen.SlopeTile(i, j, 3);
							}
							else if (WorldGen.SolidTile(i + 1, j) && !WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i + 1, j - 1))
							{
								WorldGen.SlopeTile(i, j, 4);
							}
						}
					}
				}
			}
			for (int k = topX; k < bottomX; k++)
			{
				for (int l = topY; l < bottomY; l++)
				{
					if (WorldGen.genRand.Next(2) == 0 && !Main.tile[k, l - 1].active() && Main.tile[k, l].type != 137 && Main.tile[k, l].type != 48 && Main.tile[k, l].type != 232 && Main.tile[k, l].type != 191 && Main.tile[k, l].type != 151 && Main.tile[k, l].type != 274 && Main.tile[k, l].type != 75 && Main.tile[k, l].type != 76 && WorldGen.SolidTile(k, l) && Main.tile[k - 1, l].type != 137 && Main.tile[k + 1, l].type != 137)
					{
						if (WorldGen.SolidTile(k, l + 1) && WorldGen.SolidTile(k + 1, l) && !Main.tile[k - 1, l].active())
						{
							WorldGen.SlopeTile(k, l, 2);
						}
						if (WorldGen.SolidTile(k, l + 1) && WorldGen.SolidTile(k - 1, l) && !Main.tile[k + 1, l].active())
						{
							WorldGen.SlopeTile(k, l, 1);
						}
					}
					if (Main.tile[k, l].slope() == 1 && !WorldGen.SolidTile(k - 1, l))
					{
						WorldGen.SlopeTile(k, l, 0);
						WorldGen.PoundTile(k, l);
					}
					if (Main.tile[k, l].slope() == 2 && !WorldGen.SolidTile(k + 1, l))
					{
						WorldGen.SlopeTile(k, l, 0);
						WorldGen.PoundTile(k, l);
					}
				}
			}
			Main.tileSolid[137] = true;
		}

		public class GenHelper
		{
			public GenHelper(Action<int, int> gen)
			{
				this.generate = gen;
			}

			public void Gen(int x, int y)
			{
				this.Gen(x, y, this.rotX, this.rotY, this.rotation);
			}

			public void Gen(int x, int y, int rotationX, int rotationY, float genRotation)
			{
				this.tiles.Clear();
				Tile[,] tile = Main.tile;
				Main.tile = new Tile[Main.maxTilesX, Main.maxTilesY];
				this.generate(x, y);
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					for (int j = 0; j < Main.maxTilesY; j++)
					{
						Tile tile2 = Main.tile[i, j];
						if (tile2 != null)
						{
							this.tiles.Add(new BaseWorldGen.GenHelper.TileData(i, j, tile2));
						}
					}
				}
				Main.tile = tile;
				Vector2 origin;
				origin..ctor((float)((x + rotationX) * 16), (float)((y + rotationY) * 16));
				List<Point> list = new List<Point>();
				foreach (BaseWorldGen.GenHelper.TileData tileData in this.tiles)
				{
					Vector2 vecToRot;
					vecToRot..ctor((float)(tileData.X * 16), (float)(tileData.Y * 16));
					vecToRot = BaseUtility.RotateVector(origin, vecToRot, genRotation);
					int num = (int)vecToRot.X / 16;
					int num2 = (int)vecToRot.Y / 16;
					if (vecToRot.X % 16f > 0f)
					{
						num--;
					}
					if (vecToRot.Y % 16f > 0f)
					{
						num2--;
					}
					Point point;
					point..ctor(num, num2);
					new Point?(point);
					list.Add(point);
					Main.tile[num, num2] = tileData.tile;
				}
				foreach (Point point2 in list)
				{
					WorldGen.TileFrame(point2.X, point2.Y, false, false);
					Tile tile3 = Main.tile[point2.X, point2.Y];
					if (tile3 != null && tile3.wall > 0)
					{
						Framing.WallFrame(point2.X, point2.Y, false);
					}
				}
				list.Clear();
			}

			public bool CheckTile(ref int x, ref int y, ref Point point, int offsetX, int offsetY)
			{
				int num = x + offsetX;
				int num2 = y + offsetY;
				if (this.ValidTile(num, num2))
				{
					x = num;
					y = num2;
					point = new Point(num, num2);
					return true;
				}
				return false;
			}

			public bool ValidTile(int x, int y)
			{
				return Main.tile[x, y] == null || (!Main.tile[x, y].active() && Main.tile[x, y].wall == 0);
			}

			public List<BaseWorldGen.GenHelper.TileData> tiles = new List<BaseWorldGen.GenHelper.TileData>();

			public Action<int, int> generate;

			public float rotation;

			public int rotX;

			public int rotY;

			public class TileData
			{
				public TileData(int i, int j, Tile t)
				{
					this.X = i;
					this.Y = j;
					this.tile = t;
				}

				public int X;

				public int Y;

				public Tile tile;
			}
		}
	}
}
