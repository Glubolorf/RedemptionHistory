using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DangerTapeWall2Tile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			base.AddMapEntry(new Color(100, 100, 60), null);
		}
	}
}
