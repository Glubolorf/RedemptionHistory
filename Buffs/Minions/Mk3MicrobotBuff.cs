﻿using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class Mk3MicrobotBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Mk-3 Microbot");
			base.Description.SetDefault("\"Beep boop, pew pew\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Mk3Microbot>()] > 0)
			{
				modPlayer.mk3MicrobotMinion = true;
			}
			if (!modPlayer.mk3MicrobotMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
