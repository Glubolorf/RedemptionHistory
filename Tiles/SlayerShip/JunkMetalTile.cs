using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.SlayerShip
{
	public class JunkMetalTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 650;
			this.dustType = 226;
			this.drop = ModContent.ItemType<Cyberscrap>();
			this.minPick = 200;
			this.mineResist = 4f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(190, 170, 160), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
