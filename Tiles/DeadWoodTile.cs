using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class DeadWoodTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.drop = base.mod.ItemType("DeadWood");
			this.minPick = 0;
			this.mineResist = 1.5f;
			base.AddMapEntry(new Color(100, 100, 100), null);
		}
	}
}
