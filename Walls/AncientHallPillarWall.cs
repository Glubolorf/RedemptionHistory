using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class AncientHallPillarWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			base.AddMapEntry(new Color(150, 150, 150), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
