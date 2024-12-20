using System;
using Terraria.World.Generation;

namespace Redemption
{
	public class IsNotSloped : GenCondition
	{
		protected override bool CheckValidity(int x, int y)
		{
			return GenBase._tiles[x, y].active() && GenBase._tiles[x, y].slope() == 0 && !GenBase._tiles[x, y].halfBrick();
		}
	}
}
