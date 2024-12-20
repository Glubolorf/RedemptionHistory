﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class UkkonenBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ukkonen");
			base.Description.SetDefault("\"A thundering Ukkonen to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("Ukkonen")] > 0)
			{
				modPlayer.ukkonenMinion = true;
			}
			if (!modPlayer.ukkonenMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}