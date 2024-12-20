using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ObjectData;

namespace Redemption
{
	public class BaseWorldGen
	{
		public static Tile GetTileSafely(Vector2 position)
		{
			return BaseWorldGen.GetTileSafely((int)(position.X / 16f), (int)(position.Y / 16f));
		}

		public static Tile GetTileSafely(int x, int y)
		{
			if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
			{
				return new Tile();
			}
			return Framing.GetTileSafely(x, y);
		}

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
				amountInWorld = (int)((float)(Main.maxTilesX / 4200) * 50f);
			}
			for (int count = 0; count < amountInWorld; count++)
			{
				int num = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				int j2 = WorldGen.genRand.Next(heightLimit, Main.maxTilesY - 150);
				WorldGen.OreRunner(num, j2, (double)oreStrength, oreSteps, (ushort)tileType);
			}
		}

		public static int GetFirstTileFloor(int x, int startY, bool solid = true)
		{
			if (!WorldGen.InWorld(x, startY, 0))
			{
				return startY;
			}
			for (int y = startY; y < Main.maxTilesY - 10; y++)
			{
				Tile tile = Framing.GetTileSafely(x, y);
				if (tile != null && tile.nactive() && (!solid || Main.tileSolid[(int)tile.type]))
				{
					return y;
				}
			}
			return Main.maxTilesY - 10;
		}

		public static int GetFirstTileCeiling(int x, int startY, bool solid = true)
		{
			if (!WorldGen.InWorld(x, startY, 0))
			{
				return startY;
			}
			for (int y = startY; y > 10; y--)
			{
				Tile tile = Framing.GetTileSafely(x, y);
				if (tile != null && tile.nactive() && (!solid || Main.tileSolid[(int)tile.type]))
				{
					return y;
				}
			}
			return 10;
		}

		public static int GetFirstTileSide(int startX, int y, bool left, bool solid = true)
		{
			if (!WorldGen.InWorld(startX, y, 0))
			{
				return startX;
			}
			if (left)
			{
				for (int x = startX; x > 10; x--)
				{
					Tile tile = Framing.GetTileSafely(x, y);
					if (tile != null && tile.nactive() && (!solid || Main.tileSolid[(int)tile.type]))
					{
						return x;
					}
				}
				return 10;
			}
			for (int x2 = startX; x2 < Main.maxTilesX - 10; x2++)
			{
				Tile tile2 = Framing.GetTileSafely(x2, y);
				if (tile2 != null && tile2.nactive() && (!solid || Main.tileSolid[(int)tile2.type]))
				{
					return x2;
				}
			}
			return Main.maxTilesX - 10;
		}

		public static int GetBelowFloatingIslandY()
		{
			int size = BaseWorldGen.GetWorldSize();
			return ((size == 1) ? 1200 : ((size == 2) ? 1600 : ((size == 3) ? 2000 : 1200))) + 1;
		}

		public static int GetWorldSize()
		{
			if (Main.maxTilesX == 4200)
			{
				return 1;
			}
			if (Main.maxTilesX == 6400)
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
			int radiusLeft = (int)(position.X / 16f - (float)radius);
			int radiusRight = (int)(position.X / 16f + (float)radius);
			int radiusUp = (int)(position.Y / 16f - (float)radius);
			int radiusDown = (int)(position.Y / 16f + (float)radius);
			if (radiusLeft < 0)
			{
				radiusLeft = 0;
			}
			if (radiusRight > Main.maxTilesX)
			{
				radiusRight = Main.maxTilesX;
			}
			if (radiusUp < 0)
			{
				radiusUp = 0;
			}
			if (radiusDown > Main.maxTilesY)
			{
				radiusDown = Main.maxTilesY;
			}
			float distRad = (float)radius * 16f;
			for (int x = radiusLeft; x <= radiusRight; x++)
			{
				for (int y = radiusUp; y <= radiusDown; y++)
				{
					double dist = (double)Vector2.Distance(new Vector2((float)x * 16f + 8f, (float)y * 16f + 8f), position);
					if (WorldGen.InWorld(x, y, 0) && dist < (double)distRad && Main.tile[x, y] != null && Main.tile[x, y].active())
					{
						int currentType = (int)Main.tile[x, y].type;
						int index = 0;
						if (BaseUtility.InArray(tiles, currentType, ref index))
						{
							BaseWorldGen.GenerateTile(x, y, replacements[index], -1, 0, true, false, -2, silent, false);
						}
					}
				}
			}
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, (int)(position.X / 16f), (int)(position.Y / 16f), radius * 2 + 2, 0);
			}
		}

		public static void ReplaceWalls(Vector2 position, int radius, int[] walls, int[] replacements, bool silent = false, bool sync = true)
		{
			int radiusLeft = (int)(position.X / 16f - (float)radius);
			int radiusRight = (int)(position.X / 16f + (float)radius);
			int radiusUp = (int)(position.Y / 16f - (float)radius);
			int radiusDown = (int)(position.Y / 16f + (float)radius);
			if (radiusLeft < 0)
			{
				radiusLeft = 0;
			}
			if (radiusRight > Main.maxTilesX)
			{
				radiusRight = Main.maxTilesX;
			}
			if (radiusUp < 0)
			{
				radiusUp = 0;
			}
			if (radiusDown > Main.maxTilesY)
			{
				radiusDown = Main.maxTilesY;
			}
			float distRad = (float)radius * 16f;
			for (int x = radiusLeft; x <= radiusRight; x++)
			{
				for (int y = radiusUp; y <= radiusDown; y++)
				{
					double dist = (double)Vector2.Distance(new Vector2((float)x * 16f + 8f, (float)y * 16f + 8f), position);
					if (WorldGen.InWorld(x, y, 0) && dist < (double)distRad && Main.tile[x, y] != null)
					{
						int currentType = (int)Main.tile[x, y].wall;
						int index = 0;
						if (BaseUtility.InArray(walls, currentType, ref index))
						{
							BaseWorldGen.GenerateTile(x, y, -1, replacements[index], 0, true, false, -2, silent, false);
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
			if (!WorldGen.InWorld(x, y, 0))
			{
				return;
			}
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
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
			for (int x2 = 0; x2 < width; x2++)
			{
				for (int y2 = 0; y2 < height; y2++)
				{
					BaseWorldGen.GenerateLiquid(x2 + x, y2 + y, liquidType, updateFlow, liquidHeight, false);
				}
			}
			int size = (width > height) ? width : height;
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, x + (int)((float)width * 0.5f) - 1, y + (int)((float)height * 0.5f) - 1, size + 4, 0);
			}
		}

		public static void GenerateTile(int x, int y, int tile, int wall, int tileStyle = 0, bool active = true, bool removeLiquid = true, int slope = -2, bool silent = false, bool sync = true)
		{
			try
			{
				if (WorldGen.InWorld(x, y, 0))
				{
					if (Main.tile[x, y] == null)
					{
						Main.tile[x, y] = new Tile();
					}
					TileObjectData data = (tile <= -1) ? null : TileObjectData.GetTileData(tile, tileStyle, 0);
					int width = (data == null) ? 1 : data.Width;
					int height = (data == null) ? 1 : data.Height;
					int tileWidth = (tile == -1 || data == null) ? 1 : data.Width;
					int tileHeight = (tile == -1 || data == null) ? 1 : data.Height;
					byte oldSlope = Main.tile[x, y].slope();
					bool oldHalfBrick = Main.tile[x, y].halfBrick();
					if (tile != -1)
					{
						WorldGen.destroyObject = true;
						if (width > 1 || height > 1)
						{
							Vector2 newPos = BaseTile.FindTopLeft(x, y);
							for (int x2 = 0; x2 < width; x2++)
							{
								for (int y2 = 0; y2 < height; y2++)
								{
									int x3 = (int)newPos.X + x2;
									int y3 = (int)newPos.Y + y2;
									if (x2 == 0 && y2 == 0 && Main.tile[x3, y3].type == 21)
									{
										BaseWorldGen.KillChestAndItems(x3, y3);
									}
									Main.tile[x, y].type = 0;
									Main.tile[x, y].active(false);
									if (!silent)
									{
										WorldGen.KillTile(x, y, false, false, true);
									}
									if (removeLiquid)
									{
										BaseWorldGen.GenerateLiquid(x3, y3, 0, true, 0, false);
									}
								}
							}
							for (int x4 = 0; x4 < width; x4++)
							{
								for (int y4 = 0; y4 < height; y4++)
								{
									int x5 = (int)newPos.X + x4;
									int y5 = (int)newPos.Y + y4;
									WorldGen.SquareTileFrame(x5, y5, true);
									WorldGen.SquareWallFrame(x5, y5, true);
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
							if (tileWidth <= 1 && tileHeight <= 1 && !Main.tileFrameImportant[tile])
							{
								Main.tile[x, y].type = (ushort)tile;
								Main.tile[x, y].active(true);
								if (slope == -2 && oldHalfBrick)
								{
									Main.tile[x, y].halfBrick(true);
								}
								else if (slope == -1)
								{
									Main.tile[x, y].halfBrick(true);
								}
								else
								{
									Main.tile[x, y].slope((slope == -2) ? oldSlope : ((byte)slope));
								}
								WorldGen.SquareTileFrame(x, y, true);
							}
							else
							{
								WorldGen.destroyObject = true;
								if (!silent)
								{
									for (int x6 = 0; x6 < tileWidth; x6++)
									{
										for (int y6 = 0; y6 < tileHeight; y6++)
										{
											WorldGen.KillTile(x + x6, y + y6, false, false, true);
										}
									}
								}
								WorldGen.destroyObject = false;
								int genY = (tile == 10) ? y : (y + height);
								WorldGen.PlaceTile(x, genY, tile, true, true, -1, tileStyle);
								for (int x7 = 0; x7 < tileWidth; x7++)
								{
									for (int y7 = 0; y7 < tileHeight; y7++)
									{
										WorldGen.SquareTileFrame(x + x7, y + y7, true);
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
						int sizeWidth = tileWidth + Math.Max(0, width - 1);
						int sizeHeight = tileHeight + Math.Max(0, height - 1);
						int size = (sizeWidth > sizeHeight) ? sizeWidth : sizeHeight;
						NetMessage.SendTileSquare(-1, x + (int)((float)size * 0.5f), y + (int)((float)size * 0.5f), size + 1, 0);
					}
				}
			}
			catch (Exception e)
			{
				BaseUtility.LogFancy("Redemption~ TILEGEN ERROR:", e);
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
			bool negativeY = endY < y;
			if (negativeY)
			{
				x += Math.Abs(endX - x);
			}
			if (x == endX && y == endY)
			{
				int tileID = gen.GetTile(0);
				int wallID = gen.GetWall(0);
				if ((tileID > -1 && gen.CanPlace != null && !gen.CanPlace(x, y, tileID, wallID)) || (wallID > -1 && gen.CanPlaceWall != null && !gen.CanPlaceWall(x, y, tileID, wallID)))
				{
					return;
				}
				BaseWorldGen.GenerateTile(x, y, tileID, wallID, 0, tileID != -1, true, 0, false, sync);
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
				bool vertical = x == endX;
				int tileIndex = -1;
				int wallIndex = -1;
				for (int i = 0; i < (vertical ? (endY - y) : (endX - x)); i++)
				{
					for (int j = 0; j < thickness; j++)
					{
						tileIndex = ((gen.tiles == null) ? -1 : (gen.orderTiles ? (tileIndex + 1) : WorldGen.genRand.Next(gen.tiles.Length)));
						wallIndex = ((gen.walls == null) ? -1 : (gen.orderWalls ? (wallIndex + 1) : WorldGen.genRand.Next(gen.walls.Length)));
						if (tileIndex != -1 && tileIndex >= gen.tiles.Length)
						{
							tileIndex = 0;
						}
						if (wallIndex != -1 && wallIndex >= gen.walls.Length)
						{
							wallIndex = 0;
						}
						int addonX = vertical ? j : i;
						int addonY = vertical ? i : j;
						int x2 = x + addonX;
						int y2 = y + addonY;
						bool flag = tileIndex == -1 || gen.CanPlace == null || gen.CanPlace(x2, y2, gen.GetTile(tileIndex), gen.GetWall(wallIndex));
						bool wallValid = wallIndex == -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(x2, y2, gen.GetTile(tileIndex), gen.GetWall(wallIndex));
						if (flag && wallValid)
						{
							BaseWorldGen.GenerateTile(x2, y2, gen.GetTile(tileIndex), gen.GetWall(wallIndex), 0, gen.GetTile(tileIndex) != -1, true, 0, false, false);
						}
					}
				}
				bool slope = gen.slope;
				if (sync && Main.netMode != 0)
				{
					int size = (endY - y > endX - x) ? (endY - y) : (endX - x);
					if (thickness > size)
					{
						size = thickness;
					}
					NetMessage.SendData(20, -1, -1, NetworkText.FromLiteral(""), size, (float)x, (float)y, 0f, 0, 0, 0);
					return;
				}
			}
			else
			{
				Vector2 start = new Vector2((float)x, (float)y);
				Vector2 end = new Vector2((float)endX, (float)endY);
				Vector2 dir = new Vector2((float)endX, (float)endY) - new Vector2((float)x, (float)y);
				dir.Normalize();
				float length = Vector2.Distance(start, end);
				float way = 0f;
				float rot = BaseUtility.RotationTo(start, end);
				if (rot < 0f)
				{
					rot = 6.2831855f - Math.Abs(rot);
				}
				float rotPercent = MathHelper.Lerp(0f, 1f, rot / 6.2831855f);
				bool horizontal = rotPercent < 0.125f || (rotPercent > 0.375f && rotPercent < 0.625f) || rotPercent > 0.825f;
				int tileIndex2 = -1;
				int wallIndex2 = -1;
				int lastX = x;
				int lastY = y;
				while (way < length)
				{
					Vector2 v = start + dir * way;
					Point point = new Point((int)v.X, (int)v.Y);
					for (int k = 0; k < thickness; k++)
					{
						tileIndex2 = ((gen.tiles == null) ? -1 : (gen.orderTiles ? (tileIndex2 + 1) : WorldGen.genRand.Next(gen.tiles.Length)));
						wallIndex2 = ((gen.walls == null) ? -1 : (gen.orderWalls ? (wallIndex2 + 1) : WorldGen.genRand.Next(gen.walls.Length)));
						if (tileIndex2 != -1 && tileIndex2 >= gen.tiles.Length)
						{
							tileIndex2 = 0;
						}
						if (wallIndex2 != -1 && wallIndex2 >= gen.walls.Length)
						{
							wallIndex2 = 0;
						}
						int addonX2 = horizontal ? 0 : k;
						int addonY2 = horizontal ? k : 0;
						int x3 = point.X + addonX2;
						int y3 = negativeY ? (point.Y - addonY2) : (point.Y + addonY2);
						bool flag2 = tileIndex2 == -1 || gen.CanPlace == null || gen.CanPlace(x3, y3, gen.GetTile(tileIndex2), gen.GetWall(wallIndex2));
						bool wallValid2 = wallIndex2 == -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(x3, y3, gen.GetTile(tileIndex2), gen.GetWall(wallIndex2));
						if (flag2 && wallValid2)
						{
							BaseWorldGen.GenerateTile(x3, y3, gen.GetTile(tileIndex2), gen.GetWall(wallIndex2), 0, gen.GetTile(tileIndex2) != -1, true, 0, false, false);
						}
					}
					if (sync && Main.netMode != 0 && ((!horizontal && Math.Abs(lastY - point.Y) >= 5) || (horizontal && Math.Abs(lastY - point.Y) >= 5) || way + 1f > length))
					{
						int size2 = Math.Max(5, thickness);
						NetMessage.SendData(10, -1, -1, NetworkText.FromLiteral(""), lastX, (float)lastY, (float)size2, (float)size2, 0, 0, 0);
						lastX = point.X;
						lastY = point.Y;
					}
					way += 1f;
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
			bool negativeY = endY < y;
			int nx = flag ? -1 : 1;
			int ny = negativeY ? -1 : 1;
			Vector2 start = new Vector2((float)x, (float)y);
			Vector2 end = new Vector2((float)endX, (float)endY);
			float rotPercent = MathHelper.Lerp(0f, 1f, BaseUtility.RotationTo(start, end) / 6.2831855f);
			bool horizontal = rotPercent < 0.125f || (rotPercent > 0.375f && rotPercent < 0.625f) || rotPercent > 0.825f;
			Vector2 topEnd = new Vector2((float)endX, (float)endY);
			(new int[1])[0] = -2;
			Vector2 wallStart = new Vector2((float)(horizontal ? x : (x + 2 * nx)), (float)(horizontal ? (y + 2 * ny) : y));
			Vector2 wallEnd = new Vector2((float)(horizontal ? endX : (endX + 2 * nx)), (float)(horizontal ? (endY + 2 * ny) : endY));
			Vector2 bottomStart = new Vector2((float)(horizontal ? x : (x + (thickness * 2 + height) * nx)), (float)(horizontal ? (y + (thickness * 2 + height) * ny) : y));
			Vector2 bottomEnd = new Vector2((float)(horizontal ? endX : (endX + (thickness * 2 + height) * nx)), (float)(horizontal ? (endY + (thickness * 2 + height) * ny) : endY));
			int[] tiles = gen.tiles;
			int[] walls = gen.walls;
			gen.tiles = null;
			BaseWorldGen.GenerateLine(gen, (int)wallStart.X, (int)wallStart.Y, (int)wallEnd.X, (int)wallEnd.Y, thickness * 3 + height - 2, false);
			gen.tiles = tiles;
			gen.walls = null;
			BaseWorldGen.GenerateLine(gen, x, y, (int)topEnd.X, (int)topEnd.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, (int)bottomStart.X, (int)bottomStart.Y, (int)bottomEnd.X, (int)bottomEnd.Y, thickness, false);
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
			Vector2 start = new Vector2((float)x, (float)y);
			Vector2 end = new Vector2((float)endX, (float)endY);
			float rotPercent = MathHelper.Lerp(0f, 1f, BaseUtility.RotationTo(start, end) / 6.2831855f);
			bool horizontal = rotPercent < 0.125f || (rotPercent > 0.375f && rotPercent < 0.625f) || rotPercent > 0.825f;
			Vector2 topEnd = new Vector2((float)endX, (float)endY);
			Vector2 wallStart = new Vector2((float)(x + thickness), (float)(y + thickness));
			Vector2 wallEnd = new Vector2((float)(horizontal ? endX : (endX + thickness)), (float)(horizontal ? (endY + thickness) : endY));
			Vector2 bottomStart = new Vector2((float)(horizontal ? x : (x + thickness * 2 + height)), (float)(horizontal ? (y + thickness * 2 + height) : y));
			Vector2 bottomEnd = new Vector2((float)(horizontal ? endX : (endX + thickness * 2 + height)), (float)(horizontal ? (endY + thickness * 2 + height) : endY));
			int[] tiles = gen.tiles;
			int[] walls = gen.walls;
			gen.tiles = null;
			BaseWorldGen.GenerateLine(gen, (int)wallStart.X, (int)wallStart.Y, (int)wallEnd.X, (int)wallEnd.Y, thickness + height, false);
			gen.tiles = tiles;
			gen.walls = null;
			BaseWorldGen.GenerateLine(gen, x, y, (int)topEnd.X, (int)topEnd.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, (int)bottomStart.X, (int)bottomStart.Y, (int)bottomEnd.X, (int)bottomEnd.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, x, y, (int)bottomStart.X, (int)bottomStart.Y, thickness, false);
			BaseWorldGen.GenerateLine(gen, (int)topEnd.X, (int)topEnd.Y, horizontal ? ((int)bottomEnd.X) : ((int)bottomEnd.X + thickness), horizontal ? ((int)bottomEnd.Y + thickness) : ((int)bottomEnd.Y), thickness, false);
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
			for (int x2 = 0; x2 < width; x2++)
			{
				for (int y2 = 0; y2 < height; y2++)
				{
					int x3 = x2 + x;
					int y3 = y2 + y;
					if ((wallEnds || tileCeiling != -1) && y2 < ceilingThickness)
					{
						BaseWorldGen.GenerateTile(x3, y3, tileCeiling, (wallEnds && y2 == 0) ? wall : -1, 0, tileCeiling != -1 || !wallEnds, true, 0, false, false);
					}
					else if ((wallEnds || tileFloor != -1) && y2 >= height - floorThickness)
					{
						BaseWorldGen.GenerateTile(x3, y3, tileFloor, (wallEnds && y2 >= height - 1) ? wall : -1, 0, tileFloor != -1 || !wallEnds, true, 0, false, false);
					}
					else if ((wallEnds || tileSides != -1) && (x2 < sideThickness || x2 >= width - sideThickness))
					{
						BaseWorldGen.GenerateTile(x3, y3, tileSides, (wallEnds && x2 > 0 && x2 < width - 1) ? wall : -1, 0, tileSides != -1 || !wallEnds, true, 0, false, false);
					}
					else if (x2 >= sideThickness && x2 < width - sideThickness && y2 >= ceilingThickness && y2 < height - floorThickness)
					{
						BaseWorldGen.GenerateTile(x3, y3, -1, wall, 0, false, true, 0, false, false);
					}
				}
			}
			int size = (width > height) ? width : height;
			if (sync && Main.netMode != 0)
			{
				NetMessage.SendTileSquare(-1, x + (int)((float)width * 0.5f) - 1, y + (int)((float)height * 0.5f) - 1, size + 4, 0);
			}
		}

		public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, bool randomAmounts = false, bool randomPrefix = false, bool sync = true)
		{
			int[] amounts = new int[20];
			for (int i = 0; i < amounts.Length; i++)
			{
				if (randomAmounts)
				{
					amounts[i] = WorldGen.genRand.Next(1, 6);
				}
				else
				{
					amounts[i] = 1;
				}
			}
			BaseWorldGen.GenerateChest(x, y, type, chestStyle, stackIDs, amounts, randomPrefix, sync);
		}

		public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, int[] stackAmounts, bool randomPrefix = false, bool sync = true)
		{
			int[] prefixes = new int[20];
			for (int i = 0; i < prefixes.Length; i++)
			{
				if (randomPrefix)
				{
					prefixes[i] = -1;
				}
				else
				{
					prefixes[i] = -10;
				}
			}
			BaseWorldGen.GenerateChest(x, y, type, chestStyle, stackIDs, stackAmounts, prefixes, sync);
		}

		public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, int[] stackAmounts, int[] stackPrefixes, bool sync = true)
		{
			int num2 = WorldGen.PlaceChest(x - 1, y, (ushort)type, false, chestStyle);
			if (num2 >= 0)
			{
				int i = 0;
				while (i < Main.chest[num2].item.Length && stackIDs != null && stackIDs.Length > i)
				{
					Main.chest[num2].item[i].SetDefaults(stackIDs[i], false);
					Main.chest[num2].item[i].stack = stackAmounts[i];
					if (stackPrefixes[i] != -10)
					{
						Main.chest[num2].item[i].Prefix(stackPrefixes[i]);
					}
					i++;
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
			for (int x = topX; x < bottomX; x++)
			{
				for (int y = topY; y < bottomY; y++)
				{
					if (Main.tile[x, y].type != 48 && Main.tile[x, y].type != 137 && Main.tile[x, y].type != 232 && Main.tile[x, y].type != 191 && Main.tile[x, y].type != 151 && Main.tile[x, y].type != 274)
					{
						if (!Main.tile[x, y - 1].active())
						{
							if (WorldGen.SolidTile(x, y))
							{
								if (!Main.tile[x - 1, y].halfBrick() && !Main.tile[x + 1, y].halfBrick() && Main.tile[x - 1, y].slope() == 0 && Main.tile[x + 1, y].slope() == 0)
								{
									if (WorldGen.SolidTile(x, y + 1))
									{
										if (!WorldGen.SolidTile(x - 1, y) && !Main.tile[x - 1, y + 1].halfBrick() && WorldGen.SolidTile(x - 1, y + 1) && WorldGen.SolidTile(x + 1, y) && !Main.tile[x + 1, y - 1].active())
										{
											if (WorldGen.genRand.Next(2) == 0)
											{
												WorldGen.SlopeTile(x, y, 2);
											}
											else
											{
												WorldGen.PoundTile(x, y);
											}
										}
										else if (!WorldGen.SolidTile(x + 1, y) && !Main.tile[x + 1, y + 1].halfBrick() && WorldGen.SolidTile(x + 1, y + 1) && WorldGen.SolidTile(x - 1, y) && !Main.tile[x - 1, y - 1].active())
										{
											if (WorldGen.genRand.Next(2) == 0)
											{
												WorldGen.SlopeTile(x, y, 1);
											}
											else
											{
												WorldGen.PoundTile(x, y);
											}
										}
										else if (WorldGen.SolidTile(x + 1, y + 1) && WorldGen.SolidTile(x - 1, y + 1) && !Main.tile[x + 1, y].active() && !Main.tile[x - 1, y].active())
										{
											WorldGen.PoundTile(x, y);
										}
										if (WorldGen.SolidTile(x, y))
										{
											if (WorldGen.SolidTile(x - 1, y) && WorldGen.SolidTile(x + 1, y + 2) && !Main.tile[x + 1, y].active() && !Main.tile[x + 1, y + 1].active() && !Main.tile[x - 1, y - 1].active())
											{
												WorldGen.KillTile(x, y, false, false, false);
											}
											else if (WorldGen.SolidTile(x + 1, y) && WorldGen.SolidTile(x - 1, y + 2) && !Main.tile[x - 1, y].active() && !Main.tile[x - 1, y + 1].active() && !Main.tile[x + 1, y - 1].active())
											{
												WorldGen.KillTile(x, y, false, false, false);
											}
											else if (!Main.tile[x - 1, y + 1].active() && !Main.tile[x - 1, y].active() && WorldGen.SolidTile(x + 1, y) && WorldGen.SolidTile(x, y + 2))
											{
												if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.KillTile(x, y, false, false, false);
												}
												else if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.PoundTile(x, y);
												}
												else
												{
													WorldGen.SlopeTile(x, y, 2);
												}
											}
											else if (!Main.tile[x + 1, y + 1].active() && !Main.tile[x + 1, y].active() && WorldGen.SolidTile(x - 1, y) && WorldGen.SolidTile(x, y + 2))
											{
												if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.KillTile(x, y, false, false, false);
												}
												else if (WorldGen.genRand.Next(5) == 0)
												{
													WorldGen.PoundTile(x, y);
												}
												else
												{
													WorldGen.SlopeTile(x, y, 1);
												}
											}
										}
									}
									if (WorldGen.SolidTile(x, y) && !Main.tile[x - 1, y].active() && !Main.tile[x + 1, y].active())
									{
										WorldGen.KillTile(x, y, false, false, false);
									}
								}
							}
							else if (!Main.tile[x, y].active() && Main.tile[x, y + 1].type != 151 && Main.tile[x, y + 1].type != 274)
							{
								if (Main.tile[x + 1, y].type != 190 && Main.tile[x + 1, y].type != 48 && Main.tile[x + 1, y].type != 232 && WorldGen.SolidTile(x - 1, y + 1) && WorldGen.SolidTile(x + 1, y) && !Main.tile[x - 1, y].active() && !Main.tile[x + 1, y - 1].active())
								{
									WorldGen.PlaceTile(x, y, (int)Main.tile[x, y + 1].type, false, false, -1, 0);
									if (WorldGen.genRand.Next(2) == 0)
									{
										WorldGen.SlopeTile(x, y, 2);
									}
									else
									{
										WorldGen.PoundTile(x, y);
									}
								}
								if (Main.tile[x - 1, y].type != 190 && Main.tile[x - 1, y].type != 48 && Main.tile[x - 1, y].type != 232 && WorldGen.SolidTile(x + 1, y + 1) && WorldGen.SolidTile(x - 1, y) && !Main.tile[x + 1, y].active() && !Main.tile[x - 1, y - 1].active())
								{
									WorldGen.PlaceTile(x, y, (int)Main.tile[x, y + 1].type, false, false, -1, 0);
									if (WorldGen.genRand.Next(2) == 0)
									{
										WorldGen.SlopeTile(x, y, 1);
									}
									else
									{
										WorldGen.PoundTile(x, y);
									}
								}
							}
						}
						else if (!Main.tile[x, y + 1].active() && WorldGen.genRand.Next(2) == 0 && WorldGen.SolidTile(x, y) && !Main.tile[x - 1, y].halfBrick() && !Main.tile[x + 1, y].halfBrick() && Main.tile[x - 1, y].slope() == 0 && Main.tile[x + 1, y].slope() == 0 && WorldGen.SolidTile(x, y - 1))
						{
							if (WorldGen.SolidTile(x - 1, y) && !WorldGen.SolidTile(x + 1, y) && WorldGen.SolidTile(x - 1, y - 1))
							{
								WorldGen.SlopeTile(x, y, 3);
							}
							else if (WorldGen.SolidTile(x + 1, y) && !WorldGen.SolidTile(x - 1, y) && WorldGen.SolidTile(x + 1, y - 1))
							{
								WorldGen.SlopeTile(x, y, 4);
							}
						}
					}
				}
			}
			for (int x2 = topX; x2 < bottomX; x2++)
			{
				for (int y2 = topY; y2 < bottomY; y2++)
				{
					if (WorldGen.genRand.Next(2) == 0 && !Main.tile[x2, y2 - 1].active() && Main.tile[x2, y2].type != 137 && Main.tile[x2, y2].type != 48 && Main.tile[x2, y2].type != 232 && Main.tile[x2, y2].type != 191 && Main.tile[x2, y2].type != 151 && Main.tile[x2, y2].type != 274 && Main.tile[x2, y2].type != 75 && Main.tile[x2, y2].type != 76 && WorldGen.SolidTile(x2, y2) && Main.tile[x2 - 1, y2].type != 137 && Main.tile[x2 + 1, y2].type != 137)
					{
						if (WorldGen.SolidTile(x2, y2 + 1) && WorldGen.SolidTile(x2 + 1, y2) && !Main.tile[x2 - 1, y2].active())
						{
							WorldGen.SlopeTile(x2, y2, 2);
						}
						if (WorldGen.SolidTile(x2, y2 + 1) && WorldGen.SolidTile(x2 - 1, y2) && !Main.tile[x2 + 1, y2].active())
						{
							WorldGen.SlopeTile(x2, y2, 1);
						}
					}
					if (Main.tile[x2, y2].slope() == 1 && !WorldGen.SolidTile(x2 - 1, y2))
					{
						WorldGen.SlopeTile(x2, y2, 0);
						WorldGen.PoundTile(x2, y2);
					}
					if (Main.tile[x2, y2].slope() == 2 && !WorldGen.SolidTile(x2 + 1, y2))
					{
						WorldGen.SlopeTile(x2, y2, 0);
						WorldGen.PoundTile(x2, y2);
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
				Tile[,] tempTiles = Main.tile;
				Main.tile = new Tile[Main.maxTilesX, Main.maxTilesY];
				this.generate(x, y);
				for (int x2 = 0; x2 < Main.maxTilesX; x2++)
				{
					for (int y2 = 0; y2 < Main.maxTilesY; y2++)
					{
						Tile tile = Main.tile[x2, y2];
						if (tile != null)
						{
							this.tiles.Add(new BaseWorldGen.GenHelper.TileData(x2, y2, tile));
						}
					}
				}
				Main.tile = tempTiles;
				Vector2 rotVec = new Vector2((float)((x + rotationX) * 16), (float)((y + rotationY) * 16));
				List<Point> points = new List<Point>();
				foreach (BaseWorldGen.GenHelper.TileData data in this.tiles)
				{
					Vector2 rot = new Vector2((float)(data.X * 16), (float)(data.Y * 16));
					rot = BaseUtility.RotateVector(rotVec, rot, genRotation);
					int x3 = (int)rot.X / 16;
					int y3 = (int)rot.Y / 16;
					if (rot.X % 16f > 0f)
					{
						x3--;
					}
					if (rot.Y % 16f > 0f)
					{
						y3--;
					}
					Point point = new Point(x3, y3);
					points.Add(point);
					Main.tile[x3, y3] = data.tile;
				}
				foreach (Point point2 in points)
				{
					WorldGen.TileFrame(point2.X, point2.Y, false, false);
					Tile tile2 = Main.tile[point2.X, point2.Y];
					if (tile2 != null && tile2.wall > 0)
					{
						Framing.WallFrame(point2.X, point2.Y, false);
					}
				}
				points.Clear();
			}

			public bool CheckTile(ref int x, ref int y, ref Point point, int offsetX, int offsetY)
			{
				int validX = x + offsetX;
				int validY = y + offsetY;
				if (this.ValidTile(validX, validY))
				{
					x = validX;
					y = validY;
					point = new Point(validX, validY);
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
