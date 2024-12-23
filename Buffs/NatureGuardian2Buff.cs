﻿using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian2Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nature Guardian");
			base.Description.SetDefault("\"A Nature Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			player.statDefense += 8;
			p.staveSpeed += 0.35f;
			player.dryadWard = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian2>()] > 0)
			{
				modPlayer2.natureGuardian2 = true;
			}
			if (!modPlayer2.natureGuardian2)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
