using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class LeadFenceBlackWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			base.AddMapEntry(new Color(30, 30, 30), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
