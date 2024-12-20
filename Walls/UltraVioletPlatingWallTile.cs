using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class UltraVioletPlatingWallTile : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = true;
			this.drop = base.mod.ItemType("UltraVioletPlatingWall");
			base.AddMapEntry(new Color(100, 0, 100), null);
		}
	}
}
