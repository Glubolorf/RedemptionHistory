﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XenomiteDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Mildly Infected");
			base.Description.SetDefault("\"You are in contact with Xenomite...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -10;
			player.meleeDamage *= 1.25f;
			player.statDefense -= 8;
		}
	}
}
