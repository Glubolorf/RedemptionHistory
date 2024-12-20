using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.WorldGeneration.Soulless
{
	public class GenUtils
	{
		public static void ObjectPlace(Point Origin, int x, int y, int TileType)
		{
			WorldGen.PlaceObject(Origin.X + x, Origin.Y + y, TileType, true, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, Origin.X + x, Origin.Y + y, TileType, 0, 0, -1, -1);
		}

		public static void ObjectPlace(int x, int y, int TileType)
		{
			WorldGen.PlaceObject(x, y, TileType, true, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, x, y, TileType, 0, 0, -1, -1);
		}

		public static void ObjectPlaceAlt(Point Origin, int x, int y, int TileType)
		{
			WorldGen.PlaceObject(Origin.X + x, Origin.Y + y, TileType, true, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, Origin.X + x, Origin.Y + y, TileType, 0, 0, -1, 1);
		}

		public static void ObjectPlaceAlt(int x, int y, int TileType)
		{
			WorldGen.PlaceObject(x, y, TileType, true, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, x, y, TileType, 0, 0, -1, 1);
		}

		public static void ObjectPlaceRand1(Point Origin, int x, int y, int TileType)
		{
			WorldGen.PlaceObject(Origin.X + x, Origin.Y + y, TileType, true, WorldGen.genRand.Next(3), 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, Origin.X + x, Origin.Y + y, TileType, WorldGen.genRand.Next(3), 0, -1, -1);
		}

		public static void ObjectPlaceRand1(int x, int y, int TileType)
		{
			WorldGen.PlaceObject(x, y, TileType, true, WorldGen.genRand.Next(3), 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, x, y, TileType, WorldGen.genRand.Next(3), 0, -1, -1);
		}
	}
}
