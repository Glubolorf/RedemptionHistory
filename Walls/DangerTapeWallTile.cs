﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DangerTapeWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			base.AddMapEntry(new Color(100, 100, 60), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void KillWall(int i, int j, ref bool fail)
		{
			fail = true;
		}
	}
}
