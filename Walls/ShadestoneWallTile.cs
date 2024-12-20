using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class ShadestoneWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			base.AddMapEntry(new Color(10, 10, 10), null);
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
