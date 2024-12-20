using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AntiXenomiteBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Immunity");
			base.Description.SetDefault("\"You are completely immune to Xenomite...\"");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[ModContent.BuffType<XenomiteDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff2>()] = true;
		}
	}
}
