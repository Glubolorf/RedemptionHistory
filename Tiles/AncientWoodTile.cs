using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class AncientWoodTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileNoSunLight[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			this.drop = base.mod.ItemType("AncientWood");
			this.minPick = 0;
			this.mineResist = 2.5f;
			base.AddMapEntry(new Color(120, 91, 35), null);
		}
	}
}
