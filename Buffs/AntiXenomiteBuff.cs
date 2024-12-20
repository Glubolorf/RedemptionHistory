using System;
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
			player.buffImmune[base.mod.BuffType("XenomiteDebuff")] = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff2")] = true;
		}
	}
}
