using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class JunkMetalWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[(int)base.Type] = false;
			this.drop = ModContent.ItemType<Cyberscrap>();
			base.AddMapEntry(new Color(120, 100, 80), null);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
