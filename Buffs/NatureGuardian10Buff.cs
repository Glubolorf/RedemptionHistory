﻿using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian10Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Skeletal Guardian");
			base.Description.SetDefault("\"A Skeletal Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			player.statDefense += 12;
			player.endurance += 0.08f;
			player.noKnockback = true;
			modPlayer3.staveSpeed += 0.35f;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian10>()] > 0)
			{
				modPlayer2.natureGuardian10 = true;
			}
			if (!modPlayer2.natureGuardian10)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
