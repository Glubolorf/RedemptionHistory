﻿using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class DarkSoulBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
			base.Description.SetDefault("A Dark Soul to fight for you");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DarkSoulMinion>()] > 0)
			{
				modPlayer.darkSoulMinion = true;
			}
			if (!modPlayer.darkSoulMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 300;
		}
	}
}
