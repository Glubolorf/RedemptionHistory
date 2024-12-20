using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Redemption.Items.Materials.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Ores
{
	public class XenomiteOreBlock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 600;
			this.dustType = ModContent.DustType<XenoDust>();
			this.drop = ModContent.ItemType<XenomiteShard>();
			this.minPick = 30;
			this.mineResist = 1.5f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Xenomite Ore");
			base.AddMapEntry(new Color(120, 100, 255), name);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 2)
			{
				player.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(10, 20), true);
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
