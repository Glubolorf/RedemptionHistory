using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class DragonLeadOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 320;
			this.dustType = 6;
			this.drop = base.mod.ItemType("DragonLeadChunk");
			this.minPick = 100;
			this.mineResist = 1.4f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Dragon-Lead Chuck");
			base.AddMapEntry(new Color(251, 177, 81), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 2)
			{
				player.AddBuff(24, Main.rand.Next(10, 20), true);
			}
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
