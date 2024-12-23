﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class IsInWorld : GenAction
	{
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
			{
				return base.Fail();
			}
			return base.UnitApply(origin, x, y, args);
		}
	}
}
