using System;

namespace Redemption
{
	public class GenConditions
	{
		public int GetTile(int index)
		{
			if (this.tiles != null && this.tiles.Length > index)
			{
				return this.tiles[index];
			}
			return -1;
		}

		public int GetWall(int index)
		{
			if (this.walls != null && this.walls.Length > index)
			{
				return this.walls[index];
			}
			return -1;
		}

		public int[] tiles;

		public int[] walls;

		public bool orderTiles;

		public bool orderWalls;

		public bool slope;

		public Func<int, int, int, int, bool> CanPlace;

		public Func<int, int, int, int, bool> CanPlaceWall;
	}
}
